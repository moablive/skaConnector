//System
using System.Collections.Generic;
using System.Data;
using IntegradorEscolaDev.Models;


//Projeto
using IntegradorEscolaDev.Ska;

namespace IntegradorEscolaDev.Db
{
    public class SqlQuery
    {
        //class - SqlServer
        SqlServer _sqlServer = new SqlServer();
        
        //Caregar Dados
        public List<string> GRUPO()
        {
            DataTable DADOS = _sqlServer.SqlServerQuery("SELECT * FROM GRUPO");

            List<string> _GRUPOS = new List<string>();

            foreach (DataRow row in DADOS.Rows)
            {
                string CODIGO = row["CODIGO"].ToString();
                string DESCRICAO = row["DESCRICAO"].ToString();
                string ComboGRUPO = CODIGO + " - " + DESCRICAO;

                _GRUPOS.Add(ComboGRUPO);
            }

            return _GRUPOS;
        }

        //SelectedIndexChanged
        public List<string> SUBGRUPO(string codigoNumerico)
        {
            // Use o parâmetro na consulta SQL com uma cláusula WHERE
            DataTable DADOS = _sqlServer.SqlServerQuery($"SELECT * FROM SUBGRUPO WHERE GRUPO = '{codigoNumerico}'");

            List<string> _SUBGRUPO = new List<string>();

            foreach (DataRow row in DADOS.Rows)
            {
                string CODIGO = row["CODIGO"].ToString();
                string DESCRICAO = row["DESCRICAO"].ToString();
                string ComboGRUPO = CODIGO + " - " + DESCRICAO;

                _SUBGRUPO.Add(ComboGRUPO);
            }

            return _SUBGRUPO;
        }

        //Caregar Dados
        public List<string> FAMILIA()
        {
            DataTable DADOS = _sqlServer.SqlServerQuery("SELECT * FROM FAMILIA");

            List<string> _FAMILIA = new List<string>();

            foreach (DataRow row in DADOS.Rows)
            {
                string CODIGO = row["CODIGO"].ToString();
                string DESCRICAO = row["DESCRICAO"].ToString();
                string ComboFamilia = CODIGO + " - " + DESCRICAO;

                _FAMILIA.Add(ComboFamilia);
            }

            return _FAMILIA;
        }
        
        //SelectedIndexChanged
        public List<string> SUBFAMILIA(string codigoNumerico)
        {
            // Use o parâmetro na consulta SQL com uma cláusula WHERE
            DataTable DADOS = _sqlServer.SqlServerQuery($"SELECT * FROM SUBFAMILIA WHERE FAMILIA = '{codigoNumerico}'");

            List<string> _SUBFAMILIA = new List<string>();

            foreach (DataRow row in DADOS.Rows)
            {
                string CODIGO = row["CODIGO"].ToString();
                string DESCRICAO = row["DESCRICAO"].ToString();
                string ComboGRUPO = CODIGO + " - " + DESCRICAO;

                _SUBFAMILIA.Add(ComboGRUPO);
            }

            return _SUBFAMILIA;
        }
    }
}
