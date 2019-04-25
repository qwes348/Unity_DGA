using System;


class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    LinkedList list = new LinkedList();
    list.AddLast("one");
    print(list.Stringify() == "one");
    list.AddLast("two");
    print(list.Stringify() == "onetwo");
    list.AddLast("three");
    print(list.Stringify() == "onetwothree");
    print(list.Count == 3);     
    
    print(list.RemoveFirst() == "one");
    print(list.Stringify() == "twothree");
    list.RemoveFirst();
    list.RemoveFirst();
    print(list.RemoveFirst() == "");
    print(list.Stringify() == "");
    print(list.Count == 0);
  }
}

public class Node
{
  public string data;
  public Node next;

}

public class LinkedList
{
  Node head;
  Node tail;

  public LinkedList()
  {
    head = tail = null;
  }

  public int Count
  {
    get
    {
      Node current = head;
      int count = 0;
      while(current != null)
      {
        current = current.next;
        count++;
      }
      return count;
    } 
  }

  public void AddLast(string obj)
  {
    Node newNode = new Node();
    newNode.data = obj;
    newNode.next = null;
    if(head == null)
      head = tail = newNode;
    else
    {
      tail.next = newNode;
      tail = newNode;
    }
  }

  public string RemoveFirst()
  {
    if (head == null)
    {
      return string.Empty;
    }
    else
    {
      string v = head.data;
      head = head.next;
      return v;
    }
  }

  public string Stringify()
  {
    Node current = head;
    string s = "";
    while(current != null)
    {
      s += current.data;
      current = current.next;
    }
    return s;
  }
}

/*
Doubly LinkedList
AddLast(), AddFirst(), RemoveFirst(), RemoveLast(), ReverseStringify()
Node Search(str)
위 함수들을 포함할것!
Generic버전으로 바꿀것
*/