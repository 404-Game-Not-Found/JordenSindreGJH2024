extends Enemy_Standard_State

@export var timer: Timer = null
@export var cooldown = 1.0

func enter(previous_state_path: String, data := {}) -> void:
	entity.animated_sprite.play("cooldown")
	timer.wait_time = cooldown
	timer.one_shot = true
	timer.start()
	

func physics_update(_delta: float) -> void:
	#entity.velocity.x = entity.velocity.move_toward(Vector2(0,0), entity.SPEED).x
	if timer.is_stopped():
		finished.emit(BASE)
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)
