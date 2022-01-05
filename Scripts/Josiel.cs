using Godot;
using System;

public class Josiel : KinematicBody2D
{
	bool looking_right = false;
	public bool rstop = false;
	public bool dstop = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(looking_right){
			right_proccess_movement(delta);
		}
		else{
			down_proccess_movement(delta);
		}
	}

	private void right_proccess_movement(float delta){
		if(Input.IsActionPressed("down")){
			looking_right = false;
			GD.Print("Pra baixo!");

			//TROCAR A SPRITE
			GetNode<AnimatedSprite>("AnimatedSprite").Play("down_idle");
		}

		if(Input.IsActionJustPressed("space")){
			if(rstop){
				rstop = false;
				//LIGAR OS CARROS
				GD.Print("Liga!");
			}
			else{
				rstop = true;
				//PARAR OS CARROS
				GD.Print("Pare!");
			}
		}
	}

	private void down_proccess_movement(float delta){
		if(Input.IsActionPressed("right")){
			looking_right = true;
			GD.Print("Pra direita!");

			//TROCAR A SPRITE
			GetNode<AnimatedSprite>("AnimatedSprite").Play("right_idle");
		}

		if(Input.IsActionJustPressed("space")){
			if(dstop){
				dstop = false;
				//LIGAR OS CARROS
				GD.Print("Liga!");
			}
			else{
				dstop = true;
				//PARAR OS CARROS
				GD.Print("Pare!");
			}
		}
	}
}
