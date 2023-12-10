Feature: Delete Board Validation
  As a Trello API user
  I want to delete my board safely
  So that I want to delete board endpoint to allow delete only with valid request

  Scenario Outline: Check Delete Board With Invalid Id
    Given a request with authorization
    And the request has path params:
      | name | value |
      | id   | <id>  |
    When the 'DELETE' request is sent to 'DeleteABoard' endpoint
    Then the response status code is <status_code>
    And the response body is equal to '<error_message>'
    Examples:
      | id                       | status_code | error_message                         |
      | invalid                  | BadRequest  | invalid id                            |
      | 60d847d9aad2437cb984f8e1 | NotFound    | The requested resource was not found. |

  Scenario Outline: Check Delete Board with Invalid Auth
    Given a request without authorization
    And the request has query params:
      | name  | value   |
      | key   | <key>   |
      | token | <token> |
    And the request has path params:
      | name | value                    |
      | id   | 6288cc5d3ce8fc87542dff31 |
    When the 'DELETE' request is sent to 'DeleteABoard' endpoint
    Then the response status code is Unauthorized
    And the response body is equal to '<error_message>'
    Examples:
      | key              | token              | error_message                     |
      | empty_value      | empty_value        | invalid key                       |
      | current_user_key | empty_value        | unauthorized permission requested |
      | empty_value      | current_user_token | invalid key                       |
      | another_user_key | another_user_token | invalid token                     |