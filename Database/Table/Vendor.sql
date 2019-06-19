CREATE TABLE [dbo].[Vendor](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](150) NOT NULL,
    [Note] [nvarchar](max) NULL,
    [IsActive] [bit] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.Vendor] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Vendor] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[Vendor] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Vendor] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Vendor] ADD  DEFAULT (getdate()) FOR [Created]
GO