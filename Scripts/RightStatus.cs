using Godot;
using System;
using static Context;

public class RightStatus : Label
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
      this.Text = Context.RMoving ? "Free To Go" : "On Hold";
    }
    else
    {
      this.Text = "";
    }
  }
}
