using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SeleniumTests
{
    [TestFixture]
    public class AviasalesTests3
    {
        private IWebDriver driver;

        private By departureFieldLocator = By.XPath("//input[@placeholder='Откуда' and @tabindex='1']");
        private By arrivalFieldLocator = By.XPath("//input[@placeholder='Куда' and @tabindex='1']");
        private By departureFieldLocator2 = By.XPath("//input[@placeholder='Откуда' and @tabindex='2']");
        private By arrivalFieldLocator2 = By.XPath("//input[@placeholder='Куда' and @tabindex='2']");
        private By startDateButtonLocator = By.CssSelector("button[data-test-id='multiway-date'][tabindex='1']");
        private By startDateButtonLocator2 = By.CssSelector("button[data-test-id='multiway-date'][tabindex='2']");
        private By dateButtonLocator = By.XPath("//div[@aria-label='Fri Jul 19 2024']");
        private By dateButtonLocator2 = By.XPath("//div[@aria-label='Sat Jul 20 2024']");
        private By findButtonLocator = By.CssSelector("button[data-test-id='form-submit']");
        private By multiWayLocator = By.CssSelector("button[data-test-id='switch-to-multiwayform']");

        [SetUp]
        public void SetUp()
        {
            // Инициализация драйвера
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestFlightSearch()
        {
            // Открытие сайта
            driver.Navigate().GoToUrl("https://www.aviasales.ru/");

            // Нажатие на составление сложного маршрута
            var multiWayButton = driver.FindElement(multiWayLocator);
            multiWayButton.Click();

            // Ввод города отправления 1
            var departureField = driver.FindElement(departureFieldLocator);
            departureField.SendKeys("Амстердам");

            // Ввод города прибытия 1
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

            // Ввод города отправления 2
            var departureField2 = driver.FindElement(departureFieldLocator2);
            departureField2.SendKeys("Санкт-Петербург");
            Thread.Sleep(1000);
            // Ввод города прибытия 2
            var arrivalField2 = driver.FindElement(arrivalFieldLocator2);
            arrivalField2.SendKeys("Омск");
            Thread.Sleep(1000);
            var startDateButton2 = wait.Until(ExpectedConditions.ElementToBeClickable(startDateButtonLocator2));
            startDateButton2.Click();

            // Выбор конкретной даты 2
            var dateButton2 = driver.FindElement(dateButtonLocator2);
            dateButton2.Click();

            // Нажатие кнопки "Найти билеты" 
            var findButton = wait.Until(ExpectedConditions.ElementToBeClickable(findButtonLocator));
            findButton.Click();

            // Ожидание загрузки страницы поиска
            wait.Until(ExpectedConditions.UrlContains("search"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
