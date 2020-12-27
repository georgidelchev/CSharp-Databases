CREATE TABLE Employee
(
    employee_id INT PRIMARY KEY,
    name        NVARCHAR(100) NOT NULL,
    months      INT           NOT NULL,
    salary      INT           NOT NULL
)

INSERT INTO Employee(employee_id, name, months, salary)
    VALUES (12228, 'Rose', 15, 1968),
           (33645, 'Angela', 1, 3443),
           (45692, 'Frank', 17, 1608),
           (56118, 'Patrick', 7, 1345),
           (59725, 'Lisa', 11, 2330),
           (74197, 'Kimberley', 16, 4372),
           (78454, 'Bonnie', 8, 1771),
           (83565, 'Michael', 6, 2017),
           (98607, 'Todd', 5, 3396),
           (99989, 'Joe', 9, 3573)

INSERT INTO Employee(employee_id, name, months, salary)
    VALUES (741917, 'Kimberley', 16, 4372),
           (741927, 'Kimberley', 16, 4372)

SELECT TOP 1 (e.salary * e.months) AS Earnings,
             COUNT(*)              AS Count
    FROM Employee AS e
    GROUP BY e.salary, e.months
    ORDER BY Earnings DESC
