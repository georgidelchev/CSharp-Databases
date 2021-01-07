delete from Players
	DBCC CHECKIDENT (Players, RESEED, 0)