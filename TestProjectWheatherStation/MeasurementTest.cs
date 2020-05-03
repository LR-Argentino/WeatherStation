using WeatherStationNamespace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProjectWheatherStation
{
    
    
    /// <summary>
    ///This is a test class for MeasurementTest and is intended
    ///to contain all MeasurementTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MeasurementTest
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
        ///A test for IsComfortable
        ///</summary>
        [TestMethod()]
        public void IsComfortableTest()
        {
            Measurement target = new Measurement (19, 45 ); 
            Assert.IsFalse(target.IsComfortable);
            target.Temperature = 21.0;
            Assert.IsTrue(target.IsComfortable, "21 Grad und 45% sind komfortabel");
            target.Humidity = 60.1;
            Assert.IsFalse(target.IsComfortable);
            target.Humidity = 50;
            target.Temperature = 20.9;
            Assert.IsFalse(target.IsComfortable);
        }
    }
}
