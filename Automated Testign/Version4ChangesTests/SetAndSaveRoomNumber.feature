Feature: SetAndSaveRoomNumber

n the bottom user list, the footnote it should display the room number, screen shot will show where to.
You can get this value from the return in the API get_archive_store_users, room_num that is called.
When you click on the room number a dialog view will be shown allowing for the setting of a new room of a number (1-10) and a letter (A-E) from buttons. You can of course just enter in what you want in the edittexts too.
There should be a check for if a room number existing of the one entered in the footnote is set already and if you put nothing in it should just have no room number anymore.
The API to do this is called manage_user with the action field set to "update_room_archive" with the regular fields super_master_id, master_store_id and then the room_num, id of that user.

@tag1
Scenario:OpenRoomNumberPopup
	Given I have the main form open
	When I click Room Number
	Then I have the Room number popup open
	And the Patient name is ""
	And The Room number is ""

