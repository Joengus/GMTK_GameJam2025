class_name player_controller
extends CharacterBody3D

signal direction_changed(Input_direction : Vector3)

var movement_vector: Vector3 = Vector3(0, 0, 0) # Initialize with desired values
@export_range(0,1000,.2,"or_greater") var move_speed : float = 10.0
@export_range(0,1000,.2,"or_greater") var jump_force : float = 10.0
@export var mouse_sensitivity = 0.002
@export var vertical_look_limit = 70.0 # degrees

var camera_pivot: Node3D
var camera: Camera3D


var input_direction : Vector3 :
	set(value):
		if input_direction == value:
			return
		
		input_direction = value
		direction_changed.emit(input_direction)
		
func _ready() -> void:
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	camera_pivot = $CameraPivot # Assuming you have a Spatial/Node3D named CameraPivot
	camera = $CameraPivot/Camera3D # Assuming Camera3D is a child of CameraPivot


func _physics_process(delta: float) -> void:
		var direction = transform.basis * Vector3(input_direction.x, 0, input_direction.z) 
		velocity.x = move_speed * direction.x
		velocity.z = move_speed * direction.z
		_apply_gravity(delta)
		move_and_slide()
		movement_vector.z += 1

func _apply_gravity(delta: float):
	if not is_on_floor():
		var gravity = get_gravity()
		velocity.y += gravity.y * delta

func _input(event):
	if event is InputEventMouseMotion:
		print(event.relative)
		# Rotate player horizontally
		rotate_y(-event.relative.x * mouse_sensitivity)

		# Rotate camera vertically
		camera.rotate(camera.position, event.relative.y * mouse_sensitivity)
		var camera_rotation_x = camera_pivot.rotation.x - event.relative.y * mouse_sensitivity
		camera_rotation_x = clamp(camera_rotation_x, deg_to_rad(-vertical_look_limit), deg_to_rad(vertical_look_limit))
		camera_pivot.rotation.x = camera_rotation_x
	else:
		if event.is_action_pressed("Jump") && is_on_floor() :
			velocity.y = jump_force
		elif event.is_action_released("Jump") :
			velocity.y = 0
		elif event.is_action_pressed("Left"):
			input_direction.x -= 1
		elif event.is_action_released("Left"):
			input_direction.x += 1
		elif event.is_action_pressed("Right"):
			input_direction.x += 1
		elif event.is_action_released("Right"):
			input_direction.x -= 1
		elif event.is_action_pressed("Forward"):
			input_direction.z -= 1
		elif event.is_action_released("Forward"):
			input_direction.z += 1
		elif event.is_action_pressed("Backward"):
			input_direction.z += 1
		elif event.is_action_released("Backward"):
			input_direction.z -= 1
