SELECT wd.DepositGroup, SUM(wd.DepositAmount) AS TotalSum
    FROM WizzardDeposits AS wd
    WHERE wd.MagicWandCreator = 'Ollivander family'
    GROUP BY DepositGroup