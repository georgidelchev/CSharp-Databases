CREATE TABLE Students (
    StudentID INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Students PRIMARY KEY (StudentID)
)

CREATE TABLE Exams (
    ExamID INT NOT NULL IDENTITY(101, 1),
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Exams PRIMARY KEY (ExamID)
)

CREATE TABLE StudentsExams (
    StudentID INT NOT NULL,
    ExamID INT NOT NULL,

    CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentID, ExamID),
    CONSTRAINT FK_StudentsExams_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_StudentsExams_Exams FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
)

INSERT INTO Students (Name)
     VALUES ('Mila'),
            ('Toni'),
            ('Ron')

INSERT INTO Exams (Name)
     VALUES ('SpringMVC'),
            ('Neo4j'),
            ('Oracle 11g')

INSERT INTO StudentsExams (StudentID, ExamID)
     VALUES (1, 101),
            (1, 102),
            (2, 101),
            (3, 103),
            (2, 102),
            (2, 103)