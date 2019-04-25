using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    // 딕셔너리는 해쉬테이블의 Generic버전이라고 할수있다.
    Dictionary<string, string> ht = new Dictionary<string, string>();
    ht.Add("txt", "notepad.exe");
    ht.Add("bmp", "paint.exe");
    ht.Add("dib", "paint.exe");
    ht.Add("rtf", "wordpad.exe");

    print(ht.Count == 4);
    try
    {
      ht.Add("txt", "winword.exe");
    } 
    catch
    {
      print("already exists!");
    }
    print(ht.Count == 4);
    
    ht["max"] = "3dsmax.exe";
    print(ht.Count == 5);

    print((string)ht["max"] == "3dsmax.exe");

    print(ht.ContainsKey("bmp") == true);
    ht.Remove("bmp");
    print(ht.ContainsKey("bmp") == false);

    Dictionary<int, User> users = new Dictionary<int, User>();
    users[100] = new User(100, "Hwang", 20);
    users[101] = new User(101, "brown", 30);
    users[102] = new User(102, "hash", 10);
    print(users.Count == 3);    
  }
}

public class User
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
  public User(int id, string name, int age)
  {
    Id = id;
    Name = name;
    Age = age;
  }
}

// 해쉬테이블을 딕셔너리의 Generic으로 바꿔보기