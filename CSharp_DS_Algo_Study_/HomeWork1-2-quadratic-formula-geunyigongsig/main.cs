using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    int a=2;
    int b=-9;
    int c=10;
    double D=0;
    double minusA = 0;
    double plusA = 0;
    double resultA = 0;
    double resultB = 0;
    Console.WriteLine("계산할 식");
    Console.WriteLine(a+"x^2"+b+"x"+c);
    Console.WriteLine();
    
    D = Math.Pow(b,2)-4*a*c;

    if(D < 0)
    {
      Console.WriteLine("허근입니다.");
    }
    else if(D > 0)
    {
      // D = Math.Pow(b,2)-(4*(a*c));    // if문 전에 선언
      Console.WriteLine("Plus root");
      plusA = (-b+Math.Sqrt(D)) / (2*a);
      //plusA = (-b)+(Math.Sqrt(Math.Pow(b,2)-(4*(a*c))))/(2*a); // 더 간결하게 선언
      Console.WriteLine(plusA);

      Console.WriteLine("Minus root");
      minusA = (-b-Math.Sqrt(D)) / (2*a);
      // minusA = (-b-Math.Sqrt(Math.Pow(b,2)-4*a*c))/(2*a);
      Console.WriteLine(minusA);
    }
    else
    {
      Console.WriteLine("근이 1개인 이차방정식");
      plusA = (-b+Math.Sqrt(D)) / (2*a);
      minusA = plusA;

      Console.WriteLine(plusA);
      Console.WriteLine(minusA);
    }
    Console.WriteLine();
    Console.WriteLine("근이 맞는지 확인");
    resultA = a*(plusA*plusA)+(b*plusA)+c;
    Console.WriteLine(resultA == 0);

    resultB = a*(minusA*minusA)+(b*minusA)+c;
    Console.WriteLine(resultB == 0);
  }

  /* 시행착오
   나누기인 /연산자의 우선순위가 *보다 높은것같음
   C#에서는 초기화 되지않은 변수는 사용할수 없다고함.
   */
}