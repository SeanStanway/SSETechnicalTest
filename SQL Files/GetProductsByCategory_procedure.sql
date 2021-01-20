-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: SStanway	
-- Create date: 190121	
-- Description:	Get products by category name
-- =============================================
CREATE PROCEDURE ProductsByCategoryName 
	-- Add the parameters for the stored procedure here
	@categoryName nvarchar(20), 
	@categoryId int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @categoryId = (SELECT Categories.Id FROM Categories WHERE Name = @CategoryName);

	SELECT * FROM Products WHERE CategoryId = @categoryId;
END
GO
