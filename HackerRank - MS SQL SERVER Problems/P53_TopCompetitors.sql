SELECT h.Hacker_Id,
       h.Name
    FROM Hackers AS h,
         Challenges AS c,
         Difficulty AS d,
         Submissions AS s
    WHERE h.Hacker_Id = s.Hacker_Id
      AND c.Challenge_Id = s.Challenge_Id
      AND c.Difficulty_Level = d.Difficulty_Level
      AND s.Score = d.Score
    GROUP BY h.Hacker_Id, h.Name
    HAVING COUNT(h.Hacker_Id) > 1
    ORDER BY COUNT(c.Challenge_Id) DESC,
             h.Hacker_Id