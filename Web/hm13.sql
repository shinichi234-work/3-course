-- Очистка
DROP TABLE IF EXISTS contract CASCADE;
DROP TABLE IF EXISTS module_station CASCADE;
DROP TABLE IF EXISTS spaceship_module CASCADE;
DROP TABLE IF EXISTS player_faction CASCADE;
DROP TABLE IF EXISTS module CASCADE;
DROP TABLE IF EXISTS spaceship CASCADE;
DROP TABLE IF EXISTS station CASCADE;
DROP TABLE IF EXISTS planet CASCADE;
DROP TABLE IF EXISTS faction CASCADE;
DROP TABLE IF EXISTS player CASCADE;

-- Игроки (капитаны)
CREATE TABLE player (
    id SERIAL PRIMARY KEY,
    nickname TEXT NOT NULL UNIQUE,
    email TEXT
);

-- Фракции
CREATE TABLE faction (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    alignment TEXT
);

-- Связь player <-> faction с позывным
CREATE TABLE player_faction (
    faction_id INT NOT NULL REFERENCES faction(id) ON DELETE CASCADE,
    player_id INT NOT NULL REFERENCES player(id) ON DELETE CASCADE,
    call_sign TEXT NOT NULL,
    joined_at DATE DEFAULT CURRENT_DATE,
    PRIMARY KEY (faction_id, player_id),
    CONSTRAINT uniq_call_sign_per_faction UNIQUE (faction_id, call_sign)
);

-- Планеты
CREATE TABLE planet (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    sector TEXT,
    population BIGINT
);

-- Станции (часть привязана к планетам, часть — в глубоком космосе)
CREATE TABLE station (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    planet_id INT REFERENCES planet(id) ON DELETE SET NULL,
    orbit_altitude_km INT
);

-- Корабли
CREATE TABLE spaceship (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    player_id INT NOT NULL REFERENCES player(id) ON DELETE CASCADE,
    class TEXT,
    hull_points INT
);

-- Модули (оборудование)
CREATE TABLE module (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    module_type TEXT,
    rarity TEXT,
    power_usage INT,
    description TEXT
);

-- Связь spaceship <-> module (инвентарь/установка)
CREATE TABLE spaceship_module (
    spaceship_id INT NOT NULL REFERENCES spaceship(id) ON DELETE CASCADE,
    module_id INT NOT NULL REFERENCES module(id) ON DELETE CASCADE,
    quantity INT NOT NULL CHECK (quantity >= 0),
    equipped BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (spaceship_id, module_id)
);

-- Локация модулей на станциях (магазины/склады)
CREATE TABLE module_station (
    station_id INT NOT NULL REFERENCES station(id) ON DELETE CASCADE,
    module_id INT NOT NULL REFERENCES module(id) ON DELETE CASCADE,
    quantity INT NOT NULL CHECK (quantity >= 0),
    vendor_name TEXT,
    PRIMARY KEY (station_id, module_id)
);

-- Контракты (задания)
CREATE TABLE contract (
    id SERIAL PRIMARY KEY,
    station_id INT NOT NULL REFERENCES station(id) ON DELETE CASCADE,
    faction_id INT,
    call_sign TEXT,
    player_id INT NOT NULL REFERENCES player(id) ON DELETE CASCADE,
    spaceship_id INT NOT NULL REFERENCES spaceship(id) ON DELETE CASCADE,
    module_id INT REFERENCES module(id) ON DELETE SET NULL,
    reward_credits INT,
    duration_days INT NOT NULL CHECK (duration_days > 0),
    accepted_at DATE NOT NULL DEFAULT CURRENT_DATE,
    completed_at DATE,
    CONSTRAINT fk_faction_call_sign FOREIGN KEY (faction_id, call_sign)
        REFERENCES player_faction (faction_id, call_sign)
        ON DELETE RESTRICT
);

CREATE INDEX idx_contract_player ON contract(player_id);
CREATE INDEX idx_contract_station ON contract(station_id);
CREATE INDEX idx_contract_module ON contract(module_id);

-- Players
INSERT INTO player (id, nickname, email) VALUES
(1, 'NovaCaptain', 'nova@example.com'),
(2, 'QuantumMage', 'quantum@example.com'),
(3, 'ShadowRunner', 'shadow@example.com'),
(4, 'NewPilot', NULL),
(5, 'LoneWolf', 'lonewolf@example.com');

SELECT setval(pg_get_serial_sequence('player', 'id'), (SELECT MAX(id) FROM player));

-- Factions
INSERT INTO faction (id, name, alignment) VALUES
(1, 'Solar Union', 'Lawful'),
(2, 'Void Corsairs', 'Chaotic'),
(3, 'Neutral Traders Guild', 'Neutral');

