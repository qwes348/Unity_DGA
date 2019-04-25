using System;
using System.Threading;

// lock에 사용되면 안되는 object
// 1. this
// 2. string
// 3. GetType() : Type

class Counter
{
  public int Count { get; set; }
  readonly object thisLock = new object();
  public void Increase()  // Critical Section(임계구역 CS)
  {
    lock(thisLock)
    {
      Count++;
    }
  }
}

class MainClass 
{
  public static void Main (string[] args) 
  {
    Counter c = new Counter();
    Thread t1 = new Thread(c.Increase);
    Thread t2 = new Thread(c.Increase);
    t1.Start();
    t2.Start();

    Console.ReadLine();
  }
}