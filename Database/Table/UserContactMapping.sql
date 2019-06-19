CREATE TABLE [dbo].[UserContactMapping](
    [UserDetailId] [int] NOT NULL,
    [ContactId] [int] NOT NULL
)
GO

ALTER TABLE [dbo].[UserContactMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserContactMapping_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserContactMapping] CHECK CONSTRAINT [FK_UserContactMapping_Contact]
GO

ALTER TABLE [dbo].[UserContactMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserContactMapping_UserDetail] FOREIGN KEY([UserDetailId])
REFERENCES [dbo].[UserDetail] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserContactMapping] CHECK CONSTRAINT [FK_UserContactMapping_UserDetail]
GO