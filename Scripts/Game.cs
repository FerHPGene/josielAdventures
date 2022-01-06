using Godot;
using System;
using static Context;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{

  private PackedScene CarScene = (PackedScene)ResourceLoader.Load("res://Scenes/Car.tscn");
  public override void _ExitTree()
  {
	GameOver();
	Context.EndGame();
	Context.StopCarCleaner();
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	GD.Print("RODANDO!!!!!!!!!!!!!!\n");
	Context.StartGame();
	Context.StartCarCleaner();
	NewGame();
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
	else if (Context.RMoving)
	{
	  SpawnPos = "right";
	}
	else if (Context.DMoving)
	{
	  SpawnPos = "down";
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

  public void GameOver()
  {
	GetNode<Timer>("SpawnTimer").Stop();
  }

  public void NewGame()
  {
	Context.SpawnCount = 0;
	Context.DMoving = true;
	Context.RMoving = true;

	GetNode<Timer>("SpawnTimer").Start();
  }
}
