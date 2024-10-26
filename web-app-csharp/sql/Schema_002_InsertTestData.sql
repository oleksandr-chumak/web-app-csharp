-- Insert records into Departments, Workers, Goods, and Sales tables

-- 1. Insert into Departments
INSERT INTO Departments (Name, Info)
VALUES ('Products', NULL),                -- 8 characters
       ('Electronics', 'Electronics'),    -- 12 characters
       ('Clothing', 'Clothing items'),    -- 8 characters
       ('Stationery', 'Office supplies'), -- 11 characters
       ('Sports', 'Sports goods'); -- 6 characters

-- 2. Insert into Workers (using DeptId from Departments)
INSERT INTO Workers (Name, Address, DeptId, Information)
VALUES ('Ivan Petrenko', 'Kyiv, Shevchenko St, 10', 1, 'Department Manager'),    -- DeptId 1
       ('Olena Sydorenko', 'Lviv, Lesya Ukrainka St, 5', 2, 'Sales Specialist'), -- DeptId 2
       ('Serhiy Kovalenko', 'Odesa, Shevchenko Ave, 20', 3, 'Salesperson'),      -- DeptId 3
       ('Maria Hnatiuk', 'Kharkiv, Sumskaya St, 15', 4, 'Assistant'),            -- DeptId 4
       ('Oleksandr Kravchenko', 'Dnipro, Haharina Ave, 30', 5, 'Marketing Specialist'); -- DeptId 5

-- 3. Insert into Goods (using DeptId from Departments)
INSERT INTO Goods (Name, Price, Quantity, Producer, DeptId, Description)
VALUES ('Apples', 10.5, 100, 'Fruit Paradise', 1, 'Fresh apples'),           -- DeptId 1
       ('Television', 12000.0, 20, 'TechWorld', 2, 'Modern LED television'), -- DeptId 2
       ('T-shirt', 350.0, 50, 'Fashion', 3, 'Stylish summer t-shirt'),       -- DeptId 3
       ('Notebook', 45.0, 200, 'Office Supplies', 4, 'Notebook for notes'),  -- DeptId 4
       ('Football', 250.0, 150, 'SportStore', 5, 'Football for playing'); -- DeptId 5

-- 4. Insert into Sales (using GoodId from Goods)
INSERT INTO Sales (CheckNo, GoodId, DateSale, Quantity)
VALUES (1001, 1, '2024-10-01', 10), -- GoodId 1
       (1002, 2, '2024-10-02', 5),  -- GoodId 2
       (1003, 3, '2024-10-03', 15), -- GoodId 3
       (1004, 4, '2024-10-04', 20), -- GoodId 4
       (1005, 5, '2024-10-05', 8); -- GoodId 5
