-- 1. Create the Table
CREATE TABLE ApiKeys (
    SrNo INT IDENTITY(1,1) NOT NULL,  -- UNIQUE IDENTIFIER
    Id UNIQUEIDENTIFIER NOT NULL, -- GUID for the API Key
    KeyPrefix NVARCHAR(20) NOT NULL,
    KeyHash NVARCHAR(256) NOT NULL,
    Tier NVARCHAR(50) NOT NULL DEFAULT 'Public',
    RateLimitPerMinute INT NOT NULL,
    ExpiresAt DATETIME NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL,
    TotalUsageCount BIGINT NOT NULL DEFAULT 0,

    -- MAKE THE GUID PRIMARY KEY (BUT NON - CLUSTERED TO IMPROVE INSERT PERFORMANCE
    CONSTRAINT Pk_Apikeys_Id PRIMARY KEY NONCLUSTERED (Id),

    --Make the srno clustered index to improve (Physically sorts the table by this, super fast!)
    constraint Cx_ApiKeys_SrNo unique clustered (SrNo)
);
GO

--Create an index to make API Key lookups lightning fast during authentication
Create Nonclustered index Ix_ApiKeys_KeyHash ON ApiKeys(KeyHash);
GO