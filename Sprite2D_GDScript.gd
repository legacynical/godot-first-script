extends Sprite2D

signal health_changed(old_value, new_value)
signal health_depleted

var health = 5
var speed = 400
var angular_speed = PI

func _ready():
	var timer = get_node("Timer")
	timer.timeout.connect(_on_timer_timeout)
	
func _process(delta):
	rotation += angular_speed * delta
	var velocity = Vector2.UP.rotated(rotation) * speed
	position += velocity * delta
	
#	var direction = 0
#	if Input.is_action_pressed("ui_left"):
#		direction = -1
#	if Input.is_action_pressed("ui_right"):
#		direction = 1
#		
#	rotation += angular_speed * direction * delta
#		
#	var velocity = Vector2.ZERO
#	if Input.is_action_pressed("ui_up"):
#		velocity = Vector2.UP.rotated(rotation) * speed
#
#	if Input.is_action_pressed("ui_down"):
#		velocity = Vector2.DOWN.rotated(rotation) * speed
#
#	position += velocity * delta

func _on_button_pressed(): # toggle process on button press
	set_process(not is_processing())

func _on_timer_timeout(): # toggle sprite vis on timer timeout & take dmg if invis
	visible = not visible
	if (visible == false):
		take_damage(1)

func take_damage(amount): # reduce health by dmg & emit depleted if <= 0
	var old_health = health
	health -= amount
	health_changed.emit(old_health, health)
	print("GDScript HP: " + str(old_health) + " -> " + str(health))
	if health <= 0:
		health_depleted.emit()

#func death_event(): # don't know how to do yet
#	if (health_depleted):
		
