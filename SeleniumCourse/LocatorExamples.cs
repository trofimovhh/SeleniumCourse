using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCourse;

public class LocatorExamples
{
	private IWebDriver driver = new ChromeDriver();

	[Test]
	public void LocatorsTest()
	{
		driver.Navigate().GoToUrl("https://www.seek.co.nz/");
		// Finding "Keywords" input field
		var keywordsInput = driver.FindElement(By.Name("keywords"));
		// Finding "Where" input field
		var whereInput = driver.FindElement(By.Id("SearchBar__Where"));
		// Finding body of HTML page input field
		var body = driver.FindElement(By.TagName("body"));
		// Finding "SEEK" button
		var seekButton = driver.FindElement(By.CssSelector("button[type='submit']"));
		// Finding "Protect yourself online" link
		var protectLink = driver.FindElement(By.LinkText("Protect yourself online"));
		// Finding "Terms & conditions" link
		var termsConditionsLink = driver.FindElement(By.PartialLinkText("conditions"));
		// Finding "Search" button
		var searchButton = driver.FindElement(By.XPath("//*[@id='searchButton']"));
	}

	[Test]
	public void CssLocatorTest()
	{
		driver.Navigate().GoToUrl("https://www.seek.co.nz/");
		// Finding "Profile" link
		var css = 
			driver.FindElement(By.CssSelector("span[data-title='Profile']"));
	}
	
	[Test]
	public void XpathLocatorTest()
	{
		driver.Navigate().GoToUrl("https://www.seek.co.nz/");
		// Finding "Employer site" link
		var xPath = 
			driver.FindElement(By.XPath("//*[@data-automation='employers_link']"));
	}
}