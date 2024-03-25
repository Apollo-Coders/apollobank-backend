# ApolloBank

## Sobre o projeto

Nossa entidade principal seria a Account, onde se concentra as principais informações da conta do usuário, e suas relações, onde teremos o saldo, transações, cartões e demais. E complementado-o teremos o User junto com Address que armazenará os dados do usuário e suas credenciais de acesso.
A principal diferença no nosso banco é que decidimos incluir um sistema de crédito individual para cartões, ou seja, teremos um total de crédito geral para a conta, e esse crédito pode ser alocado para os cartões, essa ideia se deu devido a casos reais, em outros bancos, quando os dados de um cartão de um cliente são vazados, e um criminoso consegue fazer uso desse cartão, já que os clientes, em geral, não configuram um limite baixo para seu uso. Em nosso banco, é possível criar vários cartões e separar a utilidade de cada um deles setando um limite especial para cada.
Foram implementados todos os métodos solicitados com rotas extras.
Em relação a transações, temos tipo PIX, transferência, cartão, depósito e saque.
Criamos uma tabela que define o fatura do cliente no mês.
E os métodos comuns de cartão, pagar fatura, bloquear e alterar limite.

## Sobre o Desenvolvimento

A equipe foi dívida em duplas, cada dupla ficaria responsável por um Controller, sendo eles:
UserController
TransationsController
CreditCardsController, InvoiceController (Nesse caso ambos são relacionados então ficou como uma)
AuthController

Essas responsabilidades foram extraidas para Tasks dentro do Trello, os monitores (Guilherme e Luiza) ficaram responsável por essa distrubuição de tarefa e pela descrição detalhada de cada uma dessas tasks.

## Sobre as Dificuldades

No projeto em geral tivemos dúvidas pontuais, que com ajuda de estudo e dos colegas foi possível resolver. 
Mas para destacar as princípais dificuldades, temos o tempo e a organização do projeto. Como simples escoteiros mirins nesse enorme mundo da programação, coisas tal organização e planejamento prévio de projeto é algo bem difícil, tivemos impasses referentes a organização do projeto, pois a arquitetura pensada conflitava em alguns pontos e tivemos que remediar a situação de outra forma, então serviu como aprendizado.
O tempo também foi impetuoso, apesar do que o que planejamos foi desenvolvido, uma das partes principais para um código bom não conseguimos desenvolver bem, que seria os testes, seja testes manuais ou automatizados.

## Sobre as melhorias

Devido ao curto tempo temos muito onde podemos aprimorar:
No frontend referente a experiencia de usuário
Mas no backend podemos citar a parte de exceções que não deu tempo de criar exceções personalizadas para cada caso.
Testes unitários e e2e.
A parte de cartões de crédito e fatura, na integração com o front não foi totalmente concluida.

## Documentação 

![image](https://github.com/Apollo-Coders/apollobank-backend/assets/139771003/b4ba4fcb-fe19-4933-a43d-6e7155190173)

![image](https://github.com/Apollo-Coders/apollobank-backend/assets/139771003/309c6d2e-c301-4b0b-880f-a09ca2684542)

![image](https://github.com/Apollo-Coders/apollobank-backend/assets/139771003/e3b197ed-8cf0-40bc-8176-f4f9c862bb1d)

![image](https://github.com/Apollo-Coders/apollobank-backend/assets/139771003/64066cc8-3606-4c4a-a36d-a92acb5a6f48)

### /api/Auth/Authenticate

#### POST
##### Summary:

Endpoint utilizado para autenticar um usuário.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/CreditCard/CreateCreditCard

#### POST
##### Summary:

Endpoint utilizado para a criação de um novo cartão de crédito.

##### Description:

Este endpoint cria um novo cartão de crédito para o usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/CreditCard/BlockCreditCard/{cardNum}

#### PUT
##### Summary:

Endpoint utilizado para bloquear um cartão de crédito.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| cardNum | path | Número do cartão a ser bloqueado. | Yes | string |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/CreditCard/GetCreditCardsByAccountId

#### GET
##### Summary:

Endpoint utilizado para obter os cartões de crédito associados à conta do usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/CreditCard/GetAllCardByCardNumber

#### GET
##### Summary:

Endpoint utilizado para obter todos os cartões de crédito associados à conta do usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/CreditCard/SetCardLimit

#### PUT
##### Summary:

Endpoint utilizado para definir o limite de um cartão de crédito.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Invoice/GetActualMonthInvoice

#### GET
##### Summary:

Endpoint que obtém as faturas do mês atual para o usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Invoice/GetAllInvoices

#### GET
##### Summary:

Endpoint que obtém todas as faturas para o usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Invoice/PayParcialMonthInvoice/{cardNum}

#### POST
##### Summary:

Endpoint que paga parcialmente a fatura do mês atual com o número do cartão especificado.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| cardNum | path | Número do cartão. | Yes | string |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Invoice/PayTotalMonthInvoice

#### POST
##### Summary:

Endpoint que paga totalmente a fatura do mês atual para o usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/AddTransaction

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/MakeWithdrawal

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/Makedeposit

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/Scheduletransaction

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/AddTransactionCredit

#### POST
##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/GetCurrentMonthTransactions/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/GetLastSixMonthsTransactions/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/GetAllTransactions/{id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/Transactions/GetTransaction/{transaction_id}/{account_id}

#### GET
##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| transaction_id | path |  | Yes | integer |
| account_id | path |  | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/CreateUser

#### POST
##### Summary:

Endpoint utilizado para criar um novo usuário.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/GetUser

#### GET
##### Summary:

Endpoint utilizado para obter detalhes de um usuário pelo seu ID.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/UpdateUser

#### PUT
##### Summary:

Endpoint utilizado para atualizar informações de um usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/DeleteUser

#### DELETE
##### Summary:

Endpoint utilizado para excluir um usuário autenticado.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/GetUserByEmail

#### GET
##### Summary:

Endpoint utilizado para obter detalhes de um usuário pelo seu endereço de e-mail.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| email | query | O endereço de e-mail do usuário. | No | string |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/GetUserByCPF/{cpf}

#### GET
##### Summary:

Endpoint utilizado para obter detalhes de um usuário pelo seu CPF.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| cpf | path | O número de CPF do usuário. | Yes | string |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/GetUsers/GetUsers

#### GET
##### Summary:

Endpoint utilizado para obter detalhes de todos os usuários.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |

### /api/User/GetAccountInformation/GetAccount/{id}

#### GET
##### Summary:

Endpoint utilizado para obter informações de uma conta pelo seu número de identificação.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | O número de identificação da conta. | Yes | integer |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Success |



