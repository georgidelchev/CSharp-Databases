CREATE or ALTER PROCEDURE usp_GetHoldersFullName
AS
SELECT ah.FirstName + ' ' + ah.LastName AS FullName
    FROM AccountHolders AS ah
GO

EXEC usp_GetHoldersFullName
