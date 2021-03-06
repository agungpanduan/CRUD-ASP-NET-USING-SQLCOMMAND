USE [TEST]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteData]    Script Date: 07/07/2020 12:55:34 ******/
/****** AGUNGPANDUAN.COM ******/
/****** https://www.agungpanduan.com/2020/06/Select-Insert-Update-Delete-StoredProcedures-SQL-Server-ASP-NET-MVC.html ******/
SET ANSI_NULLS ON
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetDataClass] 
	@Name varchar(30),
	@Email varchar(50),
	@RowStart int,
	@RowEnd int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM (SELECT ROW_NUMBER() over (order by [Name] asc) row,
                   [Name],[Email],[Gender],[ClassM] FROM [dbo].[CLASS] Where 1=1
                   AND (NULLIF(@Name,'') IS NULL OR [Name] like '%'+@Name+'%')
                    AND (NULLIF(@Email,'') IS NULL OR [Email] like '%'+@Email+'%')
                   ) x WHERE x.row between cast(@RowStart AS varchar) AND cast (@RowEnd as varchar);
END
