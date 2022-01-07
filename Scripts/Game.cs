using Godot;
using System;
using static Context;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{

  private PackedScene CarScene = (PackedScene)ResourceLoader.Load("res://Scenes/Car.tscn");
  public override void _ExitTree()
  {
    Context.EndGame("Exit", isGameClosed: true);
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    GD.Randomize();
    Context.TimeLimitTimer = GetNode<Timer>("TimeLimitTimer");
    Context.SpawnTimer = GetNode<Timer>("SpawnTimer");
    GD.Print("Game Initiated");
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
  }

  public void OnSpawnTimerTimeout()
  {
    Context.SpawnCount++;
    GD.Print("SpawnCount: " + Context.SpawnCount);

    uint RandomNumber = GD.Randi();

    GD.Print("RandomNumber: " + RandomNumber);

    String SpawnPos = "none";

    if (RandomNumber % 2 == 0 && Context.DMoving)
    {
      SpawnPos = "down";
    }

    if (RandomNumber % 2 != 0 && Context.RMoving)
    {
      SpawnPos = "right";
    }


    GD.Print("Spawn pos: ", SpawnPos);

    if (SpawnPos != "none")
    {
      Car car = (Car)CarScene.Instance();
      car.SetSpawnSelect(SpawnPos);
      Context.AddCar(car);
      AddChild(car);
    }
  }

  public void OnTimeLimitTimerTimeout()
  {
    if (Context.DMoving)
    {
      if (Context.DRemainingTime != Context.DEFAULT_REMAING_TIME)
      {
        Context.DRemainingTime++;
      }
    }
    else
    {
      if (Context.DRemainingTime > 0)
      {
        Context.DRemainingTime--;
      }
    }

    if (Context.RMoving)
    {
      if (Context.RRemainingTime != Context.DEFAULT_REMAING_TIME)
      {
        Context.RRemainingTime++;
      }
    }
    else
    {
      if (Context.RRemainingTime > 0)
      {
        Context.RRemainingTime--;
      }
    }

    if (Context.RRemainingTime == 0 || Context.DRemainingTime == 0)
    {
      Context.EndGame("Starvation");
    }
  }
}
