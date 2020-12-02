ALTER TABLE Minions ADD TownId int

ALTER TABLE Minions
	ADD CONSTRAINT FK_Minions_Towns
		FOREIGN KEY (TownId)
			REFERENCES Towns(Id);