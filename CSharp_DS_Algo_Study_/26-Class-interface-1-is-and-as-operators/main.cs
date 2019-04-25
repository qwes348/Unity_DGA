using System;
using System.Collections.Generic;

interface IEquatable  // 관례상 I를붙이는 interface
{
  bool Equals(Car obj);  // public + abstract (디폴트가 public, abstract)
  // 모든멤버가 public 하고 abstract 하다
}

interface ICloneable : IEquatable, IComparable, IStringifyable
{
  Car Clone();
}
// interface : 규약, 조건 (?)

interface IStringifyable
{
  string Stringify();
}

// 숙제: IStringifyable을 만들어오기
class Car : ICloneable // or IEquatable, ICloneable, IStringifyable, IComparable
{
  public string Make { get; set; }
  public string Model { get; set; }
  public string Year { get; set; }

  public bool Equals(Car car)
  {
    if(Make == car.Make && Model == car.Model && Year == car.Year)
      return true;
    else
      return false;
  }

  public Car Clone()
  {
    Car car = new Car();
    car.Make = Make;
    car.Model = Model;
    car.Year = Year;
    return car;
  }

  public string Stringify()
  {
    string[] carArr = new string[3] {this.Make, this.Model, this.Year};
    return String.Join(" ", carArr); 
  }

  public int CompareTo(object obj)
  {
    return this.Year.CompareTo((obj as Car).Year);  // Asc

    // return (obj as car).Year.CompareTo(this.Year); // Dec
  }

  public override string ToString()
    {
      return Make+Year;
    }
}

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    
    Car car = new Car();
    car.Make = "BMW";
    car.Model = "Mini";
    car.Year = "2018";

    Car car2 = new Car();
    car2.Make = "BMW";
    car2.Model = "Mini";
    car2.Year = "2017";

    Car car3 = new Car();
    car3.Make = "Volvo";
    car3.Model = "X";
    car3.Year = "2012";

    print(car.Equals(car2) == true);

    Car clone = car.Clone();
    print(clone.Equals(car) == true);
    clone.Make = "Tesla";
    print(car.Make == "BMW");
    print(clone.Make == "Tesla");

    // print(car is int == false);
    // print(car is Cake == false);
    print(car is Car == true);   // 상속관계인지 클래스를 확인해주는 is
    print(car is IEquatable == true);  // interface도 확인가능
    print(car is ICloneable == true);
    print(car is object);

    ICloneable ic = car as ICloneable;
    print(ic != null);
    print(ic.GetType().Name == "Car");
    
    print(car.Stringify() == "BMW Mini 2018");
    print(clone.Stringify() == "Tesla Mini 2018");

    IComparable[] cars = {car.ToString(), car2.ToString(), car3.ToString()};
    Array.Sort(cars);
    print(cars.ToString());
  }
}

class Cake
{

}

// public static class ClassExtension
// {
//   public static string Stringify<T>(this IEnumerable<T> list) 
//   // 함수를 this - 오브젝트의 클래스에 끼워넣음
//   // 열거형 using System.Collections.Generic; 필수
//   // array string list stack 큐 ==> 열거가능한 형
//   //
//   {
//     return String.Join(" ", list);
//   }
//   // public static string Stringify(this string list) 
//   // {
//   //   return String.Join(" ", list);
//   // }
//   // public static string Stringify(this char[] list) 
//   // {
//   //   return String.Join(" ", list);
//   // }
// }