BEGIN TRANSACTION;


DROP PROCEDURE IF EXISTS GetAllTeams;
DROP PROCEDURE IF EXISTS GetTeam;
DROP PROCEDURE IF EXISTS GetAllGames;
DROP PROCEDURE IF EXISTS GetGamesByTeam;
DROP PROCEDURE IF EXISTS GetGame;
DROP PROCEDURE IF EXISTS CreateGame;
DROP PROCEDURE IF EXISTS ScoreGame;
DROP PROCEDURE IF EXISTS CreateTeam;
DROP PROCEDURE IF EXISTS UpdateTeamRecord;
DROP PROCEDURE IF EXISTS TotalPoints;

DROP TABLE IF EXISTS Game;
DROP TABLE IF EXISTS team;

CREATE TABLE Team (
	ID INT IDENTITY PRIMARY KEY,
	City VARCHAR(50) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Conference VARCHAR(10) NOT NULL,
	Division VARCHAR(10) NOT NULL,
	Wins AS dbo.winCount(ID),
	Losses AS dbo.lossCount(ID),
	Points AS dbo.total_points(ID)
);

CREATE TABLE Game (
	ID INT IDENTITY PRIMARY KEY,
	[Home Team ID] INT FOREIGN KEY REFERENCES Team(ID),
	[Visitor ID] INT FOREIGN KEY REFERENCES Team(ID),
	[Home Points] INT,
	[Visitor Points] INT,
	[Game Time] DATETIME,
	Status VARCHAR(15) DEFAULT 'SCHEDULED',
	Winner AS dbo.declare_winner(ID), 

	CONSTRAINT chk_Status CHECK (Status IN ('SCHEDULED', 'ACTIVE', 'FINISHED'))
);

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Pittsburgh', 'Squirrels', 'AWC', 'East');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('New York', 'Apples', 'AWC', 'East');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Boston', 'Coffees', 'AWC', 'East');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Chicago', 'Pizzas', 'AWC', 'Central');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Kansas City', 'Planes', 'AWC', 'Central');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('St. Louis', 'Arches', 'AWC', 'Central');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Los Angeles', 'Stars', 'AWC', 'West');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Seattle', 'Rockers', 'AWC', 'West');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Vancouver', 'Rain', 'AWC', 'West');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Atlanta', 'Peaches', 'NWC', 'East');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Baltimore', 'Fish', 'NWC', 'East');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Philadelphia', 'Bells', 'NWC', 'East');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Houston', 'Tacos', 'NWC', 'Central');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Minnesota', 'Snow', 'NWC', 'Central');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Denver', 'Miners', 'NWC', 'Central');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('San Francisco', 'Brisges', 'NWC', 'West');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('San Diego', 'Zoos', 'NWC', 'West');

INSERT INTO Team (City, Name, Conference, Division)
VALUES ('Portland', 'Punks', 'NWC', 'West');


INSERT INTO Game ([Home Team ID], [Visitor ID], [Home Points], [Visitor Points], [Game Time])
VALUES (1, 2, 35, 19, '2023-03-24 20:30:00');

INSERT INTO Game ([Home Team ID], [Visitor ID], [Home Points], [Visitor Points], [Game Time])
VALUES (3, 4, 4, 55, '2023-03-23 20:30:00');

INSERT INTO Game ([Home Team ID], [Visitor ID], [Home Points], [Visitor Points], [Game Time])
VALUES (5, 6, 16, 17, '2023-03-22 20:30:00');

INSERT INTO Game ([Home Team ID], [Visitor ID], [Home Points], [Visitor Points], [Game Time])
VALUES (7, 8, 100, 64, '2023-03-16 20:30:00');

INSERT INTO Game ([Home Team ID], [Visitor ID], [Home Points], [Visitor Points], [Game Time])
VALUES (1, 2, 79, 12, '2023-03-15 20:30:00');


GO
CREATE Procedure GetAllTeams
AS
SELECT * FROM Team;

GO 
CREATE Procedure GetTeam @ID int
AS
SELECT * FROM Team
WHERE ID = @ID;


GO
CREATE PROCEDURE GetAllGames
AS
SELECT 
Game.ID,
home_team.ID AS [Home ID],
home_team.City AS [Home City],
home_team.Name AS [Home Team Name],
[Home Points],

visitor.ID AS [Visitor ID],
visitor.City AS [Visitor City],
visitor.Name AS [Visitor Team Name],
[Visitor Points],

[Game Time], Status, Winner

FROM Game 
JOIN Team home_team 
ON Game.[Home Team ID] = home_team.ID
JOIN Team visitor
ON Game.[Visitor ID] = visitor.ID;


GO
CREATE PROCEDURE GetGamesByTeam @teamId INT
AS
SELECT 
Game.ID,
home_team.ID AS [Home ID],
home_team.City AS [Home City],
home_team.Name AS [Home Team Name],
[Home Points],

visitor.ID AS [Visitor ID],
visitor.City AS [Visitor City],
visitor.Name AS [Visitor Team Name],
[Visitor Points],

[Game Time], Status, Winner

FROM Game 
JOIN Team home_team 
ON Game.[Home Team ID] = home_team.ID
JOIN Team visitor
ON Game.[Visitor ID] = visitor.ID
WHERE [Home Team ID] = @teamId OR [Visitor ID] = @teamId;


GO
CREATE PROCEDURE GetGame @id INT
AS
SELECT 
Game.ID,
home_team.ID AS [Home ID],
home_team.City AS [Home City],
home_team.Name AS [Home Team Name],
[Home Points],

visitor.ID AS [Visitor ID],
visitor.City AS [Visitor City],
visitor.Name AS [Visitor Team Name],
[Visitor Points],

[Game Time], Status, Winner

FROM Game 
JOIN Team home_team 
ON Game.[Home Team ID] = home_team.ID
JOIN Team visitor
ON Game.[Visitor ID] = visitor.ID

WHERE Game.ID = @id;


GO
CREATE PROCEDURE CreateGame @homeId INT, @visitorId INT, @gameTime DATETIME
AS
INSERT INTO Game ([Home Team ID], [Visitor ID], [Game Time])
VALUES (@homeId, @visitorId, @gameTime)
SELECT CAST(@@IDENTITY AS INT);


GO
CREATE PROCEDURE ScoreGame @homePoints INT, @visitorPoints INT, @id INT
AS
UPDATE Game
SET [Home Points] = @homePoints,
[Visitor Points] = @visitorPoints,
Status = 'FINISHED'
WHERE ID = @id;


GO
CREATE PROCEDURE CreateTeam @city VARCHAR(50), @name VARCHAR(50), @conference VARCHAR(10), @division VARCHAR(10) 
AS
INSERT INTO Team (City, Name, Conference, Division)
VALUES (@city, @name, @conference, @division)
SELECT CAST(@@IDENTITY AS INT);


--GO
--CREATE PROCEDURE UpdateTeamRecord @ID int, @wins int, @losses int
--AS
--UPDATE Team
--SET Wins = @wins, Losses = @losses
--WHERE ID = @ID;


GO
CREATE PROCEDURE TotalPoints @teamId INT
AS
SELECT TOP 1 (SELECT SUM([Visitor Points])
	FROM Game
	WHERE [Visitor ID] = @teamId) + 
	
	(SELECT SUM([Home Points]) FROM Game
	WHERE [Home Team ID] = @teamId)
	AS Total

	FROM Game;


COMMIT;