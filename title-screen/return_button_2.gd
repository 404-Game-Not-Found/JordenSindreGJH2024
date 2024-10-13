extends Button

func _ready():
	# Connect the 'pressed' signal to the function
	pressed.connect(_on_button_pressed)

func _on_button_pressed():
	var texture_rect1 = get_node("../../BG")  
	var texture_rect2 = get_node("../../BG2")  
	var texture_rect3 = get_node("../../BG3")

	if texture_rect1 and texture_rect2:
		change_texture_rects(texture_rect1, texture_rect2, texture_rect3)

func change_texture_rects(texture_rect1: TextureRect, texture_rect2: TextureRect, texture_rect3: TextureRect):
	var target_position1 = texture_rect1.position + Vector2(0, 2*texture_rect1.size.y)
	var target_position2 = texture_rect2.position + Vector2(0, 2*texture_rect1.size.y)
	var target_position3 = texture_rect3.position + Vector2(0, 2*texture_rect1.size.y)

	# Create a new Tween instance
	var tween1 = create_tween()  # Tween for texture_rect1
	var tween2 = create_tween()  # Tween for texture_rect2
	var tween3 = create_tween()  # Tween for texture_rect2
	
	print(texture_rect1.position, " ", target_position1)
	print(texture_rect2.position, " ", target_position2)
	print(texture_rect3.position, " ", target_position3)

	tween1.tween_property(texture_rect1, "position", target_position1, 1.0).set_trans(Tween.TRANS_SINE)
	tween2.tween_property(texture_rect2, "position", target_position2, 1.0).set_trans(Tween.TRANS_SINE)
	tween3.tween_property(texture_rect3, "position", target_position3, 1.0).set_trans(Tween.TRANS_SINE)
