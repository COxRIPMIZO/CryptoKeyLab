CREATE PROCEDURE SP_CreateApiKey
	@Id UniqueIdentifier,
    @keyPrefix nvarchar(20),
    @KeyHash nvarchar(256),
    @CreatedAt datetime,
    @ExpireAt datetime,
    @RateLimitPerMinute int
AS
    BEGIN
        INSERT INTO ApiKeys (Id, KeyPrefix, KeyHash, CreatedAt, ExpireAt,RateLimitPerMinute, IsActive,Tier,TotalUsageCount)
        VALUES (@Id, @keyPrefix, @KeyHash, @CreatedAt, @ExpireAt,@RateLimitPerMinute, 1,'Public',0);
    END