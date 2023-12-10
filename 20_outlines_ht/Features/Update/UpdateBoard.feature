Feature: Update Board
  As a Trello API user
  I want to update my board
  So that I want to call a single endpoint that will update my board

  Scenario: Check Update Board
    #Update board
    Given a request with authorization
    And the request has body params:
     | name | value        |
     | name | Updated Name |
    And the request has path params:
     | name | value                    |
     | id   | 60d84769c4ce7a09f9140220 |
    When the 'Put' request is sent to '/1/boards/{id}' endpoint
    Then the response status code is OK
    And body value has the following values by paths:
      | path | expected_value |
      | name | Updated Name   |
    #Check board name updated
    Given a request with authorization
    And the request has query params:
     | name   | value   |
     | fields | id,name |
    And the request has path params:
     | name | value                    |
     | id   | 60d84769c4ce7a09f9140220 |
    When the 'Get' request is sent to '/1/boards/{id}' endpoint
    Then the response status code is OK
    And body value has the following values by paths:
      | path | expected_value |
      | name | Updated Name   |