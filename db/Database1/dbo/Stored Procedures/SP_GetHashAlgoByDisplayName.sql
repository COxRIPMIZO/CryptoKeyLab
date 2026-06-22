CREATE procedure SP_GetHashAlgoByDisplayName 
	@DisplayName varchar(255)
as
begin

	select * from HashAlgorithms
	where DisplayName = @DisplayName AND IsActive = 1
end