using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DB
{
    public class Mssql
    {
        public enum ExcuteResult { Fail = -2, Success };

        public string ConnectionString = string.Empty;
        public string InitFileName { get; set; }

        public string Address { get; private set; }
        public string Port { get; private set; }
        public string LastException { get; private set; }

        public bool IsRunning { get { return Connection.State == ConnectionState.Open ? true : false; } }

        public SqlConnection Connection { get; private set; }

        private static Mssql instance;

        public static Mssql Instance
        {
            get
            {
                if (instance == null) instance = new Mssql();

                return instance;
            }
        }

        private SqlCommand? _sqlCmd = null;

        public Mssql()
        {
            _sqlCmd = new SqlCommand();
            CheckConnection();
        }

        public bool GetConnection()
        {
            try
            {
                if (ConnectionString == string.Empty)
                    SetConnectionString();

                Connection = new SqlConnection(ConnectionString);

                Connection.Open();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage : {1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                return false;
            }

            if (Connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        public int ExecuteNonQuery(string query)
        {
            lock (this)
            {
                return Execute_NonQuery(query);
            }
        }

        public bool HasRows(string query)
        {
            lock (this)
            {
                SqlDataReader result = ExecuteReader(query);
                bool _b = result.HasRows;
                result.Close();
                return _b;
            }
        }

        public SqlDataReader ExecuteReaderQuery(string query)
        {
            lock (this)
            {
                SqlDataReader result = ExecuteReader(query);

                return result;
            }
        }

        public DataSet ExecuteDsQuery(DataSet ds, string query)
        {
            ds.Reset();

            lock (this)
            {
                //dbLoger.WriteLog(LogType.Inform, string.Format("ExecuteDsQuery - {0}", query));

                return ExecuteDataAdt(ds, query);
            }
        }

        public DataSet ExecuteProcedure(DataSet ds, string procName, params string[] pValues)
        {
            lock (this)
            {
                return ExecuteProcedureAdt(ds, procName, pValues);
            }
        }

        public void CancelQuery()
        {
            _sqlCmd.Cancel();
        }

        public void Close()
        {
            Connection.Close();
        }

        #region private


        private bool CheckConnection()
        {
            bool result = true;

            if (Network.Network.Connected == false)
            {
                this.LastException = "네트워크 연결이 끊어졌습니다.";
                result = false;
            }
            else if (this.Connection == null || this.Connection.State == ConnectionState.Closed)
            {
                result = this.GetConnection();
            }

            return result;
        }

        private void SetConnectionString()
        {
            DBInfo info = new DBInfo();

            string dataSource = $"Data Source={info.addr01};Database={info.svr};User Id={info.user};Password={info.pwd}";

            this.Address = info.addr01;
            this.ConnectionString = dataSource;
            
        }

        private int Execute_NonQuery(string query)
        {
            int result = (int)ExcuteResult.Fail;

            try
            {
                _sqlCmd = new SqlCommand();
                _sqlCmd.Connection = this.Connection;
                _sqlCmd.CommandText = query;
                result = _sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage : {1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return result;
            }

            return result;
        }

        private SqlDataReader ExecuteReader(string query)
        {
            SqlDataReader result = null;

            try
            {
                _sqlCmd = new SqlCommand();
                _sqlCmd.Connection = this.Connection;
                _sqlCmd.CommandText = query;
                result = _sqlCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage : {1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return result;
            }

            return result;
        }

        private DataSet ExecuteDataAdt(DataSet ds, string query)
        {
            try
            {
                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.SelectCommand = _sqlCmd;
                cmd.SelectCommand.Connection = this.Connection;
                cmd.SelectCommand.CommandText = query;
                cmd.Fill(ds);
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage : {1}", ex.StackTrace, ex.Message);

                LastException = ex.Message;

                if (CheckConnection() == false) return null;
            }

            return ds;
        }

        private DataSet ExecuteProcedureAdt(DataSet ds, string query, params string[] values)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = _sqlCmd;
                adapter.SelectCommand.CommandText = query;
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Connection = this.Connection;

                for (int i = 0; i < values.Length; ++i)
                {
                    adapter.SelectCommand.Parameters.Add(values[i]);
                    //adapter.SelectCommand.Parameters.Add("params", values[i]);
                }

                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}\r\nMessage : {1}", ex.StackTrace, ex.Message);

                this.LastException = ex.Message;

                if (CheckConnection() == false) return null;
            }

            return ds;
        }

        #endregion private
    }
}