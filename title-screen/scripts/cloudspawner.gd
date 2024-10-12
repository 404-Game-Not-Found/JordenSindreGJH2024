extends Node  # Use the appropriate type for your main node

# Preload the MovingTextureRect scene
@onready var moving_texture_rect_scene = preload("res://title-screen/clouds/cloud.tscn")

# Array of cloud textures
var cloud_textures: Array = [
	preload("res://title-screen/clouds/cloud1.png"),
	preload("res://title-screen/clouds/cloud2.png"),
	preload("res://title-screen/clouds/cloud3.png"),
	preload("res://title-screen/clouds/cloud4.png")
]

# Timer for spawning objects
var spawn_timer: Timer

func _ready():
	# Set up the timer
	spawn_timer = Timer.new()
	spawn_timer.wait_time = get_random_spawn_interval()  # Set initial wait time
	spawn_timer.one_shot = false  # Set the timer to repeat
	spawn_timer.connect("timeout", Callable(self, "_on_spawn_timer_timeout"))
	add_child(spawn_timer)
	spawn_timer.start()

func _on_spawn_timer_timeout():
	spawn_moving_texture_rect()
	# Restart the timer with a new random interval
	spawn_timer.wait_time = get_random_spawn_interval()

func spawn_moving_texture_rect():
	var moving_texture_rect = moving_texture_rect_scene.instantiate()
	
	# Ensure the TextureRect has a texture assigned before getting its size
	if moving_texture_rect.texture:
		# Randomly select a texture from the array
		var random_texture_index = randi() % cloud_textures.size()  # Get a random index
		moving_texture_rect.texture = cloud_textures[random_texture_index]  # Set the texture

		# Calculate the height limit
		var texture_height = moving_texture_rect.texture.get_size().y  # Get the height of the texture
		var min_spawn_height = -texture_height / 3  # Maximum height above the screen (allow 1/3 to be visible)
		var max_spawn_height = -10  # A little below the top of the screen for more flexibility

		# Set random height and position (ensuring at least 1/3 of the sprite is visible)
		var random_height = randf_range(min_spawn_height, max_spawn_height)
		
		moving_texture_rect.position = Vector2(get_viewport().size.x + moving_texture_rect.texture.get_size().x / 2, random_height)

		# Set random speed between a range
		var random_speed = randf_range(100, 120)  # Adjust these values for speed range
		moving_texture_rect.speed = random_speed  # Assign speed to the texture rect

		add_child(moving_texture_rect)  # Add to the CloudsParent node
	else:
		print("Warning: The TextureRect does not have a texture assigned.")

func get_random_spawn_interval():
	return randf_range(2.0, 4.0)  # Random interval between 0.5 to 2 seconds