SELECT setval(pg_get_serial_sequence('faction', 'id'), (SELECT MAX(id) FROM faction));

-- Player-Faction
INSERT INTO player_faction (faction_id, player_id, call_sign, joined_at) VALUES
(1, 1, 'SUN-001', '2023-01-10'),
(1, 2, 'SUN-002', '2023-06-05'),
(2, 3, 'VOID-777', '2024-02-12'),
(1, 5, 'SUN-009', '2024-11-01');

-- Planets
INSERT INTO planet (id, name, sector, population) VALUES
(1, 'Terra Prime', 'Core', 12000000000),
(2, 'New Horizon', 'Rim', 500000000),
(3, 'Astra-7', 'Outer Belt', 12000000),
(4, 'Dead Rock', 'Void Edge', NULL);

SELECT setval(pg_get_serial_sequence('planet', 'id'), (SELECT MAX(id) FROM planet));

-- Stations
INSERT INTO station (id, name, planet_id, orbit_altitude_km) VALUES
(1, 'Terra Orbital Hub', 1, 350),
(2, 'Horizon Trade Ring', 2, 420),
(3, 'Astra Mining Station', 3, 120),
(4, 'Deep Void Relay', NULL, 0),
(5, 'Horizon Research Spire', 2, 600);

SELECT setval(pg_get_serial_sequence('station', 'id'), (SELECT MAX(id) FROM station));

-- Spaceships
INSERT INTO spaceship (id, name, player_id, class, hull_points) VALUES
(1, 'SS Nova Light', 1, 'Frigate', 3000),
(2, 'QMC Entangler', 2, 'Cruiser', 4500),
(3, 'Shadow Dancer', 3, 'Corvette', 2800),
(4, 'Rookie Shuttle', 4, 'Shuttle', 800),
(5, 'Wolf Fang', 5, 'Destroyer', 3800);

SELECT setval(pg_get_serial_sequence('spaceship', 'id'), (SELECT MAX(id) FROM spaceship));

-- Modules
INSERT INTO module (id, name, module_type, rarity, power_usage, description) VALUES
(1, 'Plasma Cannon Mk I', 'Weapon', 'Rare', 50, 'Basic plasma weapon.'),
(2, 'Shield Generator Alpha', 'Defense', 'Uncommon', 40, 'Standard shield generator.'),
(3, 'Warp Drive Beta', 'Engine', 'Epic', 60, 'Improved warp capabilities.'),
(4, 'Cargo Hold Extender', 'Utility', 'Common', 10, 'Increases cargo capacity.'),
(5, 'Cloaking Device', 'Stealth', 'Legendary', 70, 'Advanced stealth module.'),
(6, 'Point Defense Laser', 'Weapon', 'Uncommon', 30, 'Anti-missile defense.'),
(7, 'Nanobot Repair Bay', 'Support', 'Rare', 35, 'Automatic hull repair.');

SELECT setval(pg_get_serial_sequence('module', 'id'), (SELECT MAX(id) FROM module));

-- Spaceship-Module (установленные/хранимые модули)
INSERT INTO spaceship_module (spaceship_id, module_id, quantity, equipped) VALUES
(1, 1, 1, TRUE),
(1, 2, 1, TRUE),
(2, 3, 1, TRUE),
(2, 4, 2, FALSE),
(3, 1, 1, TRUE),
(3, 5, 1, TRUE),
(5, 6, 2, TRUE);

-- Module availability on stations
INSERT INTO module_station (station_id, module_id, quantity, vendor_name) VALUES
(1, 2, 10, 'Terran Shields Inc'),
(1, 4, 25, 'Cargo Solutions'),
(2, 1, 3, 'Horizon Arms'),
(2, 6, 5, 'Defense Matrix'),
(2, 7, 2, 'NanoTech Vendor'),
(3, 4, 8, 'Mining Depot'),
(3, 6, 4, 'Security Hub'),
(5, 3, 1, 'Research Labs'),
(5, 5, 1, 'Black Lab Co.');

-- Contracts (миссии)
INSERT INTO contract (station_id, faction_id, call_sign, player_id, spaceship_id, module_id, reward_credits, duration_days, accepted_at) VALUES
(1, 1, 'SUN-001', 1, 1, 4, 5000, 7, '2025-11-01'),
(2, 1, 'SUN-002', 2, 2, 3, 8000, 10, '2025-10-20'),
(2, 2, 'VOID-777', 3, 3, 1, 7000, 5, '2025-11-05'),
(3, NULL, NULL, 2, 2, NULL, 3000, 3, '2025-11-10'),
(5, 1, 'SUN-009', 5, 5, 7, 9000, 12, '2025-11-12');

-- ЗАДАНИЯ ПО JOIN
-- 1. INNER JOIN
-- 1.1. Корабли с их владельцами
SELECT
    s.name AS spaceship_name,
    s.class,
    s.hull_points,
    p.nickname
