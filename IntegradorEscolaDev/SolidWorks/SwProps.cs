using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System;
using IntegradorEscolaDev.Ska;

namespace IntegradorEscolaDev.SolidWorks
{
    public class SwProps : SwAPI
    {
        private ModelDocExtension swModelDocExt;
        private CustomPropertyManager swCustPropMgr;
       
        #region SolidWorks
        public string SolidWorksGetProp(SldWorks _swApp, ModelDoc2 _ModelDoc2, string propName)
        {
            //TODO VALIDAR USO DO RESULTADO ||   => return valor;
            string valor = "", resultado = "";

            try
            {
                swModelDocExt = _ModelDoc2.Extension;

                //PEÇA ATIVA E NOME DA CONFIGURAÇÃO ATIVA
                swCustPropMgr = swModelDocExt.get_CustomPropertyManager(_swApp.GetActiveConfigurationName(_ModelDoc2.GetPathName()));

                swCustPropMgr.Get4(
                    propName,
                    false,
                    out resultado,
                    out valor
                );

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valor;
        }

        public void SolidWorksUpdateProp(SldWorks _swApp, ModelDoc2 _ModelDoc2 , string propName, string propValue)
        {
            try
            {
                swModelDocExt = _ModelDoc2.Extension;

                //PEÇA ATIVA E NOME DA CONFIGURAÇÃO ATIVA
                swCustPropMgr = swModelDocExt.get_CustomPropertyManager(_swApp.GetActiveConfigurationName(_ModelDoc2.GetPathName()));

                swCustPropMgr.Add3(
                    propName,
                    (int)swCustomInfoType_e.swCustomInfoText,
                    propValue,
                    (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool SolidWorksSaveFile(ModelDoc2 _ModelDoc2)
        {
            int Errors = 0, Warnings = 0;

            try
            {
                return _ModelDoc2.Save3(
                    (int)swSaveAsOptions_e.swSaveAsOptions_Silent,
                    Errors,
                    Warnings
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
        #endregion

        #region SKA
        public string NomeArquivoNomeConfig(SldWorks _swApp, ModelDoc2 _ModelDoc2)
        {
            string _titulo = _ModelDoc2.GetTitle();
            string _propName = _swApp.GetActiveConfigurationName(_ModelDoc2.GetPathName());

            //Combine
            string nomeTipoCombinado = $"{_titulo} - {_propName}";

            return nomeTipoCombinado;
        }

        public void GetPropriedades(TextBox txtCodigo, ComboBox comboGrupo, ComboBox comboSubGrupo, ComboBox comboFamilia, ComboBox comboSubFamilia, ComboBox comboUnidade, TextBox txtDescricao, SldWorks _swApp, ModelDoc2 _ModelDoc2)
        {
            txtCodigo.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Codigo);
            comboGrupo.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Grupo);
            comboSubGrupo.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.SubGrupo);
            comboFamilia.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Familia);
            comboSubFamilia.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.SubFamilia);
            comboUnidade.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Unidade);
            txtDescricao.Text = SolidWorksGetProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Descricao);
        }

        public void UpdatePropriedades(TextBox txtCodigo, ComboBox comboGrupo, ComboBox comboSubGrupo, ComboBox comboFamilia, ComboBox comboSubFamilia, ComboBox comboUnidade, TextBox txtDescricao, SldWorks _swApp, ModelDoc2 _ModelDoc2)
        {
            SolidWorksUpdateProp(_swApp, _ModelDoc2,SkaConfig.PropriedadesConfigModel.Codigo, txtCodigo.Text);
            SolidWorksUpdateProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Grupo, comboGrupo.Text);
            SolidWorksUpdateProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.SubGrupo, comboSubGrupo.Text);
            SolidWorksUpdateProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Familia, comboFamilia.Text);
            SolidWorksUpdateProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.SubFamilia, comboSubFamilia.Text);
            SolidWorksUpdateProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Unidade, comboUnidade.Text);
            SolidWorksUpdateProp(_swApp, _ModelDoc2, SkaConfig.PropriedadesConfigModel.Descricao, txtDescricao.Text);
        }
        #endregion
    }
}
