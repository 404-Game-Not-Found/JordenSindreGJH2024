class_name Flying_Spitter_State extends State

const BASE = "Base_state"
const AGGRESION = "Aggresion_state"


var entity: flying_spitter


func _ready() -> void:
	await owner.ready
	entity = owner as flying_spitter
	assert(entity != null, "The EnemyState state type must be used only in the enemy scene. It needs the owner to be an Enemy node.")
