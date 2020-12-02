BACKUP DATABASE SoftUni TO DISK='D:\PROJECTS\Database-Basics-MS-SQL\Backup\Softuni01-backup.bak'

DROP DATABASE SoftUni

RESTORE DATABASE SoftUni FROM DISK='D:\PROJECTS\Database-Basics-MS-SQL\Backup\Softuni01-backup.bak'