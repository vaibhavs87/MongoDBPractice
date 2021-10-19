using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBPractice
{
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


    class Program
    {
        static void Main(string[] args)
        {

            LoadData();
            Console.ReadLine();
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
