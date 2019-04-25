using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Shape[] shapes = {
      new Square(5, "Square #1"),
      new Circle(3, "Circle #1"),
    };
    double[] areas = new double[shapes.Length];
    for(int i = 0; i<shapes.Length; i++)
    {
      areas[i] = shapes[i].Area;
    } 
    Console.WriteLine(areas.Stringify() == "25 28.2743338823081");

    // Shape s = new Shape("tri"); // error 추상적(abstract) 클래스라서
  }
}


public abstract class Shape // abstract변수가 있으면 클래스에도 붙여준다
{
  string name;
  public Shape(string s)
  {
    Id = s;
  }
  public string Id
  {
    get {
      return name;
    }
    set{
      name = value;
    }
  }
  public abstract double Area { get; } 
  // 이안에서는 Area 프로퍼티를 구체화시킬수 없으니 상속받은 클래스에서 구체화해라
  // property도 override를 할수있다!!
  public override string ToString()
  {
    return $"{Id} Area = {Area}";
  }
}

public class Square : Shape
{
  int side;
  public Square(int side, string id) : base(id)  // id를 string s로 위로보냄 
  {
    this.side = side;
  }
  public override double Area
  {
    get {
      return side*side;
    }
  }
}

public class Circle : Shape
{
  int radius;
  public Circle(int radius, string id) : base(id)
  {
    this.radius = radius;
  }

  public override double Area
  {
    get {
      return radius * radius * Math.PI;
    }
  }
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  // 함수를 this - 오브젝트의 클래스에 끼워넣음
  // 열거형 using System.Collections.Generic; 필수
  // array string list stack 큐 ==> 열거가능한 형
  //
  {
    return String.Join(" ", list);
  }
  // public static string Stringify(this string list) 
  // {
  //   return String.Join(" ", list);
  // }
  // public static string Stringify(this char[] list) 
  // {
  //   return String.Join(" ", list);
  // }

  // sum, max를 Extension으로 만들어오기
}