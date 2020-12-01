CREATE TRIGGER tr_CloseReport
ON Reports
AFTER UPDATE
AS
BEGIN
    DECLARE @ReportId INT
    SET @ReportId = (    SELECT i.Id
                           FROM INSERTED i
                     INNER JOIN DELETED d
                             ON d.Id = i.Id
                          WHERE d.CloseDate IS NULL AND i.CloseDate IS NOT NULL)

    DECLARE @StatusId INT
    SET @StatusId = (SELECT s.Id FROM Status s WHERE s.Label = 'completed')

    UPDATE Reports 
       SET StatusId = @StatusId
     WHERE Id = @ReportId
END