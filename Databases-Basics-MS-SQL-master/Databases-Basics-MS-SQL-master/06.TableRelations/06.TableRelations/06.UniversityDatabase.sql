CREATE TABLE Majors (
    MajorID INT NOT NULL IDENTITY(1, 1),
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Majors PRIMARY KEY (MajorID)
)

CREATE TABLE Subjects (
    SubjectID INT NOT NULL IDENTITY(1, 1),
    SubjectName NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Subjects PRIMARY KEY (SubjectID)
)

CREATE TABLE Students (
    StudentID INT NOT NULL IDENTITY(1, 1),
    StudentNumber NVARCHAR(10) NOT NULL,
    StudentName NVARCHAR(50) NOT NULL,
    MajorID INT NOT NULL,

    CONSTRAINT PK_Students PRIMARY KEY (StudentID),
    CONSTRAINT FK_Students_Majors FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)
)

CREATE TABLE Agenda (
    StudentID INT NOT NULL,
    SubjectID INT NOT NULL,

    CONSTRAINT PK_Agenda PRIMARY KEY (StudentID, SubjectID),
    CONSTRAINT FK_Agenda_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_Agenda_Subjects FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
)

CREATE TABLE Payments (
    PaymentID INT NOT NULL IDENTITY(1, 1),
    PaymentDate DATE NOT NULL,
    PaymentAmount DECIMAL(9, 2) NOT NULL,
    StudentID INT NOT NULL,

    CONSTRAINT PK_Payments PRIMARY KEY (PaymentID),
    CONSTRAINT FK_Payments_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
)