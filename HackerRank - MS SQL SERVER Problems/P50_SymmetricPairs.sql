CREATE TABLE Functions
(
    Id INT PRIMARY KEY IDENTITY,
    X  INT,
    Y  INT
)

INSERT INTO Functions(X, Y)
    VALUES (20, 20),
           (20, 20),
           (20, 21),
           (23, 22),
           (22, 23),
           (21, 20)

SELECT f1.X, f1.Y
    FROM Functions AS f
             JOIN Functions f1 ON f.X = f1.Y AND f.Y = f1.X
    GROUP BY f1.X, f1.Y
    HAVING COUNT(f1.X) > 1
        OR f1.X < f1.Y
    ORDER BY f1.X


