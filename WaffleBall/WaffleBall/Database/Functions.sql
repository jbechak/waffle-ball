DROP FUNCTION IF EXISTS declare_winner;

CREATE FUNCTION declare_winner (@GameID AS INT)
RETURNS INT AS

BEGIN
	DECLARE @WinnerID INT;
	
	IF (SELECT [Home Points]
	FROM Game
	WHERE ID = @GameID) IS NULL

	BEGIN
		SET @WinnerID = NULL
	END;
	
	ELSE IF 
	(SELECT [Home Points]
	FROM Game
	WHERE ID = @GameID) >
	(SELECT [Visitor Points]
	FROM Game
	WHERE ID = @GameID)

	BEGIN
		SET @WinnerID = (SELECT [Home Team ID]
		FROM Game
		WHERE ID = @GameID)
	END;

	ELSE
	BEGIN
		SET @WinnerID = (SELECT [Visitor ID]
		FROM Game
		WHERE ID = @GameID);
	END;

	RETURN @WinnerID
END;


CREATE FUNCTION total_points (@id INT)
RETURNS INT 
AS
BEGIN
	DECLARE @result INT;
	DECLARE @homePts INT;
	DECLARE @visitorPts INT;

	SELECT @homePts = 
	(SELECT ISNULL(SUM([Visitor Points]), 0)
	FROM Game
	WHERE [Visitor ID] = @id);
	
	
	SELECT @visitorPts =
	(SELECT ISNULL(SUM([Home Points]), 0)
	FROM Game
	WHERE [Home Team ID] = @id);

	RETURN @homePts + @visitorPts;
END


CREATE FUNCTION winCount (@id INT)
RETURNS INT
AS
BEGIN

	DECLARE @result INT;

	SELECT @result = ISNULL(COUNT(Winner), 0)
	FROM Game
	WHERE Winner = @id;

	RETURN @result;
END


CREATE FUNCTION lossCount (@id INT)
RETURNS INT
AS
BEGIN
	DECLARE @result INT;

	SELECT @result = ISNULL(COUNT(ID), 0)
	FROM Game
	WHERE ([Home Team ID] = @id OR [Visitor ID] = @id) AND Winner <> @id;

	RETURN @result;
END


