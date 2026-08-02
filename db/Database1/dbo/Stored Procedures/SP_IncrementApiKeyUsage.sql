CREATE PROCEDURE SP_IncrementApiKeyUsage
	@keyId uniqueIdentifier
AS
	BEGIN
		UPDATE ApiKeys SET TotalUsageCount = TotalUsageCount + 1
		WHERE ID = @keyId
	END