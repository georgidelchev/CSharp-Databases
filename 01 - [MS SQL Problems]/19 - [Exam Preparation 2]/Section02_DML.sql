-- 2. Insert

INSERT INTO Teachers(FirstName, LastName, Address, Phone, SubjectId)
    VALUES ('Ruthanne', 'Bamb', '84948 Mesta Junction', '3105500146', 6),
           ('Gerrard', 'Lowin', '370 Talisman Plaza', '3324874824', 2),
           ('Merrile', 'Lambdin', '81 Dahle Plaza', '4373065154', 5),
           ('Bert', 'Ivie', '2 Gateway Circle', '4409584510', 4)

INSERT INTO Subjects(Name, Lessons)
    VALUES ('Geometry', 12),
           ('Health', 10),
           ('Drama', 7),
           ('Sports', 9)

-- 3. Update
UPDATE StudentsSubjects
SET Grade = 6.00
    WHERE SubjectId IN (1, 2)
      AND Grade >= 5.50

-- 4. Delete
DELETE StudentsTeachers
    WHERE TeacherId IN (SELECT t.Id
                            FROM Teachers AS t
                            WHERE t.Phone LIKE ('%72%'))

DELETE Teachers
    WHERE Phone LIKE ('%72%')
