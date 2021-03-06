USE [TEST]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteData]    Script Date: 07/07/2020 12:55:34 ******/
/****** AGUNGPANDUAN.COM ******/
/****** https://www.agungpanduan.com/2020/06/Select-Insert-Update-Delete-StoredProcedures-SQL-Server-ASP-NET-MVC.html ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertData]
       @ro_v_err_mesg varchar(2000) output,
	   @ro_n_return_value INT OUTPUT,
       @Name varchar(30),
	   @Email varchar(50),
	   @Gender varchar(10),
	   @ClassM varchar(7)
AS
BEGIN TRY
       SET NOCOUNT ON;
	   set @ro_v_err_mesg=''
       set @ro_n_return_value = 0
        --Tambahkan parameter
       if @ro_n_return_value <> 0
       begin
              return @ro_n_return_value
       end

       --INPUTKAN PROSES INSERT ATAU UPDATE
	   if @Name = Null or @Email = Null
	   begin
	   	set @ro_n_return_value = 1
	   	set @ro_v_err_mesg = 'ERROR: No Data Stored'
	   end
	   else
	   begin
			INSERT INTO [dbo].[CLASS]
						([Name]
						,[Email]
						,[Gender]
						,[ClassM])
			VALUES
                  (@Name, @Email, @Gender, @ClassM)

	   end

       return @ro_n_return_value
END TRY
BEGIN CATCH
       DECLARE @ErrorMessage NVARCHAR(4000),
                     @ErrorSeverity INT,
                     @ErrorState INT,
                     @ErrorLine INT

       Select @ErrorMessage =ERROR_MESSAGE(),
              @ErrorSeverity = ERROR_SEVERITY(),
              @ErrorState = ERROR_SEVERITY(),
              @ErrorLine =ERROR_LINE()

       set @ro_n_return_value = 2
       Set @ro_v_err_mesg = 'ERROR: SP_InsertData: ' + @ErrorMessage + ', at line = ' + CAST(@ErrorLine as varchar)

       PRINT 'SP_InsertData: ' + @ErrorMessage + ', at line = ' + CAST(@ErrorLine as varchar)

       RETURN @ro_n_return_value
END CATCH