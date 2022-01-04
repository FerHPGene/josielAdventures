using Godot;
using System;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{
    private PackedScene[] CarRight;
    private PackedScene[] CarDown;

    private int counter = 0;
    private float randanoninho;
    private int cap = 180;

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


    }

    
}
