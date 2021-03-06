using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using URM.Data.Infrastructure;
using Library.Extensions;

namespace URM.Data
{
    public class SQLSupport
	{

        private static string strConn;
        static SQLSupport()
        {
            strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            //for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            //{
            //    if (SelectConnectionString(ConfigurationManager.ConnectionStrings[i].ConnectionString))
            //    {
            //        strConn = ConfigurationManager.ConnectionStrings[i].ConnectionString;
            //        break;
            //    }
            //}
        }

        private SqlTransaction transaction;
        private SqlConnection sqlConn;
        public SQLSupport()
        {
            sqlConn = new SqlConnection(strConn);
        }

        public string ConnectionString
        {
            get { return SQLSupport.strConn; }
        }

        /// <summary>
        ///     The unit of work.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSetProvider"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public SQLSupport(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.sqlConn = (SqlConnection)unitOfWork.Connection;
        }

        public void TransactionBegin()
        {
            if (sqlConn.State == ConnectionState.Closed) sqlConn.Open();
            transaction = sqlConn.BeginTransaction();
        }
        public void TransactionCommit()
        {
            transaction.Commit();
            sqlConn.Close();
        }
        public void TransactionRollback()
        {
            transaction.Rollback();
            sqlConn.Close();
        }

        private static bool SelectConnectionString(string ConnectionString)
        {
            SqlConnection sqlConn = new SqlConnection(ConnectionString);
            try
            {
                sqlConn.Open();
                sqlConn.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<T> ExecuteList<T>(string cmdText) where T : new()
        {
            var dataset = this.ExecuteDataset(cmdText);
            var data = dataset.Tables[0].ToList<T>();
            return data;
        }
        public List<T> ExecuteList<T>(CommandType cmt, string cmdText) where T : new()
        {
            var dataset = this.ExecuteDataset(cmt, cmdText);
            var data = dataset.Tables[0].ToList<T>();
            return data;
        }
        public List<T> ExecuteList<T>(CommandType cmt, string cmdText, params SqlParameter[] commandParameters) where T : new()
        {
            var dataSet = this.ExecuteDataset(cmt, cmdText, commandParameters);
            var data = dataSet.Tables[0].ToList<T>();
            return data;
        }

        public DataSet ExecuteDataset(string cmdText)
		{
            if (transaction != null) return SqlHelper.ExecuteDataset(transaction, CommandType.Text, cmdText);
            return SqlHelper.ExecuteDataset(sqlConn, CommandType.Text, cmdText);
		}
        public DataSet ExecuteDataset(SqlConnection sqlConn, string cmdText)
        {
            if (transaction != null) return SqlHelper.ExecuteDataset(transaction, CommandType.Text, cmdText);
            return SqlHelper.ExecuteDataset(sqlConn, CommandType.Text, cmdText);
        }
        public DataSet ExecuteDataset(CommandType cmt, string cmdText)
        {
            if (transaction != null) return SqlHelper.ExecuteDataset(transaction, cmt, cmdText);
            return SqlHelper.ExecuteDataset(sqlConn, cmt, cmdText);
        }
        public DataSet ExecuteDataset(CommandType cmt, string cmdText, params SqlParameter[] commandParameters)
		{
            if (transaction != null) return SqlHelper.ExecuteDataset(transaction, CommandType.Text, cmdText, commandParameters);
            return SqlHelper.ExecuteDataset(sqlConn, cmt, cmdText, commandParameters);
		}

        public int ExecuteNonQuery(CommandType cmt, string cmdText, params SqlParameter[] commandParameters)
		{
            if (transaction != null) return SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, cmdText, commandParameters);
			else return SqlHelper.ExecuteNonQuery(sqlConn, cmt, cmdText, commandParameters);
		}
        public int ExecuteNonQuery(string cmdText)
		{
            if (transaction != null) return SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, cmdText);
            else return SqlHelper.ExecuteNonQuery(sqlConn, CommandType.Text, cmdText);
		}

        public object ExecuteScalar(string cmdText)
		{
            if (transaction != null) return SqlHelper.ExecuteScalar(transaction, CommandType.Text, cmdText);
            else return SqlHelper.ExecuteScalar(sqlConn,CommandType.Text,cmdText);
		}
        public object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
		{
            if (transaction != null) return SqlHelper.ExecuteScalar(transaction, CommandType.Text, cmdText, commandParameters);
            else return SqlHelper.ExecuteScalar(sqlConn, CommandType.Text, cmdText, commandParameters);
		}

