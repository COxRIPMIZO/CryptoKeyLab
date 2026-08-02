CREATE PROCEDURE SP_GetActiveHashAlgorithms
as
Begin
	SELECT * FROM HashAlgorithms WHERE IsActive = 1 ORDER BY SortOrder ASC;
end