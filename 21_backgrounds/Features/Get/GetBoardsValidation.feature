﻿Feature: Get Boards Validation
  As a Trello API user
  I want to have my board protected
  So that I want to call a single endpoint that will return my board only for me

  Scenario Outline: Check Get Board with Invalid Id
    Given a request with authorization
    And the request has path params:
      | name | value      |
      | id   | <id_value> |
    When the 'GET' request is sent to '/1/boards/{id}' endpoint
    Then the response status code is <status_code>
    And the response body is equal to '<error_message>'
    Examples:
      | id_value                 | status_code | error_message                         |
      | invalid                  | BadRequest  | invalid id                            |
      | 60d847d9aad2437cb984f8e1 | NotFound    | The requested resource was not found. |

  Scenario Outline: Check Get Board with Invalid Auth
    Given a request without authorization
    And the request has query params:
     | name  | value   |
     | key   | <key>   |
     | token | <token> |
    And the request has path params:
      | name | value                    |
      | id   | 6288cc5d3ce8fc87542dff31 |
    When the 'GET' request is sent to '/1/boards/{id}' endpoint
    Then the response status code is Unauthorized
    And the response body is equal to '<error_message>'
    Examples:
      | key                              | token                                                            | error_message                     |
      |                                  |                                                                  | unauthorized permission requested |
      | fb04999a731923c2e3137153b1ad5de0 |                                                                  | unauthorized permission requested |
      |                                  | b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1 | invalid app key                   |
      | 8b32218e6887516d17c84253faf967b6 | 492343b8106e7df3ebb7f01e219cbf32827c852a5f9e2b8f9ca296b1cc604955 | invalid token                     |