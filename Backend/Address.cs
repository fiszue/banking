namespace Banking.Backend
{
    public class Address
    {
        public string street;
        public string houseNumber;
        public string city;
        public string postalCode;
        public string country;

        public Address(string street, string houseNumber, string city, string postalCode, string country)
        {
            this.street = street;
            this.houseNumber = houseNumber;
            this.city = city;
            this.postalCode = postalCode;
            this.country = country;
        }

        public string FormattedAddress {
            get
            {
                return $"{street} {houseNumber} - {postalCode} {city} - {country}";
            }
        }
    }
}
