CREATE TABLE [dbo].[Product](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [ProductCode] [nvarchar](10) NOT NULL,
    [Name] [nvarchar](150) NULL,
    [CategoryId] [int] NOT NULL,
    [VendorId] [int] NOT NULL,
    [InputCost] [decimal](18, 4) NOT NULL,
    [BaseCost] [decimal](18, 4) NOT NULL,
    [ForeignCurrency] [nvarchar](18) NOT NULL,
    [IssuedDate] [datetime] NOT NULL,
    [Stock] [int] NOT NULL,
    [InStock] [nvarchar](15) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.Product] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ('') FOR [ProductCode]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ('VND') FOR [ForeignCurrency]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Stock]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ('INSTOCK') FOR [InStock]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Vendor]
GO