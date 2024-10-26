-- Declare the variable to hold the department name
DECLARE @name VARCHAR(20);

-- Declare the cursor to select the department name based on the result from TopGood()
DECLARE dept_cursor CURSOR LOCAL FOR
SELECT Name
FROM DEPARTMENTS
WHERE DeptId = dbo.TopGood();

-- Open the cursor
OPEN dept_cursor;

-- Fetch the first row into the @name variable
FETCH NEXT FROM dept_cursor INTO @name;

-- Check if we have a row to process
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Print the department name
    PRINT @name;

    -- Fetch the next row
FETCH NEXT FROM dept_cursor INTO @name;
END

-- Close and deallocate the cursor
CLOSE dept_cursor;
DEALLOCATE dept_cursor;
GO