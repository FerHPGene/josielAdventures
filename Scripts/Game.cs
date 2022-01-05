using Godot;
using System;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{
    private PackedScene Car = (PackedScene)ResourceLoader.Load("res://Scenes/Car.tscn");

    private KinematicBody2D[] RCar = new KinematicBody2D[100];
    private KinematicBody2D[] DCar = new KinematicBody2D[100];

    private int counter = 0;
    private int spawn_pos;
    private float time_emit = 0;
    private float time_cap = 5.0f;
    private int spawn_count = 0;
    public int R_car_count = 0;
    public int D_car_count = 0;

    private Vector2 SpawnR;
    private Vector2 SpawnD;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("RODANDO!!!!!!!!!!!!!!\n");
        SpawnR = GetNode<Position2D>("SpawnR").Position;
        SpawnD = GetNode<Position2D>("SpawnD").Position;

        GD.Print("Posição1: ", (int) SpawnR.x, " ", (int) SpawnR.y);
        GD.Print("Posição1: ", (int) SpawnD.x, " ", (int) SpawnD.y);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        time_emit += delta;

        if(time_emit > time_cap){       // SPAWNA O CARRO
            spawn_count++;
            spawn_pos = (int) GD.Randi()%2;
            if(spawn_pos == 0){
                //SPAWN DE BAIXO
                DCar[D_car_count] = (KinematicBody2D)Car.Instance();
                DCar[D_car_count].Position = SpawnD;
                DCar[D_car_count].;

                AddChild(DCar[D_car_count]);
                D_car_count++;
            }
            else{
                //SPAWN DA DIREITA
                DCar[R_car_count] = (KinematicBody2D)Car.Instance();
                DCar[R_car_count].Position = SpawnR;
                AddChild(DCar[R_car_count]);
                R_car_count++;
            }
            time_emit = 0;
        }

        if(spawn_count == 10 && time_cap > 3.0f){
            spawn_count = 0;
            time_cap -= 1.0f;
        }
    }

    private Vector2 generate_velocity(bool is_right){
        Vector2 velocity;

        if(is_right){
            velocity.x = - GD.Randf()*100;
            velocity.y = 0;
        }
        else{
            velocity.x = 0;
            velocity.y = - GD.Randf()*100;
        }

        return velocity;
    }
    
}
