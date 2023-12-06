
CREATE PROCEDURE SPLISTAGEM(
@TAB NVARCHAR(MAX),
@COLUNA NVARCHAR(MAX),
@FILTRO NVARCHAR(MAX),
@ORDEM NVARCHAR(MAX)

)
AS
BEGIN
   DECLARE @SQL NVARCHAR(MAX)
   IF @FILTRO IS NULL
      SET @SQL = ' SELECT ' + @COLUNA + ' FROM ' + @TAB;
   ELSE
      SET @SQL = ' SELECT ' + @COLUNA + ' FROM ' + @TAB + ' ORDER BY ' + @FILTRO + ' ' + @ORDEM ;
   EXEC(@SQL)
END
GO

CREATE PROCEDURE SPCONSULTA(

@TABELA NVARCHAR(MAX),
@COLUNA NVARCHAR(MAX),
@ID NVARCHAR(MAX)

)
AS
BEGIN 
   DECLARE @SQL NVARCHAR(MAX)
   IF @COLUNA IS NULL
      SET @SQL = 'SELECT * FROM ' + @TABELA;
   ELSE
       SET @SQL = 'SELECT * FROM ' + @TABELA + ' WHERE ' + @COLUNA + ' = ''' + @ID + '''';
   
   
   EXEC(@SQL)
END
GO


CREATE PROCEDURE SPDELETE (

@TABELA VARCHAR(MAX),
@ID VARCHAR(MAX)

)
AS
BEGIN

DECLARE @SQL VARCHAR(MAX)

SET @SQL = ' DELETE '+ @TABELA +' WHERE ID =  '+ @ID;

EXEC(@SQL)
END
GO
 