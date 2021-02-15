CREATE TABLE Company
(
    id           INT PRIMARY KEY IDENTITY,
    company_code NVARCHAR(30) NOT NULL,
    founder      NVARCHAR(30) NOT NULL
)

CREATE TABLE Lead_Manager
(
    id                INT PRIMARY KEY IDENTITY,
    lead_manager_code NVARCHAR(30) NOT NULL,
    company_code      NVARCHAR(30) NOT NULL
)

CREATE TABLE Senior_Manager
(
    id                  INT PRIMARY KEY IDENTITY,
    senior_manager_code NVARCHAR(30) NOT NULL,
    lead_manager_code   NVARCHAR(30) NOT NULL,
    company_code        NVARCHAR(30) NOT NULL,
)

CREATE TABLE Manager
(
    id                  INT PRIMARY KEY IDENTITY,
    manager_code        NVARCHAR(30) NOT NULL,
    senior_manager_code NVARCHAR(30) NOT NULL,
    lead_manager_code   NVARCHAR(30) NOT NULL,
    company_code        NVARCHAR(30) NOT NULL,
)

CREATE TABLE Employee
(
    id                  INT PRIMARY KEY IDENTITY,
    employee_code       NVARCHAR(30) NOT NULL,
    manager_code        NVARCHAR(30) NOT NULL,
    senior_manager_code NVARCHAR(30) NOT NULL,
    lead_employee_code  NVARCHAR(30) NOT NULL,
    company_code        NVARCHAR(30) NOT NULL,
)

INSERT INTO Company(company_code, founder)
    VALUES ('C1', 'Monika'),
           ('C2', 'Samantha')

INSERT INTO Lead_Manager(lead_manager_code, company_code)
    VALUES ('LM1', 'C1'),
           ('LM2', 'C2')

INSERT INTO Senior_Manager(senior_manager_code, lead_manager_code, company_code)
    VALUES ('SM1', 'LM1', 'C1'),
           ('SM2', 'LM1', 'C1'),
           ('SM3', 'LM2', 'C2')

INSERT INTO Manager(manager_code, senior_manager_code, lead_manager_code, company_code)
    VALUES ('M1', 'SM1', 'LM1', 'C1'),
           ('M2', 'SM3', 'LM2', 'C2'),
           ('M3', 'SM3', 'LM2', 'C2')

INSERT INTO Employee(employee_code, manager_code, senior_manager_code, lead_employee_code, company_code)
    VALUES ('E1', 'M1', 'SM1', 'LM1', 'C1'),
           ('E2', 'M1', 'SM1', 'LM1', 'C1'),
           ('E3', 'M2', 'SM3', 'LM2', 'C2'),
           ('E4', 'M3', 'SM3', 'LM2', 'C2')

SELECT c.company_code,
       c.founder,
       COUNT(DISTINCT Lm.lead_manager_code)   AS LmCount,
       COUNT(DISTINCT Sm.senior_manager_code) AS SmCount,
       COUNT(DISTINCT M.manager_code)         AS MCount,
       COUNT(DISTINCT E.employee_code)        AS ECount
    FROM Company AS c
             JOIN Lead_Manager Lm
                  ON c.company_code = Lm.company_code
             JOIN Senior_Manager Sm
                  ON c.company_code = Sm.company_code
             JOIN Manager M
                  ON c.company_code = M.company_code
             JOIN Employee E
                  ON c.company_code = E.company_code
    GROUP BY c.company_code,
             c.founder
    ORDER BY c.company_code
