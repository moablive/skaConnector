using IntegradorEscolaDev.Db;
using IntegradorEscolaDev.Message;
using IntegradorEscolaDev.Ska;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IntegradorEscolaDev.View
{
    public class LoadSqlQuery
    {
        public static void Load(ComboBox comboGrupo, ComboBox comboFamilia, SqlQuery sqlQuery)
        {
            //SQLSERVER
            List<string> _grupos = sqlQuery.GRUPO();
            List<string> _familias = sqlQuery.FAMILIA();

            //início da lista
            _grupos.Insert(0, GlobalMessage.ErroSelectGrupo);
            _familias.Insert(0, GlobalMessage.ErroSelectFamilia);

            // Adiciona os dados aos ComboBox
            comboGrupo.Items.AddRange(_grupos.ToArray());
            comboFamilia.Items.AddRange(_familias.ToArray());

            // Seleciona o primeiro item
            comboGrupo.SelectedIndex = 0;
            comboFamilia.SelectedIndex = 0;
        }
    }
}