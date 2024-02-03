Feature: GetKlant

@mytag
Scenario: Get a list containing two Klanten when calling Get in KlantenController
	Given the provided Klant ID is '<KlantId>'
	When the Klanten API is called to retrieve the Klant with the ID of '<KlantId>'
	Then the result should be a Klant with the ID of '<KlantId>'