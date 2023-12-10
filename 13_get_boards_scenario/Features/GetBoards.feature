Feature: Get Boards
  As a Trello API user
  I want to access all my boards
  So that I want to call a single endpoint that will return all my boards

  Scenario: Check Get Boards
    Given a request with authorization
    And the request has fields query param
    And the request has member path param
    When the request is sent to getBoards endpoint
    Then the getBoards response status code is 200
    And the getBoards response matches get_boards.json schema