Feature: ButtonTests

A short summary of the feature
Background: Start the app first time
 Given I start the app

@tag1
Scenario: CLick on very terrible and 3 new buttons show
	Given I have the form "MainPage" open		
	Then the "NonMobileButton" button visible "True"
	And the "AddButton" button visible "True"
	And the "ExpButton" button visible "True"
	And the "RefreshButton" button visible "True"
	And the "VeryGoodButton" button visible "True"
	And the "IndifferentButton" button visible "True"
	And the "VeryTerribleButton" button visible "True"
	And the "NoDealButton" button visible "False"
	And the "TerribleServiceButton" button visible "False"
	And the "NoDealTerribleButton" button visible "False"
	When I click "VeryTerribleButton"	
	Then the "NonMobileButton" button visible "True"
	And the "AddButton" button visible "True"
	And the "ExpButton" button visible "True"
	And the "RefreshButton" button visible "True"
	And the "VeryGoodButton" button visible "True"
	And the "IndifferentButton" button visible "True"
	And the "VeryTerribleButton" button visible "True"
	And the "NoDealButton" button visible "True"
	And the "TerribleServiceButton" button visible "True"
	And the "NoDealTerribleButton" button visible "True"

Scenario: All buttons on form

