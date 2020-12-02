UPDATE Jobs
   SET MechanicId = 3
 WHERE [Status] = 'Pending'

UPDATE Jobs
   SET [Status] = 'In Progress'
 WHERE [Status] = 'Pending'