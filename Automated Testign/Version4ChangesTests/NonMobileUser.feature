Feature: NonMobileUser

Upon the return in sava_user_data when trying to add a mobile user that doesn't exist.
status=False
message=Mobile no doesnot exits!
Or 
status=False
message=Name doesnot match!
Open a dialog that says "this mobile does not exist do you wish to register them?" With a Yes or No. If Yes open another view to enter in their date of birth and gender then call a new API:
registerDummyMobile
That takes the similar fields to all the others, super_master_id, master_store_id.
Then fist name, last name, mobile, dob and gender.
Will then add this dummy mobile user to the list upon success.

@tag1
Scenario: [scenario name]
	Given [context]
	When [action]
	Then [outcome]
