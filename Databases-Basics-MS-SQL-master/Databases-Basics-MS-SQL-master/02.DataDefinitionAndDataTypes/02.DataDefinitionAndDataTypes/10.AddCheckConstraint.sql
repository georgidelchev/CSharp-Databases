   ALTER TABLE Users
ADD CONSTRAINT CH_PasswordLength
         CHECK (LEN(Password) >= 5)