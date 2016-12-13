CREATE TABLE [dbo].[ValidadeNegociacao]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DataValidade] DATETIME NOT NULL, 
    [ValidadeDias] INT NOT NULL, 
    [DataInicio] DATETIME NOT NULL, 
    CONSTRAINT [FK_ValidadeNegociacao_Negociacao] FOREIGN KEY ([Id]) REFERENCES [Negociacao]([Id])
)
