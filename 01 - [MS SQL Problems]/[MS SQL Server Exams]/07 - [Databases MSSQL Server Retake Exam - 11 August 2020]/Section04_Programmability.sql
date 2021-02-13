-- 11. Customers With Countries
CREATE VIEW v_UserWithCountries AS
SELECT c.FirstName + ' ' + c.LastName AS CustomerName,
       c.Age,
       c.Gender,
       C2.Name                        AS CountryName
    FROM Customers AS c
             JOIN Countries C2
                  ON C2.Id = c.CountryId

SELECT TOP 5 *
    FROM v_UserWithCountries
    ORDER BY Age

-- 12.	Delete Products
CREATE TRIGGER tr_DeleteProducts
    ON Products
    INSTEAD OF DELETE AS
BEGIN
    DECLARE @deletedProducts INT = (
        SELECT p.Id
            FROM Products AS p
                     JOIN deleted AS d
                          ON p.Id = d.Id)

    DELETE
        FROM Feedbacks
        WHERE ProductId = @deletedProducts

    DELETE
        FROM ProductsIngredients
        WHERE ProductId = @deletedProducts

    DELETE Products
        WHERE Id = @deletedProducts
END
    DELETE
        FROM Products
        WHERE Id = 7
