USE master;

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'INDIGODB')
BEGIN
    CREATE DATABASE INDIGODB;
END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'USUARIOS')
BEGIN
    CREATE TABLE USUARIOS (
        ID INT UNIQUE IDENTITY(1,1),
        EMAIL VARCHAR(MAX) NOT NULL,
        CNPJ VARCHAR(90) PRIMARY KEY NOT NULL,
        SENHA VARCHAR(MAX) NOT NULL,
		CODIGO_SEGURANCA VARCHAR(MAX) NULL
    );

	INSERT INTO USUARIOS (EMAIL,SENHA,CNPJ)
	VALUES ('1@1.com','1','1')

END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EQUIPAMENTO')
BEGIN
    CREATE TABLE EQUIPAMENTO (
        ID INT UNIQUE IDENTITY(1,1),
        CNPJ_DOMINIO VARCHAR(90),
	    SETOR VARCHAR(MAX) NOT NULL,
        MODELO VARCHAR(MAX) NOT NULL,
        MARCA VARCHAR(MAX) NOT NULL,
        NOME VARCHAR(MAX) NOT NULL,
        NUMERO_DE_SERIE VARCHAR(90) PRIMARY KEY NOT NULL,

		CONSTRAINT FK_USUARIOS_EQUIPAMENTOS FOREIGN KEY (CNPJ_DOMINIO) REFERENCES USUARIOS(CNPJ) 
    );
END;


IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SETORES')
BEGIN
    CREATE TABLE SETORES (
        ID INT UNIQUE IDENTITY(1,1),
        NOME VARCHAR(MAX) NOT NULL,
		CNPJ_DOMINIO VARCHAR(90),
		CONSTRAINT FK_SETORES_USUARIOS FOREIGN KEY (CNPJ_DOMINIO) REFERENCES USUARIOS(CNPJ) 
    );
    	INSERT INTO SETORES(NOME,CNPJ_DOMINIO)
		VALUES ('LINHA 1','1'),
			   ('LINHA 2','1'),
			   ('LINHA 3','1')
END;


IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'REPORTS')
BEGIN
    CREATE TABLE REPORTS (
        ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        NUMERO_EQUIPAMENTO VARCHAR(90) NOT NULL,
        ORIGEM_REPORT VARCHAR(12) NOT NULL,
        COMPONENTE_DE_FALHA VARCHAR(150) NOT NULL,
        DATA_FINAL DATE NULL,
        TIPO_DE_FALHA VARCHAR(MAX) NULL,
        DATA_DA_OCORRENCIA DATE NOT NULL,
        DESCRICAO VARCHAR(MAX) NULL,
        INICIO TIME(7) NOT NULL,
        FINAL TIME(7) NULL,
        STATUS VARCHAR(50) NOT NULL,

		CONSTRAINT FK_EQUIPAMENTOS_REPORTS FOREIGN KEY (NUMERO_EQUIPAMENTO) REFERENCES EQUIPAMENTO(NUMERO_DE_SERIE) 

    );
END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TIPO_DE_FALHA')
BEGIN
    CREATE TABLE TIPO_DE_FALHA (
        ID INT PRIMARY KEY NOT NULL,
        ORIGEM VARCHAR(MAX)NULL,
        NUMERO_EQUIPAMENTO VARCHAR(MAX) NOT NULL,
        COMPONENTE VARCHAR(MAX) NOT NULL,
        CAUSA_DA_FALHA VARCHAR(MAX) NOT NULL,
    );
END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'COMPONENTE_DE_FALHA_SELECAO')
BEGIN
    CREATE TABLE COMPONENTE_DE_FALHA_SELECAO (
        ID INT PRIMARY KEY NOT NULL,
        ORIGEM VARCHAR(MAX) NULL,
    );
END;

