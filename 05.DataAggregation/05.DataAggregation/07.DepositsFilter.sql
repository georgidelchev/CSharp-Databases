  SELECT wd.DepositGroup,
         SUM(wd.DepositAmount) AS [TotalSum]
    FROM WizzardDeposits wd
   WHERE wd.MagicWandCreator = 'Ollivander family'
GROUP BY wd.DepositGroup
  HAVING SUM(wd.DepositAmount) < 150000
ORDER BY [TotalSum] DESC