FROM spaceship s
INNER JOIN player p ON s.player_id = p.id;

-- 1.2. Модули, установленные на кораблях
SELECT
    s.name AS spaceship_name,
    m.name AS module_name,
    m.module_type,
    sm.quantity,
    sm.equipped
FROM spaceship_module sm
INNER JOIN spaceship s ON sm.spaceship_id = s.id
INNER JOIN module m ON sm.module_id = m.id;

-- 2. LEFT JOIN
-- 2.1. Все корабли и их модули (включая корабли без модулей)
SELECT
    s.name AS spaceship_name,
    m.name AS module_name,
    m.module_type,
    sm.quantity,
    sm.equipped
FROM spaceship s
LEFT JOIN spaceship_module sm ON s.id = sm.spaceship_id
LEFT JOIN module m ON sm.module_id = m.id;

-- 2.2. Количество кораблей у каждого игрока (включая игроков без кораблей)
SELECT
    p.nickname,
    COUNT(s.id) AS spaceship_count
FROM player p
LEFT JOIN spaceship s ON p.id = s.player_id
GROUP BY p.id, p.nickname;

-- 3. RIGHT JOIN
-- 3.1. Все станции и модули на них (включая станции без модулей)
SELECT
    st.name AS station_name,
    m.name AS module_name,
    ms.quantity,
    ms.vendor_name
FROM module_station ms
RIGHT JOIN station st ON ms.station_id = st.id
LEFT JOIN module m ON ms.module_id = m.id;

-- 3.2. Количество станций для каждого типа модуля (включая типы без станций)
SELECT
    m.module_type,
    COUNT(DISTINCT ms.station_id) AS station_count
FROM module m
LEFT JOIN module_station ms ON m.id = ms.module_id
GROUP BY m.module_type;

-- 4. FULL JOIN
-- 4.1. Объединённый вывод всех модулей и записей module_station
SELECT
    m.id AS module_id,
    m.name AS module_name,
    m.module_type,
    ms.station_id,
    ms.quantity,
    ms.vendor_name
FROM module m
FULL JOIN module_station ms ON m.id = ms.module_id;

-- 4.2. Полный перечень игроков и фракций
SELECT
    p.nickname,
    f.name AS faction_name,
    pf.call_sign
FROM player p
FULL JOIN player_faction pf ON p.id = pf.player_id
FULL JOIN faction f ON pf.faction_id = f.id;

-- 5. CROSS JOIN
-- 5.1. Все пары (станция, тип модуля)
SELECT
    st.name AS station_name,
    m.module_type
FROM station st
CROSS JOIN (SELECT DISTINCT module_type FROM module) m;

-- 5.2. Первые 100 строк декартова произведения (spaceship × station)
SELECT
    s.name AS spaceship_name,
    st.name AS station_name
FROM spaceship s
CROSS JOIN station st
LIMIT 100;

-- 6. LATERAL JOIN
-- 6.1. Для каждой станции выбрать модуль с максимальным quantity
SELECT
    st.name AS station_name,
    lat.module_name,
    lat.max_quantity
FROM station st
LEFT JOIN LATERAL (
    SELECT
        m.name AS module_name,
        ms.quantity AS max_quantity
    FROM module_station ms
    INNER JOIN module m ON ms.module_id = m.id
    WHERE ms.station_id = st.id
    ORDER BY ms.quantity DESC
    LIMIT 1
) lat ON TRUE;

-- 6.2. Для каждого игрока выбрать последний контракт
SELECT
    p.nickname,
    lat.station_name,
    lat.module_name,
    lat.accepted_at
FROM player p
LEFT JOIN LATERAL (
    SELECT
        st.name AS station_name,
        m.name AS module_name,
        c.accepted_at
    FROM contract c
    INNER JOIN station st ON c.station_id = st.id
    LEFT JOIN module m ON c.module_id = m.id
    WHERE c.player_id = p.id
    ORDER BY c.accepted_at DESC
    LIMIT 1
) lat ON TRUE;

-- 7. SELF JOIN
-- 7.1. Пары кораблей одного игрока с разными именами
SELECT
    s1.name AS spaceship_1,
    s2.name AS spaceship_2,
    p.nickname AS owner
FROM spaceship s1
INNER JOIN spaceship s2 ON s1.player_id = s2.player_id AND s1.id < s2.id
INNER JOIN player p ON s1.player_id = p.id;

-- 7.2. Пары модулей одинаковой редкости (исключая дубли)
SELECT
    m1.name AS module_1,
    m2.name AS module_2,
    m1.rarity
FROM module m1
INNER JOIN module m2 ON m1.rarity = m2.rarity AND m1.id < m2.id;