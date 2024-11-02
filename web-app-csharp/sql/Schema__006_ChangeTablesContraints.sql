-- Check constraints

SELECT
    tc.TABLE_NAME,
    kcu.COLUMN_NAME,
    tc.CONSTRAINT_NAME
FROM
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
        JOIN
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS kcu
    ON
        tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
WHERE
    tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
  AND kcu.COLUMN_NAME = 'DeptId'
  AND tc.TABLE_NAME IN ('Workers', 'Goods');

-- Change constraints
ALTER TABLE Workers
    DROP CONSTRAINT FK__Workers__DeptId__4E88ABD4;

ALTER TABLE Goods
    DROP CONSTRAINT FK__Goods__DeptId__5165187F;

ALTER TABLE Workers
    ADD CONSTRAINT FK_Workers_DeptId FOREIGN KEY (DeptId)
        REFERENCES Departments(DeptId)
        ON DELETE SET NULL;

ALTER TABLE Goods
    ADD CONSTRAINT FK_Goods_DeptId FOREIGN KEY (DeptId)
        REFERENCES Departments(DeptId)
        ON DELETE SET NULL;

GO