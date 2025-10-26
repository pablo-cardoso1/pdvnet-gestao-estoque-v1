    -- Script de criação do banco e tabela para PDVnet.GestaoProdutos
    -- Ajuste o nome do banco conforme necessário (ex.: PDVnet_GestaoProdutos)

    IF DB_ID('PDVnet_GestaoProdutos') IS NULL
    BEGIN
        CREATE DATABASE PDVnet_GestaoProdutos;
        PRINT 'Banco de dados PDVnet_GestaoProdutos criado com sucesso.';
    END
    ELSE
    BEGIN
        PRINT 'Banco de dados PDVnet_GestaoProdutos já existe.';
    END
    GO

    USE PDVnet_GestaoProdutos;
    GO

    -- Tabela de Produtos
    IF OBJECT_ID('dbo.Produtos', 'U') IS NOT NULL
    BEGIN
        DROP TABLE dbo.Produtos;
        PRINT 'Tabela Produtos removida e será recriada.';
    END
    GO

    CREATE TABLE dbo.Produtos
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(100) NOT NULL,
        Descricao NVARCHAR(255) NULL,
        Preco DECIMAL(10,2) NOT NULL CHECK (Preco >= 0),
        Quantidade DECIMAL(10,3) NOT NULL CHECK (Quantidade >= 0),
        DataCadastro DATETIME NOT NULL DEFAULT(GETDATE()),
        DataAtualizacao DATETIME NULL,
        EstoqueAtual Decimal(10,3) NOT NULL DEFAULT(0)
    );
    GO

    PRINT 'Tabela Produtos criada com sucesso.';

    -- Índices recomendados
    CREATE INDEX IX_Produtos_Nome ON dbo.Produtos(Nome);
    GO

    PRINT 'Índice IX_Produtos_Nome criado com sucesso.';


    -- Dados de exemplo
    INSERT INTO dbo.Produtos (Nome, Descricao, Preco, Quantidade)
    VALUES
    ('Caneta Azul', 'Caneta esferográfica azul 1.0mm', 1.50, 100),
    ('Caderno 100 folhas', 'Caderno universitário 100 folhas', 15.00, 20),
    ('Lápis HB', 'Lápis grafite HB', 0.75, 300),
    ('Borracha Branca', 'Borracha branca macia', 1.20, 150),
    ('Régua 30cm', 'Régua plástica 30cm', 2.50, 80);
    GO

    PRINT 'Dados de exemplo inseridos com sucesso.';

    -- Consulta para verificar os dados inseridos
    SELECT * FROM dbo.Produtos;
    GO