extends TextureRect

# Speed at which the TextureRect will move
var speed: float = 200.0  # Default speed

func _ready():
	# Start the TextureRect just outside the right side of the screen
	position.x = get_viewport().size.x + get_size().x / 2

func _process(delta: float):
	# Move the TextureRect to the left
	position.x -= speed * delta

	# Check if the TextureRect has exited the camera view
	if position.x < -get_size().x * 3:
		queue_free()  # Remove the TextureRect from the scene
