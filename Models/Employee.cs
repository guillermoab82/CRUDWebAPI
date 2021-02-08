using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudWithMongoDB.Models
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String Name { get; set; }
        public String Department { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
    }
}