CREATE PROCEDURE SP_GetValidApiKey
	@KeyHash NVARCHAR(256)
AS
	BEGIN
		--- Only returns the key if it exists, is active, and hasn't expired!
		SELECT * FROM ApiKeys 
		WHERE KeyHash = @KeyHash
		AND IsActive = 1 
		AND ExpiresAt > GETUTCDATE(); 
	END