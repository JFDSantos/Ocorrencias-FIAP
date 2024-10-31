Feature: Localizacao Management
    Como um usuário do sistema
    Eu quero gerenciar localizações
    Para que eu possa adicionar, atualizar, buscar e deletar localizações

    Scenario: Get all localizacoes
        Given existe uma lista de localizações
        When eu solicito a lista de localizações
        Then eu recebo uma resposta de sucesso com a lista de localizações

    Scenario: Get localizacao by id
        Given uma localização com id 1 existe
        When eu solicito a localização com id 1
        Then eu recebo uma resposta de sucesso com a localização

    Scenario: Post a new localizacao
        Given um novo modelo de localização válido
        When eu adiciono a nova localização
        Then eu recebo uma resposta de criação com a nova localização

    Scenario: Put an existing localizacao
        Given uma localização existente com id 1
        When eu atualizo a localização com id 1
        Then eu recebo uma resposta sem conteúdo

    Scenario: Delete an existing localizacao
        Given uma localização existente com id 1
        When eu deleto a localização com id 1
        Then eu recebo uma resposta sem conteúdo
