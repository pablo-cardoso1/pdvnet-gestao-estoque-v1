# 🏪 PDVNet - Sistema de Gestão de Estoque

Sistema desktop completo para gerenciamento de produtos em estoque, desenvolvido em **C# WPF** com **SQL Server**, seguindo arquitetura em camadas e padrão MVVM.

## 🎯 Objetivo do Projeto

Sistema desenvolvido para processo seletivo da PDVNet, demonstrando habilidades em:
- Desenvolvimento C# WPF com MVVM
- Estruturação de banco de dados SQL Server
- Implementação de operações CRUD
- Organização em camadas (UI, Business, Data, Model)
- Validações de negócio e testes unitários

## 📋 Funcionalidades Implementadas

### 🚀 CRUD Completo
- ✅ **Cadastrar** novos produtos
- ✅ **Listar** todos os produtos em DataGrid
- ✅ **Editar** produtos existentes
- ✅ **Excluir** produtos com confirmação

### 📊 Dashboard e Relatórios
- ✅ **Total de produtos** cadastrados
- ✅ **Valor total do estoque** (somatório Preço × Quantidade)
- ✅ **Alertas de estoque baixo** (quantidade < 5)
- ✅ **Lista em tempo real** de produtos com estoque crítico

### 🛡️ Validações e Regras de Negócio
- ✅ **Campos obrigatórios**: Nome, Preço, Quantidade
- ✅ **Valores não negativos**: Preço ≥ 0.01, Quantidade ≥ 0
- ✅ **Data de cadastro** automática
- ✅ **Validação em tempo real** com Data Annotations

## 🏗️ Arquitetura e Tecnologias

### 📁 Estrutura do Projeto

```
PDVnet.GestaoProdutos/
├── 📱 PDVnet.GestaoProdutos.UI/          # WPF + MVVM (Views/ViewModels)
├── ⚙️ PDVnet.GestaoProdutos.Business/     # Lógica de negócio + Validações
├── 🗄️ PDVnet.GestaoProdutos.Data/         # Acesso a dados (Repository)
├── 📦 PDVnet.GestaoProdutos.Model/        # Entidades e Models
└── 🧪 PDVnet.GestaoProdutos.Tests/        # Testes unitários (MSTest)
```

### 🛠 Stack Tecnológica
- **Frontend**: WPF, XAML, MVVM Pattern
- **Backend**: C#, .NET 6.0
- **Database**: SQL Server + ADO.NET
- **Validação**: Data Annotations + Custom Validation
- **Testes**: MSTest Framework
- **Commands**: RelayCommand Pattern

## 🚀 Como Executar

### Pré-requisitos
- **SQL Server** (Express, Developer ou LocalDB)
- **.NET 8.0 SDK** ou superior
- **Visual Studio 2022**

### 📥 Configuração Passo a Passo

#### 1. **Banco de Dados**
```sql
-- Execute o script em Scripts/CreateDatabase.sql
-- O script cria:
-- - Database PDVnet_GestaoProdutos
-- - Tabela Produtos com constraints
-- - Dados de exemplo
-- - Views para relatórios
```

#### 2. **Connection String**
```xml
<!-- No App.config do projeto UI -->
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=PDVnet_GestaoProdutos;Integrated Security=true;TrustServerCertificate=true;"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

#### 3. **Executar Aplicação**
- Defina `PDVnet.GestaoProdutos.UI` como Startup Project
- Pressione **F5** para executar

### 🧪 Executando Testes
- Abra **Test Explorer** (View → Test Explorer)
- Execute todos os testes
- 5 testes unitários devem passar com sucesso

## 📊 Estrutura do Banco

### Tabela `Produtos`

| Coluna | Tipo | Descrição |
|--------|------|-----------|
| Id | INT IDENTITY(1,1) | Chave primária |
| Nome | NVARCHAR(100) NOT NULL | Nome do produto |
| Descricao | NVARCHAR(255) NULL | Descrição opcional |
| Preco | DECIMAL(10,2) NOT NULL | Preço unitário (≥ 0) |
| Quantidade | INT NOT NULL | Quantidade em estoque (≥ 0) |
| DataCadastro | DATETIME NOT NULL | Data de cadastro (DEFAULT GETDATE()) |

## 🎨 Funcionalidades da Interface

### Tela Principal
- DataGrid com lista completa de produtos
- Botões de ação (Novo, Editar, Excluir, Atualizar)
- Dashboard com métricas em tempo real
- Alertas visuais para estoque baixo

### Formulário de Produto
- Validação em tempo real
- Modo Editar/Adicionar
- Mensagens de erro contextualizadas
- Interface responsiva

## Desenvolvido por:
Pablo Cardoso para o processo seletivo da PDVnet
