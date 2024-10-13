extends Node2D


const RAMMUS = "rammus"
const FLYING_SPITTER = "flying_spitter"
const MOB_LIST = [RAMMUS, FLYING_SPITTER]
const MOB_LIST_LENGTH = len(MOB_LIST)

@export var player: Camera2D = null
@export var world: StaticBody2D = null
@export var wave_count = 5
@export var wave_cooldown: float = 30
@export var rammus_wave: Array[int] = [1, 1, 0, 2, 2]
@export var spitter_wave: Array[int] = [0, 1, 2, 1, 2]
@export var spawn_bound: float = 80

@onready var wave_timer: Timer = $Wave_timer
@onready var spawners: Node = $Spawners
# "rammus == index 0, "flyuing_spitter" == index 1
# Called when the node enters the scene tree for the first time.
var current_wave = 0
var mob_count = 0
var killed_mobs = 0

func _ready() -> void:
	wave_timer.wait_time = wave_cooldown
	wave_timer.one_shot = true
	wave_timer.start()
	
	while len(rammus_wave) < wave_count:
		rammus_wave.append(0)
	while len(spitter_wave) < wave_count:
		spitter_wave.append(0)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if wave_timer.is_stopped() and current_wave < wave_count:
		spawn_waves(current_wave)
	pass


func spawn_waves(wave):
	for i in range(rammus_wave[wave]):
		var spawned = 0
		var viable_spawner = null
		while not spawned:
			viable_spawner = spawners.get_children().pick_random()
			if player.position.distance_to(viable_spawner.position) > spawn_bound:
				viable_spawner.spawn_enemy(RAMMUS)
				spawned = 1
				mob_count += 1
				
	for i in range(spitter_wave[wave]):
		var spawned = 0
		var viable_spawner = null
		while not spawned:
			viable_spawner = spawners.get_children().pick_random()
			if player.position.distance_to(viable_spawner.position) > spawn_bound:
				viable_spawner.spawn_enemy(FLYING_SPITTER)
				spawned = 1
				mob_count += 1
	current_wave += 1
	wave_timer.wait_time = wave_cooldown
	wave_timer.start()
