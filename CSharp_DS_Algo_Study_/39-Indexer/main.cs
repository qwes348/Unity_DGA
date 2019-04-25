using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Student s = new Student();
    s[0] = 0;  // Dictionary 와 Hashtable의 방식
    s[1] = 1;  // Dictionary 와 Hashtable은 Indexer를 사용해서 만듦
    s[2] = 2;
    s[3] = 3;
    
    print(s[2] == 2);
  }
}

public class Student
{
  int[] scores = new int[4];
  
  public int this[int i]  // Indexer 
  {
    get {return scores[i];}
    set {scores[i] = value;}
  }
}