using DomainTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

            // TODO: 在此加入建構函式的程式碼
             //
    public class AdmDS
    {
        public string ADID { get; set; }
        public string ADAcc { get; set; }
        public string ADPass { get; set; }
        public string ADDomain { get; set; }

        public AdmDS()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
            MySettings Myset = new MySettings();
            //string connString = "Data Source=192.168.0.232;Initial Catalog=APPIN;Persist Security Info=True;User ID=mis1;Password=IcpApp123@";
            if (Myset.DBType == "0")
            {
                using (SqlConnection conn = new SqlConnection(Myset.DBConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Employee_GetEmployee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TopN", "");
                        cmd.Parameters.AddWithValue("@StrWhere", "");
                        cmd.Parameters.AddWithValue("@Sort", "");

                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            ADID = dr["ID"].ToString();
                            ADAcc = dr["Account"].ToString();
                            ADPass = dr["Password"].ToString();
                            ADDomain = dr["Domain"].ToString();
                        }
                        dr.Dispose();
                        conn.Close();
                    }
                }
            }
            else if (Myset.DBType == "1")
            {
                //using (MySqlConnection myconn = new MySqlConnection(sa.DBConnection))  
                //{
                //    string sellist = string.Empty;
                //    sellist = string.Format("SELECT * FROM employee_employee");
                //    using (MySqlCommand mycmd = new MySqlCommand(sellist, myconn))
                //    {
                //        // mycmd.CommandType = CommandType.StoredProcedure;
                //        myconn.Open();
                //        MySqlDataReader mydr = mycmd.ExecuteReader();
                //        if (mydr.Read())
                //        {
                //            ADID = mydr["ID"].ToString();
                //            ADAcc = mydr["Account"].ToString();
                //            ADPass = mydr["Password"].ToString();
                //            ADDomain = mydr["Domain"].ToString();
                //        }
                //        //MessageBox.Show(ADID);
                //        mydr.Dispose();
                //        myconn.Close();
                //    }

                //}
            }
            else
            {
                ADID = "";
                ADAcc = "";
                ADPass = "";
                ADDomain = "";
            }

        }
    }




