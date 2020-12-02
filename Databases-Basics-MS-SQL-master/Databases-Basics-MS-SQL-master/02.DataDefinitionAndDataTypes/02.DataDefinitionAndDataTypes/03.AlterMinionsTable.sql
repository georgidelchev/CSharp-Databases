ALTER TABLE Minions
        ADD TownId INT

   ALTER TABLE Minions
ADD CONSTRAINT FK_Minions_Towns
   FOREIGN KEY (TownId)
    REFERENCES Towns(Id)