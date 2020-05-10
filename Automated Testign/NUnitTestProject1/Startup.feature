Feature: "While slide" setup

A short summary of the feature
Background: Start the app first time
	Given I clear the valid user Json file
	And I clear the Maste store json file
	Then I start the app

Scenario: Sales rep sign in and Register New address
	Given I have the form "StoreValidationPage" open
	And I enter "Store4" in the name 
	And I enter "111111" in the ID
	When I click Submit
	Then I have the form "RegisterMasterStore1Page" open
    Given I leave the default values in place
	When I click Submit
	Then I have the form "RegisterMasterStore2Page" open
    Given I leave the default values in place
	When I click Submit
	Then I have the form "RegisterMasterStoreReviewPage" open
	When I click Complete regisration
	Then I have the form "MainPage" open

Scenario:Sales rep wrong sign in
	Given I have the form "StoreValidationPage" open
	And I enter a invalid name
	And I enter a valid id
	When I click Submit
	Then the application closes without any message


Scenario: Register with invalid Canadian postal code
	Given I have the form "StoreValidationPage" open
	And I enter a valid name 
	And I enter a valid ID
	When I click Submit
	Then I have the form "RegisterMasterStore1Page" open
	Given I enter "Canada" in country
	And I enter "Ontario" in Province
	And I enter "Ottawa in City
	And I enter "ssss" in Postal code
	And I select Eastern time
	When I click Submit
	Then I see the invalid postal code message

Scenario: Register with invalid US postal code
	Given I have the form "StoreValidationPage" open
	And I enter a valid name 
	And I enter a valid ID
	When I click Submit
	Then I have the form "RegisterMasterStore1Page" open
	Given I enter "Canada" in country
	And I enter "Ontario" in Province
	And I enter "Ottawa in City
	And I enter "ssss" in Postal code
	And I select Eastern time
	When I click Submit
	Then I see the invalid postal code message


Scenario: Register addres go back to make a change
	Given I have the form "StoreValidationPage" open
	And I enter a valid name 
	And I enter a valid ID
	When I click Submit
	Then I have the form "RegisterMasterStore1Page" open
	Given I enter "Canada" in country
	And I enter "Ontario" in Province
	And I enter "Ottawa in City
	And I enter "1k1 2o9" in Postal code
	And I select Eastern time
	When I click Submit
	Then I have the form "" open
	Given I enter "Mac77" in store
	And I enter "777 777 7777" in phone number
	And I enter "700" in address and "Street 77"
	When I click back
	Then I have the form "" open
	And the country is "Canada"
	And the provence is "Ontario"
	And the city is "Ottawa"
	And the postal code is "1k1 2o9"
	And the time zone is "Eastern"
	When I change the city to "Toronto"
	And I click submit 



	



	
