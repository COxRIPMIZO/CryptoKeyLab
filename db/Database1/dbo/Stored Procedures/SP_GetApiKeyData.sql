
CREATE PROCEDURE SP_GetApiKeyData
	@IsActive BIT ,@RowToFetch int
AS
	BEGIN
		SELECT TOP (@RowToFetch) ID,RateLimitPerMinute,ExpiresAt,IsActive,TotalUsageCount,LastUsageReset
		FROM ApiKeys
		WHERE IsActive = @IsActive
	END
