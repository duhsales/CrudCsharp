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
             #if WEB
                get { return "E:\\CrudCsharp\\CrudCsharp\\Crud\\bin\\Debug\\Banco\\"; }
            #else
                get { return Application.StartupPath + "\\Banco\\"; }
            #endif
        }

        public static void CriarDiretorios()
        {
            if (!Directory.Exists(Diretorios.Dir_Banco))
                Directory.CreateDirectory(Diretorios.Dir_Banco);
        }
    }
}
