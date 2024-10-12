extends Flying_Spitter_State
#@export var timer: Timer = null
#@export var charge_cooldown = 3.0

func enter(previous_state_path: String, data := {}) -> void:
	pass
	#entity.animated_sprite.play("base")
	#timer.wait_time = charge_cooldown
	#timer.one_shot = true
	#timer.start()
	

func physics_update(_delta: float) -> void:
	var enemy_pos = entity.player.global_transform.basis_xform_inv(entity.position)
	var player_pos = entity.player.global_transform.basis_xform_inv(entity.player.position)
	if enemy_pos.x + 3 < player_pos.x:
		entity.direction.x = 1
		entity.animated_sprite.flip_h = false
	elif enemy_pos.x - 3 > player_pos.x:
		entity.direction.x = -1
		entity.animated_sprite.flip_h = true
	else:
		entity.direction.x = 0
	
	if entity.aggression_range.has_overlapping_bodies():
		finished.emit(AGGRESION) 

	entity.velocity_.x = entity.direction.x * entity.SPEED_HORIZONTAL
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)
