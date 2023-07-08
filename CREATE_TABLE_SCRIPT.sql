CREATE TABLE Pesquisador (
	Id INT NOT NULL PRIMARY KEY,
	Nome VARCHAR(100) NOT NULL,
	CodigoInstituicao INT NOT NULL,
	Formacao VARCHAR(100) NOT NULL,
	Lattes VARCHAR(100) NOT NULL
);

CREATE TABLE ALUNO (
	ID INT NOT NULL PRIMARY KEY,
	NOME VARCHAR(100) NOT NULL,
	CODIGOINSTITUICAO INT NOT NULL,
	CURSO VARCHAR(100) NOT NULL
);

CREATE TABLE ESTABELECIMENTO(
	ID INT NOT NULL PRIMARY KEY,
	NOME VARCHAR(100) NOT NULL
);

CREATE TABLE PessoaEstabelecimento (
	Id INT NOT NULL PRIMARY KEY,
    PesquisadorId INT NULL,
	AlunoId INT NULL,
    EstabelecimentoId INT NOT NULL,
    FOREIGN KEY (PesquisadorId) REFERENCES PESQUISADOR (ID),
	FOREIGN KEY (AlunoId) REFERENCES ALUNO (ID),
	FOREIGN KEY (EstabelecimentoId) REFERENCES ESTABELECIMENTO (ID)
);

CREATE TABLE PROJETO (
	ID INT NOT NULL PRIMARY KEY,
	NOME VARCHAR(100) NOT NULL,
	AREA VARCHAR(100) NOT NULL,
	STATUS BOOLEAN NOT NULL
);

CREATE TABLE ProjetoPessoa (
    Id INT NOT NULL PRIMARY KEY,
    AlunoId INT NULL,
	PesquisadorId INT NULL,
	ProjetoId INT NOT NULL,
	FOREIGN KEY (PesquisadorId) REFERENCES PESQUISADOR (ID),
	FOREIGN KEY (AlunoId) REFERENCES ALUNO (ID),
	FOREIGN KEY (ProjetoId) REFERENCES PROJETO (ID)
);

CREATE TABLE ProjetoEstabelecimento (
    Id INT NOT NULL PRIMARY KEY,
	EstabelecimentoId INT NOT NULL,
	ProjetoId INT NOT NULL,
	FOREIGN KEY (EstabelecimentoId) REFERENCES ESTABELECIMENTO (ID),
	FOREIGN KEY (ProjetoId) REFERENCES PROJETO (ID)   
);

CREATE TABLE RESULTADO (
	ID INT NOT NULL PRIMARY KEY,
	INFORMACOES VARCHAR(100) NOT NULL,
	ProjetoId INT NOT NULL,
	FOREIGN KEY (ProjetoId) REFERENCES PROJETO (ID)
);


-- Inserindo dados na tabela Pesquisador
INSERT INTO Pesquisador (Id, Nome, CodigoInstituicao, Formacao, Lattes)
VALUES 
(1, 'André Adami', 100, 'Doutorado em Engenharia Elétrica', 'http://lattes.cnpq.br/5522739048962125'),
(2, 'André Martinotto', 101, 'Doutorado em Ciências dos Materiais', ' http://lattes.cnpq.br/0695249700868677'),
(3, 'Carine Webber', 102, 'Doutora em Ciência da Computação', 'http://lattes.cnpq.br/0887721217165252'),
(4, 'Daniel Faccin', 103, 'Especialista em Gestão Empresarial e em Sistemas de Informação', ' http://lattes.cnpq.br/6276942613011802'),
(5, 'Daniel Notari', 104, 'Doutorado em Biotecnologia', 'http://lattes.cnpq.br/0051814460033485'),
(6, 'Helena Ribeiro', 105, 'Doutora em Informática', 'http://lattes.cnpq.br/7373035136785578'),
(7, 'Maria de Fátima Lima', 106, 'Doutorado em Informática na Educação', ' http://lattes.cnpq.br/1565774983982811'),
(8, 'Ricardo Dorneles', 107, 'Doutorado em Computação', 'http://lattes.cnpq.br/5862460242840326'),
(9, 'Scheila de Ávila e Silva', 108, 'Doutorado em Biotecnologia', ' http://lattes.cnpq.br/7731423725040717'),
(10, 'Ricardo Oliveira', 109, 'Mestrado em Ciências da Computação', 'http://lattes.cnpq.br/ricardo_oliveira');

-- Inserindo dados na tabela Aluno
INSERT INTO Aluno (ID, NOME, CODIGOINSTITUICAO, CURSO)
VALUES 
(1, 'Luiz Souza', 100, 'Graduação em Biologia'),
(2, 'Carlos Ferreira', 101, 'Graduação em Física'),
(3, 'Juliana Santos', 102, 'Graduação em Química'),
(4, 'Lívia Mendes', 103, 'Graduação em Engenharia'),
(5, 'Renato Alves', 104, 'Graduação em História'),
(6, 'Tatiana Silva', 105, 'Graduação em Literatura'),
(7, 'Fábio Costa', 106, 'Graduação em Economia'),
(8, 'Rosana Freitas', 107, 'Graduação em Ciências da Computação'),
(9, 'Pedro Barbosa', 108, 'Graduação em Matemática'),
(10, 'Bianca Melo', 109, 'Graduação em Ciências');

-- Inserindo dados na tabela Estabelecimento
INSERT INTO Estabelecimento (ID, NOME)
VALUES 
(1, 'Universidade de Caxias do Sul'),
(2, 'Universidade de São Paulo'),
(3, 'Universidade Estadual de Campinas'),
(4, 'Universidade Federal de Minas Gerais'),
(5, 'Universidade Federal do Rio Grande do Sul'),
(6, 'Universidade de Brasília'),
(7, 'Universidade Federal de São Carlos'),
(8, 'Universidade Federal de Pernambuco'),
(9, 'Universidade Federal da Bahia'),
(10, 'Universidade Estadual Paulista');