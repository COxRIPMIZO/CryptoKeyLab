
CREATE PROCEDURE SP_CreateApiKey
	@Id UniqueIdentifier,
    @keyPrefix nvarchar(20),
    @KeyHash nvarchar(256),
    @CreatedAt datetime,
    @ExpiresAt datetime,
    @RateLimitPerMinute int
AS
    BEGIN
        INSERT INTO ApiKeys (Id, KeyPrefix, KeyHash, CreatedAt, ExpiresAt,RateLimitPerMinute, IsActive,Tier,TotalUsageCount)
        VALUES (@Id, @keyPrefix, @KeyHash, @CreatedAt, @ExpiresAt,@RateLimitPerMinute, 1,'Public',0);
    END
