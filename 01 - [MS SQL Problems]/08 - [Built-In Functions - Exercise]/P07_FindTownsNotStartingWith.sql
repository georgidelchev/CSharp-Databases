SELECT TownID, Name
    FROM Towns
    WHERE Name NOT LIKE ('[RBD]%')
    ORDER BY Name