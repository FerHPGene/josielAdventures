using Godot;
using System;
using System.Collections.Generic;
using static Car;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{

    private PackedScene CarSce = (PackedScene)ResourceLoader.Load("res://Scenes/Car.tscn");
    private List<Car> RCars = new List<Car>();
    private List<Car> DCars = new List<Car>();
    private Vector2 SpawnR;
    private Vector2 SpawnD;
    private int SpawnCount;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("RODANDO!!!!!!!!!!!!!!\n");
        SpawnR = GetNode<Position2D>("SpawnR").Position;
        SpawnD = GetNode<Position2D>("SpawnD").Position;

        GD.Print("Posição1: ", (int)SpawnR.x, " ", (int)SpawnR.y);
        GD.Print("Posição1: ", (int)SpawnD.x, " ", (int)SpawnD.y);
        NewGame();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
    }

    public void OnSpawnTimerTimeout()
    {
        SpawnCount++;
        GD.Print("SpawnCount: " + SpawnCount);

        uint RandomNumber = GD.Randi();

        GD.Print("Random: ", RandomNumber);

        String SpawnPos = (((int) RandomNumber%2 == 0) ? "down" : "right");

        GD.Print("Spawn pos: ", SpawnPos);
        
        

        if(String.Equals(SpawnPos, "down")){
            //SPAWN DE BAIXO
            Car car = (Car) CarSce.Instance();
            car.Position = SpawnD;
            car.SetSpawnSelect(SpawnPos);
            car.SetVelocity();
            DCars.Add(car);
            AddChild(car);
        }
        else{
            //SPAWN DA DIREITA
            Car car = (Car) CarSce.Instance();
            car.Position = SpawnR;
            car.SetSpawnSelect(SpawnPos);
            car.SetVelocity();
            RCars.Add(car);
            AddChild(car);
        }
    }

    public void GameOver()
    {
        GetNode<Timer>("SpawnTimer").Stop();
    }

    public void NewGame()
    {
        SpawnCount = 0;
        GetNode<Timer>("SpawnTimer").Start();
    }


}
