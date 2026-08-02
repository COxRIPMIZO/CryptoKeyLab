CREATE PROCEDURE [dbo].[SP_GetActiveEncodingAlgorithms]
	
AS
	
BEGIN
	SELECT * FROM EncodingAlgorithms where IsActive = 1 order by SortOrder asc;
END

