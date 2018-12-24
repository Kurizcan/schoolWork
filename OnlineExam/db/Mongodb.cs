using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace OnlineExam.db
{
    public class Mongodb
    {
        string url = "mongodb://user:password@IP:port/JiaXiao_New?authSource=admin";
        private static IMongoDatabase database = null;
        IMongoCollection<BsonDocument> collection = null;
        public Mongodb()
        {
            var client = new MongoClient(url);
            database = client.GetDatabase("JiaXiao_New");
        }

        public void  GetCollection(string collectionName)
        {
            collection = database.GetCollection<BsonDocument>(collectionName);
            
        }

        public void Insert(BsonDocument document)
        {
            collection.InsertOne(document);
        }

        public BsonDocument Query(string filters, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(filters, value);
            BsonDocument document = collection.Find(filter).First();
            return document;
        }

        public ArrayList GetQuestion_random(int length, int index)
        {
            ArrayList Question = new ArrayList();
            int count = (int)collection.Find(filter: new BsonDocument()).Count();
            Random ran = new Random();
            int skipnum = ran.Next(0, count - length);
            var cursor = collection.Find(new BsonDocument()).Skip(skipnum).Limit(length).ToCursor();
            int i = index;
            foreach (var document in cursor.ToEnumerable())
            {
                document["_id"] = document["_id"].ToString();
                Exam_Question exam_Question = new Exam_Question
                {
                    Index = i.ToString() + "、 ",
                    Id = document["id"].ToString(),
                    Target = document["target"].ToString(),
                    Question = document["question"].ToString(),
                    Type = document["Type"].ToString(),
                    Imageurl = document["imageurl"].ToString(),
                    Sinaimg = document["sinaimg"].ToString(),
                    Bestanswer = document["bestanswer"].ToString(),
                    Bestanswerid = document["bestanswerid"].ToString(),
                    A = document["options"]["a"].ToString(),
                    B = document["options"]["b"].ToString(),
                    C = document["options"]["c"].ToString(),
                    D = document["options"]["d"].ToString()
                };
                i++;
                Question.Add(exam_Question);
            }
            return Question;
        }


    }
}

