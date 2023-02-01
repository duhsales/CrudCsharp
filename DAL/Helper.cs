using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acesso
{
    public abstract class Helper
    {
        //Variáveis.
        protected string strNomeTabela;
        private bool IsListCommand = false;

        //Construtor.
        public Helper(string strNomeTabela)
        {
            this.strNomeTabela = strNomeTabela;
        }

        //Atualiza as informações da tabela.
        public abstract void UpdateData();

        //Gravando informações na tabela.
        public virtual void Save()
        {
            try
            {
                if (!IsListCommand)
                {
                    if (IsExists())
                        DAL.Update(strNomeTabela, GetKeys(), GetValues(), GetKeyParams());
                    else
                        DAL.Insert(strNomeTabela, GetValues());
                }
            }
            catch
            {
                throw new Exception("Erro ao salvar a informação no banco de dados.");
            }
        }

        //Retorna a chave da tabela.
        public abstract string GetKeys();

        //Retorna os parametos.
        public abstract List<DbParameter> GetKeyParams();

        //Retorna os valores a serem atualizados ou inseridos.
        public abstract List<DbParameter> GetValues();

        //Verificando se existe o registro na tabela.
        public bool IsExists()
        {
            string sql = string.Format("SELECT TOP(1) 1 FROM {0} WHERE {1}", strNomeTabela, GetKeys());
            return DAL.GetInt(sql, GetKeyParams()) > 0 ? true : false;
        }

        //Apagando informações da tabela.
        public virtual void Delete()
        {
            DAL.Delete(strNomeTabela, GetKeys(), GetKeyParams());
        }

        public virtual void Delete(System.Data.SqlServerCe.SqlCeTransaction trans)
        {
            DAL.Delete(strNomeTabela, GetKeys(), GetKeyParams(), trans);
        }

        public void beginListCommand()
        {
            IsListCommand = true;
        }

        public void endListCommand()
        {
            IsListCommand = false;
            Save();
        }
    }
}
