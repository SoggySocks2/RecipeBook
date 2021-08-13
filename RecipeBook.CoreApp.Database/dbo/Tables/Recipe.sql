CREATE TABLE [dbo].[Recipe] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (100)   NOT NULL,
    [Description] NVARCHAR (250)   NOT NULL,
    [Notes]       NVARCHAR (1000)  NOT NULL,
    [Score]       SMALLINT         NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER NOT NULL,
    [Created]     DATETIME         NOT NULL,
    [ModifiedBy]  UNIQUEIDENTIFIER NOT NULL,
    [Modified]    DATETIME         NOT NULL,
    CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Recipe_UserAccount_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserAccount] ([Id]),
    CONSTRAINT [FK_Recipe_UserAccount_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[UserAccount] ([Id])
);

