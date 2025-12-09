CREATE TABLE item (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    rarity TEXT NOT NULL CHECK (rarity IN ('common', 'rare', 'epic', 'legendary')),
    stack_limit INTEGER NOT NULL CHECK (stack_limit > 0)
);

-- Связь 1:1 с ITEM
CREATE TABLE item_info (
    item_id INTEGER PRIMARY KEY,
    weight NUMERIC(10, 2) NOT NULL CHECK (weight >= 0),
    is_tradeable BOOLEAN NOT NULL,
    durability_max INTEGER NOT NULL CHECK (durability_max >= 0),
    FOREIGN KEY (item_id) REFERENCES item(id) ON DELETE CASCADE
);

-- Связь 1:N с ITEM
CREATE TABLE recipe (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    output_item_id INTEGER NOT NULL,
    craft_time_seconds INTEGER NOT NULL CHECK (craft_time_seconds > 0),
    required_station TEXT NOT NULL CHECK (required_station IN ('campfire', 'anvil', 'alchemy_table', 'workbench')),
    FOREIGN KEY (output_item_id) REFERENCES item(id) ON DELETE RESTRICT
);

-- Связь M:N
CREATE TABLE recipe_ingredient (
    recipe_id INTEGER NOT NULL,
    item_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    PRIMARY KEY (recipe_id, item_id),
    FOREIGN KEY (recipe_id) REFERENCES recipe(id) ON DELETE CASCADE,
    FOREIGN KEY (item_id) REFERENCES item(id) ON DELETE RESTRICT
);


INSERT INTO item (name, rarity, stack_limit) VALUES
('Wood Log', 'common', 100),
('Iron Ore', 'common', 50),
('Gold Ore', 'rare', 30),
('Dragon Scale', 'legendary', 10),
('Herb Bundle', 'common', 64);

INSERT INTO item (name, rarity, stack_limit) VALUES
('Iron Ingot', 'common', 64),
('Gold Ingot', 'rare', 32),
('Iron Sword', 'rare', 1),
('Golden Sword', 'epic', 1),
('Health Potion', 'common', 10),
('Dragon Armor', 'legendary', 1);

-- Расширенная информация о предметах (связь 1:1)
INSERT INTO item_info (item_id, weight, is_tradeable, durability_max) VALUES
(1, 2.5, TRUE, 0),      -- Wood Log
(2, 5.0, TRUE, 0),      -- Iron Ore
(3, 8.0, TRUE, 0),      -- Gold Ore
(4, 15.0, FALSE, 0),    -- Dragon Scale
(5, 0.5, TRUE, 0),      -- Herb Bundle
(6, 3.5, TRUE, 0),      -- Iron Ingot
(7, 6.0, TRUE, 0),      -- Gold Ingot
(8, 4.5, TRUE, 100),    -- Iron Sword
(9, 5.5, TRUE, 150),    -- Golden Sword
(10, 0.3, TRUE, 0),     -- Health Potion
(11, 25.0, FALSE, 500); -- Dragon Armor

-- Рецепты (связь 1:N - один предмет может иметь несколько рецептов)
INSERT INTO recipe (name, output_item_id, craft_time_seconds, required_station) VALUES
('Smelt Iron Ingot', 6, 30, 'campfire'),
('Smelt Gold Ingot', 7, 45, 'campfire'),
('Forge Iron Sword', 8, 120, 'anvil'),
('Forge Golden Sword', 9, 180, 'anvil'),
('Brew Health Potion', 10, 60, 'alchemy_table'),
('Craft Dragon Armor', 11, 300, 'anvil');

-- Альтернативный рецепт для Iron Sword (демонстрация 1:N)
INSERT INTO recipe (name, output_item_id, craft_time_seconds, required_station) VALUES
('Quick Forge Iron Sword', 8, 90, 'workbench');

-- Ингредиенты для рецептов (связь M:N)

-- Smelt Iron Ingot: 3x Iron Ore
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(1, 2, 3);

-- Smelt Gold Ingot: 2x Gold Ore
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(2, 3, 2);

-- Forge Iron Sword: 4x Iron Ingot + 1x Wood Log
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(3, 6, 4),
(3, 1, 1);

-- Forge Golden Sword: 4x Gold Ingot + 1x Wood Log
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(4, 7, 4),
(4, 1, 1);

-- Brew Health Potion: 3x Herb Bundle
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(5, 5, 3);

-- Craft Dragon Armor: 10x Dragon Scale + 5x Gold Ingot
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(6, 4, 10),
(6, 7, 5);

-- Quick Forge Iron Sword (альтернативный рецепт): 3x Iron Ingot + 2x Wood Log
INSERT INTO recipe_ingredient (recipe_id, item_id, quantity) VALUES
(7, 6, 3),
(7, 1, 2);