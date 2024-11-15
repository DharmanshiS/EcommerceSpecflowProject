## E-commerce Final Project

### Objective: Develop two end-to-end test that uses SpecFlow and WebDriver.
1) The test will login to an e-commerce site as a registered user, purchase an item of clothing, apply a discount code and check that the total is correct after the discount & shipping is applied.
2) The test will login to an e-commerce site as a registered user, purchase an item of clothing and go through checkout. It will capture the order number and check the order is also present in the ‘My Orders’ section of the site.

### Execution
1) Open terminal at .csproj file
2) "dotnet test"
3) Navigate to bin > debug > net6.0
4) "allure generate --clean" (for allure report)
5) allure open (to open the web server)
