-- image_id refers to the id on the picsum site:
-- 'https://picsum.photos/id/$image_id/200/150'

CREATE TABLE game_pictures (
    id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    image_id INTEGER NOT NULL
);

INSERT INTO game_pictures (name, image_id) VALUES
('Schuh' , 21),
('Gabel' , 23),
('Tasse', 30),
('Laptop', 48),
('Controller', 96),
('Dreamcatcher', 104),
('Auto', 133),
('Kugeln', 139),
('Roller', 146),
('Skateboard', 157),
('Uhr', 175),
('Bus', 183),
('Rind', 200),
('Teekanne', 225),
('Kaktus', 248),
('Kamera', 250),
('Pfosten', 264),
('Vase', 305),
('E-Reader', 367),
('Boot', 380);
