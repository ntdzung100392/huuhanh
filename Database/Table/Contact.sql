CREATE TABLE [dbo].[Contact](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [Address] [nvarchar](max) NULL,
    [Email] [nvarchar](50) NULL,
    [Phone] [nvarchar](20) NULL,
    [Fax] [nvarchar](20) NULL,
    [TaxCode] [nvarchar](50) NULL,
    [IsActive] [bit] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.Contact] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contact] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[Contact] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Contact] ADD  DEFAULT (getdate()) FOR [Created]
GO