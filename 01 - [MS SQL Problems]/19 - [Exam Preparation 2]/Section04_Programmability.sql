-- 18. Exam Grades
CREATE OR
ALTER FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(18, 4))
    RETURNS NVARCHAR(100)
AS
BEGIN
    IF ((SELECT se.StudentId
             FROM StudentsExams AS se
             WHERE se.StudentId = @studentId) IS NULL)
        RETURN 'The student with provided id does not exist in the school!'

    IF (@grade > 6.00)
        RETURN 'Grade cannot be above 6.00!'

    RETURN 'You have to update ' + CAST((SELECT COUNT(se.StudentId) AS Count
                                             FROM StudentsExams AS se
                                             WHERE se.StudentId = @studentId
                                               AND se.Grade BETWEEN @grade AND @grade + 0.50) AS NVARCHAR(100)) +
           ' grades for the student ' + (SELECT s.FirstName
                                             FROM Students AS s
                                             WHERE s.Id = @studentId)
END
GO

SELECT dbo.udf_ExamGradesToUpdate(12, 6.20)
-- Grade cannot be above 6.00!

SELECT dbo.udf_ExamGradesToUpdate(12, 5.50)
-- You have to update 2 grades for the student Agace

SELECT dbo.udf_ExamGradesToUpdate(121, 5.50)
-- The student with provided id does not exist in the school!

-- 19. Exclude from school
CREATE OR
ALTER PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS
    IF ((SELECT se.StudentId
             FROM StudentsExams AS se
             WHERE se.StudentId = @StudentId) IS NULL)
        BEGIN
            THROW 100000, 'This school has no student with the provided id!', 1
        END

DELETE StudentsTeachers
    WHERE StudentId = @StudentId

DELETE StudentsSubjects
    WHERE StudentId = @StudentId

DELETE StudentsExams
    WHERE StudentId = @StudentId

DELETE Students
    WHERE Id = @StudentId
GO

EXEC usp_ExcludeFromSchool 1

SELECT COUNT(*)
    FROM Students
-- 119

EXEC usp_ExcludeFromSchool 301
-- This school has no student with the provided id!

-- 20. Deleted Student
CREATE TABLE ExcludedStudents
(
    StudentId   INT,
    StudentName NVARCHAR(100)
)

CREATE TRIGGER tr_DeletedStudents
    ON Students
    FOR DELETE
    AS
BEGIN
    INSERT INTO ExcludedStudents
        (StudentId, StudentName)

    SELECT Id, FirstName + ' ' + LastName
        FROM deleted
END
GO

DELETE
    FROM StudentsExams
    WHERE StudentId = 1

DELETE
    FROM StudentsTeachers
    WHERE StudentId = 1

DELETE
    FROM StudentsSubjects
    WHERE StudentId = 1

DELETE
    FROM Students
    WHERE Id = 1

SELECT *
    FROM ExcludedStudents
