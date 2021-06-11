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

namespace DomainTool
{
    class MyCommon
    {

        public static void MesBox(string message)
        {
            MessageBox.Show(string.Format("{0}", message));
            //return returnValue;
        }
        public static string ToInsterAPP(string con, string appname, string domainname, string username, string SecurityNu, string dbtype, string toolsname)
        {
            string valueid = string.Empty;

            //MySqlConnection MYconn = new MySqlConnection(con);

            if (dbtype == "0")
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_App_InsertApp", conn))
                    {
                        //MessageBox.Show(string.Format("\"{0}\"", APPID + 1));
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@ID", APPID+1);
                        cmd.Parameters.AddWithValue("@AppName", appname);
                        cmd.Parameters.AddWithValue("@Domain", domainname);
                        cmd.Parameters.AddWithValue("@Name", username);
                        cmd.Parameters.AddWithValue("@Security", SecurityNu);
                        cmd.Parameters.AddWithValue("@Enabled", 0);
                        //回傳-
                        SqlParameter outputParameterID = new SqlParameter("@ID", SqlDbType.Int);
                        //回傳-

                        outputParameterID.Direction = ParameterDirection.Output;
                        //outputParameterID.Direction = ParameterDirection.ReturnValue;

                        cmd.Parameters.Add(outputParameterID);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            valueid = outputParameterID.Value.ToString();
                            //Fro2proid = outputParameterID.Value.ToString();
                            //MessageBox.Show(string.Format("\"{0}\"",outputParameterID.Value.ToString()));
                            //MessageBox.Show(outputParameterID.Value.ToString());
                            if (Convert.ToInt32(outputParameterID.SqlValue.ToString()) < 0)
                            {
                                MessageBox.Show("該程式已申請過，請聯繫資訊人員");
                            }
                            else
                            {
                                MessageBox.Show("程式安裝權限申請中，請聯繫資訊人員");
                                //MessageBox.Show(string.Format("\"{0}\"", outputParameterID.SqlValue));
                            }
                        }
                        catch (Exception ex)
                        {
                            MyCommon.ErrorLogWriter(ex.Message, toolsname);
                            MessageBox.Show("連線失敗");
                        }
                        //catch (Exception ex)
                        //{
                        //    throw ex.GetBaseException();
                        //}
                        //MessageBox.Show(string.Format("\"{0}\"", cmd.ExecuteNonQuery()));
                        finally
                        {
                            cmd.Dispose();
                            conn.Close();
                            //int APPID = Convert.ToInt32(MyCommon.GetAppnu());
                            //MessageBox.Show(APPID.ToString());
                        }

                    }
                }
            }
            else if (dbtype == "1")
            {

                //try
                //{
                //    MYconn.Open();
                //    var strSQL = "SELECT COUNT(*) FROM app_products ";
                //    strSQL = strSQL + string.Format("WHERE AppName = '{0}' and Security = '{1}' ", appname, SecurityNu);
                //    var cmd = new MySqlCommand(strSQL, MYconn);
                //    var intNumRows = Convert.ToInt32(cmd.ExecuteScalar());

                //    string strSQLcu = "SELECT COUNT(*) FROM app_products ";
                //    var cmdcu = new MySqlCommand(strSQLcu, MYconn);
                //    var intNumRowscu = Convert.ToInt32(cmdcu.ExecuteScalar());


                //    if (intNumRows > 0)
                //    {
                //        valueid = intNumRowscu.ToString();
                //        // MessageBox.Show(intNumRowscu.ToString());
                //        //作重複資料的提示
                //        MessageBox.Show("該程式已申請過，請聯繫資訊人員");
                //    }
                //    else
                //    {
                //        DateTime myDate = DateTime.Now;
                //        string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");
                //        string sql = @"INSERT INTO `app_products`(`AppName`, `Domain`, `Security`, `Name`, `Enabled`, `AddDateTime` ,`UpdateDateTime` ,`EnabledDateTime`) VALUES " +
                //        string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}') ", appname, domainname, SecurityNu, username, "0", myDateString, "", "");
                //        ;
                //        MySqlCommand myCommand = new MySqlCommand(sql);
                //        myCommand.Connection = MYconn;
                //        myCommand.ExecuteNonQuery();
                //        //MessageBox.Show(valueid);
                //        myCommand.Connection.Close();
                //        intNumRowscu = intNumRowscu + 1;
                //        valueid = intNumRowscu.ToString();
                //        //MessageBox.Show(intNumRowscu.ToString());
                //        MessageBox.Show("程式安裝權限申請中，請聯繫資訊人員");
                //        //作更新動作
                //    }


                //    MYconn.Close();
                //}
                //catch (MySql.Data.MySqlClient.MySqlException ex)
                //{
                //    ErrorLogWriter(ex.Message, toolsname);
                //    MessageBox.Show("連線失敗");
                //    //MessageBox.Show("Error " + ex.Number + " : " + ex.Message);
                //    MYconn.Close();
                //}
                //finally
                //{

                //    MYconn.Close();
                //    //int APPID = Convert.ToInt32(MyCommon.GetAppnu());
                //    //MessageBox.Show(APPID.ToString());
                //}
            }
            else
            {
                MessageBox.Show("請設定資料庫類型");
            }


            return valueid;
        }
        public static void ToUpdataApp(string ID, string connString, string dbtype, string toolsname)
        {
            if (dbtype == "0")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_APP_UpdateAppProducts", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", ID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogWriter(ex.Message, toolsname);
                }
            }
            else if (dbtype == "1")
            {
                //try
                //{
                //    MySqlConnection Myconn = new MySqlConnection(connString);
                //    Myconn.Open();
                //    DateTime myDate = DateTime.Now;
                //    string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");
                //    string sql = string.Empty;
                //    sql = string.Format("update app_products set UpdateDateTime = '{0}', Enabled={1} WHERE ID = '{2}'", myDateString, true, ID);
                //    MySqlCommand UPmyCommand = new MySqlCommand(sql);
                //    UPmyCommand.Connection = Myconn;
                //    UPmyCommand.ExecuteNonQuery();
                //    UPmyCommand.Connection.Close();
                //    Myconn.Close();
                //}
                //catch (MySql.Data.MySqlClient.MySqlException ex)
                //{
                //    ErrorLogWriter(ex.Message, toolsname);
                //}
            }

        }

        public static void ToEnabledTime(string ID, string DBS, string conType, string toolsname)
        {

            if (conType == "0")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBS))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_APP_UpdateAppProductsEnabled", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue("@Enabled", true);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogWriter(ex.Message, toolsname);
                }
            }
            else if (conType == "1")
            {
                //try
                //{

                //    MySqlConnection Myconn = new MySqlConnection(DBS);
                //    Myconn.Open();
                //    DateTime myDate = DateTime.Now;
                //    string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");
                //    string sql = string.Empty;
                //    sql = string.Format("update app_products set EnabledDateTime='{0}', Enabled={1} WHERE ID = '{2}'", myDateString, true, ID);
                //    MySqlCommand UPmyCommand = new MySqlCommand(sql);
                //    UPmyCommand.Connection = Myconn;
                //    UPmyCommand.ExecuteNonQuery();
                //    UPmyCommand.Connection.Close();
                //    Myconn.Close();
                //}
                //catch (MySql.Data.MySqlClient.MySqlException ex)
                //{
                //    ErrorLogWriter(ex.Message, toolsname);
                //}
            }
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
