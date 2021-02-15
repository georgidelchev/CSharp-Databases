CREATE TABLE BST
(
    Id INT PRIMARY KEY IDENTITY,
    N  INT,
    P  INT
)

INSERT INTO BST(N, P)
    VALUES (1, 2),
           (3, 2),
           (6, 8),
           (9, 8),
           (2, 5),
           (8, 5),
           (5, NULL)

SELECT CAST(N AS NVARCHAR(10)) + ' ' +
       CASE
           WHEN P IS NULL THEN 'Root'
           WHEN N IN (SELECT DISTINCT P
                        FROM BST) THEN 'Inner'
           ELSE 'Leaf'
           END
    FROM BST AS b
    ORDER BY N