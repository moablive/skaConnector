//System
using System;
using System.Diagnostics;
using System.Linq;
using IntegradorEscolaDev.Message;

//DLL
using SolidWorks.Interop.sldworks;

namespace IntegradorEscolaDev.SolidWorks
{
    public class SwAPI
    {
        //ARQUIVO ABERTO
        public ModelDoc2 swModelDoc;

        public SldWorks OpenSolidWorks(int Versao)
        {
            try
            {
                SldWorks swApp = Activator.CreateInstance(Type.GetTypeFromProgID(
                    $"SldWorks.Application.{Versao.ToString()}")
                ) as SldWorks;

                return swApp;
            }
            catch (Exception e)
            {
                throw new Exception($"{GlobalMessage.ErroVersaoSolidWorks} - {e.Message}\n{e.StackTrace}");
            }
        }
    }
}