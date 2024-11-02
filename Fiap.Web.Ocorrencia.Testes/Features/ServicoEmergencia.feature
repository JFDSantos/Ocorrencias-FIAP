Feature: Serviço de Emergência
  Como um usuário,
  Quero consultar os serviços de emergência disponíveis
  Para garantir que estou recebendo informações corretas

  Scenario: Solicitar lista de serviços de emergência com sucesso
    Given que existem serviços de emergência cadastrados
    When eu faço uma solicitação de serviços de emergência GET para "/api/servicoemergencia"
    Then o status da resposta de serviços de emergência deve ser 200
    And o corpo da resposta de serviços de emergência deve conter uma lista de serviços de emergência
    And a lista de serviços de emergência deve conter duas entradas

  Scenario: Solicitar lista de serviços de emergência sem nenhum registro
    Given que não existem serviços de emergência cadastrados
    When eu faço uma solicitação de serviços de emergência GET para "/api/servicoemergencia"
    Then o status da resposta de serviços de emergência deve ser 204
    And o corpo da resposta de serviços de emergência deve estar vazio

  Scenario: Solicitar serviço de emergência com ID inválido
    Given que o ID do serviço de emergência é inválido
    When eu faço uma solicitação de serviços de emergência GET para "/api/servicoemergencia/{id_invalido}" com o ID inválido
    Then o status da resposta de serviços de emergência deve ser 400
    And o corpo da resposta de serviços de emergência deve conter uma mensagem de erro
