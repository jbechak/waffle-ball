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
