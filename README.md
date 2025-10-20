# ?? PDVNet - Sistema de Gest�o de Estoque

Sistema desktop completo para gerenciamento de produtos em estoque, desenvolvido em **C# WPF** com **SQL Server**, seguindo arquitetura em camadas e padr�o MVVM.

## ?? Objetivo do Projeto

Sistema desenvolvido para processo seletivo da PDVNet, demonstrando habilidades em:
- Desenvolvimento C# WPF com MVVM
- Estrutura��o de banco de dados SQL Server
- Implementa��o de opera��es CRUD
- Organiza��o em camadas (UI, Business, Data, Model)
- Valida��es de neg�cio e testes unit�rios

## ?? Funcionalidades Implementadas

### ?? CRUD Completo
- ? **Cadastrar** novos produtos
- ? **Listar** todos os produtos em DataGrid
- ? **Editar** produtos existentes
- ? **Excluir** produtos com confirma��o

### ?? Dashboard e Relat�rios
- ? **Total de produtos** cadastrados
- ? **Valor total do estoque** (somat�rio Pre�o � Quantidade)
- ? **Alertas de estoque baixo** (quantidade < 5)
- ? **Lista em tempo real** de produtos com estoque cr�tico

### ??? Valida��es e Regras de Neg�cio
- ? **Campos obrigat�rios**: Nome, Pre�o, Quantidade
- ? **Valores n�o negativos**: Pre�o ? 0.01, Quantidade ? 0
- ? **Data de cadastro** autom�tica (definida no banco de dados)
- ? **Valida��o em tempo real** com Data Annotations

## ??? Arquitetura e Tecnologias

### ?? Estrutura do Projeto

```
PDVnet.GestaoProdutos/
??? ?? PDVnet.GestaoProdutos.UI/          # WPF + MVVM (Views/ViewModels)
??? ?? PDVnet.GestaoProdutos.Business/     # L�gica de neg�cio + Valida��es
??? ??? PDVnet.GestaoProdutos.Data/         # Acesso a dados (Repository)
??? ?? PDVnet.GestaoProdutos.Model/        # Entidades e Models
??? ?? PDVnet.GestaoProdutos.Tests/        # Testes unit�rios (MSTest)
```

### ?? Stack Tecnol�gica
- **Frontend**: WPF, XAML, MVVM Pattern
- **Backend**: C#, .NET 8.0
- **Database**: SQL Server + ADO.NET (Microsoft.Data.SqlClient)
- **Valida��o**: Data Annotations + valida��es no ViewModel
- **Testes**: MSTest Framework
- **Commands**: RelayCommand Pattern

## ?? Como Executar

### Pr�-requisitos
- **SQL Server** (Express, Developer ou LocalDB)
- **.NET 8.0 SDK** ou superior
- **Visual Studio 2022/2023** (com workload de .NET desktop)

### ?? Configura��o Passo a Passo

#### 1. **Banco de Dados**

O script est� em `PDVnet.GestaoProdutos.Data\Scripts\CreateDatabase.sql`.

Op��es para executar o script:

- Usando SQL Server Management Studio (SSMS): abra o arquivo e clique em "Execute" apontando para a inst�ncia desejada.

- Usando `sqlcmd` (exemplo para SQLEXPRESS):

```bash
sqlcmd -S .\SQLEXPRESS -i "PDVnet.GestaoProdutos.Data\Scripts\CreateDatabase.sql"
```

- Usando LocalDB (inicie a inst�ncia e altere connection string para `(localdb)\MSSQLLocalDB` se preferir):

```powershell
sqllocaldb start "MSSQLLocalDB"
sqlcmd -S (localdb)\\MSSQLLocalDB -i "PDVnet.GestaoProdutos.Data\Scripts\CreateDatabase.sql"
```

> Observa��o: o script cria o banco `PDVnet_GestaoProdutos`, a tabela `Produtos`, um �ndice e insere alguns dados de exemplo.

#### 2. **Connection String**

Verifique e ajuste a connection string presente em `PDVnet.GestaoProdutos.UI\App.config` (projeto `PDVnet.GestaoProdutos.UI`). Exemplo padr�o usado no projeto:

```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=PDVnet_GestaoProdutos;Integrated Security=true;TrustServerCertificate=true;"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

- Para usar LocalDB, substitua `Data Source` por `(localdb)\MSSQLLocalDB`.
- Assegure-se de que o `App.config` com essa connection string esteja no projeto de startup (`PDVnet.GestaoProdutos.UI`).

#### 3. **Build e Execu��o**

- Pelo Visual Studio: abra a solu��o, defina `PDVnet.GestaoProdutos.UI` como *Startup Project* e pressione **F5**.

- Pela CLI:

```bash
dotnet build
cd PDVnet.GestaoProdutos.UI
dotnet run
```

(Na pr�tica o `dotnet run` em WPF normalmente � feito via Visual Studio; use o IDE para debugging/execu��o.)

### ?? Executando Testes

Existem 5 testes unit�rios de valida��o (arquivo `PDVnet.GestaoProdutos.Tests\ProdutoTests.cs`). Para rodar os testes:

```bash
dotnet test PDVnet.GestaoProdutos.Tests
```

## ?? Estrutura do Banco

### Tabela `Produtos`

| Coluna | Tipo | Descri��o |
|--------|------|-----------|
| Id | INT IDENTITY(1,1) | Chave prim�ria |
| Nome | NVARCHAR(100) NOT NULL | Nome do produto |
| Descricao | NVARCHAR(255) NULL | Descri��o opcional |
| Preco | DECIMAL(10,2) NOT NULL | Pre�o unit�rio (? 0) |
| Quantidade | INT NOT NULL | Quantidade em estoque (? 0) |
| DataCadastro | DATETIME NOT NULL | Data de cadastro (DEFAULT GETDATE()) |

> Observa��o: a fonte da verdade para `DataCadastro` � o banco (DEFAULT GETDATE()). O model tamb�m inicializa `DateTime.Now` para conveni�ncia em mem�ria, mas recomenda-se confiar no valor do banco ao persistir.

## ?? Funcionalidades da Interface

### Tela Principal
- DataGrid com lista completa de produtos
- Bot�es de a��o (Novo, Editar, Excluir, Atualizar)
- Dashboard com m�tricas em tempo real
- Alertas visuais para estoque baixo

### Formul�rio de Produto
- Valida��o em tempo real
- Modo Editar/Adicionar
- Mensagens de erro contextualizadas

## Desenvolvido Por:

Pablo Cardoso para o processo seletivo da PDVNet.