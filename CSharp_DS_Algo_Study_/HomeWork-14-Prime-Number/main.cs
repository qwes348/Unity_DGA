using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

// https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/concepts/async/
// http://www.csharpstudy.com/csharp/CSharp-async-await.aspx

class MainClass 
{
  public static void Main (string[] args) 
  {

    CheckPrimerNumber p = new CheckPrimerNumber();
    p.AddTask(0, 11); //0부터 11까지 소수구하는 AddTask
    p.AddTask(12, 20);
    p.AddTask(21, 1000);
    p.Start().Wait(); // 밑에 GetNumberOfPrime이 먼저실행되지 않게 Block을 걸어줌

    int n = p.GetNumberOfPrime();
    Console.WriteLine("#PrimeNumber = " + n);
    Console.WriteLine(n == 168);

    // Console.ReadLine();
  }
}

public class CheckPrimerNumber
{
  List<Task> taskList = new List<Task>();  // AddTask로 실행전Task가 저장될 list
  List<int> primes = new List<int>(); // GetPrime으로 구한 primes가 저장될 list
  
  readonly object lockPrimes = new object();  // primes list를 임계구역(CS)으로 지정할 lock

  public void AddTask(int num1, int num2) 
  {
    // Console.WriteLine("AddRun");
    taskList.Add(new Task( () => GetPrime(num1, num2))); // AddTask에받은 param 2개를 GetPrime을 실행하는 Task에 저장
  }

  public void GetPrime(int num1, int num2)  
  // Task가 실행되면 소수를구함 primes리스트는 각 Task들이 동시에 접근 리스트이기 때문에 lock으로 잠궈 CS로 설정
  {
    for(int i=num1; i<=num2; i++)
    {
      if(IsPrime(i))
        {
          lock(lockPrimes)
          {
            primes.Add(i);
          }
        }
      // Console.WriteLine("GetPrime");
    }
  }

  public async Task<int> Start() // Task를 담아놓은 taskList에 foreach를돌며 실행 그리고 완료할때까지 대기해줌
  {
    foreach(var n in taskList)
    {
      n.Start();
    }
    await Task.WhenAll(taskList);
    return GetNumberOfPrime(); // Wait를 쓰기위해 return값을 만들어줌 사용은 안함
  }

  public int GetNumberOfPrime()
  {
    // await Task.WhenAll(taskList);
    // Console.ReadLine();
    return primes.Count;  // 소수를저장한 primes리스트의 원소수를 리턴
  }

  public static bool IsPrime(int number)
  {
    if(number < 2)
      return false;
    if(number % 2 == 0 && number != 2)
      return false;
    for(int i=2; i < number; i++)
    {
      if(number % i == 0)
        return false;
    }
    return true;
  }  
}