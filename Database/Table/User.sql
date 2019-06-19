CREATE TABLE [dbo].[User](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [UserName] [nvarchar](18) NULL,
    [UserCode] [nvarchar](10) NOT NULL,
    [PassWord] [nvarchar](20) NULL,
    [LastAccess] [datetime] NULL,
    [UserDetailId] [int] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserDetail] FOREIGN KEY([UserDetailId])
REFERENCES [dbo].[UserDetail] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserDetail]
GO