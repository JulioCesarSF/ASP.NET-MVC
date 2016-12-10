CREATE TABLE [dbo].[Negociacao]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdComprador] NVARCHAR(MAX) NULL, 
    [IdVendedor] NVARCHAR(MAX) NULL, 
    [Valor] DECIMAL(18, 2) NOT NULL, 
    [Status] NVARCHAR(250) NOT NULL, 
    [Tipo] INT NOT NULL, 
    [IdProduto] INT NOT NULL, 
    CONSTRAINT [FK_Negociacao_Produto] FOREIGN KEY ([IdProduto]) REFERENCES [Produto]([Id])
)
