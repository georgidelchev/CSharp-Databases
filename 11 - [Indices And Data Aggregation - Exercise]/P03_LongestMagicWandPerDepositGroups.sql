-- 1
SELECT wd.DepositGroup, MAX(wd.MagicWandSize) AS LongestMagicWand
    FROM WizzardDeposits AS wd
    GROUP BY DepositGroup