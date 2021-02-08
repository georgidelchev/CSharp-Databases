-- 05. Products By Price
SELECT p.Name,
       p.Price,
       p.Description
    FROM Products AS p
    ORDER BY p.Price DESC,
             p.Name

-- 06. Negative Feedback
SELECT f.ProductId,
       f.Rate,
       f.Description,
       f.CustomerId,
       C.Age,
       C.Gender
    FROM Feedbacks AS f
             JOIN Customers C
                  ON C.Id = f.CustomerId
    WHERE f.Rate < 5.0
    ORDER BY f.ProductId DESC,
             f.Rate

-- 07. Customers without Feedback
SELECT CONCAT(c.FirstName, ' ', c.LastName),
       c.PhoneNumber,
       c.Gender
    FROM Customers AS c
    WHERE c.Id NOT IN
          (SELECT f.CustomerId
               FROM Feedbacks AS f)
    ORDER BY c.Id

-- 08. Customers by Criteria
SELECT c.FirstName,
       c.Age,
       c.PhoneNumber
    FROM Customers AS c
    WHERE (c.Age >= 21
        AND c.FirstName LIKE ('%an%'))
       OR (c.PhoneNumber LIKE ('%38')
        AND c.CountryId != (SELECT c.Id
                                FROM Countries AS c
                                WHERE c.Name = 'Greece'))
    ORDER BY c.FirstName,
             c.Age DESC

-- 09. Middle Range Distributors
SELECT d.Name,
       I.Name,
       P.Name,
       AVG(F.Rate) AS AvgRate
    FROM Distributors AS d
             JOIN Ingredients I
                  ON d.Id = I.DistributorId
             JOIN ProductsIngredients Pi
                  ON I.Id = PI.IngredientId
             JOIN Products P
                  ON P.Id = PI.ProductId
             JOIN Feedbacks F
                  ON P.Id = F.ProductId
    GROUP BY d.Name,
             I.Name,
             P.Name
    HAVING AVG(F.Rate) BETWEEN 5 AND 8
    ORDER BY d.Name,
             I.Name,
             P.Name

-- 10. Country Representative
SELECT CountryName,
       DistributorName
    FROM (SELECT c.Name                                                             AS CountryName,
                 D.Name                                                             AS DistributorName,
                 DENSE_RANK() OVER (PARTITION BY c.Name ORDER BY COUNT(I.Id) DESC ) AS Ranking
              FROM Countries AS c
                       JOIN Distributors D
                            ON c.Id = D.CountryId
                       LEFT JOIN Ingredients I
                                 ON D.Id = I.DistributorId
              GROUP BY c.Name,
                       D.Name) AS temp
    WHERE Ranking = 1
    ORDER BY CountryName,
             DistributorName

SELECT rankQuery.Name, rankQuery.DistributorName
    FROM (
             SELECT c.Name, D.Name                                                    AS DistributorName,
                    DENSE_RANK() OVER (PARTITION BY c.Name ORDER BY COUNT(I.Id) DESC) AS rank
                 FROM Countries AS c
                          JOIN Distributors D ON c.Id = D.CountryId
                          LEFT JOIN Ingredients I ON D.Id = I.DistributorId
                 GROUP BY c.Name, D.Name
         ) AS rankQuery
    WHERE rankQuery.rank = 1
    ORDER BY rankQuery.Name, rankQuery.DistributorName