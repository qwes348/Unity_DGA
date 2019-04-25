using System;

delegate int Callback(int a, int b);

class MainClass 
{
  static int Sum(int i, int j)
  {
    Console.WriteLine("Sum");
    return i+j;
  }

  static int Multiply(int i, int j)
  {
    Console.WriteLine("Multiply");
    return i*j;
  }

  static int Power(int i, int j)
  {
    Console.WriteLine("Power");
    return (int)Math.Pow((double)i, (double)j);  // i**j
  }

  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    
    Callback cb;
    cb = Sum;
    print(cb(1,2) == 3);

    cb += Multiply;
    print(cb(3, 2) == 6);
    cb += Power;
    print(cb(3, 2) == 9);  // 가장마지막에 더한 Power가 실행된다
    // 호출결과로 알수있는것 = Sum,  Multiply도 호출은 되지만 return은 Power값을가짐

    cb -= Multiply;
    print(cb(3, 2) == 9);   //Sum과 Power만 호출하고 리턴값은 Power
  }
}