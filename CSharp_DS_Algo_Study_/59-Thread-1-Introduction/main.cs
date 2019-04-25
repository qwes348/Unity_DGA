using System;
using System.Threading;

// https://magi82.github.io/process-thread/

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    print("Thread ID: " + Thread.CurrentThread.ManagedThreadId);
    // Main함수를 실행중인 Thread의 ID

    Thread t = new Thread(Todo);
    print("before: " + t.ThreadState);  // Unstarted
    t.Start();
    print("after: " + t.ThreadState);  // Running
    
    // delegate방식의 Thread생성법
    // delegate void ThreadStart();
    // Thread t2 = new Thread(new ThreadStart(Todo));
    // t2.Start();

    // Console.ReadLine();
    t.Join();  // MainThread에 합류할때까지 대기(t가 종료할때까지 기다려달라)

    print("end: " + t.ThreadState);   // Stopped
  }

  static void Todo()
  {
    Thread.Sleep(500);  // 0.5s
    Console.WriteLine("Todo: " + Thread.CurrentThread.ManagedThreadId);
  }
}
