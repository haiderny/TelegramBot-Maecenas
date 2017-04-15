using System.Collections.Generic;

namespace CollectionService.Domain
{
    public class Collection
    {
        public bool Status { get; private set; }
        public string Target { get; private set; }
        public int Donation { get; private set; }
        public string Time { get; private set; }
        public List<string> Members { get; private set; }

        public Collection(bool status, string target, int donation, string time, List<string> members)
        {
            Status = status;
            Target = target;
            Donation = donation;
            Time = time;
            Members = members;
        }
    }
}
