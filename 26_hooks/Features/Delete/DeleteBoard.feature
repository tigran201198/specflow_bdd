Feature: Delete Boards
  As a Trello API user
  I want to delete my board
  So that I want to call a single endpoint that will delete my board

  @CreateBoard
  Scenario: Check Delete Board
    #Delete a board
    Given a request with authorization
    And the request has path params:
      | name | value            |
      | id   | created_board_id |
    When the 'DELETE' request is sent to 'DeleteABoard' endpoint
    Then the response status code is OK
    And body value has the following values by paths:
      | path   | expected_value |
      | _value | empty_string   |
    #Check board deleted
    Given a request with authorization
    And the request has query params:
      | fields | id,name |
    And the request has path params:
      | name   | value        |
      | member | learnpostman |
    When the 'GET' request is sent to 'GetAllBoards' endpoint
    Then the response status code is OK
    And the response body doesn't have any item by paths:
     | path | expected_value   |
     | id   | created_board_id |
