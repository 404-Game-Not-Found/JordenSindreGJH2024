extends Enemy_Standard_State

@export var timer: Timer = null
@export var charge_cooldown = 3.0

#func enter(previous_state_path: String, data := {}) -> void:
	#entity.animated_sprite.play("default")
	#timer.wait_time = charge_cooldown
	#timer.one_shot = true
	#timer.start()
	

func physics_update(_delta: float) -> void:
	print(entity)
	print(entity.player)
	var enemy_pos = entity.player.global_transform.basis_xform_inv(entity.position)
	var player_pos = entity.player.global_transform.basis_xform_inv(entity.player.position)
	if enemy_pos.x + 3 < player_pos.x:
		entity.direction.x = 1
		entity.animated_sprite.flip_h = true
	elif enemy_pos.x - 3 > player_pos.x:
		entity.direction.x = -1
		entity.animated_sprite.flip_h = false
	else:
		entity.direction.x = 0
		
	if (entity.rc_right.is_colliding() or entity.rc_left.is_colliding()) and entity.is_on_floor():
		entity.jump()
	#if (entity.snipe_left.is_colliding() or entity.snipe_right.is_colliding()):
	if (entity.snipe_range.has_overlapping_bodies() and entity.is_on_floor):
		if timer.is_stopped():
			finished.emit(CHARGING_UP)
	entity.velocity_.x = entity.direction.x * entity.SPEED
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)
