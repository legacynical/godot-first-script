using Godot;
using System;

public partial class Sprite2D_Csharp : Sprite2D
{
  [Signal]
  public delegate void HealthChangedEventHandler(int oldValue, int newValue);
  [Signal]
  public delegate void HealthDepletedEventHandler();

  private int _health = 5;
  private float _speed = 400;
  private float _angularSpeed = Mathf.Pi;

  public override void _Ready()
  {
    var timer = GetNode<Timer>("Timer");
    timer.Timeout += OnTimerTimeout;
  }
    
  public override void _Process(double delta)
  {
    // var direction = 0;
    // if (Input.IsActionPressed("ui_left"))
    // {
    //   direction = -1;
    // }
    // if (Input.IsActionPressed("ui_right"))
    // {
    //   direction = 1;
    // }

    // Rotation += _angularSpeed * direction * (float)delta;

    // var velocity = Vector2.Zero;
    // if (Input.IsActionPressed("ui_up"))
    // {
    //   velocity = Vector2.Up.Rotated(Rotation) * _speed;
    // }
        
    // if (Input.IsActionPressed("ui_down"))
    // {
    //   velocity = Vector2.Down.Rotated(Rotation) * _speed;
    // }

    // Position += velocity * (float)delta;

    Rotation += _angularSpeed * (float)delta;
    var velocity = Vector2.Up.Rotated(Rotation) * _speed;
    Position += velocity * (float)delta;
  }

  private void OnButtonPressed()
  {
    SetProcess(!IsProcessing());
  }

  private void OnTimerTimeout()
  {
    Visible = !Visible;
    if (Visible == false)
    {
      TakeDamage(1);
    }
  }

  public void TakeDamage(int amount)
  {
    int oldHealth = _health;
    _health -= amount;
    EmitSignal(SignalName.HealthChanged, oldHealth, _health);
    
    GD.Print("CSharp HP: " + oldHealth + " -> " + _health);
    
    if (_health <= 0)
    {
      EmitSignal(SignalName.HealthDepleted);
    }
  }
}
