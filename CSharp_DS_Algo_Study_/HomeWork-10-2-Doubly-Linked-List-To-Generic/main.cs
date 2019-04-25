using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    LinkedList<string> list = new LinkedList<string>();

    // AddLast
    list.AddLast("one");
    print(list.Stringify() == "one");
    list.AddLast("two");
    print(list.Stringify() == "onetwo");
    list.AddLast("three");
    print(list.Stringify() == "onetwothree");
    
    // AddFirst
    list.AddFirst("four");
    print(list.Stringify() == "fouronetwothree");
    list.AddFirst("five");
    print(list.Stringify() == "fivefouronetwothree");
    print(list.Count == 5);     
    
    // Search
    print(list.Search("five").data == "five");
    print(list.Search("five").next.data == "four");

    // Stringify, ReverseStringify
    print(list.Stringify() == "fivefouronetwothree");
    print(list.ReverseStringify() == "threetwoonefourfive");

    // RemoveFirst, RemoveLast, count
    print(list.RemoveFirst() == "five");
    print(list.RemoveFirst() == "four");
    print(list.RemoveLast() == "three");
    print(list.RemoveLast() == "two");
    print(list.RemoveLast() == "one");
    print(list.RemoveLast() == "");
    print(list.Stringify() == "");
    print(list.Count == 0);
    // Node Search(str)

    // int Type Test
    LinkedList<int> list2 = new LinkedList<int>();
    list2.AddFirst(1);
    list2.AddLast(2);
    list2.AddLast(3);
    list2.AddLast(4);
    
    print(list2.Stringify() == "1234");
    print(list2.ReverseStringify() == "4321");

    print(list2.RemoveFirst() == "1");
    print(list2.RemoveLast() == "4");
    print(list2.Count == 2);
    print(list2.Search(2).next.data == 3);
  }
}

public class Node<T>
{
  public T data;
  public Node<T> next;
  public Node<T> prev;
}

public class LinkedList<T>
{
  Node<T> head;
  Node<T> tail;

  public LinkedList()
  {
    head = tail = null;
  }

  public int Count
  {
    get
    {
      Node<T> current = head;
      int count = 0;
      while(current != null)
      {
        current = current.next;
        count++;
      }
      return count;
    }
  }

  public void AddFirst(T obj)
  {
    Node<T> newNode = new Node<T>();
    newNode.data = obj;
    newNode.prev = null;
    if(head == null && tail == null)
      head = tail = newNode;
    else
    {
      head.prev = newNode;
      newNode.next = head;
      head = newNode;
    }
  }

  public void AddLast(T obj)
  {
    Node<T> newNode = new Node<T>();
    newNode.data = obj;
    newNode.next = null;
    if(head == null && tail == null)
      head = tail = newNode;
    else
    {
      tail.next = newNode;
      newNode.prev = tail;
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
      if(head.next != null)
      {
        T v = head.data;
        head = head.next;
        head.prev = null;
        return v.ToString();
      }
      else
      {
        T v = head.data;
        head = tail = null;
        return v.ToString();
      }
      
    }
  }

  public string RemoveLast()
  {
    if(tail == null)
    {
      return string.Empty;
    }
    else
    {
      // Console.WriteLine("pass");
      if(tail.prev != null)
      {
        string v = tail.data.ToString();
        tail = tail.prev;
        tail.next = null;
        return v.ToString();
      }
      else
      {
        string v = tail.data.ToString();
        head = tail = null;
        return v.ToString();
      }

    }
  }

  public string Stringify()
  {
    Node<T> current = head;
    string s = "";
    while(current != null)
    {
      s += current.data;
      current = current.next;
    }
    return s;
  }  

  public string ReverseStringify()
  {
    Node<T> current = tail;
    string s = "";
    while(current != null)
    {
      s += current.data;
      current = current.prev;
    }
    return s;
  }

  public Node<T> Search(T str)
  {
    Node<T> current = head;
    while(current != null)
    {
      if(current.data.ToString() == str.ToString()) // current.data.Equals(obj)
        return current;
      else
        current = current.next;
    }
    return null;
  }
  
}