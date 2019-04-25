using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    print("*Two Root*");
    int a = 2;
    int b = -9;
    int c = 10;
    
    double D = Math.Pow(b,2)-4*a*c;
    double[] root = new double[2];
    
    QuadraticFormula(a, b, c, D, root);
    if(root.Length > 1)
    {
      print("aX^2+bX+C의 근1: "+root[0]);
      print("aX^2+bX+C의 근2: "+root[1]);
      print(a*(root[0]*root[0])+(b*root[0])+c == 0);
      print(a*(root[1]*root[1])+(b*root[1])+c == 0);
    }
    else
    {
      print("aX^2+bX+C의 근: "+root[0]);
      print(a*(root[0]*root[0])+(b*root[0])+c == 0);    
    }
    
  }

  public static void QuadraticFormula(int a, int b, int c, double D, double[] root)
  {

    if(D>0)
    {
      root[0] = (-b+Math.Sqrt(D)) / (2*a);
      root[1] = (-b-Math.Sqrt(D)) / (2*a);
    }
    else if(D==0)
    {
      root[0] = (-b+Math.Sqrt(D)) / (2*a);
    }
    else
    {
      Console.WriteLine("해가 없습니다.");
      return;
    }
  }
}