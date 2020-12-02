  SELECT wd.DepositGroup,
         wd.MagicWandCreator,
         MIN(wd.DepositCharge) AS [MinDepositCharge]
    FROM WizzardDeposits wd
GROUP BY wd.DepositGroup, wd.MagicWandCreator