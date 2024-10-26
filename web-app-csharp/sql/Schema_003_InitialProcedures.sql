-- Check and drop the procedure for inserting into Departments if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_insert_depd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_insert_depd]
GO

-- Create the procedure for inserting into Departments
CREATE PROCEDURE usp_insert_depd
    @new_name VARCHAR(20),
    @new_info VARCHAR(40)
AS
BEGIN
    -- Inserting values into Departments table specifying the column names
INSERT INTO Departments (Name, Info) VALUES (@new_name, @new_info);
END
GO

-- Check and drop the procedure for inserting sales if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usr_ins_sale]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usr_ins_sale]
GO

-- Create the procedure for inserting sales
CREATE PROCEDURE usr_ins_sale
    @GoodId INT,
    @Check_no INT,
    @Quantity SMALLINT
AS
BEGIN
    -- Check if the product exists in the Goods table
    DECLARE @ProductExists INT
    SET @ProductExists = (SELECT COUNT(*) FROM [Goods] WHERE GoodId = @GoodId)

    IF @ProductExists = 0
BEGIN
        PRINT 'There is no product with number #' + LTRIM(CAST(@GoodId AS VARCHAR(10)));
RETURN 0;  -- Exit the procedure
END

    -- Check if the sale already exists
    DECLARE @SaleExists INT
    SET @SaleExists = (SELECT COUNT(*) FROM [Sales] WHERE GoodId = @GoodId AND CheckNo = @Check_no)

    IF @SaleExists > 0
BEGIN
        -- Update the existing sale's quantity
UPDATE [Sales]
SET Quantity = Quantity + @Quantity  -- Add to the existing quantity
WHERE GoodId = @GoodId AND CheckNo = @Check_no;

PRINT 'Sale updated for Check_no #' + LTRIM(CAST(@Check_no AS VARCHAR(10))) + ' for Good #' + LTRIM(CAST(@GoodId AS VARCHAR(10))) + '.';
END
ELSE
BEGIN
        -- Insert new sale record
INSERT INTO [Sales] ([CheckNo], [GoodId], [Quantity], [DateSale])
VALUES (@Check_no, @GoodId, @Quantity, GETDATE());

PRINT 'New sale recorded for Check_no #' + LTRIM(CAST(@Check_no AS VARCHAR(10))) + ' for Good #' + LTRIM(CAST(@GoodId AS VARCHAR(10))) + '.';
END
END
GO
