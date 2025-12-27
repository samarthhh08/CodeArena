START TRANSACTION;

-- =========================================================
-- PROBLEM 1 : TWO SUM
-- =========================================================
INSERT INTO problems
(Title, Slug, Description, Difficulty, TimeLimitMs, MemoryLimitMb, IsPublished, CreatedAt)
VALUES
(
 'Two Sum',
 'two-sum',
 'Given an array of integers and a target value, find two distinct indices such that the numbers at those indices add up to the target.

Input:
n
n space separated integers
target

Output:
two indices (0-based)

Constraints allow negative values and large integers.
Exactly one solution exists.',
 'EASY',
 1000,
 256,
 1,
 UTC_TIMESTAMP()
);

SET @p1 = LAST_INSERT_ID();

INSERT INTO tags (Name) VALUES ('Array'),('Hash Table'),('Math');
INSERT INTO problemtags SELECT @p1, Id FROM tags WHERE Name IN ('Array','Hash Table','Math');

INSERT INTO testcases (ProblemId, Input, ExpectedOutput, IsSample) VALUES
(@p1,'4\n2 7 11 15\n9','0 1',1),
(@p1,'3\n3 2 4\n6','1 2',1),
(@p1,'5\n1 5 3 7 9\n10','1 3',0),
(@p1,'4\n0 4 3 0\n0','0 3',0),
(@p1,'6\n-1 -2 -3 -4 -5 -6\n-8','2 4',0),
(@p1,'7\n10 20 30 40 50 60 70\n110','4 6',0),
(@p1,'5\n2 5 5 11 15\n10','1 2',0),
(@p1,'8\n1000000000 -1000000000 5 7 9 11 13 15\n0','0 1',0),
(@p1,'6\n4 6 8 12 16 20\n28','2 4',0),
(@p1,'10\n1 2 3 4 5 6 7 8 9 10\n19','8 9',0);

-- =========================================================
-- PROBLEM 2 : REVERSE STRING
-- =========================================================
INSERT INTO problems
(Title, Slug, Description, Difficulty, TimeLimitMs, MemoryLimitMb, IsPublished, CreatedAt)
VALUES
(
 'Reverse a String',
 'reverse-string',
 'Given a string, reverse it character by character.

Input:
a single line string

Output:
reversed string

The string may contain spaces and special characters.',
 'EASY',
 1000,
 128,
 1,
 UTC_TIMESTAMP()
);

SET @p2 = LAST_INSERT_ID();

INSERT INTO tags (Name) VALUES ('String'),('Two Pointers');
INSERT INTO problemtags SELECT @p2, Id FROM tags WHERE Name IN ('String','Two Pointers');

INSERT INTO testcases VALUES
(NULL,@p2,'hello','olleh',1),
(NULL,@p2,'OpenAI','IAnepO',1),
(NULL,@p2,'abcd','dcba',0),
(NULL,@p2,'a','a',0),
(NULL,@p2,'racecar','racecar',0),
(NULL,@p2,'12345','54321',0),
(NULL,@p2,'hello world','dlrow olleh',0),
(NULL,@p2,'!@#$',' $#@!',0),
(NULL,@p2,'','',0),
(NULL,@p2,'Programming','gnimmargorP',0);

-- =========================================================
-- PROBLEM 3 : VALID PARENTHESES
-- =========================================================
INSERT INTO problems
(Title, Slug, Description, Difficulty, TimeLimitMs, MemoryLimitMb, IsPublished, CreatedAt)
VALUES
(
 'Valid Parentheses',
 'valid-parentheses',
 'Check whether a given string of brackets is valid.
A string is valid if every opening bracket has a corresponding closing bracket in correct order.',
 'MEDIUM',
 1000,
 256,
 1,
 UTC_TIMESTAMP()
);

SET @p3 = LAST_INSERT_ID();

INSERT INTO tags (Name) VALUES ('Stack'),('String');
INSERT INTO problemtags SELECT @p3, Id FROM tags WHERE Name IN ('Stack','String');

INSERT INTO testcases VALUES
(NULL,@p3,'()','true',1),
(NULL,@p3,'()[]{}','true',1),
(NULL,@p3,'(]','false',0),
(NULL,@p3,'([)]','false',0),
(NULL,@p3,'{[]}','true',0),
(NULL,@p3,'','true',0),
(NULL,@p3,'(((((','false',0),
(NULL,@p3,'()))','false',0),
(NULL,@p3,'[{()}]','true',0),
(NULL,@p3,'{[}','false',0);

-- =========================================================
-- PROBLEM 4 : MAX SUBARRAY SUM
-- =========================================================
INSERT INTO problems
(Title, Slug, Description, Difficulty, TimeLimitMs, MemoryLimitMb, IsPublished, CreatedAt)
VALUES
(
 'Maximum Subarray Sum',
 'maximum-subarray-sum',
 'Find the contiguous subarray with the maximum sum using an efficient algorithm.',
 'MEDIUM',
 1000,
 256,
 1,
 UTC_TIMESTAMP()
);

SET @p4 = LAST_INSERT_ID();

INSERT INTO tags (Name) VALUES ('Array'),('Dynamic Programming');
INSERT INTO problemtags SELECT @p4, Id FROM tags WHERE Name IN ('Array','Dynamic Programming');

INSERT INTO testcases VALUES
(NULL,@p4,'9\n-2 1 -3 4 -1 2 1 -5 4','6',1),
(NULL,@p4,'5\n1 2 3 4 5','15',1),
(NULL,@p4,'1\n-1','-1',0),
(NULL,@p4,'3\n-2 -3 -1','-1',0),
(NULL,@p4,'4\n4 -1 2 1','6',0),
(NULL,@p4,'6\n-1 2 3 -5 4 6','10',0),
(NULL,@p4,'5\n0 0 0 0 0','0',0),
(NULL,@p4,'7\n1 -1 1 -1 1 -1 1','1',0),
(NULL,@p4,'2\n-2 -1','-1',0),
(NULL,@p4,'8\n5 -3 5 -2 1 -1 6 -4','11',0);

-- =========================================================
-- PROBLEM 5 : BINARY SEARCH
-- =========================================================
INSERT INTO problems
(Title, Slug, Description, Difficulty, TimeLimitMs, MemoryLimitMb, IsPublished, CreatedAt)
VALUES
(
 'Binary Search',
 'binary-search',
 'Given a sorted array and a target value, find the index of the target using binary search.',
 'EASY',
 1000,
 128,
 1,
 UTC_TIMESTAMP()
);

SET @p5 = LAST_INSERT_ID();

INSERT INTO tags (Name) VALUES ('Binary Search'),('Array');
INSERT INTO problemtags SELECT @p5, Id FROM tags WHERE Name IN ('Binary Search','Array');

INSERT INTO testcases VALUES
(NULL,@p5,'5\n1 2 3 4 5\n3','2',1),
(NULL,@p5,'5\n1 2 3 4 5\n6','-1',1),
(NULL,@p5,'1\n1\n1','0',0),
(NULL,@p5,'3\n1 3 5\n3','1',0),
(NULL,@p5,'4\n2 4 6 8\n6','2',0),
(NULL,@p5,'5\n10 20 30 40 50\n10','0',0),
(NULL,@p5,'5\n10 20 30 40 50\n50','4',0),
(NULL,@p5,'5\n10 20 30 40 50\n35','-1',0),
(NULL,@p5,'6\n1 2 4 8 16 32\n8','3',0),
(NULL,@p5,'7\n1 3 5 7 9 11 13\n11','5',0);

COMMIT;
