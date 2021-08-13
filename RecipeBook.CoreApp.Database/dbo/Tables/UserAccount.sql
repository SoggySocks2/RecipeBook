CREATE TABLE [dbo].[UserAccount] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Firstname] NVARCHAR (30)    NOT NULL,
    [Lastname]  NVARCHAR (50)    NOT NULL,
    [Username]  NVARCHAR (50)    NOT NULL,
    [Password]  NVARCHAR (30)    NOT NULL,
    [IsActive]  BIT              NOT NULL DEFAULT 1,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);

