from fastapi import FastAPI, Depends, HTTPException, Query, Path
from typing import Annotated, Any
from .db import get_pool, close_pool
from .auth import get_user_by_token
from contextlib import asynccontextmanager
from pydantic import BaseModel
import bcrypt
import secrets
from datetime import datetime, timedelta

pool = None

@asynccontextmanager
async def lifespan(app: FastAPI):
    global pool
    pool = await get_pool()
    yield
    pool = None
    await close_pool()

app = FastAPI(title="SQLi Lab (safe edition)", lifespan=lifespan)

class AuthRequest(BaseModel):
    name: str
    password: str

@app.post("/auth/token")
async def auth_token(body: AuthRequest):
    global pool
    async with pool.acquire() as conn:
        # ИСПРАВЛЕНО: получаем хэш из БД для проверки через bcrypt
        row = await conn.fetchrow(
            "SELECT id, password_hash FROM users WHERE name = $1",
            body.name
        )
        if not row:
            raise HTTPException(status_code=401, detail="Invalid username or password")
        
        # ИСПРАВЛЕНО: проверка пароля через bcrypt вместо MD5
        if not bcrypt.checkpw(body.password.encode(), row["password_hash"].encode()):
            raise HTTPException(status_code=401, detail="Invalid username or password")
        
        # ИСПРАВЛЕНО: проверяем срок действия токена
        token_row = await conn.fetchrow(
            "SELECT value FROM tokens WHERE user_id = $1 AND is_valid = TRUE AND expires_at > NOW() LIMIT 1",
            row["id"]
        )
        if not token_row:
            token = secrets.token_urlsafe(64)
            # ИСПРАВЛЕНО: устанавливаем срок действия токена (7 дней)
            expires_at = datetime.utcnow() + timedelta(days=7)
            await conn.fetch(
                "INSERT INTO tokens (user_id, value, expires_at) VALUES ($1, $2, $3)",
                row["id"], token, expires_at
            )
        else:
            token = token_row["value"]
        return {"token": token}

@app.get("/orders")
async def list_orders(
    user: Annotated[dict[str, Any], Depends(get_user_by_token)],
    limit: int = Query(10, ge=1, le=100),
    offset: int = Query(0, ge=0)
):
    global pool
    async with pool.acquire() as conn:
        # ИСПРАВЛЕНО: параметризованный запрос + типизация Query параметров
        rows = await conn.fetch(
            "SELECT id, user_id, created_at FROM orders WHERE user_id = $1 ORDER BY created_at DESC LIMIT $2 OFFSET $3",
            user["id"], limit, offset
        )
    return [{"id": r["id"], "user_id": r["user_id"], "created_at": r["created_at"].isoformat()} for r in rows]

@app.get("/orders/{order_id}")
async def order_details(
    user: Annotated[dict[str, Any], Depends(get_user_by_token)],
    order_id: int = Path(..., ge=1)
):
    global pool
    async with pool.acquire() as conn:
        # ИСПРАВЛЕНО: параметризованный запрос + типизация Path параметра
        order = await conn.fetchrow(
            "SELECT id, user_id, created_at FROM orders WHERE id = $1 AND user_id = $2",
            order_id, user["id"]
        )
        if not order:
            raise HTTPException(status_code=404, detail="Order not found")
        
        # ИСПРАВЛЕНО: параметризованный запрос
        goods = await conn.fetch(
            "SELECT id, name, count, price FROM goods WHERE order_id = $1",
            order_id
        )
    return {
        "order": {"id": order["id"], "user_id": order["user_id"], "created_at": order["created_at"].isoformat()},
        "goods": [{"id": g["id"], "name": g["name"], "count": g["count"], "price": float(g["price"])} for g in goods]
    }