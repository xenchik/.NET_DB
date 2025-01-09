namespace NET_DB.DataModel
{
    public class MunicipalityTaxRecord
    {
        public string Municipality { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TaxRate { get; set; } 
        public TaxFrequency Frequency { get; set; } // yearly, monthly, weekly, daily

        private void VerifyArguments(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency taxFrequency)
        {
            if (String.IsNullOrEmpty(municipality)) { throw new Exception("Municipality name can't be empty"); }
            if (endDate < startDate) { throw new Exception("EndDate can't be before StartDate"); }
            if (taxRate <= 0) { throw new Exception("TaxRate can't be zero or negative"); }
        }

        public MunicipalityTaxRecord(string municipality, DateTime startDate, DateTime endDate, decimal taxRate, TaxFrequency taxFrequency) 
        {
            VerifyArguments(municipality, startDate, endDate, taxRate, taxFrequency);
            this.Municipality = municipality;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.TaxRate = taxRate;
            this.Frequency = taxFrequency;
        }
    }
}
