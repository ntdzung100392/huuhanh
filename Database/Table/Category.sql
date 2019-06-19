CREATE TABLE [dbo].[Category](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](50) NOT NULL,
    [Code] [nvarchar](10) NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.Category] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Category] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[Category] ADD  DEFAULT (getdate()) FOR [Created]
GO