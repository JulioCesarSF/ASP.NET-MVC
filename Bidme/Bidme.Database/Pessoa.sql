CREATE TABLE [dbo].[Pessoa]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] NVARCHAR(250) NOT NULL, 
    [Cpf] NVARCHAR(30) NOT NULL, 
    [Cep] NVARCHAR(30) NOT NULL, 
    [Numero] INT NOT NULL, 
    [Complemento] NVARCHAR(250) NULL, 
    [DataNascimento] DATETIME NOT NULL, 
    [Telefone] NVARCHAR(50) NOT NULL, 
    [IdUser] VARCHAR(MAX) NOT NULL
)
