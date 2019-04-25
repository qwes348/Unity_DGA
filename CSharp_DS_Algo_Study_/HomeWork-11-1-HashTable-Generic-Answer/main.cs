using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    var ht = new HashTable<StudentID, Student>(10);
    Action <object> print = Console.WriteLine;
    ht.Add(new StudentID(100), new Student("Smith"));
    ht.Add(new StudentID(101), new Student("Dee"));
    ht.Add(new StudentID(1002), new Student("Baker"));
    ht.Add(new StudentID(10), new Student("Hwang"));
    print(ht.Stringify() == "10/100/101/1002/");
    ht[new StudentID(103)] = new Student("Kim");
    print(ht.Stringify() == "10/100/101/103/1002/");
    Student student = ht[new StudentID(1002)];
    print(student.ToString() == "Baker");
    // print(ht[new StudentID(1)].ToString() == null);

    print(ht.Remove(new StudentID(103)) == true);
    print(ht.Stringify() == "10/100/101/1002/");
    
    print(ht.ContainsKey(new StudentID(103)) == false);
    print(ht.ContainsKey(new StudentID(10)) == true);
  }

  class StudentID
  {
    int id;
    public StudentID(int id) { this.id = id; }
    public override bool Equals(object other)
    {
      return this.id == ((StudentID)other).id;
    }
    public override int GetHashCode()
    {
      return id;
    }
    public override string ToString() { return id.ToString(); }
  }

  class Student
  {
    string name;
    public Student(string name) { this.name = name; }
    public override string ToString() { return name; }
  }
}

public class HashTable<TKey, TValue>
{
  class Node
  {
    public TKey key;
    public TValue value;
    public Node next;
    public Node(TKey key, TValue value)
    {
      this.key = key;
      this.value = value;
      this.next = null;
    }
  }
  Node[] buckets;
  public HashTable(int size)
  {
    buckets = new Node[size];
  }

  public int HashFunc(TKey key)
  {
    return key.ToString().Length;
  }

  public bool Add(TKey key, TValue value)  // Add-------------------------
  {
    int index = HashFunc(key) % buckets.Length;
    if(buckets[index] == null)
    {
      buckets[index] = new Node(key, value);
      return true;
    }
    else
    {
      Console.WriteLine("Collision!");
      Node current = buckets[index];
      while(current != null)
      {
        if(current.key.Equals(key))
        {  
          Console.WriteLine("already exists");
          return false;
        }
        if(current.next == null)
          break;
        current = current.next;
      }
      Node newNode = new Node(key, value);
      current.next = newNode;
      return true;
    }
  }

  public TValue this[TKey key]
  {
    get
    {
      int index = HashFunc(key) % buckets.Length;
      if(buckets[index] != null)
      {
        Node current = buckets[index];
        while(current != null)
        {
          if(current.key.Equals(key))
            return current.value;
          current = current.next;
        }
      }
      return default(TValue);
    }
    set
    {
      Add(key, value);
    }
  }

  public bool Remove(TKey key)
  {
    int index = HashFunc(key) % buckets.Length;
    if(buckets[index] != null)
    {
      Node current = buckets[index];
      Node prev = null;
      while(true)
      {
        if(current.key.Equals(key))
        {
          if(prev == null)
          {
            buckets[index] = current.next;
          }
          else
          {
            prev.next = current.next;
          }
          return true;
        }
        if(current.next == null)
          break;
        prev = current;
        current = current.next;
      }
    }
    
    return false;
  }

  public bool ContainsKey(TKey key)
  {
    if(this[key] != null)
      return true;
    else
      return false;
  }

  public string Stringify()
  {
    string s = "";
    for(int i=0; i<buckets.Length; i++)
    {
      if(buckets[i] != null)
      {
        Node current = buckets[i];
        while(current != null)
        {
          s += current.key.ToString() + "/";
          current = current.next;
        }
      }
    }
    return s;
  }
}