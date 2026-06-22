

CREATE procedure SP_BulkDeactivateExpiredKeys
@jsonApiKeysId nvarchar(max)
as 
begin
	update ApiKeys set IsActive = 0,LastUsageReset= getdate() where id in (select cast(value as uniqueidentifier) from OPENJSON(@jsonApiKeysId));
end
