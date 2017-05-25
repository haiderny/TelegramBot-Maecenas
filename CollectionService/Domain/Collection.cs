using System.Collections.Generic;
using MongoDB.Bson;

namespace CollectionService.Domain
{
    public class Collection
    {
        public string _id { get; set; }
        public bool Status { get; set; }
        public string Target { get; set; }
        public int Donation { get; set; }
        public int Amount { get; set; }
        public double NumberCreditCard { get; set; }
        public string Time { get; set; }
        public List<string> Members { get; set; }

        public Collection(bool status, string target, int donation, string time, double numberCreditCard)
        {
            Amount = 0;
            _id = ObjectId.GenerateNewId().ToString();
            Status = status;
            Target = target;
            Donation = donation;
            Time = time;
            NumberCreditCard = numberCreditCard;
        }
    }
}
