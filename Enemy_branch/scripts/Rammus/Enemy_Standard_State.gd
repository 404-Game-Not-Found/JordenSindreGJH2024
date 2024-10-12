class_name Enemy_Standard_State extends State

const BASE = "Base_state"
const CHARGING_UP = "Chargeup_state"
const EXECUTE_CHARGE = "Execute_charge"
const EXECUTE_CHARGE_2 = "Execute_charge_2"
const COOLDOWN = "Cooldown_state"

var entity: rammus


func _ready() -> void:
	await owner.ready
	entity = owner as rammus
	assert(entity != null, "The EnemyState state type must be used only in the enemy scene. It needs the owner to be an Enemy node.")
