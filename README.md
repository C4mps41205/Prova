# Gerenciamento de Tarefas (To-Do List)

Este repositório contém a solução desenvolvida para a prova de vaga de desenvolvedor, conforme os requisitos especificados. A aplicação é um sistema de gerenciamento de tarefas que permite criar, ler, atualizar, deletar e categorizar tarefas, além de incluir um sistema de autenticação simples.

## Introdução

Esta aplicação foi desenvolvida para demonstrar competências em desenvolvimento full-stack, aplicando boas práticas de arquitetura de software e padrões de projeto. Utilizando Clean Architecture tanto no frontend quanto no backend, a solução garante modularidade, escalabilidade e facilidade de manutenção.


![image](https://github.com/user-attachments/assets/e37dc340-405f-4b29-b201-7ad570d4bd1c)


## Objetivo

Desenvolver uma aplicação de gerenciamento de tarefas (To-Do List) que permita:

- Criar, ler, atualizar e deletar tarefas.
- Categorizar tarefas.
- Autenticação de usuários para garantir segurança e personalização das tarefas. (Jwt)

## Design da Base de Dados

O banco de dados foi estruturado com três tabelas principais:

1. **Category**
   - `Id`: Identificador único.
   - `Name`: Nome da categoria.
   - `UserId`: Id do usuário.

2. **User**
   - `Id`: Identificador único.
   - `Username`: Nome de usuário.
   - `PasswordHash`: Hash da senha para autenticação segura.
   - `CreatedAt`: Data de criação.

3. **TaskToDo**
   - `Id`: Identificador único.
   - `Title`: Título da tarefa.
   - `Description`: Descrição detalhada da tarefa.
   - `IsCompleted`: Status de conclusão.
   - `CategoryId`: Chave estrangeira para a categoria.
   - `CreatedAt`: Data de criação.
   - `UpdatedAt`: Data da última atualização.
   - `UserId`: Chave estrangeira para o usuário proprietário.
     
![WhatsApp Image 2024-09-02 at 15 32 47](https://github.com/user-attachments/assets/d5fa23cc-33a4-4652-a1b1-17c39f4a193c)

## Padrões de Projeto Implementados

- **Clean Architecture**: Aplicada tanto no frontend quanto no backend para garantir uma separação clara entre as camadas da aplicação.
- **Adapter Pattern**: Utilizado para integrar diferentes componentes e facilitar a comunicação entre módulos.
- **Factory Pattern**: Implementado para a criação de instâncias de objetos complexos, promovendo flexibilidade e escalabilidade.

## Vídeo de demonstração



https://github.com/user-attachments/assets/1bdd3fde-4e0b-4217-9f1e-5c15fe087c93


