using System;
using System.Threading;
using System.Threading.Tasks;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    Console.WriteLine("in Main: " + Thread.CurrentThread.ManagedThreadId); // 1

    Task task0 = new Task( () => {
      Thread.Sleep(1000);
      Console.WriteLine("in task: " + Thread.CurrentThread.ManagedThreadId); // 4
    });
    // task0.Start();
    task0.RunSynchronously();
    
    Task task1 = Task.Run( () => { // Run 메소드로 Start없이 실행
      for(int i=0; i < 5; i++)
      {
        Thread.Sleep(1000);
        Console.WriteLine("in task1: " + Thread.CurrentThread.ManagedThreadId);     
      }
    });

    // Factory에서 꺼내서 바로실행
    Task task2 = Task.Factory.StartNew( () => Console.WriteLine("task2 !"));

    task0.Wait();  // Task가 종료될때까지 대기
    task1.Wait();
    task2.Wait();
  }
}