        public List<string> getPrimaryKey(string tableName)
        {
            StringBuilder SqlStr = new StringBuilder();
            SqlStr.Append("select c.column_NAME ");
            SqlStr.Append("from INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk , INFORMATION_SCHEMA.KEY_column_USAGE c ");
            SqlStr.Append("where pk.TABLE_NAME = '");
            SqlStr.Append(tableName);
            SqlStr.Append("' and CONSTRAINT_TYPE = 'PRIMARY KEY' and c.TABLE_NAME = pk.TABLE_NAME and c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME");
            DataTable table = SqlHelper.ExecuteDataset(sqlConn, CommandType.Text, SqlStr.ToString()).Tables[0];

            List<string> lst = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                lst.Add(row[0].ToString());
            }

            return lst;
        }
        public DataTable getColunm(string tableName)
        {
            StringBuilder SqlStr = new StringBuilder();
            SqlStr.Append("SELECT column_name 'ColumnName', data_type 'DataType', character_maximURM_length 'MaximURMLength' ");
            SqlStr.Append("FROM information_schema.Columns ");
            SqlStr.Append("WHERE table_name = '");
            SqlStr.Append(tableName);
            SqlStr.Append("'");
            return SqlHelper.ExecuteDataset(sqlConn, CommandType.Text, SqlStr.ToString()).Tables[0];
        }
        public object checkIdentitycolumn(string tableName, string ColumnName)
        {
            StringBuilder SqlStr = new StringBuilder();
            SqlStr.Append("SELECT count(name) FROM sys.identity_Columns where OBJECT_NAME(OBJECT_ID) = '");
            SqlStr.Append(tableName);
            SqlStr.Append("' and name = '");
            SqlStr.Append(ColumnName);
            SqlStr.Append("'");
            return SqlHelper.ExecuteScalar(sqlConn, CommandType.Text, SqlStr.ToString());
        }

        public Dictionary<string, DataRow> loadAssocList(string query, string filedName)
        {
            DataSet ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.Text, query);
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;
            if (ds.Tables[0].Rows.Count == 0) return null;
            //return ds.Tables[0];
            DataTable dt = ds.Tables[0];
            int column = dt.Columns.Count;
            int row = dt.Rows.Count;
            Dictionary<string, DataRow> list = new Dictionary<string, DataRow>();
            foreach (DataRow dr in dt.Rows)
                list.Add(dr[filedName].ToString(), dr);
            return list;
        }

        #region backup
        public static void Backup(string path, string databaseName)
        {
            SqlConnection sqlConn = new SqlConnection(strConn);//Chuỗi connection
            SQLSupport.BackupFull(sqlConn, path, databaseName);
            Thread Different = new Thread(delegate()
            {
                while (true)
                {
                    TimeSpan interval = new TimeSpan(5, 0, 0);
                    Thread.Sleep(interval);
                    SQLSupport.BackupDifferent(sqlConn, path, databaseName);
                }
            }
                );
            Different.Start();

            Thread Transaction = new Thread(delegate()
            {
                while (true)
                {
                    TimeSpan interval = new TimeSpan(0, 20, 0);
                    Thread.Sleep(interval);
                    SQLSupport.BackupTransaction(sqlConn, path, databaseName);
                }
            }
                );
            Transaction.Start();
        }
        public static void Backup(SqlConnection sqlConnection, string path, string databaseName)
        {
            SQLSupport.BackupFull(sqlConnection, path, databaseName);
            Thread Different = new Thread(delegate()
            {
                while (true)
                {
                    TimeSpan interval = new TimeSpan(5, 0, 0);
                    Thread.Sleep(interval);
                    SQLSupport.BackupDifferent(sqlConnection, path, databaseName);
                }
            }
                );
            Different.Start();

            Thread Transaction = new Thread(delegate() {
                                                            while (true)
                                                            {
                                                                TimeSpan interval = new TimeSpan(0, 20, 0);
                                                                Thread.Sleep(interval);
                                                                SQLSupport.BackupTransaction(sqlConnection, path, databaseName);
                                                            }
                                                        }
                                                            );
            Transaction.Start();
        }
        #endregion

        #region backup & restore
        public static void BackupFull(string strConnection, string strPath, string databaseName)
        {
            SqlConnection sqlConnection = new SqlConnection(strConnection);//Chuỗi connection
            sqlConnection.Open();
            try
            {
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP DATABASE [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_FULL.bak' WITH INIT");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), sqlConnection);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {
                //thong bao that bai
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static void BackupFull(SqlConnection conn, string strPath, string databaseName)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(strConnection);//Chuỗi connection
                conn.Open();
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP DATABASE [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_FULL.bak' WITH INIT");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void BackupDifferent(string strConnection, string strPath, string databaseName)
        {
            SqlConnection sqlConn = new SqlConnection(strConnection);//Chuỗi connection
            sqlConn.Open();
            try
            {
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP DATABASE [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_DIFF.bak' WITH INIT, DIFFERENTIAL");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), sqlConn);
                cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (SqlException ex)
            {
                //thong bao that bai
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        public static void BackupDifferent(SqlConnection conn, string strPath, string databaseName)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(strConnection);//Chuỗi connection
                conn.Open();
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP DATABASE [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_DIFF.bak' WITH INIT, DIFFERENTIAL");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void BackupTransaction(string strConnection, string strPath, string databaseName)
        {
            SqlConnection sqlConn = new SqlConnection(strConnection);//Chuỗi connection
            sqlConn.Open();
            try
            {
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP LOG [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_TRAN.trn' WITH INIT");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), sqlConn);
                cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (SqlException ex)
            {
                //thong bao that bai
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        public static void BackupTransaction(SqlConnection conn, string strPath, string databaseName)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(strConnection);//Chuỗi connection
                conn.Open();
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP LOG [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_TRAN.trn' WITH INIT");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void BackupFinal(string strConnection, string strPath, string databaseName)
        {
            SqlConnection sqlConn = new SqlConnection(strConnection);//Chuỗi connection
            sqlConn.Open();
            try
            {
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP LOG [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_TRAN.trn'");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), sqlConn);
                cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (SqlException ex)
            {
                //thong bao that bai
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        public static void BackupFinal(SqlConnection conn, string strPath, string databaseName)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(strConnection);//Chuỗi connection
                conn.Open();
                //dattabase Name: Tên database mà mình cần backup
                //strPath : Đường dẫn lưu trữ file mà mình backup
                StringBuilder strBackup = new StringBuilder();
                strBackup.AppendLine("use master");
                strBackup.Append("BACKUP LOG [");
                strBackup.Append(databaseName);
                strBackup.Append("] TO DISK = '");
                strBackup.Append(strPath);
                strBackup.Append("_TRAN.trn'");
                SqlCommand cmd = new SqlCommand(strBackup.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void RestoreFull(string strConnection, string strPath, string databaseName)
        {
            SqlConnection sqlConn = new SqlConnection(strConnection);//Chuỗi  connection
            sqlConn.Open();
            StringBuilder sqlRestore = new StringBuilder();
            sqlRestore.AppendLine("use master");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_FULL' WITH NORECOVERY");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_DIFF' WITH NORECOVERY");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_TRAN.trn' WITH FILE = 1, NORECOVERY");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_TRAN.trn' WITH FILE = 2");
            sqlRestore.Append("use ");
            sqlRestore.Append(databaseName);
            SqlCommand cmd = new SqlCommand(sqlRestore.ToString(), sqlConn);
            cmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        public static void RestoreFull(SqlConnection conn, string strPath, string databaseName)
        {
            //SqlConnection conn = new SqlConnection(strConnection);//Chuỗi  connection
            conn.Open();
            StringBuilder sqlRestore = new StringBuilder();
            sqlRestore.AppendLine("use master");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_FULL' WITH NORECOVERY");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_DIFF' WITH NORECOVERY");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_TRAN.trn' WITH FILE = 1, NORECOVERY");
            sqlRestore.Append("RESTORE DATABASE [");
            sqlRestore.Append(databaseName);
            sqlRestore.Append("] FROM DISK = '");
            sqlRestore.Append(strPath);
            sqlRestore.AppendLine("_TRAN.trn' WITH FILE = 2");
            sqlRestore.Append("use ");
            sqlRestore.Append(databaseName);
            SqlCommand cmd = new SqlCommand(sqlRestore.ToString(), conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion
	}
}
