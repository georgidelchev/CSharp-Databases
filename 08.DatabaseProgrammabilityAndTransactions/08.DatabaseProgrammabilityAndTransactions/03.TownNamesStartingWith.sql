CREATE PROC usp_GetTownsStartingWith (@Letter VARCHAR(10))
AS
BEGIN
    SELECT t.Name
      FROM Towns t
     WHERE LEFT(t.Name, LEN(@Letter)) = @Letter
END