namespace DonationMessegeBuilder.Application
{
    public interface IMessageBuilder
    {
        void BuildTarget(string target);
        void BuildDonation(string donation);
        void BuildTime(string time);
        string GetResult();
    }
}
