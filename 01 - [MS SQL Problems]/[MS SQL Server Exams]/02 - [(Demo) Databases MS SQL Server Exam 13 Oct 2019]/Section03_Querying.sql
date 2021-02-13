-- 5. Commits
SELECT c.Id,
       c.Message,
       c.RepositoryId,
       c.ContributorId
    FROM Commits AS c
    ORDER BY c.Id,
             c.Message,
             c.RepositoryId,
             c.ContributorId

-- 6. Heavy HTML
SELECT f.Id,
       f.Name,
       f.Size
    FROM Files AS f
    WHERE f.Size > 1000
      AND f.Name LIKE ('%html')
    ORDER BY f.Size DESC,
             f.Id,
             f.Name

-- 7. Issues and Users
SELECT i.Id,
       U.Username + ' : ' + i.Title AS IssueAssignee
    FROM Issues AS i
             JOIN Users U
                  ON U.Id = i.AssigneeId
    ORDER BY i.Id DESC,
             IssueAssignee

-- 8. Non-Directory Files
SELECT f.Id,
       f.Name,
       CAST(f.Size AS NVARCHAR(100)) + 'KB' AS Size
    FROM Files AS f
    WHERE f.Id != f.ParentId
    ORDER BY f.Id,
             f.Name,
             Size DESC

-- 9. Most Contributed Repositories
SELECT TOP 5 r.Id,
             r.Name,
             COUNT(Rc.ContributorId) AS Commits
    FROM Repositories AS r
             JOIN Commits C
                  ON r.Id = C.RepositoryId
             JOIN RepositoriesContributors Rc
                  ON r.Id = Rc.RepositoryId
    GROUP BY r.Id, r.Name
    ORDER BY Commits DESC,
             r.Id,
             r.Name

-- 10. User and Files
SELECT u.Username,
       AVG(F.Size) AS Size
    FROM Users AS u
             JOIN Commits C
                  ON u.Id = C.ContributorId
             JOIN Files F
                  ON C.Id = F.CommitId
    GROUP BY u.Username
    ORDER BY Size DESC,
             u.Username
