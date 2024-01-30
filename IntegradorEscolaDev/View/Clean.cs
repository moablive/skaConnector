using System.Linq;
using System.Windows.Forms;
using IntegradorEscolaDev.Message;

namespace IntegradorEscolaDev.View
{
    internal class Clean
    {
        public static void LimpaSubGrupo(ComboBox comboGrupo, ComboBox comboSubGrupo)
        {
            comboSubGrupo.Items.Clear();
            comboSubGrupo.Text = string.Empty;

            if (comboGrupo.SelectedIndex > 0)
            {
                // Adiciona "Selecione um Sub Grupo" na posição 0
                comboSubGrupo.Items.Insert(0, GlobalMessage.ErroSelectSubGrupo);
                comboSubGrupo.SelectedIndex = 0;
            }
        }

        public static void LimpaSubFamilia(ComboBox comboFamilia, ComboBox comboSubFamilia)
        {
            comboSubFamilia.Items.Clear();
            comboSubFamilia.Text = string.Empty;

            if (comboFamilia.SelectedIndex > 0)
            {
                // Adiciona "Selecione um Sub Família" na posição 0
                comboSubFamilia.Items.Insert(0, GlobalMessage.ErroSelectSubFamilia);
                comboSubFamilia.SelectedIndex = 0;
            }
        }

        public static string FiltrarPrimeiroNumerico(string input)
        {
            // Filtra os caracteres não numéricos
            string resultadoNumerico = new string(input.Where(char.IsDigit).ToArray());

            // Retorna apenas o primeiro caractere numérico encontrado
            if (!string.IsNullOrEmpty(resultadoNumerico))
            {
                return resultadoNumerico[0].ToString();
            }

            return string.Empty;
        }
    }
}
