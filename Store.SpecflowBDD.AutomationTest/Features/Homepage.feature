Feature: Homepage Feature

Opens the homepage and verifies that the title is correct

@HomepageTest
Scenario: Open the homepage
	Given Open the browser
	When Enter the URL
	Then Verify that homepage title matched
