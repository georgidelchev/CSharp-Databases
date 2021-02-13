-- 5. Commits
SELECT c.Id,
       c.Message,
       c.RepositoryId,
       c.ContributorId
    FROM Commits AS c
    ORDER BY c.Id,
             c.Message,
             c.RepositoryId

-- 6. Front-end
SELECT f.Id,
       f.Name,
       f.Size
    FROM Files AS f
    WHERE f.Size > 1000
      AND f.Name LIKE ('%html%')
    ORDER BY f.Size DESC,
             f.Id,
             f.Name

-- 7. Issue Assignment
SELECT i.Id,
       U.Username + ' : ' + i.Title
    FROM Issues AS i
             JOIN Users U
                  ON U.Id = i.AssigneeId
    ORDER BY i.Id DESC,
             U.Username

-- 8. Single Files
SELECT f1.Id,
       f1.Name,
       CAST(f1.Size AS VARCHAR(100)) + 'KB'
    FROM Files AS f
             RIGHT JOIN Files AS f1
                        ON f.ParentId = f1.Id
    WHERE f.ParentId IS NULL
    ORDER BY f.Id,
             f.Name,
             f.Size DESC

-- 9. Commits in Repositories
SELECT TOP 5 r.Id,
             r.Name,
             COUNT(C.Id) AS Count
    FROM Repositories AS r
             JOIN Commits C
                  ON r.Id = C.RepositoryId
             JOIN RepositoriesContributors Rc
                  ON r.Id = Rc.RepositoryId
    GROUP BY r.Id,
             r.Name
    ORDER BY Count DESC,
             r.Id,
             r.Name

-- 10. Average Size
SELECT u.Username,
       AVG(F.Size) AS Avg
    FROM Users AS u
             JOIN Commits C
                  ON u.Id = C.ContributorId
             JOIN Files F
                  ON C.Id = F.CommitId
    WHERE C.ContributorId IS NOT NULL
    GROUP BY u.Username
    ORDER BY Avg DESC,
             u.Username