using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCourse;

public class Tests
{
	private IWebDriver driver;
	[SetUp]
	public void Setup()
	{
		// Creating an instance of Chrome Driver
		driver = new ChromeDriver();
	}
	[TearDown]
	public void CleanUp()
	{
		// Closing the browser
		driver.Quit();
		driver.Dispose();
	}
	[Test]
	public void Test1()
	{
		//Opening a Google page
		driver.Navigate().GoToUrl("https://www.google.com/");
		// Search for a Search Bar element by name
		IWebElement element = driver.FindElement(By.Name("q"));
		// Entering text into the search bar
		element.SendKeys("Whitireia and Weltec");
		// Submitting a form to search 
		element.Submit();
	}
}