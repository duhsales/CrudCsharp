using Acesso;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.Data.SqlServerCe;

namespace Banco
{
    public class CriarBanco
    {
        Result Result;

        //Criando banco de dados e tabelas
        public void Create()
        {
            try
            {
                Result = CreateDataBase();

                if (!Result.isResult())
                {
                    CreateTables();
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }

        //Criando o banco de dados
        private Result CreateDataBase()
        {
            Result = new Result();

            try
            {
                SqlCeEngine SqlEng = DAL.getEngine();
                Result.setResult(File.Exists(Diretorios.Dir_Banco + "banco.sdf"));

                if (!Result.isResult())
                {
                    SqlEng.CreateDatabase();
                }
            }
            catch
            {
                throw new System.ArgumentException("Erro ao criar o banco de dados.");
            }

            return Result;
        }

        //Criando as tabelas do banco de dados
        private void CreateTables()
        {
            Type aCLasses = Assembly.GetExecutingAssembly().GetTypes()[1];

            foreach (MethodInfo mtd in typeof(CriarTabelas).GetMethods())
            {
                try
                {
                    if (mtd.Name.Contains("T_"))
                    {
                        mtd.Invoke(aCLasses, null);
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
