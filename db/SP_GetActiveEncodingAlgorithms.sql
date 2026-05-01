USE [CryptokeyLab]
GO

/****** Object: SqlProcedure [dbo].[SP_GetActiveEncodingAlgorithms] Script Date: 5/1/2026 9:27:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetActiveEncodingAlgorithms]
	
AS
	
BEGIN
	SELECT * FROM EncodingAlgorithms where IsActive = 1 order by SortOrder asc;
END
