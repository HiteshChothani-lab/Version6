Feature: MainPageGeneral

Background: Start the app first time
 Given I start the app

 Scenario Outline:  Open Page and add std order Very Good
	Given I have the form "MainPage" open
	And I enter <first> in the "FirstName" boxs
	And I enter <last> in the "LastName" boxs
	And I enter <phone> in the "MobileNumber" boxs
	And I click "VeryGood"
	When I click "AddButton"
	Then I have a entry on the list
	
Examples: 
 | first  | last | phone      |
 | Barry  | Mann | 6134556550 |

 ##| Some   | Guy  | 6136666765 |
 ##| Sherry | Shaw | 6139015432 |
 #| Barry  | Mann | 6134556550 |
 #| James  | Dean | 6132001000 |

#Scenario: Open Page and add express order Very Good
#	Given I have the form "MainPage" open
#	And I enter "Some" in the "FirstName" box
#	And I enter "Guy" in the "LastName" box
#	And I enter "6736666765" in the "MobileNumber" box
#	And I click "VeryGood"
#	When I click "EXP"

Scenario:  find user in list
 Given the "Barry" "Mann" is already on the list
 And I have the form "MainPage" open
 When I search for list entry by name "Barry Mann"

Scenario: Open Page and add express order Indifferent
Scenario: Open Page and add nonmobil order


Scenario: Non Mobile User Register
	Given I have the form "MainPage" open		
	When I click "NonMobileButton"	
	Then I have the form "NonMobileUserPopupPage" open
	Given I enter "Robert" in the "NonMobileFirstName" box
	And I enter "McCabe" in the "NonMobileLastName" box
	And I enter "11/11/2011" in the "NonMobileDateOfBirth" box
	When I click "NonMobileRegisterButton"

Scenario: Non Mobile User Fill
	Given I have the form "MainPage" open		
	When I click "NonMobileButton"	
	Then I have the form "NonMobileUserPopupPage" open
	Given I enter "Robert" in the "NonMobileFirstName" box
	And I enter "McCabe" in the "NonMobileLastName" box
	And I enter "11/11/2011" in the "NonMobileDateOfBirth" box
	When I click "NonMobileFillButton"
