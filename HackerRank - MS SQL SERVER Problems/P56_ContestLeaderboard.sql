SELECT h.Hacker_Id,
       h.Name,
       SUM(score)
    FROM (
             SELECT Hacker_Id,
                    Challenge_Id,
                    MAX(Score) AS score
                 FROM Submissions
                 GROUP BY Hacker_Id, Challenge_Id
         ) t
             JOIN Hackers h
                  ON t.hacker_id = h.Hacker_Id
    GROUP BY h.Hacker_Id, h.Name
    HAVING SUM(score) > 0
    ORDER BY SUM(score) DESC,
             h.Hacker_Id