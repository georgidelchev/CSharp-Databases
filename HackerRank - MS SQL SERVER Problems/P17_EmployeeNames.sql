CREATE TABLE Employee
(
    employee_id INT IDENTITY PRIMARY KEY,
    name        NVARCHAR(20)  NOT NULL,
    months      INT           NOT NULL,
    salary      DECIMAL(6, 2) NOT NULL
)

INSERT INTO Employee(name, months, salary)
    VALUES ('Rose', 15, 1968),
           ('Angela', 1, 3443),
           ('Frank', 17, 1608),
           ('Patrick', 7, 1345),
           ('Lisa', 11, 2330),
           ('Kimberley', 16, 4372),
           ('Bonnie', 8, 1771),
           ('Michael', 6, 2017),
           ('Todd', 5, 3396),
           ('Joe', 9, 3573)

SELECT name
    FROM Employee
    ORDER BY name