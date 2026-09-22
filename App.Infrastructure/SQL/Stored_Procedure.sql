SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID(N'dbo.SaveFileRecord', N'P') IS NOT NULL
	DROP PROCEDURE dbo.SaveFileRecord;
GO
	CREATE PROCEDURE dbo.SaveFileRecord
		@FileName	NVARCHAR(255),
		@Average	DECIMAL(12, 2),
		@Result		BIT OUTPUT
	AS
		BEGIN
			SET NOCOUNT ON;

			BEGIN TRY
				
				INSERT INTO [dbo].[FileRecords] (PublicId,[FileName],Average)
				VALUES (NEWID(),@FileName,@Average);

				SET @Result = 1;
			END TRY
			BEGIN CATCH
				DECLARE @ErrorMessage NVARCHAR(4000);
				SET @ErrorMessage = ERROR_MESSAGE();
				SET @Result = 0;
				THROW 51000, @ErrorMessage,1;
			END CATCH
		END;

GO

IF OBJECT_ID(N'dbo.GetFileRecords', N'P') IS NOT NULL
	DROP PROCEDURE dbo.GetFileRecords;
GO
	CREATE PROCEDURE dbo.GetFileRecords
	AS
		BEGIN
			SET NOCOUNT ON;

			SELECT 
				PublicId
				,[FileName]
				,ProcessingTime
				,Average
			FROM dbo.FileRecords
		END;