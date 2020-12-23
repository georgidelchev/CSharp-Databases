SELECT wd.DepositGroup, wd.MagicWandCreator, MIN(wd.DepositCharge) AS MinDepositCharge
    FROM WizzardDeposits AS wd
    GROUP BY wd.DepositGroup, wd.MagicWandCreator
    ORDER BY wd.MagicWandCreator, wd.DepositGroup