using System.Data.SqlClient;
using IndigoErp.Models;
using System.Data;
namespace IndigoErp.DAO
{
    public class EmployeeDAO
    {
        private SqlParameter[] CriaParametros(EmployeeModel model)
        {
            SqlParameter[] parametros = new SqlParameter[6] ;

            parametros[0] = new SqlParameter("nome", model.Nome);
            parametros[1] = new SqlParameter("login", model.Login);
            parametros[2] = new SqlParameter("senha", model.Senha);
            parametros[3] = new SqlParameter("funcao", model.Funcao);
            parametros[4] = new SqlParameter("turno", model.Setor);
            parametros[5] = new SqlParameter("cnpj", model.Cnpj);

            return parametros;
        }
        private EmployeeModel MontaSelect(DataRow origem)
        {
            EmployeeModel f = new EmployeeModel();

            f.Id = Convert.ToInt32(origem["ID"]);
            f.Nome = origem["NOME_DO_FUNCIONARIO"].ToString();
            f.Cnpj = origem["CNPJ_ORIGEM"].ToString();
            f.Login = origem["LOGIN_DO_FUNCIONARIO"].ToString();
            f.Senha = origem["SENHA_DO_FUNCIONARIO"].ToString();
            f.Funcao = origem["FUNCAO_DO_FUNCIONARIO"].ToString();
            f.Setor = origem["TURNO_DO_FUNCIONARIO"].ToString();

            return f;
        }

        public List<EmployeeModel> Consulta(string cnpj)
        {

            List<EmployeeModel> lista = new List<EmployeeModel>();

            string consulta = $"SELECT * FROM FUNCIONARIOS WHERE CNPJ_ORIGEM = {cnpj} ORDER BY NOME_DO_FUNCIONARIO ASC";

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow item in table.Rows)
            {

                lista.Add(MontaSelect(item));

            }

            return lista;

        }

        public void Inserir(EmployeeModel model)
        {

            string insercao = "INSERT INTO FUNCIONARIOS (CNPJ_ORIGEM,NOME_DO_FUNCIONARIO,LOGIN_DO_FUNCIONARIO,SENHA_DO_FUNCIONARIO,FUNCAO_DO_FUNCIONARIO,TURNO_DO_FUNCIONARIO) " +
                "VALUES (@cnpj,@nome,@login,@senha,@funcao,@turno)";

            GeneralDAO.ExecutaSql(insercao, CriaParametros(model));

        }

        public void Excluir(int id)
        {
            string deletar = "DELETE FUNCIONARIOS WHERE ID = " + id;

            GeneralDAO.ExecutaSql(deletar, null);

        }

        public EmployeeModel ConsultaPorId(int id)
        {

            string consulta = "SELECT * FROM FUNCIONARIOS WHERE ID = " + id;

            EmployeeModel model = new EmployeeModel();

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            if (table == null)
            {
                return null;
            }
            else
            {
                return MontaSelect(table.Rows[0]);
            }
        }

        public void Alterar(EmployeeModel model)
        {
            string alteracao = "UPDATE[FUNCIONARIOS]" +
                              " SET NOME_DO_FUNCIONARIO = @nome , " +
                              " LOGIN_DO_FUNCIONARIO = @login ," +
                              " SENHA_DO_FUNCIONARIO = @senha ," +
                              " FUNCAO_DO_FUNCIONARIO = @funcao, " +
                              " TURNO_DO_FUNCIONARIO = @turno " +
                              $" WHERE ID = {model.Id}";

            GeneralDAO.ExecutaSql(alteracao, CriaParametros(model));

        }




        public List<EmployeeModel> ConsultaPorFuncao(string funcao,string cnpj)
        {

            List<EmployeeModel> lista = new List<EmployeeModel>();

            string consulta = $"SELECT * FROM FUNCIONARIOS WHERE CNPJ_ORIGEM = {cnpj} AND FUNCAO_DO_FUNCIONARIO = " + funcao;

            DataTable table = GeneralDAO.SelectSql(consulta, null);

            foreach (DataRow item in table.Rows)
            {

                lista.Add(MontaSelect(item));

            }

            return lista;

        }
    }
}
