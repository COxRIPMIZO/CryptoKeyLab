--select * from apikeys

create procedure SP_BulkResetUsageCounts
@jsonApiKeysId nvarchar(max)
as 
begin
	update ApiKeys set TotalUsageCount = 0 where id in (select cast(value as uniqueidentifier) from OPENJSON(@jsonApiKeysId));
end

go

create procedure SP_BulkDeactivateExpiredKeys
@jsonApiKeysId nvarchar(max)
as 
begin
	update ApiKeys set IsActive = 0 where id in (select cast(value as uniqueidentifier) from OPENJSON(@jsonApiKeysId));
end