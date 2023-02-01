using Acesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    public class CriarTabelas
    {
        public static void T_USUARIO()
        {
            string sql = @"CREATE TABLE T_USUARIO
                                                (
                                                    CODIGO INT IDENTITY(1,1) PRIMARY KEY,
                                                    NOME NVARCHAR(500) NOT NULL,
                                                    CIDADE NVARCHAR(100) NOT NULL,
                                                    TELEFONE NVARCHAR(10) NULL
                                                )";

            DAL.GetRun(sql);
        }
    }
}
