CREATE TABLE [dbo].[Client](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](150) NULL,
    [ContactId] [int] NOT NULL,
    [IsOrganization] [bit] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [Created] [datetime] NOT NULL,
    [Updated] [datetime] NULL,
 CONSTRAINT [PK_dbo.Client] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Client] ADD  DEFAULT (newid()) FOR [UniqueId]
GO

ALTER TABLE [dbo].[Client] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Client] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Client] ADD  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Contact]
GO