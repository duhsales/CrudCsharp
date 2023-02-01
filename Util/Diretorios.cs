using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Util
{
    public static class Diretorios
    {
        //Diretório do Banco de dados
        public static string Dir_Banco
        {
            get { return Application.StartupPath + "\\Banco\\"; }
        }

        public static void CriarDiretorios()
        {
            if (!Directory.Exists(Diretorios.Dir_Banco))
                Directory.CreateDirectory(Diretorios.Dir_Banco);
        }
    }
}
