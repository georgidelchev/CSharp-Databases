   ALTER TABLE Users
ADD CONSTRAINT DF_LastLoginTime 
       DEFAULT GETDATE()
           FOR LastLoginTime