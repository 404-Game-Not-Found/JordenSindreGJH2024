extends Node2D


var direction = Vector2(0.0,0.0)
var speed = 300.0
@onready var life_timer: Timer = $lifetime
@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite2D
@onready var body: Area2D = $body

@export var lifetime = 4.0
func _ready() -> void:
	life_timer.wait_time = lifetime
	life_timer.start()
	body.set_monitoring(true)



func _process(delta):
	position = position + speed * direction * delta
	if life_timer.time_left < 0.5:
		#get_parent().get_node("body")
		speed = 0
		animated_sprite.scale = Vector2(1.2, 1.2)
		animated_sprite.play("explode")
		if life_timer.is_stopped():
			queue_free()
	elif body.has_overlapping_bodies() or body.has_overlapping_areas():
		var time = life_timer.time_left
		life_timer.stop()
		life_timer.wait_time = min(time, 0.5)
		life_timer.start()
	#elif life_timer.time_left <= 0.5:
	
#some collision detection stuff here
