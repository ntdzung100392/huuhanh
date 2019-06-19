CREATE TABLE [dbo].[UserDetail](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [UserId] [int] NOT NULL,
    [FirstName] [nvarchar](255) NOT NULL,
    [MiddleName] [nvarchar](255) NOT NULL,
    [LastName] [nvarchar](255) NOT NULL,
    [Sex] [nvarchar](10) NOT NULL,
    [DOB] [datetime] NOT NULL,
    [IDCard] [nvarchar](50) NULL,
    [JoiningDate] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.UserDetail] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserDetail] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[UserDetail] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[UserDetail] ADD  DEFAULT (getdate()) FOR [Created]
GO