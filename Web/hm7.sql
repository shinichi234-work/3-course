-- variant 10
CREATE TABLE orders (
    order_id INTEGER PRIMARY KEY,
    dish_name TEXT NOT NULL,
    quantity INTEGER NOT NULL,
    unit_price NUMERIC(10, 2) NOT NULL,
    total_amount NUMERIC(10, 2) NOT NULL,
    CONSTRAINT check_quantity_positive CHECK (quantity > 0),
    CONSTRAINT check_unit_price_positive CHECK (unit_price > 0),
    CONSTRAINT check_total_amount CHECK (total_amount = quantity * unit_price)
);

INSERT INTO orders (order_id, dish_name, quantity, unit_price, total_amount)
VALUES (1, 'Цезарь с курицей', 2, 450.00, 900.00);

INSERT INTO orders (order_id, dish_name, quantity, unit_price, total_amount)
VALUES (2, 'Том Ям', 1, 650.00, 650.00);

INSERT INTO orders (order_id, dish_name, quantity, unit_price, total_amount)
VALUES (3, 'Тирамису', 3, 320.00, 960.00);