using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace DomainTool
{
    public partial class Domain : Form
    {
        public Domain()
        {

            InitializeComponent();
            int formWidth;
            int formHieht;
            formWidth = this.ClientSize.Width;
            formHieht = this.ClientSize.Height;
            this.MinimumSize = new Size(formWidth + 20, formHieht + 40);
            this.MaximumSize = new Size(formWidth + 20, formHieht + 40);
            string toolsname = "Domain 安裝程式 資料庫類型 : ";
            if (myset.DBType == "0")
            {
                this.Text = toolsname + "MS SQL Server";
            }
            else if (myset.DBType == "1")
            {
                this.Text = toolsname + "MYSQL Server";
            }
            else
            {
                this.Text = toolsname + "資料庫類型 : 錯誤";
            };
        }


        private string FilePathName_x;

        //public string myset.DBConnection = "Data Source=192.168.0.232;Initial Catalog=APPIN;Persist Security Info=True;User ID=mis1;Password=IcpApp123@";
        //public string myset.DBConnection = string.Format("Data Source=\"{0}\"; Initial Catalog=APPIN;Persist Security Info=True;User ID=\"{1}\";Password=\"{2}\"", "192.168.0.232","mis1","IcpApp123@");
        MySettings myset = new MySettings();

        private void ButSelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select file",
                InitialDirectory = ".\\",
                Filter = "exe,msi files (*.*)|*.exe;*.msi"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePathName_x = dialog.FileName;
                textBox1.Text = dialog.FileName;
                //Process.Start();
            }
        }


        // public string FilePathName { get; set; }//.Net 3.0 語法
        public SecureString ConvertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }
        public string Fro2proid { get; set; }
        private void ButApplication_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox1.Text != string.Empty)
            {
                try
                {
                    FileInfo f = new FileInfo(textBox1.Text);
                    string connString = myset.DBConnection;
                    string TuKEY = myset.TueKey;
                    string DBtype = myset.DBType;
                    string appname = Path.GetFileNameWithoutExtension(textBox1.Text) + Path.GetExtension(textBox1.Text);
                    string SecurityNu = MySecure.Encrypt(f.Length + appname, TuKEY);
                    string domainname = System.Environment.UserDomainName;
                    string username = System.Environment.UserName;
                    string toolsname = string.Empty;
                    toolsname = "MSDomainInt";
                    if (DBtype.Equals("1"))
                    {
                        toolsname = "MYDomainInt";
                    }
                    Fro2proid = MyCommon.ToInsterAPP(connString, appname, domainname, username, SecurityNu, DBtype, toolsname).ToString();
                    //MessageBox
                    //MessageBox.Show(Fro2proid);
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("請選擇檔案");
                    MyCommon.ErrorLogWriter(ex.Message + "錯誤檔名", "DomainToolsApp");
                }
            }
            else
            {
                MessageBox.Show("請選擇檔案");
            }
        }



        private void ButUpdata_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != string.Empty)
            {
                AdmDS sa = new AdmDS();
                string ADID = sa.ADID;
                string ADAcc = sa.ADAcc;
                string ADPass = sa.ADPass;
                string ADDomain = sa.ADDomain;

                FileInfo f = new FileInfo(textBox1.Text);

                string APPID = string.Empty;
                string AppName = string.Empty;
                string AppUserName = string.Empty;
                string AppEnabled = string.Empty;
                string AppSecurity = string.Empty;
                string appname = Path.GetFileNameWithoutExtension(textBox1.Text) + Path.GetExtension(textBox1.Text);
                string TuKEY = myset.TueKey;
                string SecurityNu = MySecure.Encrypt(f.Length + appname, TuKEY);
                string winname = System.Environment.UserName;
                //MessageBox.Show(string.Format(" WHERE  name ='{0}' and Security = '{1}' ", winname, SecurityNu));
                //ClientScript.RegisterStartupScript(Page.GetType(), "_alert", string.Format("alert(\"{0}\");", "無此權限操作，請先確認您的權限後重新登入!"), true);
                string connString = myset.DBConnection;
                string conType = myset.DBType;
                string exit = string.Empty;
                try
                {
                    if (conType.Equals("0"))
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
                    else if (conType.Equals("1"))
                    {
                        //using (MySqlConnection myconn = new MySqlConnection(connString))
                        //{
                        //    string sellist = string.Empty;
                        //    sellist = string.Format("SELECT * FROM app_products WHERE Name ='{0}' and Security = '{1}' ", winname, SecurityNu);
                        //    using (MySqlCommand mycmd = new MySqlCommand(sellist, myconn))
                        //    {
                        //        // mycmd.CommandType = CommandType.StoredProcedure;
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
                    else
                    {
                        MessageBox.Show("請設定資料庫類型");
                    }

                }
                catch (Exception ex)
                {
                    MyCommon.ErrorLogWriter(ex.Message, "DomainTools");
                    exit = "F";
                    //Application.Exit();
                }

                if (exit != "F")
                {
                    Fro2proid = APPID;
                    string apptype = Path.GetExtension(textBox1.Text);
                    apptype = apptype.Replace('.', ' ').Trim(' ').ToUpper();

                    SecureString sec_strPassword = new SecureString();
                    sec_strPassword = ConvertToSecureString(ADPass);

                    //convertToUNSecureString(sec_strPassword);
                    if (AppUserName == winname && AppSecurity == SecurityNu && AppEnabled == "True")
                    {

                        try
                        {
                            MyCommon.ToUpdataApp(APPID, connString, conType, myset.DBType);
                            //updateAPP(APPID, Path.GetFileNameWithoutExtension(textBox1.Text), Path.GetExtension(textBox1.Text));
                            if (apptype != "MSI")
                            {
                                System.Diagnostics.Process.Start(textBox1.Text, ADAcc, sec_strPassword, ADDomain);
                            }
                            else
                            {
                                Process p = new Process();
                                p.StartInfo.UseShellExecute = false;
                                p.StartInfo.FileName = "msiexec";
                                p.StartInfo.UserName = ADAcc;
                                p.StartInfo.Password = sec_strPassword;
                                p.StartInfo.Domain = ADDomain;
                                p.StartInfo.Arguments = "/i " + textBox1.Text;
                                p.Start();
                            }
                        }
                        catch
                        {
                            MessageBox.Show(string.Format("\"{0}\"", "執行密碼錯誤 請聯絡資訊人員"));
                            //updateAPP(APPID, Path.GetFileNameWithoutExtension(textBox1.Text), Path.GetExtension(textBox1.Text));
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("\"{0}\"", "尚未申請啟動碼 或是 啟動碼未啟用 ， 請聯絡資訊人員"));
                    }

                }
                else
                {
                    MessageBox.Show(string.Format("\"{0}\"", "連線失敗"));
                }
            }
            else
            {
                MessageBox.Show(string.Format("\"{0}\"", "請選擇檔案"));
            }
        }

        //public static void toUpdataApp()
        public string From2String { get; set; }

        public void SetFalse(string text) //實作一個公開方法，使其他Form可以傳遞資料進來
        {
            From2String = text;
        }

        private void ButEnableWin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox1.Text != string.Empty)
            {
                Enb Form1 = new Enb();
                Form1.F2adm(textBox1.Text);
                switch (Form1.ShowDialog(this))
                {
                    case DialogResult.Yes: //Form2中按下ToForm1按鈕
                        this.Show(); //顯示父視窗
                        break;
                    case DialogResult.No: //Form2中按下關閉鈕
                        this.Close();  //關閉父視窗 (同時結束應用程式)
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("請選擇檔案");
            }
        }


    }
}
