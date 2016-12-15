# Projeto ASP.NET MVC Bidme
> A proposta do projeto é a de lances em um produto até que o vendedor aceite vender pelo preço.
> O usuário terá créditos que possuem determinado valor e ele poderá usar estes créditos para diminuir o valor do produto que deseja comprar.
> Créditos parcialmente implementados (12/12/16)

## Author
> Júlio César Schincariol Filho

## Conteúdo dos Projetos dentro da Solution
1. Database : MS SQL Server
2. Dominio : Models e DataAccess(para login)
3. Persistencia: Repositories e UnitOfWork
4. MVC.Web : Controllers, Views, ViewModels
5. Service (API) : Controllers, DTOs

## Imagens

> Painel de Créditos
![painel Credito](https://s24.postimg.org/8zz9dxywl/painel_credito_resumo.png)

> Painel para iniciar uma Venda e Cadastro de Produto
![painel vender](https://s23.postimg.org/cg55sv5gr/painel_venda_cadastro_produtos.png)

> Painel com Produtos para Compra
![painel comprar](https://s28.postimg.org/z4fsj56xp/painel_produtos_comprar.png)

> Painel de Login
![painel login](https://s27.postimg.org/qpsjo6nkj/painel_user_login.png)

### TODO
1. Implementar buscas em alguns locais do projeto para o usuário (funcionalidades presentes somente na API)
2. Implementar editar/deletar produto (já é possível pela API)
3. Implementar cancelamento de venda
4. Histórico de transações