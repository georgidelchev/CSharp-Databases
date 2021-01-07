BACKUP DATABASE SoftUni TO DISK = 'D:\Downloads\Softuni-backup.bak'

USE master

DROP DATABASE SoftUni

RESTORE DATABASE SoftUni FROM DISK = 'D:\Downloads\Softuni-backup.bak';