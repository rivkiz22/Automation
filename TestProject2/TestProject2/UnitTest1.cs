
////using NUnit.Framework;
////using OpenQA.Selenium;
////using OpenQA.Selenium.Chrome;
////using SeleniumProject.PagesObjects;

//namespace TestProject2

//{
//  [TestFixture]
//  public class Tests
//  {

//    private IWebDriver driver;

//    public Tests()
//    { }


//    [SetUp]
//    public void Setup()
//    {
//      string path = "C:\\Users\\user\\Desktop\\אוטומציה\\TestProject2\\TestProject2\\chromedriver";
//      //Creates the ChomeDriver object, Executes tests on Google Chrome

//      driver = new ChromeDriver(path);
//      driver.Manage().Window.Maximize();
//    }

//    [Test]
//    public void Test1()
//    {
//      // Step 1: Navigate to Google
//      driver.Navigate().GoToUrl("https://www.google.com");

//      // Step 2: Verify the title of the page
//      Assert.AreEqual("Google", driver.Title);

//      // Step 3: Find the search box using its name attribute
//      IWebElement searchBox = driver.FindElement(By.Name("q"));

//      // Step 4: Enter the search term and submit the search
//      searchBox.SendKeys("Selenium WebDriver");
//      searchBox.Submit();
//    }
//    [TearDown]
//    public void TearDown()
//    {
//      driver.Dispose();
//    }
//  }
//}
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumProject.PagesObjects;

namespace SeleniumProject
{
  [TestFixture]
  public class GoogleSearchTest
  {
    private IWebDriver driver;
    private GoogleHomePage googleHomePage;
    private GoogleResultsPage googleResultsPage;
    public static IEnumerable<TestData> TestCases => XmlDataReader.ReadTestData("C:\\Users\\user\\Desktop\\אוטומציה\\TestProject2\\TestProject2\\chromedriver\\TestData.xml");


    [OneTimeSetUp]
    public void SetUp()
    {
      string path = "C:\\Users\\user\\Desktop\\אוטומציה\\TestProject2\\TestProject2\\chromedriver";

      //Creates the ChomeDriver object, Executes tests on Google Chrome

      driver = new ChromeDriver(path + @"\drivers\");
      googleHomePage = new GoogleHomePage(driver);
      googleResultsPage = new GoogleResultsPage(driver);
    }

    [Test, TestCaseSource(nameof(TestCases))]

    public void TestGoogleSearch(TestData testData)
    {
      // Navigate to Google
      googleHomePage.NavigateTo();

      // Verify the title of the page
      Assert.AreEqual("Google", driver.Title);

      // Search for a term
      googleHomePage.Search(testData.SearchTerm);

      googleHomePage.Search("Selenium WebDriver");

      // Verify that results are displayed
      //  ClassicAssert.IsTrue(googleResultsPage.ResultsDisplayed());

      // Get the title of the first result and click it
      string firstResultTitle = googleResultsPage.GetFirstResultTitle();
      googleResultsPage.ClickFirstResult();

      // Verify the title of the new page
      //  ClassicAssert.IsTrue(driver.Title.Contains(firstResultTitle));

      // Navigate back to the Google search results page
      driver.Navigate().Back();

      // Verify the search box still contains the search term
      //  ClassicAssert.AreEqual("Selenium WebDriver", driver.FindElement(By.Name("q")).GetAttribute("value"));
    }



    [OneTimeTearDown]
    public void TearDown()
    {
      driver.Dispose();
    }
  }
}
