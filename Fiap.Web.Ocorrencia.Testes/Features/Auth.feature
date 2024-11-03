Feature: AuthFeature
  Como usuário do sistema
  Eu quero fazer login
  Para acessar recursos protegidos

  Scenario: Login com sucesso retorna um token JWT válido
    Given que o usuário possui credenciais válidas
    When eu realizo o login
    Then eu recebo uma resposta de sucesso com um token JWT válido

  Scenario: Login com falha retorna não autorizado
    Given que o usuário possui credenciais inválidas
    When eu realizo o login
    Then eu recebo uma resposta de não autorizado
