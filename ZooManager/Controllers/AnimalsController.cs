using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPDemo.Models;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ASPDemo.Access_Data;

namespace ASPDemo.Controllers
{
    public class AnimalsController : Controller
    {
        //private AnimalDBContext db = new AnimalDBContext();
        //SqlConnection sqlConnection;


        // GET: Animals
        public ActionResult DisplayAnimals()
        {
            string query = "select * from Animal";
            var data = DataAccess.LoadData<Animals>(query).ToList();
            List<Animals> animals = new List<Animals>();


            foreach (var row in data)
            {
                //return row.AnimalName;

                animals.Add(new Animals
                {
                    Id = row.Id,
                    Name = row.Name
                });

             }

             return View(animals);           
        }
    }
}