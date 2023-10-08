
CREATE SCHEMA IF NOT EXISTS box_factory;
create table box_factory.Boxes
(
    
    BoxId serial PRIMARY KEY,
    BoxName VARCHAR(255),
    BoxWeight FLOAT
);
INSERT INTO box_factory.Boxes (BoxName, BoxWeight)
VALUES
    ('Box1', 10.5),
    ('Box2', 15.2),
    ('Box3', 12.8),
    ('Box4', 18.7),
    ('Box5', 9.3),
    ('Box6', 20.0);