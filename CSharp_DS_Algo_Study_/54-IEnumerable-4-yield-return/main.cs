using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    foreach(int n in OddNumbers())
      print(n);
    print("\n");

    IEnumerator<int> enumerator = OddNumbers().GetEnumerator();
    while(enumerator.MoveNext())
      print(enumerator.Current);
  }

  public static IEnumerable<int> OddNumbers() // Async function
  { 
    yield return 1;
    yield return 3;
    yield return 5;
  }
  // 장점 == 동시처리 유니티에서 코루틴으로 활용됨
}