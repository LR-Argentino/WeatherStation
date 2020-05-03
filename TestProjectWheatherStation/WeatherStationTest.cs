using WeatherStationNamespace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProjectWheatherStation
{
    
    
    /// <summary>
    ///This is a test class for WeatherStationTest and is intended
    ///to contain all WeatherStationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WeatherStationTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ParseTimeString
        ///</summary>
        [TestMethod()]
        public void ParseTimeStringTest()
        {
            Assert.AreEqual(34, WeatherStation.ParseTimeString("08:30"));
            Assert.AreEqual(0, WeatherStation.ParseTimeString("00:00"));
            Assert.AreEqual(95, WeatherStation.ParseTimeString("23:45"));
            Assert.AreEqual(-1, WeatherStation.ParseTimeString("23:46"), "23:46 ist keine ganze Viertelstunde");
            Assert.AreEqual(-1, WeatherStation.ParseTimeString("24:00"), "24:00 liegt außerhalb des gültigen Bereiches");
            Assert.AreEqual(-1, WeatherStation.ParseTimeString("-1:00"), "-1:00 liegt außerhalb des gültigen Bereiches");
            Assert.AreEqual(-1, WeatherStation.ParseTimeString("10:60"), "10:60 liegt außerhalb des gültigen Bereiches");
            Assert.AreEqual(-1, WeatherStation.ParseTimeString("hallo"), "hallo ist keine gültige Uhrzeit");

        }

        [TestMethod()]
        public void SetOneMeasurementAtPeriodTest()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtPeriod(10, 20, 50));
            Assert.AreEqual(1, target.Count);
        }

        [TestMethod()]
        public void SetMultipleMeasurementAtPeriodTest()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtPeriod(10, 20, 50));
            Assert.IsTrue(target.SetMeasurementAtPeriod(11, 22, 55));
            Assert.IsTrue(target.SetMeasurementAtPeriod(12, 24, 60));
            Assert.AreEqual(3, target.Count);
        }

        [TestMethod()]
        public void SetSameTimeMeasurement()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtPeriod(10, 20, 50));
            Assert.IsTrue(target.SetMeasurementAtPeriod(11, 22, 55));
            Assert.IsTrue(target.SetMeasurementAtPeriod(12, 24, 60));
            Assert.IsTrue(target.SetMeasurementAtPeriod(11, 26, 65));
            Assert.AreEqual(3, target.Count);
        }

        [TestMethod()]
        public void SetOneMeasurementAtTimeTest()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtTime("10:15", 20, 50));
            Assert.AreEqual(1, target.Count);
        }

        /// <summary>
        ///A test for GetAverageAllDay
        ///</summary>
        [TestMethod()]
        public void GetAverageAllDayTest()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtPeriod(10, 20, 50));
            Assert.IsTrue(target.SetMeasurementAtPeriod(11, 22, 55));
            Assert.IsTrue(target.SetMeasurementAtPeriod(12, 24, 60));
            double temp;
            double hum;
            target.GetAverageAllDay(out temp, out hum);
            Assert.AreEqual(22, temp, 0.001, "Durchschnittstemperatur stimmt nicht");
            Assert.AreEqual(55, hum, 0.001, "Durchschnittsluftfeuchte stimmt nicht");
        }

        /// <summary>
        ///A test for GetAverageAllDay
        ///</summary>
        [TestMethod()]
        public void GetAverageWithDuplicates()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtPeriod(10, 20, 50));
            Assert.IsTrue(target.SetMeasurementAtPeriod(11, 22, 55));
            Assert.IsTrue(target.SetMeasurementAtPeriod(12, 24, 60));
            Assert.IsTrue(target.SetMeasurementAtPeriod(10, 26, 65));
            double temp;
            double hum;
            target.GetAverageAllDay(out temp, out hum);
            Assert.AreEqual(24, temp, 0.001, "Durchschnittstemperatur stimmt nicht");
            Assert.AreEqual(60, hum, 0.001, "Durchschnittsluftfeuchte stimmt nicht");
        }

        /// <summary>
        ///A test for CountInInterval
        ///</summary>
        [TestMethod()]
        public void CountInIntervalTest()
        {
            WeatherStation target = new WeatherStation();
            Assert.IsTrue(target.SetMeasurementAtTime("02:30", 20, 50));
            Assert.IsTrue(target.SetMeasurementAtTime("05:00", 22, 55));
            Assert.IsTrue(target.SetMeasurementAtTime("07:30", 24, 60));
            Assert.AreEqual(3, target.CountInInterval("00:00", "23:45"));
            Assert.AreEqual(-1, target.CountInInterval("02:35", "07:30"), "Unzulässige Zeitangabe 00:05");
            Assert.AreEqual(3, target.CountInInterval("02:30", "07:30"), "Genau eingegrenzt");
            Assert.AreEqual(1, target.CountInInterval("02:45", "07:15"), "Rand genau ausgegrenzt");
        }

    }
}
