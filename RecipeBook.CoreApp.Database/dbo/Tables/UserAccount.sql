CREATE TABLE [dbo].[UserAccount] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [FirstName]     NVARCHAR (30)    NOT NULL,
    [LastName]      NVARCHAR (50)    NOT NULL,
    [UserName]      NVARCHAR (50)    NOT NULL,
    [Password]      NVARCHAR (30)    NOT NULL,
    [Role]          NVARCHAR (50)    NOT NULL,
    [Created]       DATETIME         NOT NULL,
    [CreatedBy]     UNIQUEIDENTIFIER NOT NULL,
    [Modified]      DATETIME         NOT NULL,
    [ModifiedBy]    UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted]     BIT              NOT NULL DEFAULT 0,
    CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);

