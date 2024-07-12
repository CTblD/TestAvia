using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SeleniumTests
{
    [TestFixture]
    public class AviasalesTests
    {
        private IWebDriver driver;

        private By departureFieldLocator = By.XPath("//input[@placeholder='������']");
        private By arrivalFieldLocator = By.XPath("//input[@placeholder='����']");
        private By startDateButtonLocator = By.CssSelector("button[data-test-id='start-date-field']");
        private By dateButtonLocator = By.XPath("//div[@aria-label='Fri Jul 19 2024']");
        private By oneWayButtonLocator = By.CssSelector("button[data-test-id='calendar-action-button']");
        private By findButtonLocator = By.CssSelector("button[data-test-id='form-submit']");

        [SetUp]
        public void SetUp()
        {
            // ������������� ��������
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestFlightSearch()
        {
            // �������� �����
            driver.Navigate().GoToUrl("https://www.aviasales.ru/");

            // ���� ������ �����������
            var departureField = driver.FindElement(departureFieldLocator);
            departureField.SendKeys("����");

            // ���� ������ ��������
            var arrivalField = driver.FindElement(arrivalFieldLocator);
            arrivalField.SendKeys("�����-���������");
            Thread.Sleep(3000);

            // �������� � ����� ���� ������
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var startDateButton = wait.Until(ExpectedConditions.ElementToBeClickable(startDateButtonLocator));
            startDateButton.Click();

            // ����� ���������� ����
            var dateButton = driver.FindElement(dateButtonLocator);
            dateButton.Click();

            // ������� �� ������ "�������� ����� �� �����"
            var oneWayButton = driver.FindElement(oneWayButtonLocator);
            oneWayButton.Click();

            // ������� ������ "����� ������"
            var findButton = wait.Until(ExpectedConditions.ElementToBeClickable(findButtonLocator));
            findButton.Click();

            // �������� �������� �������� ������
            wait.Until(ExpectedConditions.UrlContains("search"));
        }

        [TearDown]
        public void TearDown()
        {
            // �������� ��������
            driver.Quit();
        }
    }
}
