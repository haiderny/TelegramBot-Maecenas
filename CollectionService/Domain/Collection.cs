using System.Collections.Generic;
using MongoDB.Bson;

namespace CollectionService.Domain
{
    public class Collection
    {
        public string Id { get; set; }
        public bool Status { get; private set; }
        public string Target { get; private set; }
        public int Donation { get; private set; }
        public string Time { get; private set; }
        public List<string> Members { get; set; }

        public Collection(bool status, string target, int donation, string time)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Status = status;
            Target = target;
            Donation = donation;
            Time = time;
        }
    }
}
