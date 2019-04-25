using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    var names = new List<string>() {"John", "Tom", "Peter"};
    // 이렇게 타입이 확실할때는 var 타입을 쓰는게 좋다!!
    
    string s = "";
    foreach(var name in names)
    {
      s += name + " ";
    }
    Console.WriteLine(s == "John Tom Peter ");

    s = "";
    var enumerator = names.GetEnumerator(); // List<string>.Enumerator 타입
    while(enumerator.MoveNext())
    {
      s += enumerator.Current + " ";
    }
    Console.WriteLine(s == "John Tom Peter ");

    string nick = "Game Academy"; // or String nick = new string(new char[] {'G','a'...})
    s = "";
    var stringEnumerator = nick.GetEnumerator(); // CharEnumerator(타입)
    while(stringEnumerator.MoveNext())
    {
      s += stringEnumerator.Current;
    }
    Console.WriteLine(s == "Game Academy");
  }
}