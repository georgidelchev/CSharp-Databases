-- 1
SELECT wd.DepositGroup, SUM(wd.DepositAmount) AS TotalSum
    FROM WizzardDeposits AS wd
    GROUP BY wd.DepositGroup