Feature: ClinicConfig

Read and write config vile

Scenario: Write out default config
	Given I have a Clinic IO  object
	When I populate the object
#	| Button Type | BUtton Text | Button cmd | button Visiblity |
#	| AppButton1  | Button 1    | Button 1   | Visible          |
#	| AppButton2  | Button 2    | Button 2   | Visible          |
#	| AppButton3  | Button 3    | Button 3   | Visible          |
#	| AppButton3a | Button 3a   | Button 3a  | Visible          |
#	| AppButton3b | Button 3b   | Button 3b  | Visible          |
#	| AppButton3c | Button 3c   | Button 3c  | Visible          |
	And I write out the file
	When I read the file
	Then the clinics are the same
#	Given I load the app
#	Then the buttons are
#	| Button Type | BUtton Text | Button cmd | button Visiblity |
#	| AppButton1  | Button 1    | Button 1   | Visible          |
#	| AppButton2  | Button 2    | Button 2   | Visible          |
#	| AppButton3  | Button 3    | Button 3   | Visible          |
#	| AppButton3a | Button 3a   | Button 3a  | Visible          |
#	| AppButton3b | Button 3b   | Button 3b  | Visible          |
#	| AppButton3c | Button 3c   | Button 3c  | Visible          |
                
