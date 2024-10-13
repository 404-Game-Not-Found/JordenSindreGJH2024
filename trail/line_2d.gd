extends Line2D


var max_points: int = 30

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	var pos = get_global_mouse_position()
	add_point(pos)
	
	if points.size() > max_points:
		remove_point(0)
