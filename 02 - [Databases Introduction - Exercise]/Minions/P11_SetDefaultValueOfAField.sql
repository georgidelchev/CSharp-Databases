ALTER TABLE Users
	ADD CONSTRAINT CK_Default_Users_LastLoginTime_Value DEFAULT GETDATE()
		FOR LastLoginTime