using System;
using System.Collections;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Stack stack = new Stack();
    stack.Push(100);
    stack.Push(200);
    stack.Push(300);
    stack.Push(400);
    stack.Push(500);
    print(stack.Count == 5); // List와 인터페이스가 같다 즉 같은 내장 함수와 프로퍼티를 사용한다
    print((int)stack.Peek() == 500);
    print(stack.ToArray().Stringify() == "500 400 300 200 100");

    while(stack.Count > 0)
      print(stack.Pop());
    print(stack.Count == 0);
  }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list)
  {
    return String.Join(" ", list);
  }
}

// 제네릭버전으로 바꿔보기