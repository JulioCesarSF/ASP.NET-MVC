CREATE TABLE [dbo].[TransacaoCredito]
(
    [IdCredito] INT NOT NULL, 
    [IdTransacao] INT NOT NULL, 
    CONSTRAINT [FK_TransacaoCredito_Credito] FOREIGN KEY ([IdCredito]) REFERENCES [Credito]([Id]),
	CONSTRAINT [FK_TransacaoCreidto_Transacao] FOREIGN KEY([IdTransacao]) REFERENCES [Transacao]([Id])
)
