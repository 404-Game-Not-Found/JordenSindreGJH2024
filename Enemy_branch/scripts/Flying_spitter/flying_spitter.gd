class_name flying_spitter extends CharacterBody2D

@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite2D
@onready var snipe_range: Area2D = $snipe_range
@onready var aggression_range: Area2D = $aggression_range
@onready var world: StaticBody2D = $"../%World"
@onready var audio_stream_player_2d: AudioStreamPlayer2D = $AudioStreamPlayer2D

@onready var player: CharacterBody2D = $"../%Player"



@export var SPEED_HORIZONTAL = 100.0
@export var SPEED_VERTICAL = 4.0
@export var MAX_SPEED_VERTICAL = 8*SPEED_VERTICAL
@export var hover_range = 100
var velocity_ = Vector2(0, 0)
var direction = Vector2(0,0)
var rng = RandomNumberGenerator.new()

func dist_to_ground():
	var pos_world = world.position
	var world_radius = world.get_node("CollisionShape2D")
	return position.distance_to(pos_world) - (world_radius.shape.radius * world.transform.get_scale().x)

func hover_y_direction():
	var relative_to_hover = 1 - dist_to_ground()/hover_range
	var weighted_dice = rng.randf() + relative_to_hover
	if weighted_dice > 0.5:
		return -1
	else:
		return 1


func hover(delta):
	velocity_.y += hover_y_direction()*SPEED_VERTICAL
	velocity_.y -= 980 * delta
	if velocity_.y > 0: 
		velocity_.y = min(velocity_.y, MAX_SPEED_VERTICAL)
	else: 
		velocity_.y = max(velocity_.y, -1*MAX_SPEED_VERTICAL)
		
	

func _physics_process(delta: float) -> void:
	#negate the gravity
	if get_node("Health").health <= 0:
		queue_free()
	velocity_ = global_transform.basis_xform_inv(velocity)
	hover(delta)
	
	move_and_slide()
