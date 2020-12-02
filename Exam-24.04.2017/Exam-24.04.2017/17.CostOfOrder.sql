CREATE FUNCTION udf_GetCost (@JobId INT)
RETURNS DECIMAL(15, 2)
AS
BEGIN
    DECLARE @TotalSum DECIMAL(15, 2)
    SET @TotalSum = (     SELECT ISNULL(SUM(p.Price * op.Quantity), 0)
                            FROM Jobs j
                      INNER JOIN Orders o
                              ON o.JobId = j.JobId
                      INNER JOIN OrderParts op
                              ON op.OrderId = o.OrderId
                      INNER JOIN Parts p
                              ON p.PartId = op.PartId
                           WHERE j.JobId = @JobId)
    RETURN @TotalSum
END