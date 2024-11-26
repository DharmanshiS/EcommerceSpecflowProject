Feature: SuccessfulOrder
Orders should be successfully submitted
	
Background:
	Given the user is logged in with username and password
	And the cart is empty
	And I add the 'Beanie' to the cart
	And I am on the cart page

Scenario Outline: Check the coupon code is applied correctly
	When I apply the coupon '<coupon>'
	Then it should successfully apply the discount '<discount>'
	And the total cost is correct

Examples:
	| coupon    | discount |
	| edgewords | 15       |

Scenario: Check the order is present in My Orders
	When I checkout with the billing details
	| firstname | surname | street              | city    | postcode | phone      |
	| D         | S       | e-Innovation Centre | Telford | TF2 9FT  | 0123456789 |
	Then it should show the order in My Orders


