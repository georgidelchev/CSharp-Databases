CREATE TABLE Triangles
(
    A INT NOT NULL,
    B INT NOT NULL,
    C INT NOT NULL
)

INSERT INTO Triangles(A, B, C)
    VALUES (20, 20, 23),
           (20, 20, 20),
           (20, 21, 22),
           (13, 14, 30)

SELECT CASE
           WHEN A + B > C AND B + C > A AND A + C > B THEN
               CASE
                   WHEN A = B AND B = C THEN 'Equilateral'
                   WHEN A = B OR B = C OR A = C THEN 'Isosceles'
                   ELSE 'Scalene'
                   END
           ELSE 'Not A Triangle'
           END
    FROM Triangles