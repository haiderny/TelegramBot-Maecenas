using System.Collections.Generic;

namespace CollectionService.Domain
{
    public class Collection
    {
        public int Id { get; private set; }
        public bool Status { get; private set; }
        public string Target { get; private set; }
        public int Donation { get; private set; }
        public string Time { get; private set; }
        public List<string> Members { get; private set; }

        public Collection(int id, bool status, string target, int donation, string time, List<string> members)
        {
            Id = id;
            Status = status;
            Target = target;
            Donation = donation;
            Time = time;
            Members = members;
        }
    }
}
