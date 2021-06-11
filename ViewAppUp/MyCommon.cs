using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ViewAppUp
{
    class MyCommon
    {

        public static void MesBox(string message)
        {
            MessageBox.Show(string.Format("{0}", message));
            //return returnValue;
        }
        public static void ErrorLogWriter(string Message, string type)
        {
            //桌面新增LOG
            string fild_m = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//桌面位置

            string fild_l = string.Empty;
            fild_l = fild_m + @"\ErrLOG\";
            //fild_l = fild_l + string.Format("{0}\\Error_{1:yyyyMM}_Log.txt", AppDomain.CurrentDomain.BaseDirectory.ToString(), DateTime.Now);

            if (!Directory.Exists(fild_l))
            {
                //新增資料夾
                Directory.CreateDirectory(fild_m + @"\ErrLOG\");
                //資料夾存在
            }

            fild_l = string.Format("{0}Error_{1:yyyyMM}_{2}_Log.txt", fild_l, DateTime.Now, type);

            if (!File.Exists(fild_l))
            {
                using (StreamWriter sw = File.CreateText(fild_l))
                {
                    sw.WriteLine(string.Format("{0: yyyy/MM/dd HH:mm:ss} {1}", System.DateTime.Now, Message));
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fild_l))
                {
                    sw.WriteLine(string.Format("{0: yyyy/MM/dd HH:mm:ss} {1}", System.DateTime.Now, Message));
                }
            }

        }
        public static string GetAppnu()
        {
            try
            {
                MySettings myset = new MySettings();
                string connString = myset.DBConnection;
                string theme = "";
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_AppProductsNu_GetAppProductsNu";
                cmd.Parameters.Add("@TopN", SqlDbType.NVarChar, 500).Value = string.Empty;
                cmd.Parameters.Add("@StrWhere", SqlDbType.NVarChar, 1000).Value = string.Empty;
                cmd.Parameters.Add("@Sort", SqlDbType.NVarChar, 500).Value = string.Empty;
                SqlDataReader dr = cmd.ExecuteReader();
                string APPnu = string.Empty;
                if (dr.Read())
                {
                    APPnu = dr["nu"] == DBNull.Value ? "0" : dr["nu"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                dr.Close();
                conn.Close();
                return APPnu;
            }
            catch
            {

                return "連線失敗";
            }



        }


        public static bool GetProductsEnable(int ProductsID , string dbtype)
        {

            bool Enabled = false;
            bool status = false;
            MySettings sa = new MySettings();
            if (dbtype == "0")
            {
                using (SqlConnection conn = new SqlConnection(sa.DBConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Applist_GetApplist", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TopN", "");
                        cmd.Parameters.AddWithValue("@StrWhere", " WHERE APP_Products.ID = " + ProductsID);
                        cmd.Parameters.AddWithValue("@Sort", "");

                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            //status = Boolean.Parse(dr["Enabled"].ToString());
                            status = dr["Enabled"] == DBNull.Value ? false : Boolean.Parse(dr["Enabled"].ToString());
                        }
                        dr.Dispose();
                        dr.Close();
                    }
                }
            }
            else if (dbtype == "1")
            {
                using (MySqlConnection myconn = new MySqlConnection(sa.DBConnection))
                {
                    string sellist = string.Empty;
                    sellist = string.Format("SELECT * FROM app_products WHERE APP_Products.ID = '{0}' ", ProductsID);


                    using (MySqlCommand mycmd = new MySqlCommand(sellist, myconn))
                    {
                        // mycmd.CommandType = CommandType.StoredProcedure;

                        myconn.Open();


                        MySqlDataReader mydr = mycmd.ExecuteReader();
                        if (mydr.Read())
                        {
                            status = mydr["Enabled"] == DBNull.Value ? false : Boolean.Parse(mydr["Enabled"].ToString());
                        }

                        //    string cmd = "SELECT * FROM `app_products`";
                        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);
                        //    DataTable dt = new DataTable();
                        //    da.Fill(dt);
                        //    dataGridView1.DataSource = dt;
                        mydr.Dispose();
                        myconn.Close();
                    }

                }
            }
            return status;
        }
        //public string GepAppList(string ID) 
        //{
        //string status = false;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SP_Applist_GetApplist", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@TopN", "");
        //            cmd.Parameters.AddWithValue("@StrWhere", string.Format(" WHERE  Name ='{0}' and Security = '{1}' ", winname, SecurityNu));
        //            cmd.Parameters.AddWithValue("@Sort", "");

        //            conn.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.Read())
        //            {
        //                APPID = dr["ID"] == DBNull.Value ? string.Empty : dr["ID"].ToString();
        //                AppName = dr["AppName"] == DBNull.Value ? string.Empty : dr["AppName"].ToString();
        //                AppUserName = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString();
        //                AppEnabled = dr["Enabled"] == DBNull.Value ? "false" : dr["Enabled"].ToString();
        //                AppSecurity = dr["Security"] == DBNull.Value ? "" : dr["Security"].ToString();
        //            }
        //            dr.Dispose();
        //            conn.Close();
        //        }
        //    }
        // return status;
        //}

        public SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }


        public string convertToUNSecureString(SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }




    }
}
