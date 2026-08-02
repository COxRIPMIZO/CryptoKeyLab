CREATE TABLE [dbo].[EncodingAlgorithms] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [DisplayName] VARCHAR (256) NOT NULL,
    [ClassName]   VARCHAR (256) NOT NULL,
    [Category]    VARCHAR (256) NOT NULL,
    [Family]      VARCHAR (256) NOT NULL,
    [FolderName]  VARCHAR (100) NOT NULL,
    [IsActive]    BIT           DEFAULT ((1)) NOT NULL,
    [SortOrder]   INT           DEFAULT ((0)) NOT NULL,
    [CreatedOn]   DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

