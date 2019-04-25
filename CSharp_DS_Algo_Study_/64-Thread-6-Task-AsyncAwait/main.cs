using System;
using System.Threading;
using System.Threading.Tasks;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Chain();
    Console.ReadLine();
  }

  public static async void Chain()  // async가 나오면 내부에 await가 무조건 있어야함
  // async = 중간에 멈췄다 진행할수 있는 함수
  {
    int a = await Task<int>.Run( ()=> { // 해당 Task가 종료될때까지 대기
      Thread.Sleep(100);
      Console.WriteLine("1 task");
      return 100;
    });
    int b = await Task<int>.Run( ()=> {
      Thread.Sleep(100);
      Console.WriteLine("1 task");
      return 200 + a;
    });
    int result = await Task<int>.Run( ()=> {
      Thread.Sleep(100);
      Console.WriteLine("1 task");
      return 300 + b;
    });        
    Console.WriteLine(result == 600);
    // result값이 나올때까지 main이 종료되지않는걸 보장받는 이유 = await가 있기때문
  }
}