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

        private By departureFieldLocator = By.XPath("//input[@placeholder='Откуда']");
        private By arrivalFieldLocator = By.XPath("//input[@placeholder='Куда']");
        private By startDateButtonLocator = By.CssSelector("button[data-test-id='start-date-field']");
        private By dateButtonLocator = By.XPath("//div[@aria-label='Fri Jul 19 2024']");
        private By oneWayButtonLocator = By.CssSelector("button[data-test-id='calendar-action-button']");
        private By findButtonLocator = By.CssSelector("button[data-test-id='form-submit']");

        [SetUp]
        public void SetUp()
        {
            // Инициализация драйвера
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestFlightSearch()
        {
            // Открытие сайта
            driver.Navigate().GoToUrl("https://www.aviasales.ru/");

            // Ввод города отправления
            var departureField = driver.FindElement(departureFieldLocator);
            departureField.SendKeys("Омск");

            // Ввод города прибытия
            var arrivalField = driver.FindElement(arrivalFieldLocator);
            arrivalField.SendKeys("Санкт-Петербург");
            Thread.Sleep(3000);

            // Ожидание и выбор даты вылета
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var startDateButton = wait.Until(ExpectedConditions.ElementToBeClickable(startDateButtonLocator));
            startDateButton.Click();

            // Выбор конкретной даты
            var dateButton = driver.FindElement(dateButtonLocator);
            dateButton.Click();

            // Нажатие на кнопку "Обратный билет не нужен"
            var oneWayButton = driver.FindElement(oneWayButtonLocator);
            oneWayButton.Click();

            // Нажатие кнопки "Найти билеты"
            var findButton = wait.Until(ExpectedConditions.ElementToBeClickable(findButtonLocator));
            findButton.Click();

            // Ожидание загрузки страницы поиска
            wait.Until(ExpectedConditions.UrlContains("search"));
        }

        [TearDown]
        public void TearDown()
        {
            // Закрытие браузера
            driver.Quit();
        }
    }
}
