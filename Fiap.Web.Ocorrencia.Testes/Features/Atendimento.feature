Feature: AtendimentoFeature
  Como um usuário do sistema
  Eu quero buscar uma lista de atendimentos
  Para que eu possa ver quais atendimentos estão disponíveis

  Scenario: Retornar uma lista de atendimentos com sucesso
    Given que existem atendimentos cadastrados
    When eu solicito a lista de atendimentos
    Then eu recebo uma resposta de sucesso com a lista de atendimentos
    And a lista contém 2 atendimentos

  Scenario: Retornar uma resposta de não encontrado quando não há atendimentos
    Given que não existem atendimentos cadastrados
    When eu solicito a lista de atendimentos
    Then eu recebo uma resposta de não encontrado

  Scenario: Retornar um erro ao solicitar uma lista de atendimentos com parâmetros inválidos
    Given que existem atendimentos cadastrados
    When eu solicito a lista de atendimentos com referência -1 e tamanho -10
    Then eu recebo uma resposta de erro
