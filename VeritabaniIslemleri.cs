using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaOtomasyonuOdev
{
    internal class VeritabaniIslemleri
    {
        public static SqlConnection BaglantiDondur()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("Data Source=DESKTOP-5MRDCNR\\SQLEXPRESS;Initial Catalog=SinemaVeri; Integrated Security=True;");

            }
            catch
            {
                //connection lost

            }
            return con;
        }

    }
}
