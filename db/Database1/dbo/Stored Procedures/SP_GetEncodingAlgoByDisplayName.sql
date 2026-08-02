CREATE PROCEDURE [dbo].[SP_GetEncodingAlgoByDisplayName]
	@DisplayName varchar(256)
as
BEGIN
	SELECT * FROM EncodingAlgorithms WHERE DisplayName = @DisplayName 
	AND IsActive = 1;
END