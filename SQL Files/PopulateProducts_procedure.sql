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
-- Description:	Populate Products
-- =============================================
CREATE PROCEDURE PopulateProducts 
	-- Add the parameters for the stored procedure here
	@productCount int = 0, 
	@skuCount int = 10000,
	@moneyVal decimal(18,2) = 0.99,
	@name nvarchar(20) = '',
	@category int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	WHILE @skuCount < 60000
	BEGIN
		SELECT @moneyVal = CONVERT(decimal(18,2), RAND()*300);

		SET @name =   
        CASE   
            WHEN @skuCount > 9999 AND @skuCount < 20000 THEN 'home'
            WHEN @skuCount > 19999 AND @skuCount < 30000 THEN 'garden'  
            WHEN @skuCount > 29999 AND @skuCount < 40000 THEN 'electronics'  
            WHEN @skuCount > 39999 AND @skuCount < 50000 THEN 'fitness'  
			WHEN @skuCount > 49999 AND @skuCount < 60000 THEN 'toys' 
        END;  

		SET @category =   
        CASE   
            WHEN @skuCount > 9999 AND @skuCount < 20000 THEN 1
            WHEN @skuCount > 19999 AND @skuCount < 30000 THEN 2  
            WHEN @skuCount > 29999 AND @skuCount < 40000 THEN 3 
            WHEN @skuCount > 39999 AND @skuCount < 50000 THEN 4  
			WHEN @skuCount > 49999 AND @skuCount < 60000 THEN 5
        END;  

		INSERT INTO dbo.Products 
		VALUES ((@skuCount+@productCount), @category, (@name + CAST(@productCount AS nvarchar(20))), null, @moneyVal);

		SET @productCount = @productCount+1;

		IF @productCount >= 100
		BEGIN
			SET @skuCount = @skuCount + 10000;
			SET @productCount = 0;
		END
	END
	
END
GO
