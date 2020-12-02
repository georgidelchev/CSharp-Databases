  SELECT j.Status,
         j.IssueDate
    FROM Jobs j
   WHERE j.Status <> 'Finished'
ORDER BY j.IssueDate, j.JobId