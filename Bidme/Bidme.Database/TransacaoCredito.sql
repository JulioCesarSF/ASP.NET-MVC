CREATE TABLE [dbo].[TransacaoCredito]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Data] DATETIME NOT NULL, 
    [Valor] DECIMAL NOT NULL, 
    [Quantidade] INT NOT NULL
)
