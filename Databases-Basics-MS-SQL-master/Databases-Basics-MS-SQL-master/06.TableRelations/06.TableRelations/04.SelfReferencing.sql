CREATE TABLE Teachers (
    TeacherID INT NOT NULL IDENTITY(101, 1),
    Name NVARCHAR(50) NOT NULL,
    ManagerID INT,

    CONSTRAINT PK_Teachers PRIMARY KEY (TeacherID),
    CONSTRAINT FK_Teachers FOREIGN KEY (ManagerID) REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers (Name, ManagerID)
     VALUES ('John', NULL),
            ('Maya', 106),
            ('Silvia', 106),
            ('Ted', 105),
            ('Mark', 101),
            ('Greta', 101)