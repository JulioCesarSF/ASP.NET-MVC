CREATE TABLE [dbo].[Historico]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdNegociacao] INT NOT NULL, 
    CONSTRAINT [FK_Historico_Negociacao] FOREIGN KEY ([IdNegociacao]) REFERENCES [Negociacao]([Id])
)
