CREATE VIEW v_ClientBalances 
AS
        SELECT (c.FirstName + ' ' + c.LastName) AS [Name], 
               act.Name AS [Account Type],
               a.Balance
          FROM Clients AS c
    INNER JOIN Accounts AS a
            ON a.ClientId = c.ClientId
    INNER JOIN AccountTypes AS act
            ON act.AccountTypeId = a.AccountTypeId
GO