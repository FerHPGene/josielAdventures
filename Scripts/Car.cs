using Godot;
using System;

public class Car : KinematicBody2D
{
    // Declare member variables here. Examples:
    private Vector2 velocity = new Vector2(0,0);
    private bool in_movement = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(in_movement){
            MoveAndSlide(velocity);
        }
    }

    public void set_velocity(float x, float y){
        velocity.x = x;
        velocity.y = y;
    }
    public void Stop(){
        in_movement = false;
    }

    public void Start(){
        in_movement = true;
    }
}
