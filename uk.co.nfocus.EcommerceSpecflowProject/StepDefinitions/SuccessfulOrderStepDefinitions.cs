using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.EcommerceSpecflowProject.POMs;
using uk.co.nfocus.EcommerceSpecflowProject.Utilities;

namespace uk.co.nfocus.EcommerceSpecflowProject.StepDefinitions
{
    [Binding]
    public class SuccessfulOrderStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public SuccessfulOrderStepDefinitions(WebDriverWrapper webDriverWrapper, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext; //stpres the relevant variables that we need
            //_driver = (IWebDriver)_scenarioContext["webdriver"]; //naive method to pass around the webdriver
            _driver = webDriverWrapper.Driver;

        }


        [Given(@"the user is logged in with username '([^']*)' and password '([^']*)'")]
        public void GivenTheUserIsLoggedInWithUsernameAndPassword(string username, string password)
        {
            LoginPage LoginPagePOM = new LoginPage(_driver);
            LoginPagePOM.ClickDismiss(); //remove the warning
            LoginPagePOM.Login(username, password); //log in with capture group details from Feature File
            Console.WriteLine("Attempted correct login details");

            AccountPage AccountPagePOM = new AccountPage(_driver);
            Assert.That(AccountPagePOM.AccountTitle, Does.Contain("My account"), "Login was not successful."); //Check that login was successful
            Console.WriteLine("Successfully logged in.");
        }

        [Given(@"I add a product to an empty cart")]
        public void GivenIAddAProductToAnEmptyCart()
        {
            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.NavigateToShop();
            Console.WriteLine("Successfully on the Shop Page.");

            ShopPage ShopPagePOM = new ShopPage(_driver);
            ShopPagePOM.HoverOverCartSymbol();
            //Assert.That(ShopPagePOM.CartMessage, Does.Contain("No products in the cart"), "The cart is not empty."); //Check that the cart is empty
            //Console.WriteLine("Cart is empty.");

            if (!ShopPagePOM.CartMessage.Contains("No products in the cart"))
            {
                CartPage CartPagePOM = new CartPage(_driver);
                CartPagePOM.DeleteAllItems();
                Console.WriteLine("Deleted all the items.");
            }
            Console.WriteLine("Cart is now empty.");

            ShopPagePOM.AddToCart();
            Console.WriteLine("Successfully added first product to the cart.");

        }

        [Given(@"I am on the cart page")]
        public void GivenIAmOnTheCartPage()
        {
            ShopPage ShopPagePOM = new ShopPage(_driver);
            ShopPagePOM.ViewCart();
            Console.WriteLine("Successfully navigated to the cart page.");
        }



        //Test case 1



        [When(@"I apply the coupon '([^']*)'")]
        public void WhenIApplyTheCoupon(string coupon)
        {
            CartPage CartPagePOM = new CartPage(_driver);
            CartPagePOM.AddCoupon(coupon);
            bool CouponCondition = CartPagePOM.CouponSuccessMessage.Contains("Coupon code applied successfully.");
            Assert.That(CouponCondition, Is.True, "Coupon code was not applied successfully."); // Assert that the coupon condition is true
            Console.WriteLine("Successfully applied the coupon.");

            
        }

        [Then(@"it should successfully apply '([^']*)' percent off")]
        public void ThenItShouldSuccessfullyApplyPercentOff(string percent)
        {
            CartPage CartPagePOM = new CartPage(_driver);
            decimal subtotal = CartPagePOM.CartTotalSubtotal;
            decimal discount = CartPagePOM.CartTotalCouponDiscount;
            decimal expectedDiscount = subtotal * 0.15m;
            Assert.That(discount, Is.EqualTo(expectedDiscount), "The discount applied is not 15 percent.");
            Console.WriteLine("The coupon has taken off 15 percent.");
        }

        [Then(@"the total cost is correct")]
        public void ThenTheTotalCostIsCorrect()
        {
            CartPage CartPagePOM = new CartPage(_driver);
            Assert.That(CartPagePOM.CartTotalTotal, Is.EqualTo(CartPagePOM.CartTotalSubtotal + CartPagePOM.CartTotalShipping - CartPagePOM.CartTotalCouponDiscount));
            Console.WriteLine("The cart total is equal to the sum of the subtotal and delivery, minus discount");
        }



        //Test Case 2



        [When(@"I checkout")]
        public void WhenICheckout()
        {
            CartPage CartPagePOM = new CartPage(_driver);
            CartPagePOM.Checkout();
            Console.WriteLine("Successfully pressed 'checkout'.");

            CheckoutPage CheckoutPagePOM = new CheckoutPage(_driver);
            CheckoutPagePOM.FillBillingDetails("d", "s", "abc", "def", "IG5 0QL", "0123456789");
            Console.WriteLine("Successfully filled in the billing details.");
            CheckoutPagePOM.PlaceOrder();
            Console.WriteLine("Successfully placed the order.");

            OrderReceivedPage OrderReceivedPagePOM = new OrderReceivedPage(_driver);
            _scenarioContext["OrderNumberOnOrderReceivedPage"] = OrderReceivedPagePOM.GetOrderNumber();
            Console.WriteLine("Successfully captured the order number onthe Order Received page.");
        }

        [Then(@"it should show the order in My Orders")]
        public void ThenItShouldShowTheOrderInMyOrders()
        {
            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.NavigateToMyAccount();
            NavigationBarPOM.NavigateToOrders();
           
            OrdersPage OrdersPagePOM = new OrdersPage(_driver);
            string OrderNumberOnOrdersPage = OrdersPagePOM.GetLatestOrderNumber();
            Assert.That(OrderNumberOnOrdersPage, Is.EqualTo(_scenarioContext["OrderNumberOnOrderReceivedPage"]), "Order numbers do not match from Order Received page and Orders Page");
            Console.WriteLine("The order number given at Order Received matches with My Orders.");
        }
    }
}
