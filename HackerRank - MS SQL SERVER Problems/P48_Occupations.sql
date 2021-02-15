SELECT e.Name
    FROM Occupations AS e
    WHERE e.Occupation = 'Doctor'
    ORDER BY Name

SELECT e.Name
    FROM Occupations AS e
    WHERE e.Occupation = 'Professor'
    ORDER BY Name

SELECT e.Name
    FROM Occupations AS e
    WHERE e.Occupation = 'Singer'
    ORDER BY Name

SELECT e.Name
    FROM Occupations AS e
    WHERE e.Occupation = 'Actor'
    ORDER BY Name

SELECT Doctor, Professor, Singer, Actor
    FROM
    (
        SELECT ROW_NUMBER() OVER (PARTITION BY Occupation ORDER BY Name), *
           FROM Occupations
    ) AS tempTable
    PIVOT
    (
        MAX(Name)
        FOR Occupation IN (Doctor, Professor, Singer, Actor)
    ) AS PivotTable

