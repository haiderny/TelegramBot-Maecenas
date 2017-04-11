using DonationMessegeBuilder.Application;

namespace DonationMessegeBuilder.Domain
{
    public class MessageBuilder : IMessageBuilder
    {
        private string _messageDonation;

        public void BuildTarget(string target)
        {
            _messageDonation = "Цель пожертвования: " + target;
        }

        public void BuildDonation(string donation)
        {
            _messageDonation = _messageDonation + "\n" + "Сумма пожертвования: " + donation;
        }

        public void BuildTime(string time)
        {
            _messageDonation = _messageDonation + "\n" + "Сроки пожертвования: " + time;
        }

        public string GetResult()
        {
            return _messageDonation + "\n" +"\U0001F44D \U0001F44D \U0001F44D \U0001F44D \U0001F44D \U0001F44D \U0001F44D \U0001F44D \U0001F44D \U0001F44D" + " 100%";

        }
    }
}
//\U0001F44D