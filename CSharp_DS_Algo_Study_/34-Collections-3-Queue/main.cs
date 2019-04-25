using System;
using System.Collections;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Queue q = new Queue();
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

