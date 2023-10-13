using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCourse;

public class ActionsOnElements
{
	private IWebDriver driver;

	private ExtentReports extent;

	[OneTimeSetUp]
	public void OneTimeSetUp()
	{
		extent = new ExtentReports();
		var spark = new ExtentSparkReporter(
			@"C:\Users\user\RiderProjects\SeleniumCourse\SeleniumCourse\Reports\Report.html");
		extent.AttachReporter(spark);
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		extent.Flush();
	}

	private ExtentTest test;

	[SetUp]
	public void SetUp()
	{
		test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
		driver = new ChromeDriver();
	}

	[TearDown]
	public void TearDown()
	{
		driver.Quit();
		var status = TestContext.CurrentContext.Result.Outcome.Status;
		var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
			? ""
			: string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
		Status logstatus;
		switch (status)
		{
			case TestStatus.Failed:
				logstatus = Status.Fail;
				break;
			default:
				logstatus = Status.Pass;
				break;
		}

		test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
	}

	[Test]
	public void ClickTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/buttons");
		var clickMeButton = driver.FindElement(By.XPath("//button[text()='Click Me']"));
		clickMeButton.Click();
		var clickMessageText = driver.FindElement(By.Id("dynamicClickMessage")).Text;
		Assert.AreEqual(clickMessageText, "You have done a dynamic click");
	}

	[Test]
	public void TextInput()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/text-box");
		driver.FindElement(By.Id("userName")).SendKeys("Alex Smith");
		driver.FindElement(By.Id("userEmail")).SendKeys("alex_smith@whitireia.nz");
		driver.FindElement(By.Id("currentAddress")).SendKeys("Te Auaha Campus");
		driver.FindElement(By.Id("permanentAddress")).SendKeys("Petone Campus");
		new Actions(driver).ScrollByAmount(0, 250).Build().Perform();
		driver.FindElement(By.Id("submit")).Click();
		var outputText = driver.FindElement(By.Id("output")).Text;
		Assert.That(outputText.Contains("Alex Smith"));
		Assert.That(outputText.Contains("alex_smith@whitireia.nz"));
		Assert.That(outputText.Contains("Te Auaha Campus"));
		Assert.That(outputText.Contains("Petone Campus"));
	}

	[Test]
	public void ClearTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/text-box");
		driver.FindElement(By.Id("userName")).SendKeys("Alex Smith");
		driver.FindElement(By.Id("userEmail")).SendKeys("alex_smith@whitireia.nz");
		driver.FindElement(By.Id("currentAddress")).SendKeys("Te Auaha Campus");
		driver.FindElement(By.Id("permanentAddress")).SendKeys("Petone Campus");
		new Actions(driver).ScrollByAmount(0, 250).Build().Perform();
		driver.FindElement(By.Id("submit")).Click();
		var outputText = driver.FindElement(By.Id("output")).Text;
		Assert.That(outputText.Contains("Alex Smith"));
		Assert.That(outputText.Contains("alex_smith@whitireia.nz"));
		Assert.That(outputText.Contains("Te Auaha Campus"));
		Assert.That(outputText.Contains("Petone Campus"));
		driver.FindElement(By.Id("userName")).Clear();
		driver.FindElement(By.Id("userEmail")).Clear();
		driver.FindElement(By.Id("userName")).SendKeys("John Doe");
		driver.FindElement(By.Id("userEmail")).SendKeys("john_doe@whitireia.nz");
		driver.FindElement(By.Id("submit")).Click();
		outputText = driver.FindElement(By.Id("output")).Text;
		Assert.That(outputText.Contains("John Doe"));
		Assert.That(outputText.Contains("john_doe@whitireia.nz"));
		Assert.That(outputText.Contains("Te Auaha Campus"));
		Assert.That(outputText.Contains("Petone Campus"));
	}

	[Test]
	public void DragAndDropTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/droppable");
		var dragElement = driver.FindElement(By.Id("draggable"));
		var dropArea = driver.FindElement(By.Id("droppable"));
		Assert.That(dropArea.Text.Contains("Drop here"));
		new Actions(driver)
			.ClickAndHold(dragElement)
			.MoveToElement(dropArea)
			.Release(dragElement)
			.Build()
			.Perform();
		Assert.That(dropArea.Text.Contains("Dropped!"));
	}

	[Test]
	public void SelectTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/select-menu");
		var selectMenu = driver.FindElement(By.Id("oldSelectMenu"));
		var colourSelector = new SelectElement(selectMenu);
		Assert.That(colourSelector.SelectedOption.Text.Equals("Red"));
		colourSelector.SelectByValue("1");
		Assert.That(colourSelector.SelectedOption.Text.Equals("Blue"));
		colourSelector.SelectByIndex(4);
		Assert.That(colourSelector.SelectedOption.Text.Equals("Purple"));
		colourSelector.SelectByText("Black");
		Assert.That(colourSelector.SelectedOption.Text.Equals("Black"));
	}

	[Test]
	public void DatePickerTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/date-picker");
		var datePicker = driver.FindElement(By.Id("datePickerMonthYearInput"));
		var today = DateTime.Today.ToString("MM'/'dd'/'yyyy");
		Assert.That(datePicker.GetAttribute("value").Equals(today));
		var newDate = DateTime.Today.AddDays(7).ToString("MM'/'dd'/'yyyy");
		var script = $"arguments[0].value = '{newDate}';";
		((IJavaScriptExecutor)driver).ExecuteScript(script, datePicker);
		Assert.That(datePicker.GetAttribute("value").Equals(newDate));
	}

	[Test]
	public void AlertTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/alerts");
		var confirmButton = driver.FindElement(By.Id("confirmButton"));
		confirmButton.Click();
		var alert = driver.SwitchTo().Alert();
		Assert.That(alert.Text.Equals("Do you confirm action?"));
		alert.Accept();
		var confirmResult = driver.FindElement(By.Id("confirmResult"));
		Assert.That(confirmResult.Text.Equals("You selected Ok"));
		var frameElement = driver.FindElement(By.TagName("iframe"));
		frameElement.Click();
	}

	[Test]
	public void FrameTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/frames");
		var frameElement = driver.FindElement(By.Id("frame1"));
		driver.SwitchTo().Frame(frameElement);
		var frameHeading = driver.FindElement(By.Id("sampleHeading"));
		Assert.That(frameHeading.Text.Equals("This is a sample page"));
		driver.SwitchTo().DefaultContent();
		var framesWrapper = driver.FindElement(By.Id("framesWrapper"));
		Assert.That(framesWrapper.Text.Contains("Sample Iframe page"));
	}

	[Test]
	public void WindowTest()
	{
		driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");
		var currentWindow = driver.CurrentWindowHandle;
		var windowButton = driver.FindElement(By.Id("windowButton"));
		windowButton.Click();
		var otherWindows = driver.WindowHandles.Where(x => !x.Equals(currentWindow));
		driver.SwitchTo().Window(otherWindows.First());
		var newWindowHeading = driver.FindElement(By.Id("sampleHeading"));
		Assert.That(newWindowHeading.Text.Equals("This is a sample page"));
	}

	[Test]
	public void UploadTest()
	{
		driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/upload");
		var uploadPath = driver.FindElement(By.Id("file-upload"));
		var filePath = Path.Combine(Environment.CurrentDirectory, "SampleFile.docx");
		uploadPath.SendKeys(filePath);
		var fileSubmitButton = driver.FindElement(By.Id("file-submit"));
		fileSubmitButton.Click();
		var uploadedFiles = driver.FindElement(By.Id("uploaded-files"));
		Assert.That(uploadedFiles.Text.Equals("SampleFile.docx"));
	}

	[Test]
	public void ExplicitWaitTest()
	{
		driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/dynamic_loading/2");
		var explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
		var start = driver.FindElement(By.TagName("button"));
		start.Click();
		var finish = explicitWait
			.Until(
				SeleniumExtras
					.WaitHelpers
					.ExpectedConditions
					.ElementExists(By.Id("finish")));
		Assert.That(finish.Text.Equals("Hello World!"));
	}
}