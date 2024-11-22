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
            _scenarioContext = scenarioContext; 
            _driver = webDriverWrapper.Driver;

        }

        [Given(@"the user is logged in with username and password")]
        public void GivenTheUserIsLoggedInWithUsernameAndPassword()
        {
            LoginPage LoginPagePOM = new LoginPage(_driver);
            LoginPagePOM.ClickDismiss(); //remove the warning
            LoginPagePOM.Login("dharmanshi.sangani@nfocus.co.uk", "mystrongpassword!"); //log in with capture group details from Feature File
            Console.WriteLine("Attempted correct login details");

            AccountPage AccountPagePOM = new AccountPage(_driver);
            Assert.That(AccountPagePOM.GetAccountTitle(), Does.Contain("My account"), "Login was not successful."); //Check that login was successful
            Console.WriteLine("Successfully logged in.");
        }

        [Given(@"the cart is empty")]
        public void GivenTheCartIsEmpty()
        {
            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.HoverOverCartSymbol();

            if (!NavigationBarPOM.GetCartEmptyMessage().Contains("No products in the cart."))
            {
                CartPage CartPagePOM = new CartPage(_driver);
                CartPagePOM.DeleteAllItems();
                Console.WriteLine("Deleted all the items.");
            }

            NavigationBarPOM.HoverOverCartSymbol();
            Assert.That(NavigationBarPOM.GetCartEmptyMessage(), Does.Contain("No products in the cart"), "The cart is not empty yet."); //Check that the cart is empty
            Console.WriteLine("Cart is empty.");
        }

        [Given(@"I add the '([^']*)' to the cart")]
        public void GivenIAddTheToTheCart(string product)
        {
            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.NavigateToShop();
            Console.WriteLine("Successfully on the Shop Page.");

            ShopPage ShopPagePOM = new ShopPage(_driver);
            ShopPagePOM.AddProductToCart(product);
            Console.WriteLine("Successfully added first product to the cart.");
        }

        [Given(@"I am on the cart page")]
        public void GivenIAmOnTheCartPage()
        {
            NavigationBar NavigationBarPOM = new NavigationBar(_driver);
            NavigationBarPOM.ViewCartFromSymbol();
            Console.WriteLine("Successfully navigated to the cart page.");
        }



        //Test case 1

        [When(@"I apply the coupon '([^']*)'")]
        public void WhenIApplyTheCoupon(string coupon)
        {
            CartPage CartPagePOM = new CartPage(_driver);
            Console.WriteLine(coupon);
            CartPagePOM.AddCoupon(coupon);
            bool CouponCondition = CartPagePOM.GetCouponSuccessMessage().Contains("Coupon code applied successfully.");
            Assert.That(CouponCondition, Is.True, "Coupon code was not applied successfully."); // Assert that the coupon condition is true
            Console.WriteLine("Successfully applied the coupon.");
        }


        [Then(@"it should successfully apply the discount '([^']*)'")]
        public void ThenItShouldSuccessfullyApplyTheDiscount(int requiredPercentOff)
        {
            CartPage CartPagePOM = new CartPage(_driver);
            decimal Subtotal = CartPagePOM.GetCartSubtotal();
            decimal Discount = CartPagePOM.GetCartTotalCouponDiscount();
            decimal ExpectedDiscount = Subtotal * requiredPercentOff / 100;
            decimal ActualPercentOff = (Discount / Subtotal) * 100;
            Assert.That(Discount, Is.EqualTo(ExpectedDiscount),
                $"The discount applied is not {requiredPercentOff}%. The actual discount applied is {ActualPercentOff:F2}%.");
            Console.WriteLine($"The coupon has taken off {ActualPercentOff:F2} percent.");
        }

        [Then(@"the total cost is correct")]
        public void ThenTheTotalCostIsCorrect()
        {
            CartPage CartPagePOM = new CartPage(_driver);
            Assert.That(CartPagePOM.GetCartTotal(), Is.EqualTo(CartPagePOM.GetCartSubtotal() + CartPagePOM.GetCartTotalShipping() - CartPagePOM.GetCartTotalCouponDiscount()));
            Console.WriteLine("The cart total is equal to the sum of the subtotal and delivery, minus discount");
        }



        //Test Case 2

        [When(@"I checkout with the billing details")]
        public void WhenICheckoutWithTheBillingDetails(Table details)
        {
            CartPage CartPagePOM = new CartPage(_driver);
            CartPagePOM.Checkout();
            Console.WriteLine("Successfully pressed 'checkout'.");

            CheckoutPage CheckoutPagePOM = new CheckoutPage(_driver);
            BillingDetails billingDetails = new BillingDetails(
                details.Rows[0]["firstname"],
                details.Rows[0]["surname"],
                details.Rows[0]["street"],
                details.Rows[0]["city"],
                details.Rows[0]["postcode"],
                details.Rows[0]["phone"]
                );  //billing details object
            CheckoutPagePOM.FillBillingDetails(billingDetails);
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















