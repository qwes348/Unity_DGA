using System;

class MainClass 
{
  // 재귀호출을 이용한 문자열길이 구하기
  // 재귀호출을 이용해 array의 최댓값 구하기
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    print(SumIterative(10) == 55);
    print(SumRecursive(10) == 55);

    // 0 1 2 3 4 5 6  7  8
    // 0 1 1 2 3 5 8 13 21 ...
    print(FiboRecursive(7) == 13);
    print(FiboIterative(7) == 13);
  }

  public static int FiboRecursive(int n)
  {
    if(n < 2)
      return n;
    return FiboRecursive(n-1) + FiboRecursive(n-2);
  }
  // 보통 Recursive보다 Iterative가 자원관리적으로 유리하다
  // 피보나치의 재귀호출함수는 2의 n승의 자원을 소모한다

  public static int FiboIterative(int n)
  {
    if(n < 2)
      return n;
    int fibo=0, fibo1 = 0, fibo2 = 1;
    for(int i =2; i<=n; i++)
    {
      fibo = fibo1 + fibo2;    // fibo1 전전 fibo2 전
      fibo1 = fibo2;
      fibo2 = fibo;
    }
    return fibo;
  }

  public static int SumIterative(int n)
  {
    int sum = 0;
    for(int i = 1; i <= n; i++)
    {
      sum += i;
    }
    return sum;
  }

  public static int SumRecursive(int n)
  {
    if(n < 2)
      return n;
    return SumRecursive(n-1) + n;  // 재귀호출
  }

  /* n==10
  SumRecursive(10-1) + 10
  9
  SumRecursive(9-1) + 9 + 10
  .....
  */
}