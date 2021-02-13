-- 11. Get Colonists Count
CREATE OR
ALTER FUNCTION udf_GetColonistsCount(@PlanetName VARCHAR(30))
    RETURNS INT
AS
BEGIN
    RETURN (SELECT COUNT(C.Id)
                FROM Planets AS p
                         JOIN Spaceports S
                              ON p.Id = S.PlanetId
                         JOIN Journeys J
                              ON S.Id = J.DestinationSpaceportId
                         JOIN TravelCards Tc
                              ON J.Id = Tc.JourneyId
                         JOIN Colonists C
                              ON C.Id = Tc.ColonistId
                WHERE p.Name = @PlanetName)
END
GO

SELECT dbo.Udf_Getcolonistscount('Otroyphus')
-- 35

-- 12. Change Journey Purpose
CREATE OR
ALTER PROCEDURE usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose NVARCHAR(30))
AS
    IF (@JourneyId NOT IN (SELECT j.Id
                               FROM Journeys AS j))
        BEGIN
            THROW 50001,'The journey does not exist!', 1
        END
    IF ((SELECT j.Purpose
             FROM Journeys AS j
             WHERE j.Id = @JourneyId) = @NewPurpose)
        BEGIN
            THROW 50002,'You cannot change the purpose!', 1
        END

UPDATE Journeys
SET Purpose=@NewPurpose
    WHERE Id = @JourneyId
GO

EXEC usp_ChangeJourneyPurpose 4, 'Technical'
-- not output
-- 1 row affected

EXEC usp_ChangeJourneyPurpose 2, 'Educational'
-- You cannot change the purpose!

EXEC usp_ChangeJourneyPurpose 196, 'Technical'
-- The journey does not exist!