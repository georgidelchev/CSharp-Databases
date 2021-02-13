-- 11. User Total Commits
CREATE OR
ALTER FUNCTION udf_UserTotalCommits(@username NVARCHAR(100))
    RETURNS INT
AS
BEGIN
    DECLARE @commitsCount INT = (SELECT COUNT(C.Id) AS CommitsCount
                                     FROM Users AS u
                                              JOIN Commits C
                                                   ON u.Id = C.ContributorId
                                     WHERE u.Username = @username)

    RETURN @commitsCount
END
GO

SELECT dbo.udf_UserTotalCommits('UnderSinduxrein')
-- 6

-- 12. Find by Extensions
CREATE OR
ALTER PROCEDURE usp_FindByExtension(@extension NVARCHAR(20))
AS
SELECT f.Id,
       f.Name,
       CAST(f.Size AS NVARCHAR(100)) + 'KB' AS Size
    FROM Files AS f
    WHERE f.Name LIKE ('%' + @extension)
    ORDER BY f.Id,
             f.Name,
             Size DESC
GO

EXEC usp_FindByExtension 'txt'
--  Id	   Name	        Size
--  28	 Jason.txt	 10317.54KB
--  31	 file.txt	 5514.02KB
--  43	 init.txt	 16089.79KB










