ALTER TABLE Users
	ADD CONSTRAINT CK_Users_PasswordLength
		CHECK(LEN([Password]) >= 5)