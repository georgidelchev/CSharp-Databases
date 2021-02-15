SELECT T1.Start_Date, T2.End_Date
    FROM (SELECT Start_Date,
                 ROW_NUMBER() OVER (ORDER BY Start_Date) RN
              FROM Projects
              WHERE Start_Date NOT IN (SELECT End_Date
                                           FROM Projects)) AS T1
             INNER JOIN (
        SELECT End_Date, ROW_NUMBER() OVER (ORDER BY End_Date) RN
            FROM Projects
            WHERE End_Date NOT IN (SELECT Start_Date
                                       FROM Projects)
    ) AS T2 ON T1.RN = T2.RN
    ORDER BY DATEDIFF(DAY, T1.Start_Date, T2.End_Date),
             T1.Start_Date