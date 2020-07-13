using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NohWebApi.DB
{
    public class DBConntext
    {
        public string getConnectionString()
        {
            string strcon = ConfigurationManager.ConnectionStrings["strconndb"].ConnectionString;
            //create new sqlconnection and connection to database by using connection string from web.config file  
            return strcon;
        }
    }
}