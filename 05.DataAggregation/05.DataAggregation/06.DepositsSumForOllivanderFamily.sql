  SELECT wd.DepositGroup,
         SUM(wd.DepositAmount) AS [TotalSum]
    FROM WizzardDeposits wd
   WHERE wd.MagicWandCreator = 'Ollivander family'
GROUP BY wd.DepositGroup