extends Flying_Spitter_State

@export var shot_timer: Timer = null
@export var swap_dir_timer: Timer = null
@export var shot_cooldown = 3.0
@export var swap_dir_time = 1.5
@export var chase_speed_multiplier = 0.4

var bullet_scene = preload("res://Enemy_branch/scenes/Hawk_tuah.tscn")
var spit = preload("res://Enemy_branch/SFX/spit.mp3")
func fire(angle):
	var direction = Vector2(1.0,0.0).rotated(angle).normalized()
	var bullet = bullet_scene.instantiate()
	entity.audio.play(0.1)
	bullet.direction = direction
	bullet.position = entity.position
	bullet.rotation = angle - PI/2
	entity.get_parent().add_child(bullet)

func enter(previous_state_path: String, data := {}) -> void:
	entity.audio.stream = spit
	swap_dir_timer.wait_time = swap_dir_time
	swap_dir_timer.one_shot = true
	swap_dir_timer.start()
	#decide start hover direction
	if entity.velocity_.x > 0:
		entity.direction.x = 1
	else:
		entity.direction.x = -1
	

func physics_update(_delta: float) -> void:
	var enemy_pos = entity.player.global_transform.basis_xform_inv(entity.position)
	var player_pos = entity.player.global_transform.basis_xform_inv(entity.player.position)
	if not entity.aggression_range.has_overlapping_bodies():
		if enemy_pos.x + 3 < player_pos.x:
			entity.direction.x = 1
			entity.animated_sprite.flip_h = false
		elif enemy_pos.x - 3 > player_pos.x:
			entity.direction.x = -1
			entity.animated_sprite.flip_h = true
		else:
			entity.direction.x = 0
	else:
		if swap_dir_timer.is_stopped():
			swap_dir_timer.wait_time = swap_dir_time
			swap_dir_timer.start()
			entity.direction.x = entity.direction.x * -1
		
		entity.animated_sprite.flip_h = not enemy_pos.x < player_pos.x
	
	if shot_timer.is_stopped():
		var angle = entity.position.angle_to_point(entity.player.position)
		fire(angle)
		
		shot_timer.wait_time = shot_cooldown
		shot_timer.start()
	
	if not entity.snipe_range.has_overlapping_bodies():
		
		finished.emit(BASE)

	entity.velocity_.x = entity.direction.x * entity.SPEED_HORIZONTAL*chase_speed_multiplier
	entity.velocity = entity.global_transform.basis_xform(entity.velocity_)
