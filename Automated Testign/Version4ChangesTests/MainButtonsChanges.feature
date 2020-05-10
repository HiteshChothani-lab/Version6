Feature: MainButtonsChanges
Buttons are ow 
1) New Case
2) Follow up
3) vacination
Buttons are exclusive;
API calls:
 Test names
 | Some   | Guy  | 6136666765 |
 | Sherry | Shaw | 6139015432 |
  Barry  | Mann | 6134556550 |
  James  | Dean | 6132001000 |

Background: Start the app 
 Given I start the app

Scenario: New Main Page buttons
	Given I have the form "MainPage" open		
	Then the "NonMobileButton" button visible "True"
	And the "AddButton" button visible "True"
	And the "ExpButton" button visible "True"
	And the "RefreshButton" button visible "True"
	And the "NewCaseButton" button visible "True"
	And the "FollowUpButton" button visible "True"
	And the "VaccinationButton" button visible "True"
	

Scenario Outline: Click on first button
	Given I have the form "MainPage" open		
	And the "NewCaseButton" button visible "True"
	And the "FollowUpButton" button visible "True"
	And the "VaccinationButton" button visible "True"
	And the "NewCaseButton" button selected is "False"
	And the "FollowUpButton" button selected is "False"
	And the "VaccinationButton" button selected is "False"
	When I click on the <Click Button>
	Then the <Click Button> button selected is "True"
	And the <Other Button 1> button selected is "False"
	And the <Other Button 2> button selected is "False"
	Examples: 
	| Click Button   | Other Button 1    | Other Button 2    |
	| NewCaseButton  | FollowUpButton    | VaccinationButton |
	| FollowUpButton | VaccinationButton | NewCaseButton     |
	| VaccinationButton | FollowUpButton | NewCaseButton     |


Scenario Outline: Click on first button then second
	Given I have the form "MainPage" open		
	And the "NewCaseButton" button visible "True"
	And the "FollowUpButton" button visible "True"
	And the "VaccinationButton" button visible "True"
	And the "NewCaseButton" button selected is "False"
	And the "FollowUpButton" button selected is "False"
	And the "VaccinationButton" button selected is "False"
	When I click on the <Click Button 1>
	Then the <Click Button 1> button selected is "True"
	And the <Other Button 1> button selected is "False"
	And the <Other Button 2> button selected is "False"
	When I click on the <Click Button 2>
	Then the <Click Button 2> button selected is "True"
	And the <Click Button 1> button selected is "False"
	And the <Other Button 2> button selected is "False"

	Examples: 
	| Click Button 1    | Other Button 1    | Other Button 2    | Click Button 2    |
	| NewCaseButton     | FollowUpButton    | VaccinationButton | FollowUpButton    |
	| FollowUpButton    | NewCaseButton     | VaccinationButton | NewCaseButton     |
	| VaccinationButton | NewCaseButton     | FollowUpButton    | NewCaseButton     |
	| FollowUpButton    | VaccinationButton | NewCaseButton     | VaccinationButton |


Scenario: Change appoitment after added
Given I have  "Some  Guy" registerd
When I click on the "New Case" text
Then the Change Appointment pop up opens
When I select "Vaccination" 
And  I click on the "OK"
Then the appointment type for "Some Guy" is "Vaccination"
