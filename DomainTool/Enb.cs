using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace DomainTool
{
    public partial class Enb : Form
    {
        public Enb()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = true;
        }
        MySettings myset = new MySettings();
        //public string myset.DBConnection = "Data Source=192.168.0.232;Initial Catalog=APPIN;Persist Security Info=True;User ID=mis1;Password=IcpApp123@";

        string F2text { get; set; }

        public void F2adm(string text1string)
        {
            F2text = text1string;
        }

        public SecureString ConvertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileInfo f = new FileInfo(F2text);
            string appname = Path.GetFileNameWithoutExtension(F2text) + Path.GetExtension(F2text);
            string TuKEY = myset.TueKey;
            string SecurityNu = MySecure.Encrypt(f.Length + appname, TuKEY);
            string winname = System.Environment.UserName;
            string APPID = string.Empty;
            string AppName = string.Empty;
            string AppUserName = string.Empty;
            string AppEnabled = string.Empty;
            string AppSecurity = string.Empty;
            string conntype = myset.DBType;
            //MessageBox.Show(string.Format(" WHERE  name ='{0}' and Security = '{1}' ", winname, SecurityNu));
            //ClientScript.RegisterStartupScript(Page.GetType(), "_alert", string.Format("alert(\"{0}\");", "無此權限操作，請先確認您的權限後重新登入!"), true);
            string connString = myset.DBConnection;
            try
            {
                if (conntype == "0")
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_Applist_GetApplist", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TopN", "");
                            cmd.Parameters.AddWithValue("@StrWhere", string.Format(" WHERE  Name ='{0}' and Security = '{1}' ", winname, SecurityNu));
                            cmd.Parameters.AddWithValue("@Sort", "");
                            conn.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                APPID = dr["ID"] == DBNull.Value ? string.Empty : dr["ID"].ToString();
                                AppName = dr["AppName"] == DBNull.Value ? string.Empty : dr["AppName"].ToString();
                                AppUserName = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString();
                                AppEnabled = dr["Enabled"] == DBNull.Value ? "false" : dr["Enabled"].ToString();
                                AppSecurity = dr["Security"] == DBNull.Value ? "" : dr["Security"].ToString();
                            }
                            dr.Dispose();
                            conn.Close();
                        }
                    }
                }
                else if (conntype == "1")
                {
                    //using (MySqlConnection myconn = new MySqlConnection(connString))
                    //{
                    //    string sellist = string.Empty;
                    //    sellist = string.Format("SELECT * FROM app_products WHERE Name ='{0}' and Security = '{1}' ", winname, SecurityNu);

                    //    using (MySqlCommand mycmd = new MySqlCommand(sellist, myconn))
                    //    {
                    //        myconn.Open();
                    //        MySqlDataReader mydr = mycmd.ExecuteReader();
                    //        if (mydr.Read())
                    //        {
                    //            APPID = mydr["ID"] == DBNull.Value ? string.Empty : mydr["ID"].ToString();
                    //            AppName = mydr["AppName"] == DBNull.Value ? string.Empty : mydr["AppName"].ToString();
                    //            AppUserName = mydr["Name"] == DBNull.Value ? string.Empty : mydr["Name"].ToString();
                    //            AppEnabled = mydr["Enabled"] == DBNull.Value ? "false" : mydr["Enabled"].ToString();
                    //            AppSecurity = mydr["Security"] == DBNull.Value ? "" : mydr["Security"].ToString();
                    //        }
                    //        mydr.Dispose();
                    //        myconn.Close();
                    //    }

                    //}
                }

                AdmDS sa = new AdmDS();

                if (checkBox1.Checked == true)
                {
                    if (AppSecurity != string.Empty)
                    {
                        if (textBox1.Text == sa.ADAcc && textBox2.Text == sa.ADPass)
                        {
                            if (AppEnabled != "True")
                            {
                                MyCommon.ToEnabledTime(APPID, myset.DBConnection, myset.DBType, "MSEnbWindows");
                                MessageBox.Show(string.Format("\"{0}\"", "啟用完成"));
                            }
                            else
                            {
                                MessageBox.Show(string.Format("\"{0}\"", "已啟用"));
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("\"{0}\"", "密碼錯誤，請重新輸入"));
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("\"{0}\"", "程式未申請，請重新申請"));
                    }

                }
                else
                {
                    if (AppSecurity != string.Empty)
                    {
                        if (textBox3.Text == AppSecurity)
                        {
                            if (AppEnabled != "True")
                            {
                                MyCommon.ToEnabledTime(APPID, myset.DBConnection, myset.DBType, "MYEnbWindows");
                                MessageBox.Show(string.Format("\"{0}\"", "啟用完成"));
                            }
                            else
                            {
                                MessageBox.Show(string.Format("\"{0}\"", "已啟用"));
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("\"{0}\"", "啟用碼錯誤，請重新輸入或是聯繫資訊人員"));
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("\"{0}\"", "程式未申請，請重新申請"));
                    }

                }
            }
            //catch (IOException e1)
            catch (Exception e1)
            {
                MyCommon.ErrorLogWriter(e1.Message, "Enb1");
                MessageBox.Show("連線錯誤");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = false;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = true;
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, e);
            }
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, e);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
