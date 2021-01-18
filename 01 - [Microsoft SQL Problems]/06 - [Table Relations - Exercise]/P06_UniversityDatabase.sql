CREATE DATABASE University

USE University

CREATE TABLE Majors
(
    MajorID INT IDENTITY PRIMARY KEY,
    Name    NVARCHAR(50) NOT NULL
)

CREATE TABLE Students
(
    StudentID     INT IDENTITY PRIMARY KEY,
    StudentNumber NVARCHAR(25) NOT NULL,
    StudentName   NVARCHAR(50) NOT NULL,
    MajorID       INT          NOT NULL,

    CONSTRAINT FK_Students_Majors
        FOREIGN KEY (MajorID)
            REFERENCES Majors (MajorID)
)

CREATE TABLE Payments
(
    PaymentID     INT IDENTITY PRIMARY KEY,
    PaymentDate   DATE          NOT NULL,
    PaymentAmount DECIMAL(6, 2) NOT NULL,
    StudentID     INT           NOT NULL,

    CONSTRAINT FK_Payments_Students
        FOREIGN KEY (StudentID)
            REFERENCES Students (StudentID)
)

CREATE TABLE Subjects
(
    SubjectID   INT IDENTITY PRIMARY KEY,
    SubjectName NVARCHAR(30) NOT NULL
)

CREATE TABLE Agenda
(
    StudentID INT NOT NULL,
    SubjectID INT NOT NULL,
    
    CONSTRAINT PK_Composite_StudentID_SubjectID
  	    PRIMARY KEY (StudentID, SubjectID),

    CONSTRAINT FK_Agenda_Students
        FOREIGN KEY (StudentID)
            REFERENCES Students (StudentID),
    
    CONSTRAINT FK_Agenda_Subjects
        FOREIGN KEY (SubjectID)
            REFERENCES Subjects (SubjectID)
)
