# This script should be attached to your Button node
extends Button

# This function is called when the button is pressed
func _on_button_pressed():
	get_tree().quit()

# In the _ready() function, connect the signal properly using Callable
func _ready():
	# Connect the 'pressed' signal using a callable
	connect("pressed", Callable(self, "_on_button_pressed"))
