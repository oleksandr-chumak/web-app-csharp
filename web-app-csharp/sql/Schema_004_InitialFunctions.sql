-- Drop the LESS_THAN_AVG function if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LESS_THAN_AVG]') AND type IN (N'FN', N'IF'))
DROP FUNCTION [dbo].[LESS_THAN_AVG]
GO

-- Create the LESS_THAN_AVG function
CREATE FUNCTION LESS_THAN_AVG ()
    RETURNS INT
AS
BEGIN
    DECLARE @ROW_COUNT INT
    SET @ROW_COUNT = (
        SELECT COUNT(*) FROM Goods
        WHERE [Price] < (SELECT AVG([Price]) FROM Goods)
    )
    RETURN @ROW_COUNT;
END
GO

-- Drop the count_goods function if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[count_goods]') AND type IN (N'FN', N'IF'))
DROP FUNCTION [dbo].[count_goods]
GO

-- Create the count_goods function
CREATE FUNCTION count_goods
(
    @new_price MONEY
)
    RETURNS INT
AS
BEGIN
    DECLARE @temp_count INT
    SET @temp_count = (
        SELECT COUNT(*) FROM Goods
        WHERE [Price] < @new_price
    )
    RETURN @temp_count;
END
GO

-- Drop the TopGood function if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopGood]') AND type IN (N'FN', N'IF'))
DROP FUNCTION [dbo].[TopGood]
GO

-- Create the TopGood function
CREATE FUNCTION TopGood()
    RETURNS INT
AS
BEGIN
RETURN (
    SELECT DeptId
    FROM Goods
    WHERE Price = (SELECT MAX(Price) FROM Goods)
)
END
GO

-- 1.3.8 Завдання для самостійного виконання

/* 
Моя залікова книжка: 22.121П3П13.0110
Остання цифра залікової книжки(N): 0

Вибрати 2 варіанта завдання: перше – співпадає
з останньою цифрою залікової книжки (N), друге – N+9, написати та відлагодити
відповідну програму.

Завдання до виконання: 1(0), 9(0 + 9)
*/

-- Drop the RemoveSubstring function if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveSubstring]') AND type IN (N'FN', N'IF'))
DROP FUNCTION [dbo].[RemoveSubstring]
GO

-- Create the RemoveSubstring function
/* 
1) Розробіть функцію видалення підрядка завдовжки n з позиції m у
вхідному рядку (вхідні параметри: похідний рядок, позиція, з якої необхідно
вилучати символи, кількість символів, що підлягає вилученню);
*/
CREATE FUNCTION RemoveSubstring
(
    @inputString VARCHAR(MAX),  -- Input string
    @startPosition INT,         -- Position from which to start removing (1-based index)
    @length INT                 -- Length of the substring to remove
)
    RETURNS VARCHAR(MAX)
AS
BEGIN
    DECLARE @resultString VARCHAR(MAX);

    -- Check for valid position and length
    IF @startPosition < 1 OR @startPosition > LEN(@inputString) OR @length < 0
BEGIN
RETURN @inputString;  -- Return original string if inputs are invalid
END

    -- Construct the resulting string
    SET @resultString = STUFF(@inputString, @startPosition, @length, '');

RETURN @resultString;
END
GO


-- Drop the GetLargestOrderByProductName function if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLargestOrderByProductName]') AND type IN (N'FN', N'IF'))
DROP FUNCTION [dbo].[GetLargestOrderByProductName]
GO

-- Create the GetLargestOrderByProductName function
/* 
9) Розробіть функцію, яка за назвою товару (вхідний параметр) повертає
дані щодо найбільшого замовлення цього товару;
*/
CREATE FUNCTION GetLargestOrderByProductName
(
    @productName VARCHAR(20)  -- Name of the product
)
    RETURNS TABLE
    AS
RETURN
(
    SELECT TOP 1 s.SalesId, s.CheckNo, s.Quantity, s.DateSale, s.GoodId
    FROM Sales s
    JOIN Goods g ON s.GoodId = g.GoodId
    WHERE g.Name = @productName
    ORDER BY s.Quantity DESC  -- Order by quantity to get the largest order
);
GO
