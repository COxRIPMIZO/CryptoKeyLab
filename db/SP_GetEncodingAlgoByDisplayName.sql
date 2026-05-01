USE [CryptokeyLab]
GO

/****** Object: SqlProcedure [dbo].[SP_GetEncodingAlgoByDisplayName] Script Date: 5/1/2026 9:31:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetEncodingAlgoByDisplayName]
	@DisplayName varchar(256)
as
BEGIN
	SELECT * FROM EncodingAlgorithms WHERE DisplayName = @DisplayName 
	AND IsActive = 1;
END
