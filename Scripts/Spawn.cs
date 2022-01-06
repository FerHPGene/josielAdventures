using Godot;
using System;

public class Spawn : Position2D
{


  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    if (this.Name == "SpawnR")
    {
      Context.SpawnRPos = this.Position;
    }
    else if (this.Name == "SpawnD")
    {
      Context.SpawnDPos = this.Position;
    }
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {

  }

}
