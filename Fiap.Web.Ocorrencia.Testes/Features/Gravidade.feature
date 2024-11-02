Feature: Gravidade API
  Como usuário do sistema
  Eu quero interagir com a API de gravidade
  Para que eu possa visualizar e verificar os dados de gravidade

  Scenario: Obter lista de gravidades com sucesso
    Given que existem gravidades cadastradas
    When eu faço uma solicitação GET para "/api/gravidade"
    Then o status da resposta deve ser 200
    And o corpo da resposta deve conter uma lista de gravidades
    And o contrato da resposta deve estar em conformidade com o JSON Schema "gravidade-schema.json"

  Scenario: Obter lista de gravidades com falha - Gravidades não encontradas
    Given que não existem gravidades cadastradas
    When eu faço uma solicitação GET para "/api/gravidade"
    Then o status da resposta deve ser 204
    And o corpo da resposta deve estar vazio

  Scenario: Solicitar lista de gravidades com parâmetros inválidos
    Given que existem gravidades cadastradas
    When eu faço uma solicitação GET para "/api/gravidade" com referência -1 e tamanho -10
    Then o status da resposta deve ser 400
    And o corpo da resposta deve conter uma mensagem de erro
