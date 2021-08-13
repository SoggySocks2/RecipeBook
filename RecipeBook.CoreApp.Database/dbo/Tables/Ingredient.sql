CREATE TABLE [dbo].[Ingredient] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [RecipeId]      UNIQUEIDENTIFIER NOT NULL,
    [Ingredient]    NVARCHAR (100)   NOT NULL,
    [UnitOfMeasure] NVARCHAR (50)    NOT NULL,
    [Qty]           SMALLINT         NOT NULL,
    [CreatedBy]     UNIQUEIDENTIFIER NOT NULL,
    [Created]       DATETIME         NOT NULL,
    [ModifiedBy]    UNIQUEIDENTIFIER NOT NULL,
    [Modified]      DATETIME         NOT NULL,
    CONSTRAINT [PK_Ingredient] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ingredient_Recipe] FOREIGN KEY ([RecipeId]) REFERENCES [dbo].[Recipe] ([Id]),
    CONSTRAINT [FK_Ingredient_UserAccount_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserAccount] ([Id]),
    CONSTRAINT [FK_Ingredient_UserAccount_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[UserAccount] ([Id])
);

