Feature: Ocorrências
  Como um usuário
  Eu quero obter uma lista de ocorrências
  Para que eu possa visualizar todas as ocorrências registradas

  Scenario: Solicitar lista de ocorrências com sucesso
    Given que existem ocorrências cadastradas
    When eu faço uma solicitação de ocorrências GET para "/api/ocorrencia" com referência 0 e tamanho 10
    Then o status da resposta de ocorrências deve ser 200
    And o corpo da resposta de ocorrências deve conter uma lista de ocorrências
    And o corpo da resposta de ocorrências deve conter duas ocorrências

  Scenario: Solicitar lista de ocorrências com referência inválida
    Given que existem ocorrências cadastradas
    When eu faço uma solicitação de ocorrências GET para "/api/ocorrencia" com referência -1 e tamanho 10
    Then o status da resposta de ocorrências deve ser 400
    And o corpo da resposta de ocorrências deve conter uma mensagem de erro

  Scenario: Solicitar lista de ocorrências sem ocorrências cadastradas
    Given que não existem ocorrências cadastradas
    When eu faço uma solicitação de ocorrências GET para "/api/ocorrencia" com referência 0 e tamanho 10
    Then o status da resposta de ocorrências deve ser 204
    And o corpo da resposta de ocorrências deve estar vazio
