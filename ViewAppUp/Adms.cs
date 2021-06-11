using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ViewAppUp
{
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
            string fild_m = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//桌面位置
            try
            {
                MySettings sa = new MySettings();
                //string connString = "Data Source=192.168.0.232;Initial Catalog=APPIN;Persist Security Info=True;User ID=mis1;Password=IcpApp123@";

                
                using (SqlConnection conn = new SqlConnection(sa.DBConnection))
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
                    }
                }






            }
            catch (Exception ex)
            {
                string fild_l = string.Empty;
                fild_l = fild_m + @"\CMSLOG\";

                if (Directory.Exists(fild_l))
                {
                    //資料夾存在
                    using (StreamWriter sw = new StreamWriter(fild_l + @"ErrorLog.txt"))   //小寫TXT     
                    {
                        // Add some text to the file.
                        sw.Write(DateTime.Now + ":" + ex.Message);
                        //sw.WriteLine("header for the file.");
                        //sw.WriteLine("-------------------");
                        //// Arbitrary objects can also be written to the file.
                        //sw.Write("The date is: ");
                        //sw.WriteLine(DateTime.Now);
                    }
                }
                else
                {
                    //新增資料夾
                    Directory.CreateDirectory(fild_m + @"\CMSLOG\");
                    using (StreamWriter sw = new StreamWriter(fild_l + @"ErrorLog.txt"))   //小寫TXT     
                    {
                        // Add some text to the file.
                        sw.Write(DateTime.Now + ":" + ex.Message);
                        //sw.WriteLine("header for the file.");
                        //sw.WriteLine("-------------------");
                        //// Arbitrary objects can also be written to the file.
                        //sw.Write("The date is: ");
                        //sw.WriteLine(DateTime.Now);
                    }
                }

            }


        }
    }
}
