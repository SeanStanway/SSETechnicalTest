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
-- Author:		SStanway
-- Create date: 190121
-- Description:	Populate Categories
-- =============================================
CREATE PROCEDURE PopulateCategories 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Categories VALUEs (1, 'Home');
	INSERT INTO Categories VALUEs (2, 'Garden');
	INSERT INTO Categories VALUEs (3, 'Electronics');
	INSERT INTO Categories VALUEs (4, 'Fitness');
	INSERT INTO Categories VALUEs (5, 'Toys');
END
GO
