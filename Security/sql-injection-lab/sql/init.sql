DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS orders CASCADE;
DROP TABLE IF EXISTS goods CASCADE;
DROP TABLE IF EXISTS tokens CASCADE;

CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  name TEXT NOT NULL,
  password_hash TEXT NOT NULL
);

CREATE TABLE orders (
  id SERIAL PRIMARY KEY,
  user_id INTEGER NOT NULL REFERENCES users(id),
  created_at TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE goods (
  id SERIAL PRIMARY KEY,
  name TEXT NOT NULL,
  order_id INTEGER NOT NULL REFERENCES orders(id),
  count INTEGER NOT NULL,
  price NUMERIC NOT NULL
);

CREATE TABLE tokens (
  id SERIAL PRIMARY KEY,
  value TEXT NOT NULL,
  user_id INTEGER NOT NULL REFERENCES users(id),
  is_valid BOOLEAN NOT NULL DEFAULT TRUE,
  expires_at TIMESTAMP NOT NULL DEFAULT (now() + interval '7 days')
);

-- допка: хэши паролей bcrypt вместо MD5 (потому что он не подходит для хранения паролей, как минимум потому что к него есть соль и он уязвим к радужной таблице)
-- Пароли: alice = "password123", bob = "password456", eva = "password789"
INSERT INTO users (id, name, password_hash) VALUES
  (1, 'alice', '$2b$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5NN8kN3QJqHIC'),
  (2, 'bob', '$2b$12$EixZaYVK1fsbw1ZfbX3OXePaWxn96p36WQoeG6Lruj3vjPGga31lW'),
  (3, 'eva', '$2b$12$NYy0.5ZQfPxXz/5b9a5F8OVUXhAX6/Oqj7/5C.4Kj9WxR5q5xB3jq');

INSERT INTO orders (id, user_id) VALUES (1, 1);
INSERT INTO orders (id, user_id) VALUES (2, 2);
INSERT INTO orders (id, user_id) VALUES (3, 3);
INSERT INTO orders (id, user_id) VALUES (4, 1);

INSERT INTO goods (id, name, order_id, count, price) VALUES (1, 'widget', 1, 3, 9.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (2, 'widget', 1, 4, 10.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (3, 'widget', 2, 5, 1.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (4, 'widget', 2, 6, 2.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (5, 'widget', 3, 1, 3.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (6, 'widget', 3, 2, 4.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (7, 'widget', 4, 3, 5.99);
INSERT INTO goods (id, name, order_id, count, price) VALUES (8, 'widget', 4, 4, 6.99);

--  допка: токены с датой истечения ( украденый токен - вечный доступ)
INSERT INTO tokens (value, user_id, is_valid, expires_at) VALUES 
  ('secrettokenAlice', 1, true, now() + interval '7 days'),
  ('secrettokenBob', 2, true, now() + interval '7 days'),
  ('secrettokenEva', 3, true, now() + interval '7 days');