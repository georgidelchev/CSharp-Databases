DECLARE
    @Year SMALLINT = 2020

SELECT GETDATE(), @Year AS Year

SET @Year = 2021

IF @Year = DATEPART(YEAR, GETDATE())
    BEGIN
        SELECT GETDATE()
        SET @Year = DATEPART(YEAR, GETDATE()) + 1
    END
ELSE
    IF @Year = 2029
        SELECT '2029'
    ELSE
        SELECT 'No match'

DECLARE @year INT = 1999;

WHILE @Year <= 2005
    BEGIN
        SELECT @Year, COUNT(*)
            FROM Employees
            WHERE DATEPART(YEAR, HireDate) = @Year

        SET @Year += 1
    END

CREATE FUNCTION udf_ProjectDurationWeeks(@StartDate DATETIME, @EndDate DATETIME)
    RETURNS INT
AS
BEGIN
    DECLARE @projectWeeks INT;
    IF (@EndDate IS NULL)
        BEGIN
            SET @EndDate = GETDATE()
        END
    SET @projectWeeks = DATEDIFF(WEEK, @StartDate, @EndDate)
    RETURN @projectWeeks
END

CREATE FUNCTION udf_Pow(@Base INT, @Exp INT)
    RETURNS BIGINT
AS
BEGIN
    DECLARE @result BIGINT = 1;

    WHILE (@Exp > 0)
        BEGIN
            SET @result *= @Base;
            SET @Exp -= 1;
        END

    RETURN @result;
END

SELECT dbo.udf_Pow(2, 10) AS MyPow,
       POWER(2, 10)       AS DefaultPow

CREATE OR
ALTER FUNCTION udf_GetEmployeesCountByYear(@Year INT)
    RETURNS TABLE
        AS
        RETURN
    (
        SELECT *
            FROM Employees
            WHERE DATEPART(YEAR, HireDate) = @Year

    )

SELECT *
    FROM dbo.udf_GetEmployeesCountByYear(2000)

CREATE OR
ALTER FUNCTION udf_Squares(@Count INT)
    RETURNS @square TABLE
    (
        Id     INT PRIMARY KEY IDENTITY,
        Square BIGINT
    )
AS
BEGIN
    DECLARE @i INT = 1;

    WHILE (@i <= @Count)
        BEGIN
            INSERT INTO @square
                VALUES (@i * @i)
            SET @i += 1
        END
    RETURN
END

SELECT *
    FROM dbo.udf_Squares(10)
    WHERE Id >= 5
    ORDER BY Square DESC

CREATE OR
ALTER FUNCTION ufn_GetSalaryLevel(@Salary MONEY)
    RETURNS NVARCHAR(10)
AS
BEGIN
    IF (@Salary IS NULL)
        RETURN NULL;
    IF (@Salary < 30000)
        RETURN 'Low';
    ELSE
        IF (@Salary <= 50000)
            RETURN 'Average'
        ELSE
            RETURN 'High'

    RETURN '';
END

SELECT FirstName,
       LastName,
       Salary,
       dbo.ufn_GetSalaryLevel(Salary) AS SalaryLevel
    FROM Employees

CREATE OR
ALTER PROCEDURE sp_GetEmployeesByExperience(@Year INT)
AS
SELECT *
    FROM Employees
    WHERE DATEDIFF(YEAR, HireDate, GETDATE()) > @Year
GO

EXECUTE sp_GetEmployeesByExperience 19

CREATE PROCEDURE sp_InsertEmployeeProject(@EmployeeId INT, @ProjectId INT)
AS
DECLARE @ProjectsCount INT;

    SET @ProjectsCount = (SELECT COUNT(*)
                              FROM EmployeesProjects
                              WHERE EmployeeID = @EmployeeId)
    IF (@ProjectsCount >= 3)
        THROW 50001,'Employee already have 3 or more projects', 1;

INSERT INTO EmployeesProjects(EmployeeID, ProjectID)
    VALUES (@EmployeeId, @ProjectId)
GO

sp_InsertEmployeeProject 2, 2;



