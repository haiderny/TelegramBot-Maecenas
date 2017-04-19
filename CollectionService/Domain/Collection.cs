using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectionService.Domain
{
    public class Collection
    {
        public string _id { get; set; }
        public bool Status { get; private set; }
        public string Target { get; private set; }
        public int Donation { get; private set; }
        public string Time { get; private set; }
        public List<string> Members { get; set; }

        public Collection(bool status, string target, int donation, string time)
        {
            _id = ObjectId.GenerateNewId().ToString();
            Status = status;
            Target = target;
            Donation = donation;
            Time = time;
        }
    }
}
