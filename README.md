
# Ocorrencia

Fiap.Web.Ocorrencia é uma aplicação web desenvolvida em .NET Core que permite o gerenciamento de ocorrências relacionadas a serviços de emergência, como polícia, bombeiros e ambulâncias. O sistema utiliza o Docker para containerização e pode ser facilmente configurado para rodar tanto em ambientes de desenvolvimento, homologação e produção.

## Requisitos

Antes de começar, certifique-se de ter os seguintes pré-requisitos instalados em sua máquina:

- [.NET SDK 8.0.x](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)

## Instruções para Inicialização do Projeto

### 1. Clonar o Repositório

Primeiro, clone este repositório para sua máquina local:

```bash
git clone https://github.com/JFDSantos/Ocorrencias-FIAP.git
cd Ocorrencias-FIAP
```

### 2. Inicie o repositório local e configure seu usuário

```bash
git init
git config --global user.email "seuemail@example.com"
git config --global user.name "Seu Nome"
```
### 3. Digite um comentário 
digite teste em HMA

### 3. Crie a branch staging
git checkout Homologacao

### 4. Suba o sistema para Homologacao
```bash
git add .
git commit -m "Subindo para HMA"
git push 
```
### 3. Volte a branch master
git switch master

### 3. Digite um comentário 
digite teste em PRD

### 4. Suba o sistema para homologação
```bash
git add .
git commit -m "Subindo para PRD"
git push 
```
