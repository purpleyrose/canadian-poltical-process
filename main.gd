extends Node2D

var campaign_points = 100
var weeks = 1

# Define voter preferences for each riding
var ridings = {
	"Toronto Centre": {"Liberal": 40, "Conservative": 30, "NDP": 20, "Green": 10},
	"Vancouver South": {"Liberal": 35, "Conservative": 25, "NDP": 30, "Green": 10},
	"Montreal West": {"Liberal": 45, "Conservative": 20, "NDP": 25, "Green": 10}
}

# A variable to store the player's campaign efforts
var player_campaign = {
	"Toronto Centre": 0,
	"Vancouver South": 0,
	"Montreal West": 0
}

# AI campaign efforts (focus on Conservative influence)
var ai_campaign_1 = {
	"Toronto Centre": 0,
	"Vancouver South": 0,
	"Montreal West": 0
}

# Function to calculate election results based on campaign efforts
func calculate_results():
	for riding in ridings.keys():
		print("Results for " + riding)
		
		# Duplicate the original votes for modification
		var total_votes = ridings[riding].duplicate()
		
		# Add the player's campaign influence
		total_votes["Liberal"] += player_campaign[riding]
		total_votes["Conservative"] += ai_campaign_1[riding]
		
		# Cap each party's votes at 100%
		for party in total_votes.keys():
			if total_votes[party] > 100:
				total_votes[party] = 100
		
		# Determine the winner by finding the party with the most votes
		var winner = ""
		var max_votes = 0
		for party in total_votes.keys():
			var vote_percentage = total_votes[party]
			if vote_percentage > max_votes:
				max_votes = vote_percentage
				winner = party
		
		# Print the winner and the vote percentage
		print("Winner: " + winner + " with " + str(max_votes) + "% of the vote")
		

# Called when the button is pressed
func _on_RunElectionButton_pressed():
	calculate_results()

func _on_TorontoCentreButton_pressed():
	if campaign_points > 0:
		player_campaign["Toronto Centre"] += 5
		campaign_points -= 5
		print("Campaigning in Toronto Centre! Current influence: " + str(player_campaign["Toronto Centre"]))
		print("Remaining campaign points: " + str(campaign_points))
	else:
		print("You do not have enough campaign points")

func _on_VancouverSouthButton_pressed():
	if campaign_points > 0:
		player_campaign["Vancouver South"] += 5
		campaign_points -= 5
		print("Campaigning in Vancouver South! Current influence: " + str(player_campaign["Vancouver South"]))
		print("Remaining campaign points: " + str(campaign_points))
	else:
		print("You do not have enough points")

func _on_MontrealWestButton_pressed():
	if campaign_points > 0:
		player_campaign["Montreal West"] += 5
		campaign_points -= 5
		print("Campaigning in Montreal West! Current influence: " + str(player_campaign["Montreal West"]))
		print("Remaining campaign points: " + str(campaign_points))
	else:
		print("Not enough campaign points")
		

func _on_AdvanceTimeButton_pressed():
	# Advance time by 1 week
	weeks += 1
	campaign_points = 100
	$WeeksLabel.text = "Week: " + str(weeks)
	
	# Update Conservative influence every week
	if weeks <= 10:
		for riding in ridings.keys():
			var influence_change = RandomNumberGenerator.new().randi_range(0, 30) 
			ai_campaign_1[riding] += influence_change
			print("Conservative influence in " + riding + " increased by " + str(influence_change) + ". Current influence: " + str(ai_campaign_1[riding]))

	# At the end of week 10, calculate the results
	if weeks == 10:
		calculate_results()

# Called when the node enters the scene tree for the first time
func _ready():
	$RunElectionButton.connect("pressed", Callable(self, "_on_RunElectionButton_pressed"))
	
	# Connect the 3 campaign buttons
	$TorontoCentreButton.connect("pressed", Callable(self, "_on_TorontoCentreButton_pressed"))
	$MontrealWestButton.connect("pressed", Callable(self, "_on_MontrealWestButton_pressed"))
	$VancouverSouthButton.connect("pressed", Callable(self, "_on_VancouverSouthButton_pressed"))
	
	# Connect the advance time button
	$AdvanceTimeButton.connect("pressed", Callable(self, "_on_AdvanceTimeButton_pressed"))
	
	# Initialize the label with the starting week
	$WeeksLabel.text = "Week: " + str(weeks)
