using Godot;
using System;
using static Context;
public class GameStatus : Label
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {

  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if (Context.IsGameRunning())
    {
      this.Text = "";
    }
    else if (Context.IsFreshStart())
    {
      this.Text = "Welcome!\nPress Enter To Start";
    }
    else
    {
      this.Text = "Game Over!\nYou Lost Due To " + Context.DeathReason + "!\nPress Enter To Restart";
    }

  }
}
