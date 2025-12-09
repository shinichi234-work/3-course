-- variant 5
DROP TABLE IF EXISTS shipment_raw CASCADE;

CREATE TABLE shipment_raw (
    id              SERIAL PRIMARY KEY,
    recipient_name  TEXT,
    weight_grams    INTEGER,
    shipped_at      TIMESTAMP DEFAULT NOW(),
    status          TEXT DEFAULT 'pending',
    deprecated_flag BOOLEAN DEFAULT FALSE
);

INSERT INTO shipment_raw (recipient_name, weight_grams, shipped_at, status, deprecated_flag) VALUES
('Alice Johnson',     350,    NOW() - INTERVAL '30 days',  'pending',  FALSE),
('Bob Smith',         1250,   NOW() - INTERVAL '29 days',  'shipped',  FALSE),
('Charlie Brown',     7800,   NOW() - INTERVAL '28 days',  'shipped',  FALSE),
('Diana Prince',      25000,  NOW() - INTERVAL '27 days',  'pending',  FALSE),
('Evan Lee',          520,    NOW() - INTERVAL '26 days',  'canceled', FALSE),
('Fiona Adams',       999,    NOW() - INTERVAL '25 days',  'pending',  FALSE),
('George Miller',     15400,  NOW() - INTERVAL '24 days',  'shipped',  FALSE),
('Hannah Davis',      2300,   NOW() - INTERVAL '23 days',  'pending',  FALSE),
('Ian Wright',        4875,   NOW() - INTERVAL '22 days',  'pending',  FALSE),
('Julia Stone',       300,    NOW() - INTERVAL '21 days',  'shipped',  FALSE),
('Kevin Park',        42000,  NOW() - INTERVAL '20 days',  'shipped',  FALSE),
('Laura Chen',        750,    NOW() - INTERVAL '19 days',  'pending',  FALSE),
('Mark Green',        1800,   NOW() - INTERVAL '18 days',  'canceled', FALSE),
('Nina Patel',        605,    NOW() - INTERVAL '17 days',  'pending',  FALSE),
('Oscar Diaz',        9800,   NOW() - INTERVAL '16 days',  'shipped',  FALSE),
('Paula Gomez',       1525,   NOW() - INTERVAL '15 days',  'pending',  FALSE),
('Quinn Baker',       26800,  NOW() - INTERVAL '14 days',  'shipped',  FALSE),
('Rita Ora',          410,    NOW() - INTERVAL '13 days',  'pending',  FALSE),
('Sam Young',         3500,   NOW() - INTERVAL '12 days',  'pending',  FALSE),
('Tina King',         12400,  NOW() - INTERVAL '11 days',  'shipped',  FALSE),
('Uma Reed',          890,    NOW() - INTERVAL '10 days',  'pending',  FALSE),
('Victor Hugo',       650,    NOW() - INTERVAL '9 days',   'canceled', FALSE),
('Wendy Frost',       22200,  NOW() - INTERVAL '8 days',   'shipped',  FALSE),
('Yara Novak',        510,    NOW() - INTERVAL '7 days',   'pending',  FALSE),
('Zack Cole',         17500,  NOW() - INTERVAL '6 days',   'shipped',  FALSE),
('Ola Svensson',      990,    NOW() - INTERVAL '5 days',   'pending',  FALSE),
('Maja Nilsson',      30500,  NOW() - INTERVAL '4 days',   'shipped',  FALSE),
('Erik Karlsson',     470,    NOW() - INTERVAL '3 days',   'pending',  FALSE),
('Sara Lind',         5550,   NOW() - INTERVAL '2 days',   'pending',  FALSE),
('Anton Larsson',     8200,   NOW() - INTERVAL '1 days',   'shipped',  FALSE);

-- ALTER TABLE операции

ALTER TABLE shipment_raw RENAME TO shipments;

ALTER TABLE shipments
ALTER COLUMN weight_grams TYPE NUMERIC(10, 3)
USING weight_grams / 1000.0;

ALTER TABLE shipments RENAME COLUMN weight_grams TO weight_kg;

ALTER TABLE shipments ADD COLUMN tracking_code TEXT;

UPDATE shipments
SET tracking_code = 'TRK-' || TO_CHAR(shipped_at, 'YYYYDDD') || '-' || LPAD(id::TEXT, 5, '0');

ALTER TABLE shipments DROP COLUMN deprecated_flag;