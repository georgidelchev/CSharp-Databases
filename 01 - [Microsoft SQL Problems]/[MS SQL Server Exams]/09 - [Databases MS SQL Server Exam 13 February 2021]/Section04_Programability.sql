-- 11. All User Commits
CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(30))
    RETURNS INT
BEGIN
    RETURN (SELECT COUNT(C.Id)
                FROM Users AS u
                         LEFT JOIN Commits C ON u.Id = C.ContributorId
                WHERE u.Username = @username
    )
END

-- 12. Search for Files
CREATE PROCEDURE usp_SearchForFiles(@fileExtension VARCHAR(10))
AS
BEGIN
    SELECT f.Id,
           f.Name,
           CAST(f.Size AS VARCHAR) + 'KB'
        FROM Files AS f
        WHERE f.Name LIKE ('%' + @fileExtension)
        ORDER BY f.Id,
                 f.Name,
                 f.Size DESC
END