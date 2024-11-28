namespace uk.co.nfocus.EcommerceSpecflowProject.Utilities
{
    class BillingDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }


        public BillingDetails(string firstName, string lastName, string street, string city, string postcode, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Postcode = postcode;
            Phone = phone;
        }
    }
}


