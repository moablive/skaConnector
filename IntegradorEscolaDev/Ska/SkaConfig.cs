using System;
using System.Xml;
using IntegradorEscolaDev.Message;
using IntegradorEscolaDev.Models;

namespace IntegradorEscolaDev.Ska
{
    public class SkaConfig
    {
        //MODEL => SQLSERVER
        public static SqlServerConfigModel SqlServerConfigModel;

        //MODEL => PROPRIEDADES
        public static PropriedadesConfigModel PropriedadesConfigModel;

        public SkaConfig()
        {
            //SqlServerConfigModel
            SqlServerConfigModel = new SqlServerConfigModel();
            ConfigXMLSqlServer();

            //PropriedadesConfigModel
            PropriedadesConfigModel = new PropriedadesConfigModel();
            ConfigPropriedades();
        }

        //TODO VALIDAR SE PDM é OU NAO
        private string ValidaCaminhoPDM() => "C:\\SKACONNECTOR\\Config.xml";

        public void ConfigXMLSqlServer()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                //TODO VALIDAR SE PDM é OU NAO
                xmlDoc.Load(ValidaCaminhoPDM());
                
                //NODES DO XML
                XmlNodeList nodesSqlServer = xmlDoc.SelectNodes("/Configuration/SqlServer");

                if (nodesSqlServer.Count > 0)
                {
                    XmlNode XmlNode = nodesSqlServer[0];
                    SqlServerConfigModel.Server = XmlNode["Server"].InnerText;
                    SqlServerConfigModel.Database = XmlNode["Database"].InnerText;
                    SqlServerConfigModel.User = XmlNode["User"].InnerText;
                    SqlServerConfigModel.Password = XmlNode["Password"].InnerText;
                }
                else
                {
                    throw new Exception(GlobalMessage.ErroXMLSql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{GlobalMessage.ErroXMLinvalido} Detalhes: {ex.Message}");
            }
        }

        public void ConfigPropriedades()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                //TODO VALIDAR SE PDM é OU NAO
                xmlDoc.Load(ValidaCaminhoPDM());

                //NODES DO XML
                XmlNodeList nodesPropriedades = xmlDoc.SelectNodes("/Configuration/Propriedades");

                if (nodesPropriedades.Count > 0)
                {
                    XmlNode XmlNode = nodesPropriedades[0];
                    PropriedadesConfigModel.Codigo = XmlNode["Codigo"].InnerText;
                    PropriedadesConfigModel.Grupo = XmlNode["Grupo"].InnerText;
                    PropriedadesConfigModel.SubGrupo = XmlNode["SubGrupo"].InnerText;
                    PropriedadesConfigModel.Familia = XmlNode["Familia"].InnerText;
                    PropriedadesConfigModel.SubFamilia = XmlNode["SubFamilia"].InnerText;
                    PropriedadesConfigModel.Unidade = XmlNode["Unidade"].InnerText;
                    PropriedadesConfigModel.Descricao = XmlNode["Descricao"].InnerText;
                }
                else
                {
                    throw new Exception(GlobalMessage.ErroXMLPropriedade);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"{GlobalMessage.ErroXMLinvalido} Detalhes: {ex.Message}");
            }

        }
    }
}