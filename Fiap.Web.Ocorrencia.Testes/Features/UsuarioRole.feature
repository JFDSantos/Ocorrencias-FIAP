Feature: Gerenciamento de Roles de Usuário
  Como um usuário,
  Quero consultar as roles disponíveis
  Para garantir que estou recebendo informações corretas

  Scenario: Solicitar lista de roles com sucesso
    Given que existem roles cadastradas
    When eu faço uma solicitação de roles GET para "/api/usuariorole"
    Then o status da resposta de roles deve ser 200
    And o corpo da resposta de roles deve conter uma lista de roles
    And a lista de roles deve conter duas entradas

  Scenario: Solicitar lista de roles sem nenhum registro
    Given que não existem roles cadastradas
    When eu faço uma solicitação de roles GET para "/api/usuariorole"
    Then o status da resposta de roles deve ser 204
    And o corpo da resposta de roles deve estar vazio
