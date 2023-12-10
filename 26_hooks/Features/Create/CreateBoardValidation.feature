Feature: Create Board Validation
  As a Trello API user
  I want to create my board safely
  So that I want to create board endpoint to allow create only with valid request

  Scenario: Check Create Board With Invalid Name
    Given a request with authorization
    And the request has body params:
      | name | value |
      | name |       |
    When the 'POST' request is sent to 'CreateABoard' endpoint
    Then the response status code is BadRequest
    And body value has the following values by paths:
      | path    | expected_value         |
      | message | invalid value for name |
      | error   | ERROR                  |

  Scenario Outline: Check Create Board with Invalid Auth
    Given a request without authorization
    And the request has body params:
      | name | value     |
      | name | New Board |
    And the request has query params:
      | name  | value   |
      | key   | <key>   |
      | token | <token> |
    When the 'POST' request is sent to 'CreateABoard' endpoint
    Then the response status code is Unauthorized
    And the response body is equal to '<error_message>'
    Examples:
      | key              | token              | error_message                     |
      | empty_value      | empty_value        | invalid key                       |
      | current_user_key | empty_value        | unauthorized permission requested |
      | empty_value      | current_user_token | invalid key                       |