using Godot;
using System;
using static Game;

public class Josiel : KinematicBody2D
{
	public bool LookingRight = false;
	public bool RStop = false;
	public bool DStop = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(LookingRight){
			right_process_movement(delta);
		}
		else{
			down_process_movement(delta);
		}
	}

	private void right_process_movement(float delta){
		if(Input.IsActionPressed("down")){
			LookingRight = false;
			GD.Print("Pra baixo!");

			//TROCAR A SPRITE
			GetNode<AnimatedSprite>("AnimatedSprite").Play("down_idle");
		}

		if(Input.IsActionJustPressed("space")){
			if(RStop){
				RStop = false;
				//LIGAR OS CARROS
				GD.Print("Liga!");
			}
			else{
				RStop = true;
				//PARAR OS CARROS
				GD.Print("Pare!");
			}
		}
	}

	private void down_process_movement(float delta){
		if(Input.IsActionPressed("right")){
			LookingRight = true;
			GD.Print("Pra direita!");

			//TROCAR A SPRITE
			GetNode<AnimatedSprite>("AnimatedSprite").Play("right_idle");
		}

		if(Input.IsActionJustPressed("space")){
			if(DStop){
				DStop = false;
				//LIGAR OS CARROS
				GD.Print("Liga!");
			}
			else{
				DStop = true;
				//PARAR OS CARROS
				GD.Print("Pare!");
			}
		}
	}
}
