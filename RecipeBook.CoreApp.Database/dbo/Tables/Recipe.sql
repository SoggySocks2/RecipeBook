﻿CREATE TABLE [dbo].[Recipe] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (100)   NOT NULL,
    [Description] NVARCHAR (250)   NOT NULL,
    [Note]       NVARCHAR (1000)  NOT NULL,
    [Score]       FLOAT         NULL,
    [Created]     DATETIME         NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER NOT NULL,
    [Modified]    DATETIME         NOT NULL,
    [ModifiedBy]  UNIQUEIDENTIFIER NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Recipe_UserAccount_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserAccount] ([Id]),
    CONSTRAINT [FK_Recipe_UserAccount_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[UserAccount] ([Id])
);

