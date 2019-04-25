using System;
using System.Collections.Generic;

class MainClass 
{
  static List<string> nonRepeaters = new List<string>(); 
  static List<string> repeaters = new List<string>(); 
    
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    
    Student stuA = new Student();
    stuA.Name = "Steve";
    stuA.Scores = new List<int>() {50, 60, 70, 80, 90};

    Student stuB = new Student();
    stuB.Name = "Brown";
    stuB.Scores = new List<int>() {100, 60, 70, 80, 90};    

    Student stuC = new Student();
    stuC.Name = "Hwang";
    stuC.Scores = new List<int>() {100, 90, 80, 80, 90};
    
    Student stuD = new Student();
    stuD.Name = "Kim";
    stuD.Scores = new List<int>() {40, 50, 0, 80, 90};

    // new Student() {Name="Kim", Scores = new List<int>() {...}} 이렇게 선언도가능
    

    RepeaterCheck(stuA);
    RepeaterCheck(stuB);
    RepeaterCheck(stuC);
    RepeaterCheck(stuD);

    print(nonRepeaters.Stringify() == "Brown Hwang");
    print(repeaters.Stringify() == "Steve Kim");
  }

  public static void RepeaterCheck (Student stu)
  {
    var list = stu.Scores.FindAll(x => x < 60);
    if(list.Count > 0)
      repeaters.Add(stu.Name);
    else
      nonRepeaters.Add(stu.Name);
  }
}

public class Student
{
  public string Name { get; set; }
  public List<int> Scores { get; set; }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list)
  {
    return String.Join(" ", list);
  }
}



/* public class Student
{
  public string Name { get; set; }
  public List<int> Scores { get; set; }
}
점수중에 60점 미만이 하나라도 있으면 낙제생 리스트(이름) 넣기
낙제생이 아닌 사람들 리스트(아룸) 구하기

Console.WriteLine(nonRepeaters.Stringify() == "ctkim Won);
Console.WriteLine(repeaters.Stringify() == "Steve Brown);
*/