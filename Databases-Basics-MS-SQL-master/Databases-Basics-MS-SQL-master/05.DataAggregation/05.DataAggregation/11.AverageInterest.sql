  SELECT wd.DepositGroup,
         wd.IsDepositExpired,
         AVG(wd.DepositInterest) AS [AverageInterest]
    FROM WizzardDeposits wd
   WHERE wd.DepositStartDate > '01-01-1985'
GROUP BY wd.DepositGroup, 
         wd.IsDepositExpired
ORDER BY wd.DepositGroup DESC,
         wd.IsDepositExpired