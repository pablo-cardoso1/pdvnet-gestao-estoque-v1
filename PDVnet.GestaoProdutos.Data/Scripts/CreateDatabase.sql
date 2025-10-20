-- Script de criação do banco e tabela para PDVnet.GestaoProdutos
-- Ajuste o nome do banco conforme necessário (ex.: PDVnet_GestaoProdutos)

IF DB_ID('PDVnet_GestaoProdutos') IS NULL
BEGIN
    CREATE DATABASE PDVnet_GestaoProdutos;
END
GO

USE PDVnet_GestaoProdutos;
GO

IF OBJECT_ID('dbo.Produtos', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Produtos;
END
GO

CREATE TABLE dbo.Produtos
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255) NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Quantidade INT NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT(GETDATE())
);
GO

-- Índices recomendados
CREATE INDEX IX_Produtos_Nome ON dbo.Produtos(Nome);
GO

-- Dados de exemplo
INSERT INTO dbo.Produtos (Nome, Descricao, Preco, Quantidade)
VALUES
('Caneta Azul', 'Caneta esferográfica azul 1.0mm', 1.50, 100),
('Caderno 100 folhas', 'Caderno universitário 100 folhas', 15.00, 20),
('Lápis HB', 'Lápis grafite HB', 0.75, 3);
GO
