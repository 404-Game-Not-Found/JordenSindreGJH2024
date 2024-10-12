extends Enemy_Standard_State

#@export var timer: Timer = null
#@export var cooldown = 1.0
@export var speed_threshold = 0.1
@export var slowdown = 0.95
func enter(previous_state_path: String, data := {}) -> void:
	entity.animated_sprite.play("executing_charge")
	

func physics_update(_delta: float) -> void:
	#entity.velocity.x = entity.velocity.move_toward(Vector2(0,0), entity.SPEED).x
	if entity.is_on_floor():
		#Might need to global_transform the target
		#entity.velocity_.x = entity.velocity_.move_toward(Vector2(0, 0), slowdown).x
		entity.velocity_.x = entity.velocity_.x * slowdown
		if abs(entity.velocity_.x) < entity.SPEED * speed_threshold:
			
			finished.emit(COOLDOWN)
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)
