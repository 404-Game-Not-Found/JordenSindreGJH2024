extends Enemy_Standard_State

@export var timer: Timer = null
@export var chargeup_time = 4.0

var spin = preload("res://Enemy_branch/SFX/spin.mp3")
func enter(previous_state_path: String, data := {}) -> void:
	entity.animated_sprite.play("charging")
	entity.audio.stream = spin
	entity.velocity_.x = 0
	entity.velocity_.y = 0
	#set the direction to charge in
	entity.set_charge_dir()
	timer.wait_time = chargeup_time
	timer.one_shot = true
	timer.start()
	

func physics_update(_delta: float) -> void:
	if entity.animated_sprite.frame == 3:
		entity.audio.play()
	if entity.animated_sprite.frame == 4:
		entity.animated_sprite.play("executing_charge")
	entity.velocity_.x = 0
	# If it is finished charging up
	if (timer.is_stopped()):
		finished.emit(EXECUTE_CHARGE)
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)


func exit():
	entity.audio.stop()
