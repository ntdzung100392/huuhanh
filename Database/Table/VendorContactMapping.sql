CREATE TABLE [dbo].[VendorContactMapping](
    [VendorId] [int] NOT NULL,
    [ContactId] [int] NOT NULL
)
GO

ALTER TABLE [dbo].[VendorContactMapping]  WITH CHECK ADD  CONSTRAINT [FK_VendorContactMapping_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[VendorContactMapping] CHECK CONSTRAINT [FK_VendorContactMapping_Vendor]
GO

ALTER TABLE [dbo].[VendorContactMapping]  WITH CHECK ADD  CONSTRAINT [FK_VendorContactMapping_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[VendorContactMapping] CHECK CONSTRAINT [FK_VendorContactMapping_Contact]
GO