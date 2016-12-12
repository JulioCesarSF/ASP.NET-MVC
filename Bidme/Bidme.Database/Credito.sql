CREATE TABLE [dbo].[Credito]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Total] INT NOT NULL, 
    [IdTransacao] INT NOT NULL, 
    [IdPessoa] INT NOT NULL, 
    CONSTRAINT [FK_Credito_TransacaoCredito] FOREIGN KEY ([IdTransacao]) REFERENCES [TransacaoCredito]([Id]), 
    CONSTRAINT [FK_Credito_Pessoa] FOREIGN KEY ([IdPessoa]) REFERENCES [Pessoa]([Id])
)
