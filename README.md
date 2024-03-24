# ApolloBank
## Version: v1

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



