using System;

delegate int CallBack(int a, int b);   // 델리게이트 선언

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    // print 안에 Console.WriteLine 함수를 담았으니 델리게이트와 비슷함
    
    // 1번방식
    CallBack cb = new CallBack(Sum);
    // 델리게이트 변수(같은 반환형과 같은 파라미터의 함수를 담을수 있다)

    // 2번방식
    CallBack cb2;
    cb2 = Sum;    // C의 함수포인터와 비슷함
    
    print(cb(1, 2) == 3);
    print(cb2(1, 2) == 3);
    
    cb = delegate(int i, int j){ return i+j; };  // 익명함수
    print(cb(1, 2) == 3);

    cb = (int i, int j) => { return i+j;};     // 람다식
    print(cb(1, 2) == 3);

    cb = (i, j) => i+j;   // 타입 생략, return문 생략, 중괄호 생략
    print(cb(1, 2) == 3);
  }
  public static int Sum(int a, int b)
  {
    return a+b;
  }
}