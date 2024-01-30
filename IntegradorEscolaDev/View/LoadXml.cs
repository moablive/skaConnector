using System.Windows.Forms;
using IntegradorEscolaDev.Ska;

namespace IntegradorEscolaDev.View
{
    internal class LoadXml
    {
        public static void Load(ComboBox comboUnidade, TextBox txtDescricao)
        {
            comboUnidade.Text = SkaConfig.PropriedadesConfigModel.Unidade;
            txtDescricao.Text = SkaConfig.PropriedadesConfigModel.Descricao;
        }
    }
}
