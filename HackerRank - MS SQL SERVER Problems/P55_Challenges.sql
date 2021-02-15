WITH data
         AS
         (
             SELECT c.Hacker_Id        AS id,
                    h.Name             AS name,
                    COUNT(c.Hacker_Id) AS counter
                 FROM Hackers h
                          JOIN Challenges c
                               ON c.Hacker_Id = h.Hacker_Id
                 GROUP BY c.Hacker_Id, h.Name
         )
SELECT id, name, counter
    FROM data
    WHERE counter = (SELECT MAX(counter) FROM data)
       OR counter IN (SELECT counter
                          FROM data
                          GROUP BY counter
                          HAVING COUNT(counter) = 1)
    ORDER BY counter DESC,
             id