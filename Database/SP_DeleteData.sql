USE [TEST]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteData]    Script Date: 07/07/2020 12:55:34 ******/
/****** AGUNGPANDUAN.COM ******/
/****** https://www.agungpanduan.com/2020/06/Select-Insert-Update-Delete-StoredProcedures-SQL-Server-ASP-NET-MVC.html ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteData]
       @ro_v_err_mesg varchar(2000) output, -- nilai yang akan dikembalikan ke ASP NET
	   @ro_n_return_value INT OUTPUT, --nilai yang akan dikembalikan ke ASP NET
       @Name varchar(30),
	   @Email varchar(50)
AS
BEGIN TRY
       SET NOCOUNT ON;
	   declare
	     @existcount int

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
			set @existcount = (SELECT COUNT(1) From [dbo].[CLASS] WHERE [Name] = @Name and [Email]=@Email)
			if @existcount = 1 
			begin 
				DELETE FROM [dbo].[CLASS]
				WHERE [Name] = @Name and [Email]=@Email
			end
			else
			begin
				 if @ro_n_return_value <> 0
				 begin
					return @ro_n_return_value
				 end
				set @ro_n_return_value = 1
	   			set @ro_v_err_mesg = 'ERROR: Data Not Exist'
			end
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