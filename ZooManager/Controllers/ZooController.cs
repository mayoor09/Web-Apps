using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPDemo.Models;
using ASPDemo.Access_Data;

namespace ASPDemo.Controllers
{
    public class ZooController : Controller
    {
        // GET: Zoo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayZoo()
        {
            string query = "SELECT * FROM Zoo";

            var data = DataAccess.LoadData<Zoo>(query).ToList();
            List<Zoo> ZooName = new List<Zoo>();

            foreach (var row in data)
            {
                //return row.AnimalName;

                ZooName.Add(new Zoo
                {
                    Id = row.Id,
                    Location = row.Location
                });

            }

            return View(ZooName);
        }

        public ActionResult DisplayZooAnimals(string id)
        {

            string query = @"SELECT * FROM Animal WHERE Id in (SELECT AnimalId FROM ZooAnimal WHERE ZooID =(SELECT Id FROM Zoo WHERE Location = @id))";
            var parameters = new { id = id };

            var data = DataAccess.LoadZooAnimals<Animals>(query, parameters).ToList();
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