using Godot;
using System;

public class Car : KinematicBody2D
{
	// Declare member variables here. Examples:
	private Vector2 Velocity = new Vector2(0,0);
	private bool InMovement = true;
	private String SpawnSelect = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

 // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
			if(InMovement){
				MoveAndSlide(Velocity);
			}
	}

    public void SetVelocity(Vector2 velocity){
				this.Velocity = velocity;
    }

		public void SetVelocity(){
				this.Velocity = GenerateRandomVelocity();
		}

    public Vector2 GetVelocity(){
        return Velocity;
    }

		public void SetSpawnSelect(String spawnSelect){
				this.SpawnSelect = spawnSelect;
		}

		public String GetSpawnSelect(){	
				return SpawnSelect;
		}

    public void Stop(){
        InMovement = false;
    }

		public void Start(){
			InMovement = true;
		}

		private Vector2 GenerateRandomVelocity(){
        Vector2 velocity;

        if(String.Equals(SpawnSelect, "right")){
            velocity.x = - (GD.Randf()*30 + 100);
            velocity.y = 0;
        }
        else{
            velocity.x = 0;
            velocity.y = - (GD.Randf()*30 + 100);
        }

        return velocity;
    }
}
