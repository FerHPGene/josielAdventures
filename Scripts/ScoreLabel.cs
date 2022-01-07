using Godot;
using System;
using static Context;

public class ScoreLabel : Label
{
  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if (Context.IsGameRunning())
    {
      this.Text = "Score: " + Context.GetCarsSafelyCrossed();
    }
    else
    {
      this.Text = "";
    }
  }
}
