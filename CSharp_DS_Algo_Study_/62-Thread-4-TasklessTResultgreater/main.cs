using System;
using System.Threading;
using System.Threading.Tasks;

class MainClass 
{
  public static void Main (string[] args) 
  {
    // Task<return Type>
    Task<string> task = new Task<string>( () => { // Func<string> delegate(retrun값 있음)
      Thread.Sleep(1000);
      Console.WriteLine("in Task");
      return "Task result";
    });
    task.Start();
    Console.WriteLine("in Main");

    // 네트워크에서 많이쓰임 대기해야할 일이 많기때문에
    Console.WriteLine(task.Result == "Task result"); // 이문장에서 main함수 Block
  }
}