USE master;

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'INDIGODB')
BEGIN
    CREATE DATABASE INDIGODB;
END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'USUARIOS')
BEGIN
    CREATE TABLE USUARIOS (
        ID INT UNIQUE IDENTITY(1,1),
        EMAIL VARCHAR(100) NOT NULL,
        CNPJ VARCHAR(100) PRIMARY KEY NOT NULL,
        SENHA VARCHAR(1000) NOT NULL,
		CODIGO_SEGURANCA VARCHAR(10) NULL
    );

	INSERT INTO USUARIOS (EMAIL,SENHA,CNPJ)
	VALUES ('1@1.com','1','1')

END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EQUIPAMENTO')
BEGIN
    CREATE TABLE EQUIPAMENTO (
        ID INT UNIQUE IDENTITY(1,1),
        CNPJ_DOMINIO VARCHAR(100),
	    SETOR VARCHAR(100) NOT NULL,
        NOME VARCHAR(100) NOT NULL,
        NUMERO_DE_SERIE VARCHAR(100) PRIMARY KEY NOT NULL,

		CONSTRAINT FK_USUARIOS_EQUIPAMENTOS FOREIGN KEY (CNPJ_DOMINIO) REFERENCES USUARIOS(CNPJ) 
    );
END;


IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SETORES')
BEGIN
    CREATE TABLE SETORES (
        ID INT UNIQUE IDENTITY(1,1),
        NOME VARCHAR(100) NOT NULL,
		CNPJ_DOMINIO VARCHAR(100),
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
        NUMERO_EQUIPAMENTO VARCHAR(100) NOT NULL,
        ORIGEM_REPORT VARCHAR(12) NOT NULL,
        COMPONENTE_DE_FALHA VARCHAR(150) NOT NULL,
        DATA_FINAL DATE NULL,
        TIPO_DE_FALHA VARCHAR(150) NULL,
        DATA_DA_OCORRENCIA DATE NOT NULL,
        DESCRICAO VARCHAR(144) NULL,
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
        ORIGEM VARCHAR(30) NULL,
        NUMERO_EQUIPAMENTO VARCHAR(100) NOT NULL,
        COMPONENTE VARCHAR(100) NOT NULL,
        CAUSA_DA_FALHA VARCHAR(100) NOT NULL,
    );
END;

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'COMPONENTE_DE_FALHA_SELECAO')
BEGIN
    CREATE TABLE COMPONENTE_DE_FALHA_SELECAO (
        ID INT PRIMARY KEY NOT NULL,
        ORIGEM VARCHAR(30) NULL,
    );
END;

