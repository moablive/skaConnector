//System
using System;
using System.Collections.Generic;
using System.Windows.Forms;

//Projeto
using IntegradorEscolaDev.Db;
using IntegradorEscolaDev.Message;
using IntegradorEscolaDev.Ska;
using IntegradorEscolaDev.SolidWorks;
using IntegradorEscolaDev.View;

//DLL
using SolidWorks.Interop.sldworks;

namespace IntegradorEscolaDev
{
    public partial class InterfaceSW : UserControl
    {
        #region __CLASS
        //INIT => SkaConfig - CLASS
        SkaConfig _skaConfig = new SkaConfig();

        //SqlQuery - CLASS
        SqlQuery _sqlQuery = new SqlQuery();

        //SwAPI - CLASS
        SwAPI _swapi = new SwAPI();

        //swFiles - CLASS
        swFiles _swFiles = new swFiles();

        //swProps - CLASS
        SwProps _swProps = new SwProps();
        #endregion

        #region _VAR
        //SldWorks - Instance
        private SldWorks _swApp;

        //ModelDoc2 - Instance
        private ModelDoc2 _ModelDoc2;
        #endregion

        public InterfaceSW()
        {
            InitializeComponent();
        }

        #region INTERFACESW
        //btnArquivo
        private void btnArquivo_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        //btnSave
        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO VAlIDAR REGRA DE NEGOCIO
            if (comboGrupo.SelectedIndex == 0 && comboFamilia.SelectedIndex == 0)
            {
                // Caso não haja dados, exibe uma mensagem
                MessageBox.Show(GlobalMessage.ErroVerifique, GlobalMessage.ErroAviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Propriedades => __swProps.UpdatePropriedades
            _swProps.UpdatePropriedades(txtCodigo, comboGrupo, comboSubGrupo, comboFamilia, comboSubFamilia, comboUnidade, txtDescricao, _swApp, _ModelDoc2);
            _swProps.SolidWorksSaveFile(_ModelDoc2);
        }
        #endregion

        //INIT
        private void CarregarDados()
        {
            //SOLID 2023 => Instancia
            _swApp = _swapi.OpenSolidWorks(31);

            //Valida Arquivo e Tipo
            _ModelDoc2 = _swFiles.OpenFile(_swApp);
            if (_ModelDoc2 == null)
            {
                MessageBox.Show(GlobalMessage.ErroCarregar, GlobalMessage.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Propriedades => __swProps.GetPropriedades
            _swProps.GetPropriedades(txtCodigo, comboGrupo, comboSubGrupo, comboFamilia, comboSubFamilia, comboUnidade, txtDescricao, _swApp, _ModelDoc2);

            //LoadSqlQuery.Load
            LoadSqlQuery.Load(comboGrupo, comboFamilia, _sqlQuery);

            //TODO LoadXml => MOMENTO
            LoadXml.Load(comboUnidade, txtDescricao);

            //_swProps.NomeArquivoNomeConfig()
            txtArquivo.Text = _swProps.NomeArquivoNomeConfig(_swApp, _ModelDoc2);
        }

        #region ComboBox
        // SubGrupo
        private void comboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clean
            Clean.LimpaSubGrupo(comboGrupo, comboSubGrupo);

            if (comboGrupo.SelectedIndex > 0)
            {
                // Obtém o texto do item selecionado
                string grupoSelecionado = comboGrupo.SelectedItem.ToString();

                // FiltrarPrimeiroNumerico()
                string codigoNumerico = Clean.FiltrarPrimeiroNumerico(grupoSelecionado);

                // Chama o método para carregar os dados com base no grupo selecionado
                List<string> dadosDoSubGrupo = _sqlQuery.SUBGRUPO(codigoNumerico);

                if (dadosDoSubGrupo.Count > 0)
                {
                    // Adiciona os dados ao comboSubGrupo se houver algum
                    comboSubGrupo.Items.AddRange(dadosDoSubGrupo.ToArray());
                }
                else
                {
                    // Caso não haja dados, exibe uma mensagem
                    MessageBox.Show(GlobalMessage.ErroNaoSubGrupo, GlobalMessage.ErroAviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // SubFamilia
        private void comboFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clean
            Clean.LimpaSubFamilia(comboFamilia, comboSubFamilia);

            if (comboFamilia.SelectedIndex > 0)
            {
                // Obtém o texto do item selecionado
                string familiaSelecionado = comboFamilia.SelectedItem.ToString();

                // FiltrarPrimeiroNumerico()
                string codigoNumerico = Clean.FiltrarPrimeiroNumerico(familiaSelecionado);

                // Chama o método para carregar os dados com base na Família selecionada
                List<string> dadosDaSubFamilia = _sqlQuery.SUBFAMILIA(codigoNumerico);

                if (dadosDaSubFamilia.Count > 0)
                {
                    // Adiciona os dados ao comboSubFamilia se houver algum
                    comboSubFamilia.Items.AddRange(dadosDaSubFamilia.ToArray());
                }
                else
                {
                    // Caso não haja dados, exibe uma mensagem
                    MessageBox.Show(GlobalMessage.ErroNaoSubFamilia, GlobalMessage.ErroAviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
    }
}