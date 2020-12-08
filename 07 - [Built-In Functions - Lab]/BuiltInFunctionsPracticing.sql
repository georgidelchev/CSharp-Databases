SELECT GETDATE() AS CurrentTime

SELECT *, firstname + ' ' + lastname AS FullName
    FROM employees

SELECT *, CONCAT(firstname, ' ', middlename, ' ', lastname) AS FullName
    FROM employees

SELECT *, CONCAT_WS(' ', firstname, middlename, lastname) AS FullName
    FROM employees

SELECT *, SUBSTRING(firstname, 1, 1) FirstSymbolFromName
    FROM employees

SELECT REPLACE('SoftUni', 'Soft', 'Hard')
SELECT REPLACE('VisualStudio', 'Visual', 'NotVisual')

SELECT LEN(N'Тест'), DATALENGTH(N'Тест'), N'Тест'

CREATE VIEW v_HidePartOfCustomerPaymentNumber AS
SELECT CustomerID, FirstName, LastName,
       LEFT(PaymentNumber, 6) +
       REPLICATE('*', LEN(PaymentNumber) - 6) AS PaymentNumber
    FROM Customers

SELECT *
    FROM v_HidePartOfCustomerPaymentNumber

SELECT A * H / 2.0 AS Area
    FROM Triangles2




