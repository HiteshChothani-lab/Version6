Feature: PasswordVault

A short summary of the feature


Scenario: Save User data to vault
	Given the User data
	 | AccessCode | AppVersionName | Messagee | Status | Token | UserId | Username |
	 | a          | b              | c        | d      | e     | f      | j        |
	And I save the User data to the vault
	When I retrieve the User data
	Then it is the same User data
	 | AccessCode | AppVersionName | Messagee | Status | Token | UserId | Username |
	 | a          | b              | c        | d      | e     | f      | j        |

Scenario: Save Master Data to vault
Given the Master data
       | Address | Country | CountryCode | Messagee | Phone | PostalCode | Status | StoreId | StoreName | Street | SuperMasterId | TimeZone | UserId |
       | a       | b       | c           | d        | e     | f          | j      |1        | M         | n      | o             | P        | q      |
	And I save the Master data to the vault
	When I retrieve the Master data
	Then it is the same Master data
       | Address | Country | CountryCode | Messagee | Phone | PostalCode | Status | StoreId | StoreName | Street | SuperMasterId | TimeZone | UserId |
       | a       | b       | c           | d        | e     | f          | j      |1        | M         | n      | o             | P        | q      |

Scenario: Get User data From vault No Data
	Given there is not User Data in the vault
	When I get the User data
	Then I get an null User object

Scenario: Get Master data From vaule No Data
	Given there is not Master Data in the vault
	When I get the Master data
	Then I get an null Master object
