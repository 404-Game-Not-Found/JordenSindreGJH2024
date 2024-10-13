extends Node2D

@export var spawn_width = 50
@export var spawn_height = 20

var rammus_scene = preload("res://Enemy_branch/scenes/Rammus.tscn")
var hawk_tuah_scene = preload("res://Enemy_branch/scenes/Flying_spitter.tscn")
@export var start_pos: Vector2 = Vector2(0,0)

const RAMMUS = "rammus"
const FLYING_SPITTER = "flying_spitter"
#var bullet = bullet_scene.instantiate()
# Called when the node enters the scene tree for the first time.
func spawn_enemy(type):
	var x = randf_range(-1.0, 1.0)*spawn_width/2
	var y = randf_range(-1.0, 1.0)*spawn_height/2
	var pos = Vector2(x+start_pos.x, y+start_pos.y)
	var mob = null
	if type == RAMMUS:
		mob = rammus_scene.instantiate()
	elif type == FLYING_SPITTER:
		mob = hawk_tuah_scene.instantiate()
	get_parent().get_parent().add_child(mob)
	#get_parent().mob_count += 1

func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
