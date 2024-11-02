Feature: Serviço de Usuários
  Como um usuário,
  Quero consultar os usuários disponíveis
  Para garantir que estou recebendo informações corretas

  Scenario: Solicitar lista de usuários com sucesso
    Given que existem usuários cadastrados
    When eu faço uma solicitação de usuários GET para "/api/usuario"
    Then o status da resposta de usuários deve ser 200
    And o corpo da resposta de usuários deve conter uma lista de usuários
    And a lista de usuários deve conter duas entradas

  Scenario: Solicitar lista de usuários sem nenhum registro
    Given que não existem usuários cadastrados
    When eu faço uma solicitação de usuários GET para "/api/usuario"
    When eu faço uma solicitação de usuários GET para "/api/usuario"
    Then o status da resposta de usuários deve ser 204
    And o corpo da resposta de usuários deve estar vazio

  Scenario: Solicitar usuário com ID inválido
    Given que o ID do usuário é inválido
    When eu faço uma solicitação de usuários GET para "/api/usuario/{id_invalido}" com o ID inválido
    Then o status da resposta de usuários deve ser 400
    And o corpo da resposta de usuários deve conter uma mensagem de erro
