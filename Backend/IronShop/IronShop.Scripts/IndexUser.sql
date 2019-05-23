IF EXISTS (SELECT 1 FROM sysobjects o, sysusers u WHERE o.uid=u.uid AND o.name = 'IndexPagedUser' AND u.name = 'dbo' AND o.type = 'P')
BEGIN
	drop procedure IndexPagedUser

END

GO

CREATE PROCEDURE [dbo].[IndexPagedUser]
(
	@pageNumber INT = 1,
	@pageSize INT = 25,
	@sort VARCHAR(100) = 'FullName',
	@dir VARCHAR(4) = 'ASC',
	@fullName VARCHAR(512) = NULL,
	@email VARCHAR(256) = NULL,
	@role VARCHAR(50) = NULL
)
AS
BEGIN
	DECLARE @sql VARCHAR(MAX)='';
	DECLARE @select VARCHAR(MAX)='';
	DECLARE @orderBy VARCHAR(100)='';

	SET @select = 'SELECT U.[UserId], U.FullName, U.Email, U.RoleId, R.[Description] AS [Role], U.ImageFileName, U.GoogleAuth
				   FROM [Users] U INNER JOIN [Roles] R ON (R.RoleId = U.RoleId)
				   WHERE 1 = 1 ';

	IF(NOT @fullName IS NULL)
	BEGIN
		SET @select = @select + 'AND FullName LIKE ''%' + @fullName + '%'' ';
	END

	IF(NOT @email IS NULL)
	BEGIN
		SET @select = @select + 'AND Email LIKE ''%' + @email + '%'' ';
	END

	IF(NOT @role IS NULL)
	BEGIN
		SET @select = @select + 'AND R.Description LIKE ''%' + @role + '%'' ';
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
					SELECT COUNT([UserId]) AS TotalRows FROM DataRows
				)
				SELECT [UserId], FullName, Email, RoleId, [Role], ImageFileName, GoogleAuth, [TotalRows]
				FROM [DataRows] CROSS JOIN DataTotalCount 
				ORDER BY ' + @orderBy +
			  ' OFFSET ('+ CONVERT(VARCHAR,@pageNumber)  +' - 1) * ' +CONVERT(VARCHAR, @pageSize) +' ROWS
				FETCH NEXT ' + CONVERT(VARCHAR,@pageSize) + ' ROWS ONLY;'


	EXEC(@sql)

END


GO

/*
ERROR! THis migration is executed before add columns ImageFileName, GoogleAuth --> Migration Error!

IF EXISTS (SELECT 1 FROM sysobjects o, sysusers u WHERE o.uid=u.uid AND o.name = 'IndexUser' AND u.name = 'dbo' AND o.type = 'P')
BEGIN
	drop procedure IndexUser

END

GO

CREATE PROCEDURE IndexUser
(
	@fullName VARCHAR(512) = NULL,
	@email VARCHAR(256) = NULL,
	@role VARCHAR(50) = NULL
)
AS
BEGIN
	SELECT U.[UserId], U.FullName, U.Email, U.RoleId,R.[Description] AS [Role], U.ImageFileName, U.GoogleAuth
	FROM [Users] U INNER JOIN [Roles] R ON (R.RoleId = U.RoleId)
	WHERE (@fullName IS NULL OR  FullName LIKE '%' + @fullName + '%') AND
		  (@email IS NULL OR  Email LIKE '%' + @email + '%') AND
		  (@role IS NULL OR  R.[Description] LIKE '%' + @role + '%')
END
*/

