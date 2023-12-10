Feature: Create Boards
  As a Trello API user
  I want to create my board
  So that I want to call a single endpoint that will create my board

  @DeleteBoard
  Scenario: Check Create Board
    #Create a board
    Given a request with authorization
    And the request has body params:
     | name | value             |
     | name | Created New Board |
    When the 'POST' request is sent to 'CreateABoard' endpoint
    Then the response status code is OK
    And body value has the following values by paths:
      | path | expected_value    |
      | name | Created New Board |
    When the board ID from the response is remembered
    #Check board created
    Given a request with authorization
    And the request has query params:
         | name   | value   |
         | fields | id,name |
    And the request has path params:
      | name   | value        |
      | member | learnpostman |
    When the 'GET' request is sent to 'GetAllBoards' endpoint
    Then the response status code is OK
    And the response body has any item by paths:
    | path | value            |
    | id   | created_board_id |