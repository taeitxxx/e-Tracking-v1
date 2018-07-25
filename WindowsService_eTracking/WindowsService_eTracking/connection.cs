using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService_eTracking
{
    class connection
    {
        public String getConnection_dev()
        {
            string strConnString = @"data source=DESKTOP-7PAQ8NK\MSSQLSERVER_ADEV;initial catalog=dbetracking;persist security info=True;user id=sa;password=admin50";

            return strConnString;
        }


        public DataTable getUserFollow()
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = null;
            string strSQL = null;

            strSQL = " select * from tbfollowing where flag_follow = 'y'  ";

            objConn.ConnectionString = getConnection_dev();
            var _with1 = objCmd;
            _with1.Connection = objConn;
            _with1.CommandText = strSQL;
            _with1.CommandType = CommandType.Text;
            dtAdapter.SelectCommand = objCmd;

            dtAdapter.Fill(ds);
            dt = ds.Tables[0];

            dtAdapter = null;
            objConn.Close();
            objConn = null;

            return dt;
        }


        public DataTable getLastStatusByUserid(string userid, string pr_number)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = null;
            string strSQL = null;

            strSQL = " select * from tblog_last_status where userid = '" + userid + "' and pr_number = '" + pr_number + "'  ";

            objConn.ConnectionString = getConnection_dev();
            var _with1 = objCmd;
            _with1.Connection = objConn;
            _with1.CommandText = strSQL;
            _with1.CommandType = CommandType.Text;
            dtAdapter.SelectCommand = objCmd;

            dtAdapter.Fill(ds);
            dt = ds.Tables[0];

            dtAdapter = null;
            objConn.Close();
            objConn = null;

            return dt;
        }


        public DataTable getLastStatus_PR(string pr_number)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = null;
            string strSQL = null;

            strSQL = " select top(1) s.statusid, s.statusname, l.* from tblog l inner join tbstatus s on l.statusid = s.statusid " +
                    " where pr_number = '" + pr_number + "' order by logId desc  ";

            objConn.ConnectionString = getConnection_dev();
            var _with1 = objCmd;
            _with1.Connection = objConn;
            _with1.CommandText = strSQL;
            _with1.CommandType = CommandType.Text;
            dtAdapter.SelectCommand = objCmd;

            dtAdapter.Fill(ds);
            dt = ds.Tables[0];

            dtAdapter = null;
            objConn.Close();
            objConn = null;

            return dt;
        }


        public void Insert_last_Status(string userid, string pr_number, string last_status, string lastmodifieBy)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = null;
            string strSQL = null;

            strSQL = " Insert into tblog_last_status(userid,pr_number,last_status,lastmodifieddate,lastmodifieBy) " +
                       " values('" + userid + "','" + pr_number + "','" + last_status + "',getdate(),'" + lastmodifieBy + "')  ";


            objConn.ConnectionString = getConnection_dev();
            objConn.Open();
            var _with1 = objCmd;
            _with1.Connection = objConn;
            _with1.CommandText = strSQL;
            _with1.CommandType = CommandType.Text;

            objCmd.ExecuteNonQuery();

            dtAdapter = null;
            objConn.Close();
            objConn = null;

        }

        public void Update_last_Status(string last_status, string userid, string pr_number)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = null;
            string strSQL = null;

            strSQL = " update tblog_last_status set " +
                    " last_status = '"+ last_status + "',lastmodifieddate = getdate() where userid = '"+ userid + "' and pr_number = '"+ pr_number + "' ";


            objConn.ConnectionString = getConnection_dev();
            objConn.Open();
            var _with1 = objCmd;
            _with1.Connection = objConn;
            _with1.CommandText = strSQL;
            _with1.CommandType = CommandType.Text;

            objCmd.ExecuteNonQuery();

            dtAdapter = null;
            objConn.Close();
            objConn = null;

        }
    }
}
