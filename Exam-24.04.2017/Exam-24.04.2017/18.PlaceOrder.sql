CREATE PROC usp_PlaceOrder (@JobId INT, @SerialNumber VARCHAR(50), @Quantity INT)
AS
BEGIN
    DECLARE @JobStatus VARCHAR(11)
    SET @JobStatus = (SELECT j.Status FROM Jobs j WHERE j.JobId = @JobId)
    IF (@JobStatus = 'Finished')
    BEGIN
        RAISERROR('The job is not active!', 16, 1)
        RETURN
    END

    IF (@Quantity <= 0)
    BEGIN
        RAISERROR('Part quantity must be more than zero', 16, 1)
        RETURN
    END

    IF (@JobId NOT IN (SELECT j.JobId FROM Jobs j))
    BEGIN
        RAISERROR('Job not found!', 16, 1)
        RETURN
    END

    DECLARE @PartId INT
    SET @PartId = (SELECT p.PartId FROM Parts p WHERE P.SerialNumber = @SerialNumber)
    IF (@PartId IS NULL)
    BEGIN
        RAISERROR('Part not found!', 16, 1)
        RETURN
    END

    DECLARE @OrderId INT
    SET @OrderId = (    SELECT o.OrderId
                          FROM Orders o
                    INNER JOIN OrderParts op
                            ON op.OrderId = o.OrderId
                    INNER JOIN Parts p
                            ON p.PartId = op.PartId
                         WHERE o.JobId = @JobId 
                           AND P.PartId = @PartId
                           AND o.IssueDate IS NULL)

    IF (@OrderId IS NULL)
    BEGIN
        INSERT INTO Orders (JobId, IssueDate)
             VALUES (@JobId, NULL)
        INSERT INTO OrderParts (OrderId, PartId, Quantity)
             VALUES (IDENT_CURRENT('Orders'), @PartId, @Quantity)
    END
    ELSE 
    BEGIN
        DECLARE @PartExists INT
        SET @PartExists = (SELECT @@ROWCOUNT 
                             FROM OrderParts op 
                            WHERE op.OrderId = @OrderId 
                              AND op.PartId = @PartId)

        IF (@PartExists IS NULL)
        BEGIN
            INSERT INTO OrderParts (OrderId, PartId, Quantity)
                 VALUES (IDENT_CURRENT('Orders'), @PartId, @Quantity)
        END
        ELSE
        BEGIN
            UPDATE OrderParts
               SET Quantity += @Quantity
             WHERE OrderId = @OrderId 
               AND PartId = @PartId
        END
    END
END