using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Console.WriteLine ("Hello World"); //문자열 출력
    Console.WriteLine (); //줄바꿈(\n)
    Console.WriteLine("Hello World2");

    Console.WriteLine("Hello"+" "+"World");
    string s = "Hello"+" "+"World";
    Console.WriteLine(s);
    Console.WriteLine("{0:D} | {0:F}", -123, -123.45f); //{n번째인자:표현형식} D:정수 F:실수
    Console.WriteLine(
      "Decimal{0:D}\n" +  // 정수
      "Scientific{1:E}\n" +
      "Fixed Point{1:F}\n"+ // 부동소수점
      "General{0}\n" + // 아무것도안함?
      "General{1:G}\n"+
      "Hex{0:X}\n",
    -123, -123.45f);

    string bin = Convert.ToString(5, 2); // 5를 2진수로 변환
    Console.WriteLine(bin == "101"); // bin이 101이 맞는지 확인
    
    string hex = Convert.ToString(15, 16); // 15를 16진수로 변환
    Console.WriteLine(hex == "f"); // hex가 f가 맞는지 확인 TDD, 테스트케이스
  }
}