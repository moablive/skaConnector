using System;
using System.Data.SqlClient;
using System.Data;
using IntegradorEscolaDev.Message;
using IntegradorEscolaDev.Models;
using IntegradorEscolaDev.Ska;

namespace IntegradorEscolaDev.Db
{
    public class SqlServer : IDisposable
    {
        private readonly string strConexao;
        private readonly SqlConnection conexaoDB;

        public SqlServer()
        {
            strConexao = $"Server={SkaConfig.SqlServerConfigModel.Server};Database={SkaConfig.SqlServerConfigModel.Database};User Id={SkaConfig.SqlServerConfigModel.User};Password={SkaConfig.SqlServerConfigModel.Password};";
            conexaoDB = new SqlConnection(strConexao);

            try
            {
                conexaoDB.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"{GlobalMessage.ErroSql} Detalhes: {ex.Message}");
            }
        }

        public DataTable SqlServerQuery(string SQL, SqlParameter[] parametros = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = new SqlCommand(SQL, conexaoDB))
                {
                    cmd.CommandTimeout = 0;

                    if (parametros != null)
                        cmd.Parameters.AddRange(parametros);

                    var leitor = cmd.ExecuteReader();
                    dt.Load(leitor);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{GlobalMessage.ErroConsultaSql} Detalhes: {ex.Message}");
            }

            return dt;
        }

        public void Dispose()
        {
            if (conexaoDB != null)
            {
                conexaoDB.Dispose();
            }
        }
    }
}