using Godot;
using System;
using static Game;
using static Context;

public class Josiel : KinematicBody2D
{
  public bool LookingRight = false;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
      GetNode<AnimatedSprite>("AnimatedSprite").Play("down_idle");
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if (Context.IsGameRunning())
    {
      if (LookingRight)
      {
        ProcessRightMovement();
      }
      else
      {
        ProcessDownMovement();
      }
    }
    else
    {
      ProcessGameStopped();
    }
  }

  private void ProcessGameStopped()
  {
    if (Input.IsActionJustPressed("enter"))
    {
      Context.StartGame();
    }
  }

  private void ProcessRightMovement()
  {
    if (Input.IsActionPressed("down"))
    {
      LookingRight = false;
      GD.Print("Pra baixo!");

      //TROCAR A SPRITE
      GetNode<AnimatedSprite>("AnimatedSprite").Play("down_idle");
    }

    if (Input.IsActionJustPressed("space"))
    {
      Context.RMoving = !Context.RMoving;
    }
  }

  private void ProcessDownMovement()
  {
    if (Input.IsActionPressed("right"))
    {
      LookingRight = true;
      GD.Print("Pra direita!");

      //TROCAR A SPRITE
      GetNode<AnimatedSprite>("AnimatedSprite").Play("right_idle");
    }

    if (Input.IsActionJustPressed("space"))
    {
      Context.DMoving = !Context.DMoving;
    }
  }
}
