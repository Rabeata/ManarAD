using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public class DatabaseSettings
    {
        public static string getConnectionString()
        {
            return "User ID=postgres;Password=123456;Server=127.0.0.1;Port=5432;Database=almanar;";

        }
    }
}
