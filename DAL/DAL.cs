using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Data;
using Util;

namespace Acesso
{
    public static class DAL
    {
        #region //Variáveis

        #if WEB
            private static string connString = "Data Source=E:\\CrudCsharp\\CrudCsharp\\Crud\\bin\\Debug\\Banco\\banco.sdf ;Password=duh.123;";
        #else
            private static string connString = "Data Source=" + Diretorios.Dir_Banco + "banco.sdf ;Password=duh.123;";
        #endif

        //Conexão do banco de dados
        private static SqlCeConnection conn;

        #endregion

        #region //Criando um Engine

        public static SqlCeEngine getEngine()
        {
            return new SqlCeEngine(connString);
        }

        #endregion

        #region //Criando Conexão

        private static void GetConnection()
        {
            if (conn == null)
            {
                conn = new SqlCeConnection(connString);
            }

            Open();
        }

        private static void Open()
        {
            if (conn.State.Equals(ConnectionState.Closed))
            {
                conn.Open();
            }
        }

        #endregion

        #region //Criando o DBParameter

        public static DbParameter CreateParameter(string ParameterName, object ParameterValue, DbType ParameterType)
        {
            DbParameter parameter = null;
            parameter = new SqlCeParameter();

            if (parameter != null)
            {
                parameter.ParameterName = ParameterName;
                parameter.Value = ParameterValue == null ? DBNull.Value : ParameterValue;
                parameter.DbType = ParameterType;
            }

            return parameter;
        }

        #endregion

        #region //Retorna o Command

        private static SqlCeCommand CreateCommand(SqlCeConnection conn, string sql, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            if (conn != null)
            {
                SqlCeCommand command = new SqlCeCommand(sql, conn);

                if (trans != null)
                    command.Transaction = trans;

                if (lstParametros != null)
                {
                    foreach (DbParameter parameter in lstParametros)
                        command.Parameters.Add(parameter);
                }

                return command;
            }
            else
            {
                throw new System.ArgumentException("Não foi possível abrir uma conexão");
            }
        }

        #endregion

        #region //Retorna Transação

        public static SqlCeTransaction GetTransacao()
        {
            GetConnection();
            return conn.BeginTransaction();
        }

        #endregion

        #region //Retorno DataTable

