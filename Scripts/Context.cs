using System.Collections.Generic;
using Godot;
using System;

using static Car;

public class Context : Godot.Object
{

  public const int DEFAULT_REMAING_TIME = 7;
  static public Context Instance = new Context();

  static private bool GameRunning = false;

  static public String DeathReason = "";

  static private bool isFreshStart = true;
  static public int SpawnCount;
  static public bool RMoving;
  static public bool DMoving;

  static public int RRemainingTime;
  static public int DRemainingTime;

  static public Vector2 SpawnRPos;
  static public Vector2 SpawnDPos;
  static private int InitialSpeed = 100;
  static private List<Car> RCars = new List<Car>();
  static private List<Car> DCars = new List<Car>();

  static private Thread CarCleanerR = new Thread();
  static private Thread CarCleanerD = new Thread();
  static private Semaphore CarCleanerSemaphoreR;
  static private Semaphore CarCleanerSemaphoreD;

  static private Mutex CarsSafelyCrossedMutex = new Mutex();
  static private int CarsSafelyCrossed;

  static public Timer SpawnTimer;
  static public Timer TimeLimitTimer;

  static public float GetSpeed()
  {
    float speed = InitialSpeed + ((SpawnCount / 3.0f) * 2.5f);
    return speed > 250 ? 250 : speed;
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

  static private void RemoveAllCars()
  {
    foreach (Car car in RCars)
    {
      car.QueueFree();
    }
    RCars.Clear();
    foreach (Car car in DCars)
    {
      car.QueueFree();
    }
    DCars.Clear();
  }

  private void _startCarCleaner()
  {
    CarCleanerSemaphoreR = new Semaphore();
    CarCleanerSemaphoreD = new Semaphore();
    CarCleanerR.Start(this, nameof(ClearRightCars));
    CarCleanerD.Start(this, nameof(ClearDownCars));
  }

  static public void StartCarCleaner()
  {
    Instance._startCarCleaner();
  }

  static public void StopCarCleaner()
  {


    if (CarCleanerSemaphoreD != null)
    {
      CarCleanerSemaphoreD.Post();
    }

    if (CarCleanerSemaphoreR != null)
    {
      CarCleanerSemaphoreR.Post();
    }

    if (CarCleanerR.IsActive())
    {
      CarCleanerR.WaitToFinish();
    }
    if (CarCleanerD.IsActive())
    {
      CarCleanerD.WaitToFinish();
    }
  }

  static private void ClearRightCars()
  {
    while (GameRunning)
    {

      if (RCars.Count == 0)
      {
        GD.Print("Waiting R");
        CarCleanerSemaphoreR.Wait();
        if (!GameRunning)
        {
          GD.Print("GAME WAS NOT RUNNING");
          return;
        }
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
        if (!GameRunning)
        {
          GD.Print("GAME WAS NOT RUNNING");
          return;
        }
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
    Context.isFreshStart = false;
    Context.SpawnCount = 0;
    Context.CarsSafelyCrossed = 0;
    Context.RRemainingTime = Context.DEFAULT_REMAING_TIME;
    Context.DRemainingTime = Context.DEFAULT_REMAING_TIME;
    Context.DMoving = true;
    Context.RMoving = true;
    GameRunning = true;
    SpawnTimer.Start();
    TimeLimitTimer.Start();
    Context.StartCarCleaner();
  }

  static public void EndGame(String deathReason, bool isGameClosed = false)
  {
    Context.DeathReason = deathReason;
    GameRunning = false;
    GD.Print("Game Over");
    SpawnTimer.Stop();
    TimeLimitTimer.Stop();
    Context.StopCarCleaner();
    if (!isGameClosed)
    {
      System.Threading.Thread.Sleep(500);
    }
    Context.RemoveAllCars();
  }

  static public bool IsGameRunning()
  {
    return GameRunning;
  }

  static public bool IsFreshStart()
  {
    return isFreshStart;
  }

}

