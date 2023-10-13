using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCourse;

public class Tests2
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void Test1()
	{

	IWebDriver driver;
	var options = new ChromeOptions();
	options.AddArgument("--start-maximized");
	options.AddArgument("--lang=en");
	options.AddArgument("--locale=en_EN");
	// options.AddArgument("--incognito");
	driver = new ChromeDriver(options);

	// // Creating an instance of Chrome Driver
	// IWebDriver driver = new ChromeDriver();
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