Feature: Create Boards Validation
	As a Trello user
	I want to have my board protected
	So that I want to call a single endpoint that will create my board only for me

Scenario: Check Create Board With Invalid Name
	Given a request with authorization
	And the request has body params:
	| name | value |
	| name |       |
	When the 'Post' request is sent to '/1/boards' endpoint
	Then the response status code is BadRequest
	And body value has the following values by paths:
	 | path    | expected_value         |
	 | message | invalid value for name |

Scenario Outline: Check Create Board With Invalid Auth
	Given a request without authorization
	And the request has body params:
	| name | value    |
	| name | New item |
	And the request has query params:
	| name  | value   |
	| key   | <key>   |
	| token | <token> |
	When the 'Post' request is sent to '/1/boards' endpoint
	Then the response status code is Unauthorized
	And the response body is equal to '<error_message>'
	Examples: 
	| key                              | token                                                            | error_message                     |
	| fb04999a731923c2e3137153b1ad5de0 |                                                                  | unauthorized permission requested |
	|                                  | b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1 | invalid key                       |
	|                                  |                                                                  | invalid key                       |
