SELECT DISTINCT CITY
    FROM Station
    WHERE CITY NOT LIKE ('[aeiouAEIOU]%')