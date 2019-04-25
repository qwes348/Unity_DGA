using System;

class MainClass 
{
  static double divide(int x, int y)
  {
    return (int)x/y;    
  }
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    Action act1 = () => print("Action");
    act1();

    Action<int> act2 = x => print(2 * x);
    act2(2);
    Action<double, double> act3 = (x, y) => Console.WriteLine(x/y);
    act3(10, 20);


    Func<int> fn1 = () => 10;
    print(fn1() == 10);
    Func<int, int, double> fn3 = divide;
    print(fn3(40, 20) == 2);

    Func<int, int, int, int, int, int, int, int, int, int, int, int, int> fn13;
    fn13 = (a, b, c, d, e, f, g, h, i, j, k, l) => a+b+c+d+e+f+g+h+i+j+k+l;
    Console.WriteLine(fn13(1,1,1,1,1,1,1,1,1,1,1,1) == 12);
  }
}

// Action은 리턴값이 없어야함
// Func<>의 마지막값은 리턴의 타입