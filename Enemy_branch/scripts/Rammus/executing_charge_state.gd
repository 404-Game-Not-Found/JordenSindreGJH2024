extends Enemy_Standard_State

@export var timer: Timer = Timer.new()
@export var charge_time = 0.1
@export var charge_speed = 300

var direction = null

func enter(previous_state_path: String, data := {}) -> void:
	entity.animated_sprite.play("executing_charge")
	direction = entity.charge_direction
	timer.wait_time = charge_time
	timer.one_shot = true
	timer.start()
func physics_update(_delta: float) -> void:
	
	entity.velocity_.x = entity.charge_direction.x * charge_speed
	entity.velocity_.y = entity.charge_direction.y * charge_speed
	if timer.is_stopped():
		finished.emit(EXECUTE_CHARGE_2)
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)
