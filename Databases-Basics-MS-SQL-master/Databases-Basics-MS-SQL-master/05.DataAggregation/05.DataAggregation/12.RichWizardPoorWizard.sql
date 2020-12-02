SELECT SUM(TotalDiff.Diff) AS [SumDifference]
  FROM (SELECT fw.DepositAmount - (SELECT sw.DepositAmount 
                                     FROM WizzardDeposits sw 
                                    WHERE sw.Id = fw.Id + 1) AS [Diff]
        FROM WizzardDeposits fw) AS [TotalDiff]