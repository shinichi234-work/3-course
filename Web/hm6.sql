-- variant 9
CREATE SCHEMA sports_league;

CREATE TABLE sports_league.teams (
    team_id INTEGER PRIMARY KEY,
    team_name TEXT NOT NULL UNIQUE,
    city TEXT NOT NULL,
    foundation_date DATE NOT NULL
);

CREATE TABLE sports_league.players (
    player_id INTEGER PRIMARY KEY,
    player_name TEXT NOT NULL,
    position TEXT NOT NULL,
    rating INTEGER DEFAULT 50 NOT NULL
);