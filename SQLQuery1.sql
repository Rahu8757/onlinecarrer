USE [Career]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[addJobsApplied]
		@jsName = N'smriti',
		@jobId = 1,
		@dateApply = 24-09-2021,
		@cid = 1

SELECT	@return_value as 'Return Value'

GO
