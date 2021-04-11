CREATE DATABASE hackerrank

USE hackerrank

CREATE TABLE student_information
(
	roll_number INT,
	Name NVARCHAR(MAX)
)

CREATE TABLE examination_marks
(
	roll_number INT,
	subject_one INT,
	subject_two INT,
	subject_three INT,
)

INSERT INTO student_information(roll_number, Name)
	VALUES (1, 'Sheila'),	
		   (2, 'Rachel'),
		   (3, 'Christopher')

INSERT INTO examination_marks(roll_number, subject_one, subject_two, subject_three)
	VALUES (1, 32, 48, 64),
		   (2, 24, 21, 25),
		   (3, 55, 12, 10)

SELECT CAST(si.roll_number AS NVARCHAR) + ' ' + si.Name
    FROM student_information AS si
    JOIN examination_marks AS em 
		ON em.roll_number = si.roll_number
    WHERE em.subject_one + em.subject_two + em.subject_three < 100


CREATE DATABASE hackerrank2

USE hackerrank2

CREATE TABLE customers
(
	customer_id INT,
	Name NVARCHAR(MAX),
	phone_number NVARCHAR(MAX),
	country NVARCHAR(MAX)
)

CREATE TABLE country_codes
(
	country NVARCHAR(MAX),
	country_code NVARCHAR(MAX)
)

INSERT INTO customers(customer_id, Name, phone_number, country)
	VALUES (1, 'Raghav', 951341341, 'India'),
		   (2, 'Jake', 52341351, 'USA'),
		   (3, 'Alice', 61341351, 'USA')

INSERT INTO country_codes(country, country_code)
	VALUES ('USA', 1),
		   ('India', 91)

SELECT CAST(c.customer_id AS NVARCHAR) + ' ' + c.Name + ' +' + cc.country_code + c.phone_number
	FROM customers AS c
		JOIN country_codes AS cc 
			ON cc.country = c.country
	ORDER BY c.customer_id