        //Método interno para retornar DataTable do banco de dados
        private static DataTable getTable(string sql, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            try
            {
                GetConnection();
                SqlCeDataAdapter dAd = new SqlCeDataAdapter();
                dAd.SelectCommand = CreateCommand(conn, sql, lstParametros, trans);
                DataSet dSet = new DataSet();

                dAd.Fill(dSet, "DADOS");
                return dSet.Tables["DADOS"] ?? new DataTable();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Retorna DataTable do banco de dados com transação.
        /// </summary>
        public static DataTable GetTable(string sql, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            return getTable(sql, lstParametros, trans);
        }

        /// <summary>
        /// Retorna DataTable do banco de dados sem transação.
        /// </summary>
        public static DataTable GetTable(string sql, List<DbParameter> lstParametros)
        {
            return getTable(sql, lstParametros, null);
        }

        /// <summary>
        /// Retorna DataTable do banco de dados sem transação e sem parametro.
        /// </summary>
        public static DataTable GetTable(string sql)
        {
            return getTable(sql, null, null);
        }

        #endregion

        #region //Retorno bool

        //Método interno para retornar bool do banco de dados
        private static bool getRun(string sql, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            try
            {
                GetConnection();
                return CreateCommand(conn, sql, lstParametros, trans).ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Retorna bool do banco de dados sem transação.
        /// </summary>
        public static bool GetRun(string sql, List<DbParameter> lstParametros)
        {
            try
            {
                return getRun(sql, lstParametros, null);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna bool do banco de dados com transação.
        /// </summary>
        public static bool GetRun(string sql, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            try
            {
                return getRun(sql, lstParametros, trans);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna bool do banco de dados sem transação e sem parametros.
        /// </summary>
        public static bool GetRun(string sql)
        {
            try
            {
                return getRun(sql, null, null);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region//Retorno int

        //Método interno para retornar int do banco de dados
        public static int getInt(string sql, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            try
            {
                GetConnection();
                return CreateCommand(conn, sql, lstParametros, trans).ExecuteScalar().ToInt();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Retorna int do banco de dados sem transação.
        /// </summary>
        public static int GetInt(string sql, List<DbParameter> lstParametros)
        {
            try
            {
                return getInt(sql, lstParametros, null);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna int do banco de dados sem transação.
        /// </summary>
        public static int GetInt(string sql)
        {
            try
            {
                return getInt(sql, null, null);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region //Retorna a SQL de update.

        private static string GetSQLUpdate(string strTable, string strWhere, List<DbParameter> lstParametro)
        {
            StringBuilder sbFields = new StringBuilder();

            foreach (DbParameter parameter in lstParametro)
            {
                sbFields.Append(sbFields.Length > 0 ? ", " : "");
                sbFields.Append(parameter.ParameterName);
                sbFields.Append("=");
                sbFields.Append("@");
                sbFields.Append(parameter.ParameterName);
            }

            return string.Format("UPDATE {0} SET {1} WHERE {2}", strTable, sbFields.ToString(), strWhere);
        }

        #endregion

        #region //Retorna a SQL de insert

        private static string GetSQLInsert(string strTable, List<DbParameter> lstParametros)
        {
            StringBuilder sbFields = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            foreach (DbParameter parameter in lstParametros)
            {
                sbFields.Append(sbValues.Length > 0 ? ", " : "");
                sbFields.Append(parameter.ParameterName);
                sbValues.Append(sbValues.Length > 0 ? ", " : "");
                sbValues.Append("@");
                sbValues.Append(parameter.ParameterName);
            }

            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", strTable, sbFields.ToString(), sbValues.ToString());
        }

        #endregion

        #region //Retorna a SQL de delete

        private static string GetSQLDelete(string strTable, string strWhere)
        {
            return string.Format("DELETE FROM {0} WHERE {1} ", strTable, strWhere);
        }

        #endregion

        #region //Update

        /// <summary>
        /// Atualiza os dados na tabela sem transação, OBS: Os nomes dos parametros do lstFields devem ser diferentes do lstWhere.
        /// </summary>
        public static bool Update(string strTable, string strWhere, List<DbParameter> lstFields, List<DbParameter> lstWhere, SqlCeTransaction trans)
        {
            try
            {
                GetConnection();

                SqlCeCommand Command = CreateCommand(conn, GetSQLUpdate(strTable, strWhere, lstFields), lstFields, trans);

                if (lstWhere != null)
                {
                    foreach (DbParameter parameter in lstWhere)
                        Command.Parameters.Add(parameter);
                }

                return Command.ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Atualiza os dados na tabela sem transação, OBS: Os nomes dos parametros do lstFields devem ser diferentes do lstWhere.
        /// </summary>
        public static bool Update(string strTable, string strWhere, List<DbParameter> lstFields, List<DbParameter> lstWhere)
        {
            try
            {
                GetConnection();

                SqlCeCommand Command = CreateCommand(conn, GetSQLUpdate(strTable, strWhere, lstFields), lstFields, null);

                if (lstWhere != null)
                {
                    foreach (DbParameter parameter in lstWhere)
                        Command.Parameters.Add(parameter);
                }

                return Command.ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region //Insert

        /// <summary>
        /// Insere informações na tabela.
        /// </summary>
        public static bool Insert(string strTable, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            try
            {
                GetConnection();

                return CreateCommand(conn, GetSQLInsert(strTable, lstParametros), lstParametros, trans).ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Insere informações na tabela.
        /// </summary>
        public static bool Insert(string strTable, List<DbParameter> lstParametros)
        {
            try
            {
                GetConnection();

                return CreateCommand(conn, GetSQLInsert(strTable, lstParametros), lstParametros, null).ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region //Delete

        /// <summary>
        /// Deleta os dados da tabela com transação.
        /// </summary>
        public static bool Delete(string strTable, string strWhere, List<DbParameter> lstParametros, SqlCeTransaction trans)
        {
            try
            {
                GetConnection();

                return CreateCommand(conn, GetSQLDelete(strTable, strWhere), lstParametros, trans).ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
      
        }

        /// <summary>
        /// Deleta os dados da tabela sem transação.
        /// </summary>
        public static bool Delete(string strTable, string strWhere, List<DbParameter> lstParametros)
        {
            try
            {
                GetConnection();

                return CreateCommand(conn, GetSQLDelete(strTable, strWhere), lstParametros, null).ExecuteNonQuery() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion
    }
}
