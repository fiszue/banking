namespace Banking.Backend
{
    public class Client
    {
        public long clientId;
        public string firstname;
        public string surename;
        public Address address;
        public string phone;
        public string accountNumber;
        public float amount;

        public Client(long clientId, string firstname, string surename, Address address, string phone, string accountNumber, float amount)
        {
            this.clientId = clientId;
            this.firstname = firstname;
            this.surename = surename;
            this.address = address;
            this.phone = phone;
            this.accountNumber = accountNumber;
            this.amount = amount;
        }

        public string FormattedAmount
        {
            get {
                return amount.ToString("0.00");
            }
        }
    }
}
