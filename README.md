
# Projeto Ouvidoria #

Este é um projeto elaborado por mim Eduardo Alves, como teste de proficiencia na linguagem C#.net Entity Framework de nível básico.

Foi feito por camadas MVC - Model View Control para melhor organização do código e futuras manutenções

De acordo com o que pude estudar e aprender para realização dessa tarefa, para rodar o Projeto é necessário ter o Visual Studio Community e o Sql Server Express, ambos que podem ser obtidos gratuitamente nos links a seguir: 

Visual Studio Cummunity (gratuito)
https://visualstudio.microsoft.com/pt-br/downloads/

SQL Server Express (Versão gratuita)
https://www.microsoft.com/pt-br/sql-server/sql-server-downloads

Inicialmente, é necessário para que o projeto se comunique com o banco, criar o mesmo de nome Manifesto com a tabela dbo.Manifestos, utilizando as instâncias no Banco de dados que foram feitas de acordo com o Solution na Pasta /Models/Manifesto.cs do projeto, lá existe o tratamento e definição de cada tipo.

Caso necessário, de acordo com as configurações do seu Visual Studio, haverá necessidade da instalação de Referencias pelo instalador de pacotes nugget:
entity framowork
pagedList.mvc
bootstrap
mysql.data.entity


Também foi instalado para controle de versionamento o github for visual studio ( não Obrigatório )


Na opção de resposta por email utilizei o smtp do gmail, o que pode ser alterado, entretanto, para funcionar deve-se mudar a configuração em settings do g-mail
para acessos de App menos seguros, já que o programa acessa via smtp. 
Feito por esse link:
https://www.google.com/settings/security/lesssecureapps

Achei essa opção mais interessante do que deixar o e-mail do Ouvidor logado no fonte como diversos exemplos que encontrei pela internet.


* O Arquivo a ser enviado deve estar na pasta ISS Express caso use o google Chrome. 
