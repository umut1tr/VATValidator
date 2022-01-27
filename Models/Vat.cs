namespace VATValidator.Models
{
    public class Vat
    {
        public string VatNumber { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool isVatValid { get; set; } = false;

        public Vat()
        {
        }
    }
}