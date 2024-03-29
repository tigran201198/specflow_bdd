﻿Feature: Get Boards
  As a Trello API user
  I want to access all my boards
  So that I want to call a single endpoint that will return all my boards

  Background: a request with authorization and fields query params
    Given a request with authorization
    And the request has query params:
      | fields | id,name |

  Scenario: Check Get Boards
    Given the request has path params:
     | name   | value        |
     | member | learnpostman |
    When the 'Get' request is sent to '/1/members/{member}/boards' endpoint
    Then the response status code is OK
    And the response matches 'get_boards.json' schema

  Scenario: Check Get Board
    Given the request has path params:
     | name | value                    |
     | id   | 6288cc5d3ce8fc87542dff31 |
    When the 'Get' request is sent to '/1/boards/{id}' endpoint
    Then the response status code is OK
    And body value has the following values by paths:
      | path | expected_value |
      | name | New Board      |
    And the response matches 'get_board.json' schema