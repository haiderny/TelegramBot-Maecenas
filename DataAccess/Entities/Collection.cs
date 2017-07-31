using System.Collections.Generic;
using MongoDB.Bson;

namespace DataAccess.Entities
{
    public class Collection
    {
        public string _id { get; set; }
        public bool Status { get; set; }
        public string Target { get; set; }
        public int Donation { get; set; }
        public int Amount { get; set; }
        public ulong NumberCreditCard { get; set; }
        public string Time { get; set; }
        public List<string> Members { get; set; }

        public Collection(string target, int donation, string time, ulong numberCreditCard)
        {
            Amount = 0;
            _id = ObjectId.GenerateNewId().ToString();
            Status = true;
            Target = target;
            Donation = donation;
            Time = time;
            NumberCreditCard = numberCreditCard;
        }
    }
}
