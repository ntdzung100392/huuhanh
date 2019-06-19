PRINT 'Create Product Triggers'
GO
CREATE TRIGGER [dbo].[TR_Product_UpdateInStock]
    ON [dbo].[Product]
    AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE([Stock])
    BEGIN
        UPDATE [dbo].[Product]
        SET [InStock] = 'OUTOFSTOCK'
        WHERE [Stock] = 0 AND [Stock] IN (SELECT DISTINCT [Stock] FROM Inserted)
    END
END

BEGIN
ALTER TABLE [dbo].[Product] ENABLE TRIGGER [TR_Product_UpdateInStock]
END