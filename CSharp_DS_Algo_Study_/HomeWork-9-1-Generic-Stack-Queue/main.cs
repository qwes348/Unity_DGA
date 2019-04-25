using System;
using System.Collections;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Stack<int> stack = new Stack<int>();
    stack.Push(100);
    stack.Push(200);
    stack.Push(300);
    stack.Push(400);
    stack.Push(500);
    print(stack.Count == 5); 
    print(stack.Peek() == 500);
    print(stack.ToArray().Stringify() == "500 400 300 200 100");

    while(stack.Count > 0)
      print(stack.Pop());
    print(stack.Count == 0);


    Queue<string> q = new Queue<string>();
    q.Enqueue("James");
    q.Enqueue("Brown");
    q.Enqueue("Fox");
    print(q.Count == 3);
    print(q.ToArray().Stringify() == "James Brown Fox");

    while(q.Count > 0)
      print(q.Dequeue());
    print(q.Count == 0);

  }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list)
  {
    return String.Join(" ", list);
  }
}