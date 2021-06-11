using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ViewAppUp
{
    public partial class Form1 : Form
    {
        public object DataBoundItem { get; }
        public string ID { get; set; }
        public int pos { get; set; }
        public bool selcount { get; set; }

        public bool enable { get; set; }

        public int formWidth { get; set; }
        public int formHieht { get; set; }

        MySettings myset = new MySettings();

        DataSet ds = new DataSet();
        BindingSource tb1names = new BindingSource();
        //SqlConnection cs = new SqlConnection(string.Format("Data Source=\"{0}\"; Initial Catalog=APPIN;Persist Security Info=True;User ID=\"{1}\";Password=\"{2}\"", "192.168.0.232", "mis1", "IcpApp123@"));
        SqlDataAdapter da = new SqlDataAdapter();

        public Form1()
        {
            InitializeComponent();
            string dbtypewindows = string.Empty;
            formWidth = this.ClientSize.Width;
            formHieht = this.ClientSize.Height;
            this.MinimumSize = new Size(formWidth + 20, formHieht + 45);
            dbtypewindows = "AdminTools 資料庫類型 :";
            //Form1.ActiveForm.Text = "";
            if (myset.DBType == "0")
                dbtypewindows = dbtypewindows + " MS SQL Server ";
            else if (myset.DBType == "1")
                dbtypewindows = dbtypewindows + " MYSQL Server ";
            else             
            {
                dbtypewindows = "AdminTools 資料庫類型 : 資料庫錯誤 ";
            }
            this.Text = dbtypewindows;
            //Form form1 = new Form();
            //form1.Text = "";
            //MaximizeBox = false;
            //MinimizeBox = false; ;
            //FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right |
            AnchorStyles.Top |
            AnchorStyles.Left;

            //this.MinimumSize = new Size(formWidth, formHieht);
            //int deskHeight = Screen.PrimaryScreen.Bounds.Height;
            //int deskWidth = Screen.PrimaryScreen.Bounds.Width;

            //MessageBox.Show(formWidth.ToString());
            pos = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MySettings myset = new MySettings();
            string connString = myset.DBConnection;
            string winname = System.Environment.UserName;
            if (myset.DBType == "0")
            {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string comstring = string.Empty;

                if (this.textBox1.Text != "") {
                    //comstring =string.Format("{0}", dataGridView1.CurrentRow.Index + 1);
                    //comstring = "select ID,Name,AppName,Enabled,Security FROM APP_Products";
                    comstring = string.Empty;
                    //comstring = string.Format("select ID,Name,AppName,Enabled,Security FROM APP_Products where Name = \"{0}\"", this.textBox1.Text);
                    comstring = string.Format("select ID,Name,AppName,Enabled,Security FROM APP_Products where Name ='{0}'", this.textBox1.Text);
                }
                else
                {
                    comstring = string.Empty;
                    comstring = "select ID,Name,AppName,Enabled,Security FROM APP_Products";
                    
                }
                try
                {

                    da.SelectCommand = new SqlCommand(comstring, conn);
                    ds.Clear();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //DataGridView dag = new DataGridView();
                    //dataGridView1.Columns[4].Width = 100;
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                    //dataGridView1.AutoSizeRowsMode =DataGridViewAutoSizeRowsMode.AllCells;
                    //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    tb1names.DataSource = ds.Tables[0];
                    conn.Close();
                    dgupdatesel();
                   // records(0);
                }
                    catch (Exception ex)
                    {
                        // MessageBox.Show("請選擇檔案");
                        MyCommon.ErrorLogWriter(ex.Message + "錯誤工具 :", "ViewTools");
                        conn.Close();
                    }

                }
            }
            else if (myset.DBType == "1")
            {               
            using (MySqlConnection myconn = new MySqlConnection(connString))
                    {
                        try { 
                        string sellist = string.Empty;

                        if (this.textBox1.Text != "")
                        {
                            //comstring =string.Format("{0}", dataGridView1.CurrentRow.Index + 1);
                            //comstring = "select ID,Name,AppName,Enabled,Security FROM APP_Products";
                            sellist = string.Empty;
                            //comstring = string.Format("select ID,Name,AppName,Enabled,Security FROM APP_Products where Name = \"{0}\"", this.textBox1.Text);
                            sellist = string.Format("select ID,Name,AppName,Enabled,Security FROM APP_Products where Name ='{0}'", this.textBox1.Text);
                        }
                        else
                        {
                            sellist = string.Empty;
                            sellist = "select ID,Name,AppName,Enabled,Security FROM APP_Products";

                        }

                        //sellist = string.Format("SELECT * FROM APP_Products");

                            using (MySqlCommand mycmd = new MySqlCommand(sellist, myconn))
                            {
                                // mycmd.CommandType = CommandType.StoredProcedure;

                                myconn.Open();
                                MySqlDataAdapter da = new MySqlDataAdapter(sellist, myconn);
                                ds.Clear();
                            //DataTable dt = new DataTable();
                                da.Fill(ds);
                                dataGridView1.DataSource = ds.Tables[0];
                                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                                tb1names.DataSource = ds.Tables[0];
                                //MySqlDataReader mydr = mycmd.ExecuteReader();
                                //if (mydr.Read())
                                //{
                                //APPID = dr["ID"] == DBNull.Value ? string.Empty : dr["ID"].ToString();
                                //AppName = dr["AppName"] == DBNull.Value ? string.Empty : dr["AppName"].ToString();
                                //AppUserName = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString();
                                //AppEnabled = dr["Enabled"] == DBNull.Value ? "false" : dr["Enabled"].ToString();
                                //AppSecurity = dr["Security"] == DBNull.Value ? "" : dr["Security"].ToString();
                                //}
                                //mydr.Dispose();
                                myconn.Close();
                                dgupdatesel();
                            }
                        }
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                         {
                        MyCommon.ErrorLogWriter(ex.Message + "錯誤工具 :", "ViewTools");
                        myconn.Close();
                         }
                        finally
                        {
                            myconn.Close();
                        }
                    }              
            }
            else                 
            {
                MessageBox.Show("請設定資料庫");
            }


        }


        private void dataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                ID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                enable = true;
                //enable = bool.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                //MessageBox.Show(enable.ToString());
                bool AppEna = MyCommon.GetProductsEnable(Int32.Parse(ID), myset.DBType);                  
                pos = (e.RowIndex+1);
                records();
                //label2.Text = "Record " + pos + " of " + tb1names.Count;                
                //MessageBox.Show(string.Format("{0}&{1}", MyCommon.GetProductsEnable(Int32.Parse(ID), myset.DBType),ID));
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            else
            {
                textBox1.Text = "";
            }
        }

        private void butEnable_Click(object sender, EventArgs e)
        {
            try
            {
                string id2 = "1";
                if (ID != string.Empty || ID != "")
                {
                    id2 = ID;
                }
                bool AppEna = MyCommon.GetProductsEnable(Int32.Parse(ID),myset.DBType);
                if (AppEna != true)
                {
                    MessageBox.Show("啟用完成");
                    //if (myset.DBType == "0")
                    //{
                    //    MSupEnable(ID, true);
                    //}
                    //if (myset.DBType == "1")
                    //{ 
                    //    MYupEnable(ID, true);
                    //}
                    toEnbale(ID, true);
                    button1_Click(this, e);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Int32.Parse(id2) - 1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[Int32.Parse(id2) - 1].Cells[0];
                }
                else
                {

                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Int32.Parse(ID) - 1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[Int32.Parse(ID) - 1].Cells[0];

                }
            }
            catch
            { }
            
            //foreach (DataGridView row in this.dataGridView1.SelectedRows)
            //{
            //    //row.cells[tb1names.Position].value
            //    //Customer cust = row.DataBoundItem as Customer;
            //    //if (cust != null)
            //    //{
            //    //    MessageBox.Show();
            //    //    cust.SendInvoice();
            //    //}
            //}
        }

        private void buFalse_Click(object sender, EventArgs e)
        {
            try
            {
                string id2 = "1";
                if (ID != string.Empty || ID != "")
                {
                    id2 = ID;
                }
                bool AppEna = MyCommon.GetProductsEnable(Int32.Parse(ID), myset.DBType);
                if (AppEna == true)
                {
                    MessageBox.Show("停用完成");
                    //if (myset.DBType == "0")
                    //{
                    //    MSupEnable(ID, false);
                    //}
                    //if (myset.DBType == "1")
                    //{
                    //    MYupEnable(ID, false);
                    //}
                    toEnbale(ID, false);

                    button1_Click(this, e);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Int32.Parse(id2) - 1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[Int32.Parse(id2) - 1].Cells[0];
                }
                else
                {

                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[Int32.Parse(ID) - 1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[Int32.Parse(ID) - 1].Cells[0];

                }
            }
            catch
            { }
        }
        private void butFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            tb1names.MoveFirst();            
            dgupdatesel();
            records();
        }
        private void bntPrevious_Click(object sender, EventArgs e)
        {
            pos = pos -1;
            if (pos < 0) { 
                pos = 1;
            }
            tb1names.MovePrevious();
            dgupdatesel();            
            records();
        }
        private void butNext_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(pos.ToString());
            if (pos <= 0)
            { 
                pos = 1;
            }
            pos = pos + 1;
            if (pos > tb1names.Count)
            {
                pos = tb1names.Count;
            }
            tb1names.MoveNext();           
            dgupdatesel();
            records();

        }
        private void butLast_Click(object sender, EventArgs e)
        {
            pos = tb1names.Count;
            tb1names.MoveLast();            
            dgupdatesel();
            records();
        }
        private  void dgupdatesel()
        {
            try
            {
                int labpos = 0;
                labpos = pos;
                if (labpos <= 0)
                { 
                    labpos = 1;
                }
                if (labpos > tb1names.Count)
                {
                    labpos = tb1names.Count;
                }
                dataGridView1.ClearSelection();
                dataGridView1.Rows[labpos-1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[labpos-1].Cells[0];
                ID = dataGridView1.Rows[labpos-1].Cells[0].Value.ToString();
                enable = bool.Parse(dataGridView1.Rows[labpos - 1].Cells[0].Value.ToString());
                //MessageBox.Show(ID);
                //dataGridView1.ClearSelection();
                //dataGridView1.Rows[tb1names.Position].Selected = true;
                //dataGridView1.CurrentCell = dataGridView1.Rows[tb1names.Position].Cells[0];
                //ID = dataGridView1.Rows[tb1names.Position].Cells[0].Value.ToString();

                MessageBox.Show(string.Format("{0}", dataGridView1.Rows[tb1names.Position].Index));

                //    MessageBox.Show(ID);
                //    MessageBox.Show(dataGridView1.Rows[tb1names.Position].Cells[0].ToString());
            }
            catch { }
        }

        private void records()
        {
            int labpos = 0;
            labpos = pos;
            if (labpos <= 0)
            {
                labpos = 1;
            }
            if (labpos > tb1names.Count) 
            {
                labpos = tb1names.Count;                
            }
            label2.Text = "Record " + labpos + " of " + tb1names.Count;
        }

        public static void MSupEnable(string id, bool reEnable) 
        {
            MySettings sa = new MySettings();
            //string ID = dataGridView1.Rows[tb1names.Position].Cells[0].Value.ToString();
            //MessageBox.Show(ID);
            using (SqlConnection conn = new SqlConnection(sa.DBConnection))
            {
                using (SqlCommand cmd = new SqlCommand("SP_APP_UpdateAppProductsEnabled", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Enabled", reEnable);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }

        public static void MYupEnable(string id, bool reEnable)
        {
            
            MySettings sa = new MySettings();
            MySqlConnection Myconn = new MySqlConnection(sa.DBConnection);
            Myconn.Open();
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = string.Empty;
            sql = string.Format("update app_products set UpdateDateTime='{0}', Enabled={1} WHERE ID = '{2}'", myDateString, reEnable, id);
            MySqlCommand UPmyCommand = new MySqlCommand(sql);
            UPmyCommand.Connection = Myconn;
            UPmyCommand.ExecuteNonQuery();
            UPmyCommand.Connection.Close();
            Myconn.Close();            

        }

        public static void toEnbale(string id, bool reEnable) 
        {
            MySettings sa = new MySettings();

            if (sa.DBType == "0")
            {
                using (SqlConnection conn = new SqlConnection(sa.DBConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_APP_UpdateAppProductsEnabled", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Enabled", reEnable);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            }
            if (sa.DBType == "1")
            {
                MySqlConnection Myconn = new MySqlConnection(sa.DBConnection);
                Myconn.Open();
                DateTime myDate = DateTime.Now;
                string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = string.Empty;
                sql = string.Format("update app_products set EnabledDateTime='{0}', Enabled={1} WHERE ID = '{2}'", myDateString, reEnable, id);
                MySqlCommand UPmyCommand = new MySqlCommand(sql);
                UPmyCommand.Connection = Myconn;
                UPmyCommand.ExecuteNonQuery();
                UPmyCommand.Connection.Close();
                Myconn.Close();
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, e);
            }
        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {

        }





        //private void TextBoxDec_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    char key = e.KeyChar;
        //    int value = (int)key;
        //    if ((value >= 48 && value <= 57) || value == 46 || value == 8 || value == 43 || value == 45)
        //        e.Handled = false;
        //    else
        //        e.Handled = true;
        //}

        //private void TextBoxDec_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        //    {
        //        e.Handled = true;
        //    }
        //}
    }
}
