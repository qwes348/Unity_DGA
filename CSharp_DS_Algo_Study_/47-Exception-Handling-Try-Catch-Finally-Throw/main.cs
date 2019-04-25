using System;

// class Calc
// {
//   public float Divide(int x, int y)
//   {
//     float z = 0;
//     try
//     {
//       z = x/y;
//       return z;
//     }
//     catch(Exception e)
//     {
//       Console.WriteLine(e. Message);
//     }
//     finally // Exception이 있든없든 무조건 실행, 단독사용 불가
//     {
//       Console.WriteLine("finally");
//     }
//     return 0;
//   }
// }

// class MainClass 
// {
//   public static void Main (string[] args) 
//   {
//     Calc c = new Calc();
//     Console.WriteLine(c.Divide(10, 1) == 10);
//     Console.WriteLine(c.Divide(10, 0) == 0);
//   }
// }

class Calc
{
  public float Divide(int x, int y)
  {
    float z = 0;
    try
    {
      z = x/y;
      return z;
    }
    catch(Exception e)
    {
      Console.WriteLine("Divide(): " + e.Message);
      throw e;
    }
    finally // Exception이 있든없든 무조건 실행, 단독사용 불가
    {
      Console.WriteLine("finally");
    }
    return 0;
  }
}

class MainClass 
{
  public static void Main (string[] args) 
  {
    Calc c = new Calc();
    // Console.WriteLine(c.Divide(10, 1) == 10);
    try
    {  
      Console.WriteLine(c.Divide(10, 0) == 0);
    }
    catch(Exception e)
    {
      Console.WriteLine("Main(): "+ e.Message);
    }
  }
}