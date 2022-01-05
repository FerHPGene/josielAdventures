using Godot;
using System;
using System.Collections.Generic;
using static Car;
using static Josiel;
using static Context;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{

    private PackedScene CarSce = (PackedScene)ResourceLoader.Load("res://Scenes/Car.tscn");
    private List<Car> RCars = new List<Car>();
    private List<Car> DCars = new List<Car>();
    private Josiel Player;
    private Position2D SpawnR;
    private Position2D SpawnD;
    private Vector2 SpawnRPos;
    private Vector2 SpawnDPos;
    private bool RStoppedCars = false;
    private bool DStoppedCars = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("RODANDO!!!!!!!!!!!!!!\n");
        SpawnR = GetNode<Position2D>("SpawnR");
        SpawnD = GetNode<Position2D>("SpawnD");
        SpawnRPos = SpawnR.Position;
        SpawnDPos = SpawnD.Position;
        Player = GetNode<Josiel>("Josiel");

        GD.Print("Posição1: ", (int)SpawnRPos.x, " ", (int)SpawnRPos.y);
        GD.Print("Posição1: ", (int)SpawnDPos.x, " ", (int)SpawnDPos.y);
        NewGame();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        ProcessCarMovement();
    }

    public void ProcessCarMovement(){
        if(Player.LookingRight){
            if(!RStoppedCars && Player.RStop){
                StopRightCars();
                RStoppedCars = true;
            }
            else if(RStoppedCars && !(Player.RStop)){
                StartRightCars();
                RStoppedCars = false;
            }
        }
        else{
            if(!DStoppedCars && Player.DStop){
                StopDownCars();
                DStoppedCars = true;
            }
            else if(DStoppedCars && !(Player.DStop)){
                StartDownCars();
                DStoppedCars = false;
            }
        }
    }

    public void OnSpawnTimerTimeout()
    {
        Context.SpawnCount++;
        GD.Print("SpawnCount: " + Context.SpawnCount);

        uint RandomNumber = GD.Randi();

        GD.Print("RandomNumber: " + RandomNumber);

        String SpawnPos = "none";

        if(RandomNumber % 2 == 0 && !Player.DStop){
            SpawnPos = "down";
        }else if(!Player.RStop){
            SpawnPos = "right";
        }
        else if (!Player.DStop){
            SpawnPos = "down";
        }
        

        GD.Print("Spawn pos: ", SpawnPos);

        if(String.Equals(SpawnPos, "down")){
            //SPAWN DE BAIXO
            Car car = (Car) CarSce.Instance();
            car.Position = SpawnDPos;
            car.SetSpawnSelect(SpawnPos);
            car.SetVelocity();
            if(Player.DStop) car.Stop();
            DCars.Add(car);
            AddChild(car);
        }
        else if(String.Equals(SpawnPos, "right")){
            //SPAWN DA DIREITA
            Car car = (Car) CarSce.Instance();
            car.Position = SpawnRPos;
            car.SetSpawnSelect(SpawnPos);            
            car.SetVelocity();
            if(Player.RStop) car.Stop();
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
        Context.SpawnCount = 0;
        GetNode<Timer>("SpawnTimer").Start();
    }

    public void StopRightCars(){
        for(int i = 0; i < RCars.Count; i++){
            RCars[i].Stop();
        }
    }

    public void StopDownCars(){
        for(int i = 0; i < DCars.Count; i++){
            DCars[i].Stop();
        }
    }

    public void StartRightCars(){
        for(int i = 0; i < RCars.Count; i++){
            RCars[i].Start();
        }
    }

    public void StartDownCars(){
        for(int i = 0; i < DCars.Count; i++){
            DCars[i].Start();
        }
    }
}
