using System;
using static System.Math;  // Math안에있는 static 멤버들을 그냥쓰게한다
using static System.Console;   
using Project = Outer.A.Car;  // 별칭

class MainClass 
{
  public static void Main (string[] args) 
  {
    Console.WriteLine ("Hello World");
    Console.WriteLine(PI);  // Math.PI
    WriteLine(PI);

    Outer.A.Car a = new Outer.A.Car();
    var a2 = new Outer.A.Car();
    WriteLine(a.name == "Car in namespace A");
    WriteLine(a2.name == "Car in namespace A");

    Project a3 = new Outer.A.Car();
    var a4 = new Project();

    var b = new Outer.B.Car();
    WriteLine(b.name != a.name);
  }
}

namespace Outer
{
  namespace A
  {
    class Car
    {
      public string name = "Car in namespace A";
    }
  }
  namespace B
  {
    class Car
    {
      public string name = "Car in namespace B";
    }
  }
}