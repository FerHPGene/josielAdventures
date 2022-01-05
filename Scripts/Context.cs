using Godot;
using System;

static public class Context{
  static public int SpawnCount;
  static private int InitialSpeed = 100;

  static public float GetSpeed(){
    float speed = InitialSpeed + ((SpawnCount/3.0f) * 1);
    return speed;
  }
}

