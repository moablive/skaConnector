//System
using System.IO;
using System.Windows.Forms;
using IntegradorEscolaDev.Message;

//DLL
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
namespace IntegradorEscolaDev.SolidWorks
{
    public class swFiles : SwAPI
    {
        //ABERTURA DE ARQUIVO E TIPAGEM
        public ModelDoc2 OpenFile(SldWorks swApp)
        {
            swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            
            //Sem Doc
            if (swModelDoc == null)
            {
                MessageBox.Show(GlobalMessage.ErroArquivo, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            //Somente Leitura
            if (swModelDoc.IsOpenedReadOnly())
            {
                MessageBox.Show(GlobalMessage.ErroSometeLeitura, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            //_TYPE
            swDocumentTypes_e _TYPE = (swDocumentTypes_e)swModelDoc.GetType();

            if (_TYPE == swDocumentTypes_e.swDocDRAWING)
            {
                MessageBox.Show(GlobalMessage.ErroArquivoNaoSuportado, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return swModelDoc;
        }

        //CHAPA METALLICA
        public bool VerifySheetMetal()
        {
            Feature feat = (Feature)swModelDoc.FirstFeature();

            while (feat != null)
            {
                if (feat.GetTypeName2().ToUpper() == "SHEETMETAL")
                {
                    return true;
                }

                feat = (Feature)feat.GetNextFeature();
            }

            return false;
        }

    }
}
