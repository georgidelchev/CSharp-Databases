UPDATE Payments
	SET TaxRate -= 0.03 * TaxRate

SELECT TaxRate FROM Payments