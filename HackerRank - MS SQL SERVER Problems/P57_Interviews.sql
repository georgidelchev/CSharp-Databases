SELECT con.Contest_Id,
       con.Hacker_Id,
       con.Name,
       SUM(total_submissions),
       SUM(total_accepted_submissions),
       SUM(total_views), SUM(total_unique_views)
    FROM Contests con
             JOIN Colleges col
                  ON con.Contest_Id = col.Contest_Id
             JOIN Challenges cha
                  ON col.College_Id = cha.College_Id
             LEFT JOIN
         (SELECT Challenge_Id,
                 SUM(Total_Views)        AS total_views,
                 SUM(Total_Unique_Views) AS total_unique_views
              FROM View_Stats
              GROUP BY Challenge_Id) vs
         ON cha.Challenge_Id = vs.Challenge_Id
             LEFT JOIN
         (SELECT Challenge_Id,
                 SUM(Total_Submissions)          AS total_submissions,
                 SUM(Total_Accepted_Submissions) AS total_accepted_submissions
              FROM Submission_Stats
              GROUP BY Challenge_Id) ss
         ON cha.Challenge_Id = ss.Challenge_Id
    GROUP BY con.Contest_Id, con.Hacker_Id, con.Name
    HAVING SUM(total_submissions) != 0
        OR SUM(total_accepted_submissions) != 0
        OR SUM(total_views) != 0
        OR SUM(total_unique_views) != 0
    ORDER BY Contest_Id;