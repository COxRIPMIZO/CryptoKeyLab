CREATE TABLE [dbo].[ApiKeys] (
    [SrNo]               INT              IDENTITY (1, 1) NOT NULL,
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [KeyPrefix]          NVARCHAR (20)    NOT NULL,
    [KeyHash]            NVARCHAR (256)   NOT NULL,
    [Tier]               NVARCHAR (50)    DEFAULT ('Public') NOT NULL,
    [RateLimitPerMinute] INT              NOT NULL,
    [ExpiresAt]          DATETIME         NOT NULL,
    [IsActive]           BIT              DEFAULT ((1)) NOT NULL,
    [CreatedAt]          DATETIME         NOT NULL,
    [TotalUsageCount]    BIGINT           DEFAULT ((0)) NOT NULL,
    [LastUsageReset]     DATETIME         NULL,
    CONSTRAINT [Pk_Apikeys_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [Cx_ApiKeys_SrNo] UNIQUE CLUSTERED ([SrNo] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Ix_ApiKeys_KeyHash]
    ON [dbo].[ApiKeys]([KeyHash] ASC);

