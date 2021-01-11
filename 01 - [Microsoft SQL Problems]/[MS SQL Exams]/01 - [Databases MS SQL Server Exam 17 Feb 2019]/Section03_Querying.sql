-- 5. Teen Students
SELECT s.FirstName,
       s.LastName,
       s.Age
    FROM Students AS s
    WHERE s.Age >= 12
    ORDER BY s.FirstName,
             s.LastName

-- 6. Cool Addresses
SELECT s.FirstName + ' ' + s.LastName AS FullName,
       s.Address
    FROM Students AS s
    WHERE s.Address LIKE ('%road%')
    ORDER BY s.FirstName,
             s.LastName,
             s.Address

-- 7. 42 Phones
SELECT s.FirstName,
       s.Address,
       s.Phone
    FROM Students AS s
    WHERE s.MiddleName IS NOT NULL
      AND s.Phone LIKE ('42%')
    ORDER BY s.FirstName

-- 8. Students Teachers
SELECT s.FirstName,
       s.LastName,
       COUNT(St.TeacherId) AS TeachersCount
    FROM Students AS s
             JOIN StudentsTeachers St
                  ON s.Id = St.StudentId
    GROUP BY s.FirstName,
             s.LastName

-- 9. Subjects with Students
SELECT t.FirstName + ' ' + t.LastName                 AS FullName,
       S.Name + '-' + CAST(S.Lessons AS NVARCHAR(30)) AS Subjects,
       COUNT(St.StudentId)                            AS Students
    FROM Teachers AS t
             JOIN Subjects S
                  ON S.Id = t.SubjectId
             JOIN StudentsTeachers St
                  ON t.Id = St.TeacherId
    GROUP BY t.FirstName,
             t.LastName,
             S.Name,
             S.Lessons
    ORDER BY Students DESC,
             FullName,
             Subjects

-- 10. Students to Go
-- 1
SELECT s.FirstName + ' ' + s.LastName AS FullName
    FROM Students AS s
    WHERE s.Id NOT IN (SELECT se.StudentId
                           FROM StudentsExams AS se)
    ORDER BY FullName

-- 2
SELECT s.FirstName + ' ' + s.LastName AS FullName
    FROM Students AS s
             LEFT JOIN StudentsExams Se
                       ON s.Id = Se.StudentId
    WHERE Se.ExamId IS NULL
    ORDER BY FullName

-- 11. Busiest Teachers
SELECT TOP 10 T.FirstName,
              T.LastName,
              COUNT(st.StudentId) AS StudentsCount
    FROM StudentsTeachers AS st
             JOIN Teachers T
                  ON T.Id = st.TeacherId
    GROUP BY T.FirstName,
             T.LastName
    ORDER BY StudentsCount DESC,
             T.FirstName,
             T.LastName

-- 12. Top Students
SELECT S.FirstName,
       S.LastName,
       FORMAT(AVG(st.Grade), 'F2') AS Grade
    FROM StudentsExams AS st
             JOIN Students S
                  ON S.Id = st.StudentId
    GROUP BY S.FirstName,
             S.LastName
    ORDER BY Grade DESC,
             S.FirstName,
             S.LastName

-- 13. Second Highest Grade
SELECT FirstName,
       LastName,
       Grade
    FROM (SELECT s.FirstName,
                 s.LastName,
                 ROW_NUMBER() OVER (PARTITION BY s.FirstName,s.LastName ORDER BY Ss.Grade DESC) AS Rank,
                 Ss.Grade
              FROM Students AS s
                       JOIN StudentsSubjects Ss
                            ON s.Id = Ss.StudentId) AS GradeRankQuery
    WHERE [RANK] = 2
    ORDER BY FirstName,
             LastName

-- 14. Not So In The Studying
SELECT s.FirstName + ' ' + ISNULL(s.MiddleName + ' ', '') + s.LastName AS FullName
    FROM Students AS s
    WHERE s.Id NOT IN (SELECT ss.StudentId
                           FROM StudentsSubjects AS ss)
    ORDER BY FullName

-- 15. Top Student per Teacher
SELECT TeacherFullName,
       SubjectName,
       StudentFullName,
       FORMAT(Grade, 'F2')
    FROM (SELECT TeacherFullName,
                 SubjectName,
                 StudentFullName,
                 Grade,
                 ROW_NUMBER() OVER (PARTITION BY TeacherFullName ORDER BY Grade DESC) AS Rank
              FROM (
                       SELECT t.FirstName + ' ' + t.LastName   AS TeacherFullName,
                              S.Name                           AS SubjectName,
                              S2.FirstName + ' ' + S2.LastName AS StudentFullName,
                              AVG(Ss.Grade)                    AS Grade
                           FROM Teachers AS t
                                    JOIN Subjects S
                                         ON S.Id = t.SubjectId
                                    JOIN StudentsSubjects Ss
                                         ON S.Id = Ss.SubjectId
                                    JOIN Students S2
                                         ON S2.Id = Ss.StudentId
                                    JOIN StudentsTeachers St
                                         ON S2.Id = St.StudentId AND t.Id = St.TeacherId
                           GROUP BY t.FirstName, t.LastName, S.Name, S2.FirstName, S2.LastName) AS RankQuery
         ) AS SecondQuery
    WHERE SecondQuery.Rank = 1
    ORDER BY SubjectName,
             TeacherFullName,
             Grade DESC

-- 16. Average Grade per Subject
SELECT s.Name,
       AVG(Ss.Grade) AS AverageGrade
    FROM Subjects AS s
             JOIN StudentsSubjects Ss
                  ON s.Id = Ss.SubjectId
    GROUP BY s.Name, s.Id
    ORDER BY s.Id

-- 17. Exams Information
SELECT Quarter,
       SubjectName,
       COUNT(StudentId) AS StudentsCount
    FROM (SELECT CASE
                     WHEN DATEPART(MONTH, e.Date) BETWEEN 1 AND 3 THEN 'Q1'
                     WHEN DATEPART(MONTH, e.Date) BETWEEN 4 AND 6 THEN 'Q2'
                     WHEN DATEPART(MONTH, e.Date) BETWEEN 7 AND 9 THEN 'Q3'
                     WHEN DATEPART(MONTH, e.Date) BETWEEN 10 AND 12 THEN 'Q4'
                     ELSE 'TBA'
                     END AS Quarter,
                 S.Name  AS SubjectName,
                 Se.StudentId
              FROM Exams AS e
                       JOIN Subjects S
                            ON S.Id = e.SubjectId
                       JOIN StudentsExams Se
                            ON e.Id = Se.ExamId
              WHERE Se.Grade >= 4.00) AS Query1
    GROUP BY Quarter,
             SubjectName
    ORDER BY Quarter

