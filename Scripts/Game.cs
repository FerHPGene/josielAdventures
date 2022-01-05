using Godot;
using System;

// assets do pato: https://deadmadman.itch.io/the-quaken-assets
public class Game : Node2D
{

  private int _score = 0;
  private PackedScene[] CarRight;
  private PackedScene[] CarDown;

  private int counter = 0;
  private float randanoninho;
  private int cap = 180;

  private Vector2 SpawnR;
  private Vector2 SpawnD;

  public void OnScoreTimerTimeout()
  {
    _score++;
    GD.Print("Score: " + _score);
  }

  public void GameOver()
  {
    GetNode<Timer>("ScoreTimer").Stop();
  }

  public void NewGame()
  {
    _score = 0;
    GetNode<Timer>("ScoreTimer").Start();
  }

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


}
