
class_name rammus extends CharacterBody2D

@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite2D
@onready var rc_right: RayCast2D = $RayCastRight
@onready var rc_left: RayCast2D = $RayCastLeft
@onready var snipe_range: Area2D = $snipe_range
@onready var player: CharacterBody2D = $"../%Player"



@export var SPEED = 100.0
@export var JUMP_VELOCITY = -170.0
var velocity_ = Vector2(0, 0)
var charge_direction = null
var direction = Vector2(0,0)



func jump():
	velocity_.y = JUMP_VELOCITY
	
func set_charge_dir():
	charge_direction = position.direction_to(player.position).normalized()

func _ready() -> void:
	print("player: ", player)
	floor_max_angle = 90


func _physics_process(delta: float) -> void:
	velocity_ = global_transform.basis_xform_inv(velocity)
	move_and_slide()
