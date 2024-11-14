Feature: SuccessfulOrder
Orders should be successfully submitted
	
Background:
Given the user is logged in with username 'dharmanshi.sangani@nfocus.co.uk' and password 'mystrongpassword!'
And I add a product to an empty cart
And I am on the cart page

Scenario: Check the coupon code is applied correctly
When I apply the coupon 'edgewords'
Then it should successfully apply '15' percent off
And the total cost is correct

Scenario: Check the order is present in My Orders
When I checkout
Then it should show the order in My Orders