Feature: Gerenciamento de Veículos
  Como um usuário,
  Quero consultar e gerenciar os veículos disponíveis
  Para garantir que estou recebendo informações corretas e que a API se comporta conforme esperado

  Scenario: Solicitar lista de veículos com sucesso
    Given que existem veículos cadastrados
    When eu faço uma solicitação de veículos GET para "/api/veiculos"
    Then o status da resposta de veículos deve ser 200
    And o corpo da resposta de veículos deve conter uma lista de veículos
    And a lista de veículos deve conter duas entradas
    And o contrato da resposta de veículos deve estar em conformidade com o JSON Schema "veiculos-schema.json"

  Scenario: Solicitar lista de veículos sem nenhum registro
    Given que não existem veículos cadastrados
    When eu faço uma solicitação de veículos GET para "/api/veiculos"
    Then o status da resposta de veículos deve ser 204
    And o corpo da resposta de veículos deve estar vazio

  Scenario: Solicitar veículo com ID inválido
    Given que o ID do veículo é inválido
    When eu faço uma solicitação de veículos GET para "/api/veiculos/{id_invalido}" com o ID inválido
    Then o status da resposta de veículos deve ser 400
    And o corpo da resposta de veículos deve conter uma mensagem de erro
