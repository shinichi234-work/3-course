import hashlib
import time
from passlib.context import CryptContext
from validation import validate_password
from user import User, UserStorage


pwd_context = CryptContext(schemes=["argon2"], deprecated="auto")


def register_user(storage: UserStorage, username: str, email: str, password: str) -> User:
    """
    Создает пользователя и сохраняет хэш пароля с использованием argon2.
    """
    if User.exists(storage, username):
        raise ValueError("Пользователь с таким username уже существует")

    password_hash = pwd_context.hash(password)
    user = User(username=username, email=email, password_hash=password_hash)
    user.save(storage)
    return user


def is_account_locked(storage: UserStorage, username: str) -> bool:
    """
    Проверяет, заблокирован ли аккаунт пользователя.
    """
    user = User.load(storage, username)
    if user is None:
        return False
    return user.is_locked


def verify_credentials(storage: UserStorage, username: str, password: str) -> bool:
    """
    Проверяет учетные данные пользователя с поддержкой:
    1. Ленивой миграции с MD5 на argon2
    2. Блокировки после 5 неудачных попыток
    """
    user = User.load(storage, username)
    if user is None:
        return False

    if user.is_locked:
        return False

    is_valid = False

    if pwd_context.identify(user.password_hash):
        is_valid = pwd_context.verify(password, user.password_hash)
    else:
        
        md5_hex = hashlib.md5(password.encode("utf-8")).hexdigest()
        if user.password_hash == md5_hex:
            is_valid = True
            
            user.password_hash = pwd_context.hash(password)
            user.failed_attempts = 0  
            user.save(storage)

    if is_valid:
        
        user.failed_attempts = 0
        user.save(storage)
        return True
    else:
        
        user.failed_attempts += 1
        
        
        if user.failed_attempts >= 5:
            user.is_locked = True
        
        user.save(storage)
        return False