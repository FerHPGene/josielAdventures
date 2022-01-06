using Godot;
using System;
using static Context;

public class Car : KinematicBody2D
{
  // Declare member variables here. Examples:
  private Vector2 Velocity = new Vector2(0, 0);
  private String SpawnSelect = "";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    if (this.SpawnSelect == "down")
    {
      this.Position = Context.SpawnDPos;
    }
    else
    {
      this.Position = Context.SpawnRPos;
    }

    this.SetVelocity();
  }



  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if ((SpawnSelect == "down" && Context.DMoving) || (SpawnSelect == "right" && Context.RMoving))
    {
      MoveAndSlide(Velocity);
    }

  }

  public void SetVelocity(Vector2 velocity)
  {
    this.Velocity = velocity;
  }

  public void SetVelocity()
  {
    this.Velocity = GenerateRandomVelocity();
  }

  public Vector2 GetVelocity()
  {
    return Velocity;
  }

  public void SetSpawnSelect(String spawnSelect)
  {
    this.SpawnSelect = spawnSelect;
  }

  public String GetSpawnSelect()
  {
    return SpawnSelect;
  }

  private Vector2 GenerateRandomVelocity()
  {
    Vector2 velocity;

    if (SpawnSelect == "right")
    {
      velocity.x = -(GD.Randf() * 15 + Context.GetSpeed());
      velocity.y = 0;
    }
    else
    {
      velocity.x = 0;
      velocity.y = -(GD.Randf() * 15 + Context.GetSpeed());
    }

    return velocity;
  }
}
