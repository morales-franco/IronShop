IF EXISTS (SELECT 1 FROM sysobjects o, sysusers u WHERE o.uid=u.uid AND o.name = 'IndexPagedProduct' AND u.name = 'dbo' AND o.type = 'P')
BEGIN
	drop procedure IndexPagedProduct

END

GO

CREATE PROCEDURE [dbo].[IndexPagedProduct]
(
	@pageNumber INT = 1,
	@pageSize INT = 25,
	@sort VARCHAR(100) = 'Title',
	@dir VARCHAR(4) = 'ASC',
	@title VARCHAR(100) = NULL,
	@category VARCHAR(100) = NULL
)
AS
BEGIN
	DECLARE @sql VARCHAR(MAX)='';
	DECLARE @select VARCHAR(MAX)='';
	DECLARE @orderBy VARCHAR(100)='';

	SET @select = 'SELECT [ProductId]
					     ,[Category]
						 ,[Price]
						 ,[Title]
						 ,[ImageFileName]
				   FROM [Products]
				   WHERE 1 = 1  AND Deleted = 0 ';

	IF(NOT @title IS NULL)
	BEGIN
		SET @select = @select + 'AND Title LIKE ''%' + @title + '%'' ';
	END

	IF(NOT @category IS NULL)
	BEGIN
		SET @select = @select + 'AND Category LIKE ''%' + @category + '%'' ';
	END

	IF(NOT @sort IS NULL)
	BEGIN
		SET @orderBy =  @sort + ' ' + ISNULL(@dir, 'ASC');
	END



	SET @sql = 'WITH DataRows
				AS
				(' 
				+
				@select
				+
				'), 
				DataTotalCount 
				AS 
				(
					SELECT COUNT([ProductId]) AS TotalRows FROM DataRows
				)
				SELECT [ProductId], [Category], [Price], [Title], [ImageFileName], [TotalRows]
				FROM [DataRows] CROSS JOIN DataTotalCount 
				ORDER BY ' + @orderBy +
			  ' OFFSET ('+ CONVERT(VARCHAR,@pageNumber)  +' - 1) * ' +CONVERT(VARCHAR, @pageSize) +' ROWS
				FETCH NEXT ' + CONVERT(VARCHAR,@pageSize) + ' ROWS ONLY;'


	EXEC(@sql)

END


GO