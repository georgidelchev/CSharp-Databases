CREATE TABLE Hackers
(
    hacker_id INT PRIMARY KEY,
    name      NVARCHAR(30) NOT NULL
)

CREATE TABLE Submissions
(
    submission_date DATE NOT NULL,
    submission_id   INT PRIMARY KEY,
    hacker_id       INT  NOT NULL,
    score           INT  NOT NULL
)

INSERT INTO Hackers(hacker_id, name)
    VALUES (15758, 'Rose'),
           (20703, 'Angela'),
           (36396, 'Frank'),
           (38289, 'Patrick'),
           (44065, 'Lisa'),
           (53473, 'Kimberley'),
           (62529, 'Bonnie'),
           (79722, 'Michael')

INSERT INTO Submissions(submission_date, submission_id, hacker_id, score)
    VALUES ('2016-03-01', 8494, 20703, 0),
           ('2016-03-01', 22403, 53473, 15),
           ('2016-03-01', 23965, 79722, 60),
           ('2016-03-01', 30173, 36396, 70),
           ('2016-03-02', 34928, 20703, 0),
           ('2016-03-02', 38740, 15758, 60),
           ('2016-03-02', 42769, 79722, 25),
           ('2016-03-02', 44364, 79722, 60),
           ('2016-03-03', 45440, 20703, 0),
           ('2016-03-03', 49050, 36396, 70),
           ('2016-03-03', 50273, 79722, 5),
           ('2016-03-04', 50344, 20703, 0),
           ('2016-03-04', 51360, 44065, 90),
           ('2016-03-04', 54404, 53473, 65),
           ('2016-03-04', 61533, 79722, 45),
           ('2016-03-05', 72852, 20703, 0),
           ('2016-03-05', 74546, 38289, 0),
           ('2016-03-05', 76487, 62629, 0),
           ('2016-03-05', 82439, 36396, 10),
           ('2016-03-05', 90006, 36396, 40),
           ('2016-03-06', 90404, 20703, 0)

WITH temp AS (
    SELECT submission_date, hacker_id, COUNT(submission_id) c
        FROM Submissions a
        GROUP BY submission_date, hacker_id
),
     hacker AS (
         SELECT submission_date, hacker_id, c, ROW_NUMBER()
                 OVER (PARTITION BY submission_date ORDER BY c DESC, hacker_id) rank
             FROM temp
     ),
     date AS (
         SELECT DISTINCT submission_date, ROW_NUMBER() OVER (ORDER BY submission_date) rank
             FROM (SELECT DISTINCT submission_date FROM Submissions) a
     ),
     rank AS (
         SELECT a.submission_date, RANK, hacker_id, COUNT(b.submission_date) c
             FROM date a
                      LEFT JOIN temp b ON a.submission_date >= b.submission_date
             GROUP BY a.submission_date, RANK, hacker_id
             HAVING COUNT(b.submission_date) = RANK
     )

SELECT a.submission_date, COUNT(a.hacker_id), b.hacker_id, name
    FROM rank a
             LEFT JOIN hacker b ON a.submission_date = b.submission_date AND b.rank = 1
             LEFT JOIN Hackers c ON b.hacker_id = c.hacker_id
    GROUP BY a.submission_date, b.hacker_id, name
    ORDER BY 1