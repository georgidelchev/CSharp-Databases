  SELECT wd.DepositGroup,
         SUM(wd.DepositAmount) AS [TotalSum]
    FROM WizzardDeposits wd
GROUP BY wd.DepositGroup