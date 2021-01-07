-- 1
SELECT LEFT(wd.FirstName, 1)
    FROM WizzardDeposits AS wd
    WHERE wd.DepositGroup = 'Troll Chest'
    GROUP BY LEFT(wd.FirstName, 1)
    ORDER BY LEFT(wd.FirstName, 1)

-- 2
SELECT *
    FROM (SELECT LEFT(wd.FirstName, 1) AS FirstLetter
              FROM WizzardDeposits AS wd
              WHERE wd.DepositGroup = 'Troll Chest') AS FirstLetterQuery
    GROUP BY FirstLetter
    ORDER BY FirstLetter