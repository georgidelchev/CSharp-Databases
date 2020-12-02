UPDATE Rooms
   SET Price += Price * 0.14
 WHERE HotelId IN (5, 7, 9)