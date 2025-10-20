# ?? PDVNet - Sistema de Gestão de Estoque

Sistema desktop completo para gerenciamento de produtos em estoque, desenvolvido em **C# WPF** com **SQL Server**, seguindo arquitetura em camadas e padrão MVVM.

## ?? Objetivo do Projeto

Sistema desenvolvido para processo seletivo da PDVNet, demonstrando habilidades em:
- Desenvolvimento C# WPF com MVVM
- Estruturação de banco de dados SQL Server
- Implementação de operações CRUD
- Organização em camadas (UI, Business, Data, Model)
- Validações de negócio e testes unitários

## ?? Funcionalidades Implementadas

### ?? CRUD Completo
- ? **Cadastrar** novos produtos
- ? **Listar** todos os produtos em DataGrid
- ? **Editar** produtos existentes
- ? **Excluir** produtos com confirmação

### ?? Dashboard e Relatórios
- ? **Total de produtos** cadastrados
- ? **Valor total do estoque** (somatório Preço × Quantidade)
- ? **Alertas de estoque baixo** (quantidade < 5)
- ? **Lista em tempo real** de produtos com estoque crítico

### ??? Validações e Regras de Negócio
- ? **Campos obrigatórios**: Nome, Preço, Quantidade
- ? **Valores não negativos**: Preço ? 0.01, Quantidade ? 0
- ? **Data de cadastro** automática (definida no banco de dados)
- ? **Validação em tempo real** com Data Annotations

## ??? Arquitetura e Tecnologias

### ?? Estrutura do Projeto

```
PDVnet.GestaoProdutos/
??? ?? PDVnet.GestaoProdutos.UI/          # WPF + MVVM (Views/ViewModels)
??? ?? PDVnet.GestaoProdutos.Business/     # Lógica de negócio + Validações
??? ??? PDVnet.GestaoProdutos.Data/         # Acesso a dados (Repository)
??? ?? PDVnet.GestaoProdutos.Model/        # Entidades e Models
??? ?? PDVnet.GestaoProdutos.Tests/        # Testes unitários (MSTest)
```

### ?? Stack Tecnológica
- **Frontend**: WPF, XAML, MVVM Pattern
- **Backend**: C#, .NET 8.0
- **Database**: SQL Server + ADO.NET (Microsoft.Data.SqlClient)
- **Validação**: Data Annotations + validações no ViewModel
- **Testes**: MSTest Framework
- **Commands**: RelayCommand Pattern

## ?? Como Executar

### Pré-requisitos
- **SQL Server** (Express, Developer ou LocalDB)
- **.NET 8.0 SDK** ou superior
- **Visual Studio 2022/2023** (com workload de .NET desktop)

### ?? Configuração Passo a Passo

#### 1. **Banco de Dados**

O script está em `PDVnet.GestaoProdutos.Data\Scripts\CreateDatabase.sql`.

Opções para executar o script:

- Usando SQL Server Management Studio (SSMS): abra o arquivo e clique em "Execute" apontando para a instância desejada.

- Usando `sqlcmd` (exemplo para SQLEXPRESS):

```bash
sqlcmd -S .\SQLEXPRESS -i "PDVnet.GestaoProdutos.Data\Scripts\CreateDatabase.sql"
```

- Usando LocalDB (inicie a instância e altere connection string para `(localdb)\MSSQLLocalDB` se preferir):

```powershell
sqllocaldb start "MSSQLLocalDB"
sqlcmd -S (localdb)\\MSSQLLocalDB -i "PDVnet.GestaoProdutos.Data\Scripts\CreateDatabase.sql"
```

> Observação: o script cria o banco `PDVnet_GestaoProdutos`, a tabela `Produtos`, um índice e insere alguns dados de exemplo.

#### 2. **Connection String**

Verifique e ajuste a connection string presente em `PDVnet.GestaoProdutos.UI\App.config` (projeto `PDVnet.GestaoProdutos.UI`). Exemplo padrão usado no projeto:

```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=PDVnet_GestaoProdutos;Integrated Security=true;TrustServerCertificate=true;"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

- Para usar LocalDB, substitua `Data Source` por `(localdb)\MSSQLLocalDB`.
- Assegure-se de que o `App.config` com essa connection string esteja no projeto de startup (`PDVnet.GestaoProdutos.UI`).

#### 3. **Build e Execução**

- Pelo Visual Studio: abra a solução, defina `PDVnet.GestaoProdutos.UI` como *Startup Project* e pressione **F5**.

- Pela CLI:

```bash
dotnet build
cd PDVnet.GestaoProdutos.UI
dotnet run
```

(Na prática o `dotnet run` em WPF normalmente é feito via Visual Studio; use o IDE para debugging/execução.)

### ?? Executando Testes

Existem 5 testes unitários de validação (arquivo `PDVnet.GestaoProdutos.Tests\ProdutoTests.cs`). Para rodar os testes:

```bash
dotnet test PDVnet.GestaoProdutos.Tests
```

## ?? Estrutura do Banco

### Tabela `Produtos`

| Coluna | Tipo | Descrição |
|--------|------|-----------|
| Id | INT IDENTITY(1,1) | Chave primária |
| Nome | NVARCHAR(100) NOT NULL | Nome do produto |
| Descricao | NVARCHAR(255) NULL | Descrição opcional |
| Preco | DECIMAL(10,2) NOT NULL | Preço unitário (? 0) |
| Quantidade | INT NOT NULL | Quantidade em estoque (? 0) |
| DataCadastro | DATETIME NOT NULL | Data de cadastro (DEFAULT GETDATE()) |

> Observação: a fonte da verdade para `DataCadastro` é o banco (DEFAULT GETDATE()). O model também inicializa `DateTime.Now` para conveniência em memória, mas recomenda-se confiar no valor do banco ao persistir.

## ?? Funcionalidades da Interface

### Tela Principal
- DataGrid com lista completa de produtos
- Botões de ação (Novo, Editar, Excluir, Atualizar)
- Dashboard com métricas em tempo real
- Alertas visuais para estoque baixo

### Formulário de Produto
- Validação em tempo real
- Modo Editar/Adicionar
- Mensagens de erro contextualizadas

## Desenvolvido Por:

Pablo Cardoso para o processo seletivo da PDVNet.