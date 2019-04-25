using System;
using System.Threading;
using System.Threading.Tasks;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Task<string> taskChain = Task<int>.Run( () => {
      Console.WriteLine("1 task");
      Thread.Sleep(100);
      return 100;
    }).ContinueWith( task => {
      Console.WriteLine();
      Console.WriteLine(task.Result);  // 값이 나올때까지 Block input : 100
      Console.WriteLine("2 task");
      Thread.Sleep(100);
      return 200;
    }).ContinueWith( task => {
      Console.WriteLine();
      Console.WriteLine(task.Result); // input : 200
      Console.WriteLine("3 task");
      Thread.Sleep(100);  
      return 300.ToString();    
    });

    // 순차처리가 아닌 task를 순차처리처럼 만드는 방법 taskChain
    Console.WriteLine("in Main");
    Console.WriteLine(taskChain.Result == "300");
  }
}