DROP TABLE IF EXISTS orders;

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    customer_name TEXT NOT NULL,
    order_code TEXT NOT NULL,
    notes TEXT,
    created_at TIMESTAMP DEFAULT NOW()
);

-- 2. Заполнение таблицы большим объёмом случайных данных

-- Генерируем 500,000 записей с различными данными
INSERT INTO orders (customer_name, order_code, notes, created_at)
SELECT
    'Customer ' || i,
    'ORD-' || LPAD(i::TEXT, 8, '0'),
    'Order notes for item ' || i || '. Product: ' || 
        CASE (i % 10)
            WHEN 0 THEN 'Laptop Computer'
            WHEN 1 THEN 'Smartphone Device'
            WHEN 2 THEN 'Tablet Electronics'
            WHEN 3 THEN 'Monitor Display'
            WHEN 4 THEN 'Keyboard Accessory'
            WHEN 5 THEN 'Mouse Peripheral'
            WHEN 6 THEN 'Headphones Audio'
            WHEN 7 THEN 'Webcam Camera'
            WHEN 8 THEN 'Microphone Sound'
            ELSE 'Cable Connection'
        END ||
        '. Status: ' ||
        CASE (i % 3)
            WHEN 0 THEN 'pending'
            WHEN 1 THEN 'processing'
            ELSE 'completed'
        END,
    NOW() - (RANDOM() * INTERVAL '365 days')
FROM generate_series(1, 500000) AS i;

-- Проверка количества записей
SELECT COUNT(*) AS total_records FROM orders;

-- 3. Выборка по точному совпадению order_code БЕЗ индекса

-- Включаем отображение времени выполнения
\timing on

-- EXPLAIN ANALYZE для детального анализа
EXPLAIN ANALYZE
SELECT *
FROM orders
WHERE order_code = 'ORD-00250000';

-- Простой запрос с замером времени
SELECT *
FROM orders
WHERE order_code = 'ORD-00250000';

-- Еще несколько тестовых запросов
SELECT *
FROM orders
WHERE order_code = 'ORD-00100000';

SELECT *
FROM orders
WHERE order_code = 'ORD-00400000';

\timing off

-- 4. Создание обычного индекса на order_code

CREATE INDEX idx_orders_order_code ON orders(order_code);

-- Обновление статистики
ANALYZE orders;

-- 5. Повторная выборка по order_code С индексом

\timing on

-- EXPLAIN ANALYZE для сравнения с п.3
EXPLAIN ANALYZE
SELECT *
FROM orders
WHERE order_code = 'ORD-00250000';

-- Простой запрос с замером времени
SELECT *
FROM orders
WHERE order_code = 'ORD-00250000';

-- Те же тестовые запросы
SELECT *
FROM orders
WHERE order_code = 'ORD-00100000';

SELECT *
FROM orders
WHERE order_code = 'ORD-00400000';

\timing off

-- 6. Выборка по подстроке в notes БЕЗ специального индекса

\timing on

-- EXPLAIN ANALYZE для анализа
EXPLAIN ANALYZE
SELECT *
FROM orders
WHERE notes ILIKE '%Laptop%';

-- Простой запрос с замером времени
SELECT COUNT(*)
FROM orders
WHERE notes ILIKE '%Laptop%';

-- Еще несколько тестовых запросов
SELECT COUNT(*)
FROM orders
WHERE notes ILIKE '%pending%';

SELECT COUNT(*)
FROM orders
WHERE notes ILIKE '%Camera%';

\timing off

-- 7. Создание индекса на вычисляемое выражение для ILIKE

-- Создаем индекс на LOWER(notes) для регистронезависимого поиска
CREATE INDEX idx_orders_notes_lower ON orders(LOWER(notes));

-- Альтернативный вариант: GIN индекс с pg_trgm для поиска подстрок
-- Требует расширение pg_trgm
CREATE EXTENSION IF NOT EXISTS pg_trgm;

CREATE INDEX idx_orders_notes_trgm ON orders USING GIN (notes gin_trgm_ops);

-- Обновление статистики
ANALYZE orders;

-- 8. Повторная выборка по подстроке С индексом

\timing on

-- EXPLAIN ANALYZE для сравнения с п.6
EXPLAIN ANALYZE
SELECT *
FROM orders
WHERE notes ILIKE '%Laptop%';

-- Простой запрос с замером времени
SELECT COUNT(*)
FROM orders
WHERE notes ILIKE '%Laptop%';

-- Те же тестовые запросы
SELECT COUNT(*)
FROM orders
WHERE notes ILIKE '%pending%';

SELECT COUNT(*)
FROM orders
WHERE notes ILIKE '%Camera%';

\timing off

-- ИТОГОВАЯ СТАТИСТИКА
-- Информация об индексах
SELECT
    indexname,
    indexdef
FROM pg_indexes
WHERE tablename = 'orders';

-- Размеры таблицы и индексов
SELECT
    pg_size_pretty(pg_total_relation_size('orders')) AS total_size,
    pg_size_pretty(pg_relation_size('orders')) AS table_size,
    pg_size_pretty(pg_total_relation_size('orders') - pg_relation_size('orders')) AS indexes_size;

-- Детальная информация по каждому индексу
SELECT
    schemaname,
    tablename,
    indexname,
    pg_size_pretty(pg_relation_size(indexrelid)) AS index_size
FROM pg_stat_user_indexes
WHERE tablename = 'orders';

