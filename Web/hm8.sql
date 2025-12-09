-- variant 5
CREATE TABLE rentals (
    id SERIAL PRIMARY KEY,
    customer_name TEXT NOT NULL,
    vehicle_class TEXT NOT NULL,
    pickup_city TEXT NOT NULL,
    daily_rate NUMERIC(10, 2) NOT NULL,
    pickup_date DATE NOT NULL,
    return_date DATE NOT NULL,
    status TEXT NOT NULL,
    CONSTRAINT check_daily_rate_positive CHECK (daily_rate > 0),
    CONSTRAINT check_dates_valid CHECK (return_date >= pickup_date),
    CONSTRAINT check_status_valid CHECK (status IN ('Booked', 'Confirmed', 'Completed', 'Cancelled'))
);


INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('John Smith', 'SUV', 'New York', 85.00, '2022-01-01', '2022-01-05', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Anna Johnson', 'SUV Premium', 'New Haven', 120.00, '2022-06-15', '2022-06-20', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Mike Brown', 'SUV', 'Newport', 95.00, '2023-12-31', '2024-01-03', 'Confirmed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Sarah Wilson', 'SUV', 'New Orleans', 88.00, '2021-12-30', '2022-01-02', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Tom Davis', 'Economy', 'New York', 45.00, '2022-08-10', '2022-08-15', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Lisa Martinez', 'SUV', 'Los Angeles', 92.00, '2022-09-20', '2022-09-25', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Robert Taylor', 'Compact', 'Boston', 50.00, '2024-03-15', '2024-03-20', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Emily White', 'Economy', 'Chicago', 40.00, '2024-04-01', '2024-04-05', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('David Harris', 'Compact', 'Seattle', 70.00, '2024-05-10', '2024-05-15', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Linda Clark', 'Economy', 'Miami', 39.99, '2024-06-01', '2024-06-05', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Kevin Lewis', 'Premium', 'Denver', 70.01, '2024-07-01', '2024-07-05', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Amanda Walker', 'Compact', 'Test City', 55.00, '2024-08-01', '2024-08-05', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Brian Hall', 'Economy', 'Portland', 60.00, '2024-09-01', '2024-09-05', 'Confirmed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Test User One', 'Economy', 'Atlanta', 45.00, '2018-10-01', '2018-12-15', 'Cancelled');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('John Test', 'Compact', 'Houston', 52.00, '2018-08-10', '2018-11-20', 'Cancelled');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Test Customer', 'SUV', 'Phoenix', 88.00, '2018-06-01', '2018-10-30', 'Cancelled');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Test Border', 'Economy', 'Dallas', 48.00, '2018-12-20', '2019-01-01', 'Cancelled');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Test Completed', 'Compact', 'San Diego', 55.00, '2018-11-01', '2018-12-25', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Peter Anderson', 'Economy', 'Austin', 42.00, '2018-09-01', '2018-11-15', 'Cancelled');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Jennifer Moore', 'Premium', 'San Francisco', 135.00, '2023-05-10', '2023-05-17', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Christopher Lee', 'SUV', 'Las Vegas', 98.00, '2023-07-20', '2023-07-25', 'Confirmed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Michelle Garcia', 'Compact', 'Orlando', 58.00, '2023-09-15', '2023-09-20', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Daniel Martinez', 'Economy', 'Philadelphia', 43.00, '2023-11-01', '2023-11-06', 'Completed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Ashley Robinson', 'SUV Premium', 'Nashville', 115.00, '2024-01-15', '2024-01-22', 'Confirmed');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Matthew Young', 'Premium', 'Charlotte', 125.00, '2024-02-10', '2024-02-15', 'Booked');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Jessica King', 'Economy', 'Columbus', 38.00, '2024-03-05', '2024-03-09', 'Cancelled');

INSERT INTO rentals (customer_name, vehicle_class, pickup_city, daily_rate, pickup_date, return_date, status)
VALUES ('Ryan Scott', 'Compact', 'Indianapolis', 62.00, '2024-04-12', '2024-04-18', 'Confirmed');

SELECT *
FROM rentals
WHERE LOWER(pickup_city) LIKE '%new%'
    AND LOWER(vehicle_class) LIKE 'suv%'
    AND pickup_date BETWEEN '2022-01-01' AND '2023-12-31'
ORDER BY pickup_date, customer_name;

UPDATE rentals
SET daily_rate = daily_rate * 1.08
WHERE status = 'Booked'
    AND daily_rate BETWEEN 40 AND 70
    AND LOWER(pickup_city) NOT LIKE '%test%';

DELETE FROM rentals
WHERE status = 'Cancelled'
    AND return_date < '2019-01-01'
    AND LOWER(customer_name) LIKE '%test%';