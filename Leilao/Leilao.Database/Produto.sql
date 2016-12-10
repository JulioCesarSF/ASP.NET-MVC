CREATE TABLE [dbo].[Produto]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] NVARCHAR(MAX) NOT NULL, 
    [Descricao] NVARCHAR(MAX) NULL, 
    [Imagem] NVARCHAR(MAX) NOT NULL, 
    [Valor] DECIMAL(18, 2) NOT NULL, 
    [IdVendedor] INT NOT NULL, 
    CONSTRAINT [FK_Produto_Pessoa] FOREIGN KEY ([IdVendedor]) REFERENCES [Pessoa]([Id])
)
