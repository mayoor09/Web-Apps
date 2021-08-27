using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ASPDemo.Access_Data
{
    public class DataAccess
    {

        //string dbname = "ASPDemo";
        public static string cs = ConfigurationManager.ConnectionStrings["ASPDemo"].ConnectionString;
        public static string param;
        public static List<T> LoadData<T>(string q)
        {
            using (IDbConnection cnn = new SqlConnection(cs))
            {
                return cnn.Query<T>(q).ToList();
            }
        }

        public static List<T> LoadZooAnimals<T>(string q, object param)
        {
            using (IDbConnection cnn = new SqlConnection(cs))
            {
                return cnn.Query<T>(q, param).ToList();
            }
        }


    }
}