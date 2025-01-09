using NET_DB.DataModel;
using NET_DB.Services;

namespace NET_DB.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public ProducerService? producerService;

        [TestInitialize]
        public void Initialize()
        {
            producerService = new ProducerService();

            DateTime startDate1 = DateTime.Parse("2020-01-01");
            DateTime endDate1 = DateTime.Parse("2020-12-31");
            var testRecord1 = new MunicipalityTaxRecord("TestMunicipality", startDate1, endDate1, 1.0M, TaxFrequency.Yearly);

            DateTime startDate2 = DateTime.Parse("2020-01-01");
            DateTime endDate2 = DateTime.Parse("2020-01-31");
            var testRecord2 = new MunicipalityTaxRecord("TestMunicipality", startDate2, endDate2, 2.0M, TaxFrequency.Daily);

            Database.AddTaxRecord(testRecord1);
            Database.AddTaxRecord(testRecord2);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Database._taxes.Clear();
        }

        [TestMethod]
        public void TestInitialDatabaseSize()
        {
            Assert.AreEqual(2, Database._taxes.Count);
        }

        [TestMethod]
        public void TestCanAddRecordToDatabase()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-1);
            DateTime endDate = DateTime.UtcNow;
            var newTaxRecord = new MunicipalityTaxRecord("TestMunicipalicy", startDate, endDate, 1.0M, TaxFrequency.Daily);
            Database.AddTaxRecord(newTaxRecord);

            Assert.AreEqual(3, Database._taxes.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "EndDate can't be before StartDate")]
        public void TestThrowsOnEndDateBeforeStartDate()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(1);
            DateTime endDate = DateTime.UtcNow;
            var newTaxRecord = new MunicipalityTaxRecord("TestMunicipalicy", startDate, endDate, 1.0M, TaxFrequency.Daily);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Municipality name can't be empty")]
        public void TestThrowsOnNullOrEmptyMunicipalityName()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-1);
            DateTime endDate = DateTime.UtcNow;
            var newTaxRecord = new MunicipalityTaxRecord("", startDate, endDate, 1.0M, TaxFrequency.Daily);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Tax record with given arguments already exist.")]
        public void TestThrowsOnTryingToAddDuplicateTaxRecord()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-1);
            DateTime endDate = DateTime.UtcNow;
            var newTaxRecord = new MunicipalityTaxRecord("TestMunicipalicy", startDate, endDate, 1.0M, TaxFrequency.Daily);
            
            Database.AddTaxRecord(newTaxRecord);
            Database.AddTaxRecord(newTaxRecord);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Tax record with given arguments does not exist.")]
        public void TestThrowsOnTryingToRemoveNonExistingTaxRecord()
        {
            DateTime startDate = DateTime.UtcNow.AddDays(-1);
            DateTime endDate = DateTime.UtcNow;
            var newTaxRecord = new MunicipalityTaxRecord("TestMunicipalicy", startDate, endDate, 1.0M, TaxFrequency.Daily);

            Database.RemoveTaxRecord(newTaxRecord);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestThrowsOnGettingTaxRateWithNonExistingMunicipality()
        {
            DateTime date = DateTime.UtcNow;
            Database.GetTaxRateOnDate("abc", date);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestThrowsOnGettingTaxRateOutsideExistingDatePeriod()
        {
            DateTime date = DateTime.UtcNow;
            Database.GetTaxRateOnDate("TestMunicipality", date);
        }

        [TestMethod]
        public void TestGetDailyTaxRateWhenYearlyExistInSamePeriod()
        {
            DateTime date = DateTime.Parse("2020-01-01");
            var taxRate = Database.GetTaxRateOnDate("TestMunicipality", date);

            Assert.AreEqual(2.0M, taxRate);
        }
    }
}