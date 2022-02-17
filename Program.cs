using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBPractice
{
    public class MyClass
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string X { get; set; }
    }

    public class MyClassEqualityComparer : IEqualityComparer<MyClass>
    {
        public bool Equals(MyClass x, MyClass y)
        {
            return x.Year == y.Year;
        }

        public int GetHashCode(MyClass obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
    // Class for model
    //Comments
    //Comments
    //Comments
    //Comments
    [BsonIgnoreExtraElements]
    class Shipwrecks
    {
        [BsonId]
        public ObjectId Id { get; set; }
       
        [BsonElement("feature_type")]
        public string FeatureType { get; set; }
        
        [BsonElement("chart")]
        public string Chart { get; set; }

        [BsonElement("latdec")]
        public double Lattitude { get; set; }

        [BsonElement("londec")]
        public double Longitude { get; set; }

    }

    //Branch 1 changes
    class Program
    {
        static void Main(string[] args)
        {
            var fake = new List<MyClass> {
          new MyClass { ID = 1, Year = 2011, X = "" }
        , new MyClass { ID = 2, Year = 2012, X = "" }
        , new MyClass { ID = 3, Year = 2013, X = "" }
        };

            var real = new List<MyClass> {
          new MyClass { ID = 35, Year = 2011, X = "Information" }
        , new MyClass { ID = 77, Year = 2013, X = "Important" }
        };

            var merged = real.Union(fake, new MyClassEqualityComparer()); // Added
        }

        public static void LoadData()
        {
            var client = new MongoClient("mongodb+srv://vaibhav:test@cluster0.ue8gu.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var database = client.GetDatabase("sample_geospatial");
            var collection = database.GetCollection<Shipwrecks>("shipwrecks");
            var FilterCollection = collection.Find(r => r.FeatureType == "Wrecks - Visible").ToList();

            foreach (Shipwrecks item in FilterCollection)
            {
                Console.WriteLine($"ID:"+ item.Id);
                Console.WriteLine($"FeatureType:" +  item.FeatureType);
                Console.WriteLine($"Lattitude:" + item.Lattitude);
                Console.WriteLine($"Longitude:" +  item.Longitude);
                Console.WriteLine("------------------------------------------------");
            }
        }
    }
}
