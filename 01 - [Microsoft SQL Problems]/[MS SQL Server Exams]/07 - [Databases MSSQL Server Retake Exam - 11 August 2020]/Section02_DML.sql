-- 02. Insert
INSERT INTO Distributors(Name, CountryId, AddressText, Summary)
    VALUES ('Deloitte & Touche', 2, '6 Arch St #9757', 'Customizable neutral traveling'),
           ('Congress Title', 13, '58 Hancock St', 'Customer loyalty'),
           ('Kitchen People', 1, '3 E 31st St #77', 'Triple-buffered stable delivery'),
           ('General Color Co Inc', 21, '6185 Bohn St #72', 'Focus group'),
           ('Beck Corporation', 23, '21 E 64th Ave', 'Quality-focused 4th generation hardware')

INSERT INTO Customers(FirstName, LastName, Age, Gender, PhoneNumber, CountryId)
    VALUES ('Francoise', 'Rautenstrauch', 15, 'M', '0195698399', 5),
           ('Kendra', 'Loud', 22, 'F', '0063631526', 11),
           ('Lourdes', 'Bauswell', 50, 'M', '0139037043', 8),
           ('Hannah', 'Edmison', 18, 'F', '0043343686', 1),
           ('Tom', 'Loeza', 31, 'M', '0144876096', 23),
           ('Queenie', 'Kramarczyk', 30, 'F', '0064215793', 29),
           ('Hiu', 'Portaro', 25, 'M', '0068277755', 16),
           ('Josefa', 'Opitz', 43, 'F', '0197887645', 17)

-- 03. Update
UPDATE Ingredients
SET DistributorId=35
    WHERE Name IN ('Bay Leaf', 'Paprika', 'Poppy')

UPDATE Ingredients
SET OriginCountryId=14
    WHERE OriginCountryId = 8

-- 04. Delete
DELETE Feedbacks
    WHERE CustomerId = 14
       OR ProductId = 5