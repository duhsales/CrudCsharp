using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acesso;
using System.Data.Common;
using System.Data;

namespace DAO
{
    public class DAOCadastro : Helper
    {
        public int iCodigo { get; set; }
        public string strNome { get; set; }
        public string strCidade { get; set; }
        public string strTelefone { get; set; }

        public DAOCadastro(int iCodigo)
            : base("T_USUARIO")
        {
            this.iCodigo = iCodigo;

            UpdateData();
        }

        public DAOCadastro()
            : base("T_USUARIO")
        {

        }

        //Chave para a atualizar ou buscar as informações no banco de dados.
        public override string GetKeys()
        {
            return "CODIGO = @COD";
        }

        //Lista de parametro da chave para o update.
        public override List<DbParameter> GetKeyParams()
        {
            List<DbParameter> lstParametros = new List<DbParameter>();
            lstParametros.Add(DAL.CreateParameter("COD", iCodigo, DbType.Int32));

            return lstParametros;
        }

        //Busco informação do banco e atribuo na classe
        public override void UpdateData()
        {
            string sql = string.Format(@"SELECT * FROM T_USUARIO WHERE CODIGO = {0}", iCodigo);

            DataTable dt = DAL.GetTable(sql);

            if (dt.Rows.Count > 0)
            {
                strNome     = dt.Rows[0]["NOME"].ToString();
                strCidade   = dt.Rows[0]["CIDADE"].ToString();
                strTelefone = dt.Rows[0]["TELEFONE"].ToString();
            }
        }

        //Parametros para inserir no banco de dados
        public override List<DbParameter> GetValues()
        {
            List<DbParameter> lstParametros = new List<DbParameter>();
            lstParametros.Add(DAL.CreateParameter("NOME", string.IsNullOrEmpty(strNome) ? null : strNome, DbType.String));
            lstParametros.Add(DAL.CreateParameter("CIDADE", string.IsNullOrEmpty(strCidade) ? null : strCidade, DbType.String));
            lstParametros.Add(DAL.CreateParameter("TELEFONE", string.IsNullOrEmpty(strTelefone) ? null : strTelefone, DbType.String));

            return lstParametros;
        }
    }
}
