
--select * from apikeys

CREATE procedure SP_BulkResetUsageCounts
@jsonApiKeysId nvarchar(max)
as 
begin
	update ApiKeys set TotalUsageCount = 0,LastUsageReset= getdate() where id in (select cast(value as uniqueidentifier) from OPENJSON(@jsonApiKeysId));
end
