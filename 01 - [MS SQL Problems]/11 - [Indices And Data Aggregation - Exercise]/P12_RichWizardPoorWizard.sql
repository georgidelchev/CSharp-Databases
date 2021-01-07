-- 1
SELECT SUM(Difference) AS SumDifference
    FROM (SELECT wd.FirstName                                                 AS HostWizard,
                 wd.DepositAmount                                             AS HostWizardDeposit,
                 LEAD(wd.FirstName) OVER (ORDER BY Id)                        AS GuestWizard,
                 LEAD(wd.DepositAmount) OVER (ORDER BY Id)                    AS GuestWizardDeposit,
                 wd.DepositAmount - LEAD(wd.DepositAmount) OVER (ORDER BY Id) AS Difference
              FROM WizzardDeposits AS wd) AS DifferenceQuery

-- 2
SELECT SUM(Difference) AS SumDifference
    FROM (SELECT wd.DepositAmount - LEAD(wd.DepositAmount) OVER (ORDER BY Id) AS Difference
              FROM WizzardDeposits AS wd) AS DifferenceQuery