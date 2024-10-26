-- Drop Sales table if it exists
IF OBJECT_ID('Sales', 'U') IS NOT NULL
DROP TABLE Sales;
GO

-- Drop Goods table if it exists
IF OBJECT_ID('Goods', 'U') IS NOT NULL
DROP TABLE Goods;
GO

-- Drop Workers table if it exists
IF OBJECT_ID('Workers', 'U') IS NOT NULL
DROP TABLE Workers;
GO

-- Drop Departments table if it exists
IF OBJECT_ID('Departments', 'U') IS NOT NULL
DROP TABLE Departments;
GO

-- Create the Departments table
CREATE TABLE Departments
(
    DeptId DECIMAL(4,0) IDENTITY(1,1) PRIMARY KEY, -- Auto-incremented department identifier
    Name VARCHAR(20) NOT NULL,                      -- Department name
    Info VARCHAR(40)                                 -- Department information
);

-- Create the Workers table
CREATE TABLE Workers
(
    WorkersId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Auto-incremented worker identifier
    Name VARCHAR(20) NOT NULL,                          -- Worker name
    Address VARCHAR(40),                                -- Address
    DeptId DECIMAL(4,0),                               -- Department identifier (foreign key)
    Information NVARCHAR(20),                           -- Worker information
    FOREIGN KEY (DeptId) REFERENCES Departments(DeptId) -- Foreign key reference
);

-- Create the Goods table
CREATE TABLE Goods
(
    GoodId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,     -- Auto-incremented good identifier
    Name VARCHAR(20) NOT NULL,                          -- Good name
    Price FLOAT,                                        -- Price per unit
    Quantity INT,                                       -- Quantity of goods
    Producer VARCHAR(20),                               -- Producer of goods
    DeptId DECIMAL(4,0),                               -- Department identifier (foreign key)
    Description NVARCHAR(50),                           -- Description of the good
    FOREIGN KEY (DeptId) REFERENCES Departments(DeptId) -- Foreign key reference
);

-- Create the Sales table
CREATE TABLE Sales
(
    SalesId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,    -- Auto-incremented sale identifier
    CheckNo INT NOT NULL,                              -- Check number of the sale
    GoodId INT NOT NULL,                               -- Good identifier (foreign key)
    DateSale DATE NOT NULL,                            -- Sale date
    Quantity INT,                                       -- Quantity sold
    FOREIGN KEY (GoodId) REFERENCES Goods(GoodId)     -- Foreign key reference
);

GO
