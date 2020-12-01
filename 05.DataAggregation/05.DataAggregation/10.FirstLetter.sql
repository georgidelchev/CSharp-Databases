SELECT DISTINCT LEFT(wd.FirstName, 1) AS [FirstLetter]
  FROM WizzardDeposits wd
  WHERE wd.DepositGroup = 'Troll Chest'