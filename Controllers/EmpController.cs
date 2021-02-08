using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using CrudWithMongoDB.Models;
using System.Configuration;

namespace CrudWithMongoDB.Controllers
{
    [RoutePrefix ("Api/Employee")]
    public class EmpController : ApiController
    {
        //Inserta en la base de datos los detalles del empleado.
        [Route("InsertEmployee")]
        [HttpPost]
        public object Addemployee(Employee objVM)
        {
            try
            {   ///Insert Emoloyeee  
                #region InsertDetails  
                if (objVM.Id == null)
                {
                    string constr = ConfigurationManager.AppSettings["connectionString"];
                    var Client = new MongoClient(constr);
                    var DB = Client.GetDatabase("employee");
                    var collection = DB.GetCollection<Employee>("EmployeeDetails");
                    collection.InsertOne(objVM);
                    return new Status
                    { Result = "Success", Message = "Employee Details Insert Successfully" };
                }
                #endregion
                ///Update Emoloyeee  
                #region updateDetails  
                else
                {
                    string constr = ConfigurationManager.AppSettings["connectionString"];
                    var Client = new MongoClient(constr);
                    var Db = Client.GetDatabase("employee");
                    var collection = Db.GetCollection<Employee>("EmployeeDetails");
                    var update = collection.FindOneAndUpdateAsync(Builders<Employee>.Filter.Eq("Id", objVM.Id), Builders<Employee>.Update.Set("Name", objVM.Name).Set("Department", objVM.Department).Set("Address", objVM.Address).Set("City", objVM.City).Set("Country", objVM.Country));
                    return new Status
                    { Result = "Success", Message = "Employee Details Update Successfully" };
                }
                #endregion
            }
            catch (Exception ex)
            {
                return new Status
                { Result = "Error", Message = ex.Message.ToString() };
            }
        }

        //Elimina los detalles del empleado.
        #region DeleteEmployee  
        [Route("Delete")]
        [HttpGet]
        public object Delete(string id)
        {
            try
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var Client = new MongoClient(constr);
                var DB = Client.GetDatabase("employee");
                var collection = DB.GetCollection<Employee>("EmployeeDetails");
                var DeleteRecored = collection.DeleteOneAsync(
                               Builders<Employee>.Filter.Eq("Id", id));
                return new Status
                { Result = "Success", Message = "Employee Details Delete  Successfully" };
            }
            catch (Exception ex)
            {
                return new Status
                { Result = "Error", Message = ex.Message.ToString() };
            }
        }
        #endregion

        //Código encargado de obtener los detalles del empleado.
        #region Getemployeedetails  
        [Route("GetAllEmployee")]
        [HttpGet]
        public object GetAllEmployee()
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("employee");
            var collection = db.GetCollection<Employee>("EmployeeDetails").Find(new BsonDocument()).ToList();
            return Json(collection);
        }
        #endregion

        //Código encargado de obtener los detalles de un empleado, dependiendo de su identificador único id.
        #region EmpdetaisById  
        [Route("GetEmployeeById")]
        [HttpGet]
        public object GetEmployeeById(string id)
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("employee");
            var collection = DB.GetCollection<Employee>("EmployeeDetails");
            var plant = collection.Find(Builders<Employee>.Filter.Where(s => s.Id == id)).FirstOrDefault();
            return Json(plant);
        }
        #endregion
    }
}