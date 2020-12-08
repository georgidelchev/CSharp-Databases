SELECT CountryName, IsoCode
    FROM Countries
    WHERE CountryName LIKE ('%a%a%a%')
    ORDER BY IsoCode