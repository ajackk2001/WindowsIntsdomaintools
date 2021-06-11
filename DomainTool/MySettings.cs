using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DomainTool
{
    public class MySettings
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);
        public string DBConnection { get; set; }
        public string DBType { get; set; }
        public string TueKey { get; set; }

        public string filename = "DBServer.ini";
        public MySettings()
        {
            string TuKEY = "86A2C5B1";
            int size = 30000;//temp file source size
            StringBuilder temp = new StringBuilder(size); //temp file source
            string dbu = string.Empty;
            string dbp = string.Empty;
            string dbs = string.Empty;
            string d0 = string.Empty;
            string d1 = string.Empty;
            string d2 = string.Empty;
            string d3 = string.Empty;
            TueKey = "86A2C451B375D51053680625F8A6E5B1";
            try
            {
                GetPrivateProfileString("section1", "Acc", "", temp, size, ".\\" + filename);
                d0 = Convert.ToString(temp);
                dbu = MySecure.Decrypt(d0, TuKEY);
                //dbu = Convert.ToString(temp);
                GetPrivateProfileString("section1", "pass", "", temp, size, ".\\" + filename);
                d1 = Convert.ToString(temp);
                dbp = MySecure.Decrypt(d1, TuKEY);
                GetPrivateProfileString("section2", "Server", "", temp, size, ".\\" + filename);
                d2 = Convert.ToString(temp);
                dbs = MySecure.Decrypt(d2, TuKEY);
                GetPrivateProfileString("section2", "ServerType", "", temp, size, ".\\" + filename);
                d3 = Convert.ToString(temp);

                DBConnection = string.Format("Data Source=\"{0}\"; Initial Catalog=APPIN;Persist Security Info=True;User ID=\"{1}\";Password=\"{2}\"", dbs, dbu, dbp);
                if (d3 == "1")
                {
                    //DBConnection = "server=" + dbs + ";uid=" + dbu + ";pwd=" + dbp + ";database=" + "Appin" + ";convert zero datetime=True;";
                    DBConnection = "server=" + dbs + ";uid=" + dbu + ";pwd=" + dbp + ";database=" + "Appin";
                }
                DBType = d3;
            }
            catch (Exception ex)
            {

                MyCommon.ErrorLogWriter(ex.Message, "DomainSet");

            }
            finally
            {

            }


            //
            // TODO: 在此加入建構函式的程式碼
            //
            //DBConnection = string.Format("Data Source=\"{0}\"; Initial Catalog=APPIN;Persist Security Info=True;User ID=\"{1}\";Password=\"{2}\"", "192.168.0.232", "mis1", "IcpApp123@");

            // 如果有特殊的編碼在database後面請加上;CharSet=編碼, utf8請使用utf8_general_ci




        }
    }
}
