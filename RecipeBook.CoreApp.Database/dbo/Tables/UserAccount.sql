CREATE TABLE [dbo].[UserAccount] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Username]  NVARCHAR (50)    NOT NULL,
    [Password]  NVARCHAR (30)    NOT NULL,
    [Firstname] NVARCHAR (30)    NOT NULL,
    [Lastname]  NVARCHAR (50)    NOT NULL,
    [IsActive]  BIT              NOT NULL,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);

