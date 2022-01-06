using System.Collections.Generic;
using Godot;
using System;

using static Car;

public class Context : Godot.Object
{
  static public Context Instance = new Context();

  static private bool GameRunning;
  static public int SpawnCount;
  static public bool RMoving;
  static public bool DMoving;

  static public Vector2 SpawnRPos;
  static public Vector2 SpawnDPos;
  static private int InitialSpeed = 100;
  static private List<Car> RCars = new List<Car>();
  static private List<Car> DCars = new List<Car>();

  static private Thread CarCleanerR;
  static private Thread CarCleanerD;
  static private Semaphore CarCleanerSemaphoreR = new Semaphore();
  static private Semaphore CarCleanerSemaphoreD = new Semaphore();

  static private Mutex CarsSafelyCrossedMutex = new Mutex();
  static private int CarsSafelyCrossed = 0;

  static public float GetSpeed()
  {
    float speed = InitialSpeed + ((SpawnCount / 3.0f) * 1);
    return speed;
  }

  static public void AddCar(Car car)
  {
    if (car.GetSpawnSelect() == "right")
    {
      RCars.Add(car);
      if (RCars.Count == 1)
      {
        CarCleanerSemaphoreR.Post();
      }
    }
    else
    {
      DCars.Add(car);
      if (DCars.Count == 1)
      {
        CarCleanerSemaphoreD.Post();
      }
    }
  }

  private void _startCarCleaner()
  {
    CarCleanerR = new Thread();
    CarCleanerR.Start(this, nameof(ClearRightCars));
    CarCleanerD = new Thread();
    CarCleanerD.Start(this, nameof(ClearDownCars));
  }

  static public void StartCarCleaner()
  {
    Instance._startCarCleaner();
  }

  static public void StopCarCleaner()
  {
    CarCleanerSemaphoreD.Post();
    CarCleanerSemaphoreR.Post();
    CarCleanerR.WaitToFinish();
    CarCleanerD.WaitToFinish();
  }

  static private void ClearRightCars()
  {
    while (GameRunning)
    {

      if (RCars.Count == 0)
      {
        GD.Print("Waiting R");
        CarCleanerSemaphoreR.Wait();
        GD.Print("Woken up R");
      }

      Car car = RCars[0];

      if (car != null)
      {

        if (car.Position.x < -100)
        {

          car.QueueFree();
          RCars.Remove(car);
          CarsSafelyCrossedMutex.Lock();
          CarsSafelyCrossed++;
          CarsSafelyCrossedMutex.Unlock();
        }
      }
    }
  }

  static private void ClearDownCars()
  {
    while (GameRunning)
    {
      if (DCars.Count == 0)
      {
        GD.Print("Waiting D");
        CarCleanerSemaphoreD.Wait();
        GD.Print("Woken up D");
      }

      Car car = DCars[0];

      if (car != null)
      {
        if (car.Position.y < -100)
        {
          car.QueueFree();
          DCars.Remove(car);
          CarsSafelyCrossedMutex.Lock();
          CarsSafelyCrossed++;
          CarsSafelyCrossedMutex.Unlock();
        }
      }

    }
  }

  static public int GetCarsSafelyCrossed()
  {
    CarsSafelyCrossedMutex.Lock();
    int carsSafelyCrossed = CarsSafelyCrossed;
    CarsSafelyCrossedMutex.Unlock();
    return carsSafelyCrossed;
  }

  static public void StartGame()
  {
    GameRunning = true;
  }

  static public void EndGame()
  {
    GameRunning = false;
  }
}

