CREATE TABLE [dbo].[HashAlgorithms] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [DisplayName]        VARCHAR (256) NOT NULL,
    [ClassName]          VARCHAR (256) NOT NULL,
    [Category]           VARCHAR (256) NOT NULL,
    [Family]             VARCHAR (100) NOT NULL,
    [RequiresKey]        BIT           DEFAULT ((0)) NOT NULL,
    [RequiresSalt]       BIT           DEFAULT ((0)) NOT NULL,
    [RequiresIterations] BIT           DEFAULT ((0)) NOT NULL,
    [IsActive]           BIT           DEFAULT ((1)) NOT NULL,
    [IsSecure]           BIT           DEFAULT ((1)) NOT NULL,
    [SortOrder]          INT           DEFAULT ((0)) NOT NULL,
    [CreatedOn]          DATETIME      DEFAULT (getdate()) NOT NULL,
    [FolderName]         VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

