using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;
using System.Data;
using Acesso;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace Regra
{
    public class RegraCadastro : DAOCadastro
    {
        private string strPesquisa { get; set; }
        private string strPesquisaCidade { get; set; }

        public RegraCadastro(int iCodigo) : base(iCodigo)
        {

        }

        public RegraCadastro() : base()
        {

        }

        public void setPesquisa(string str)
        {
            this.strPesquisa = str;
        }

        public void setPesquisaCidade(string str)
        {
            this.strPesquisaCidade = str;
        }

        public void setNome(string str)
        {
            base.strNome = str;
        }

        public void setTelefone(string str)
        {
            base.strTelefone = str;
        }

        public void setCidade(string str)
        {
            base.strCidade = str;
        }

        public string getNome()
        {
            return base.strNome;
        }

        public string getTelefone()
        {
            return base.strTelefone;
        }

        public string getCidade()
        {
            return base.strCidade;
        }

        public int getCodigo()
        {
            return base.iCodigo;
        }


        public DataTable getCadastros()
        {
            string sql = @"SELECT CODIGO, 
                                  NOME, 
                                  CIDADE, 
                                  TELEFONE 
                           FROM T_USUARIO 
                           WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(strPesquisa))
            {
                sql += " AND (CAST(CODIGO AS NVARCHAR) = @CODIGO OR NOME LIKE @NOME) ";
            }

            if (!string.IsNullOrEmpty(strPesquisaCidade))
            {
                sql += " AND CIDADE LIKE @CIDADE ";
            }

            List<DbParameter> lstParametros = new List<DbParameter>();
            lstParametros.Add(DAL.CreateParameter("NOME", "%" + strPesquisa + "%", DbType.String));
            lstParametros.Add(DAL.CreateParameter("CODIGO", strPesquisa, DbType.String));
            lstParametros.Add(DAL.CreateParameter("CIDADE", "%" + strPesquisaCidade + "%", DbType.String));

            return DAL.GetTable(sql, lstParametros);
        }


        public DataTable getCidades()
        {
            string sql = @"SELECT '' AS CIDADE
                            UNION
                           SELECT DISTINCT CIDADE 
                           FROM T_USUARIO ";

            return DAL.GetTable(sql);
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(getNome()))
            {
                throw new Exception("O nome é obrigatório.");
            }

            if (string.IsNullOrEmpty(getCidade()))
            {
                throw new Exception("A cidade é obrigatória.");
            }

            base.Save();
        }

        public override void Delete()
        {
            SqlCeTransaction trans = DAL.GetTransacao();
            try
            {
                base.Delete(trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();

                throw new Exception(ex.Message);
            }
        }
    }
}
