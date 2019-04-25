using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;     // void함수만 담는 Action
    // Func<object> print = Console.WriteLine;    // 리턴값이 있는 함수는 Func

    string s;
    s = "ABC";
    print(s.Length == 3);

    s = "ABCDEF";
    print(s.IndexOf("CDE") == 2);   // 첫번째 값인 C의 위치 2를 리턴
    print(s.IndexOf("ZZZ") == -1);  // 없으면 -1 리턴

    s = "Mr Song";
    print(s.Replace("Mr", "Miss") == "Miss Song");   // 문자열을 교체

    int[] arrayA = {1, 3, 5, 7, 9};
    print(String.Join(" ", arrayA) == "1 3 5 7 9");   // int배열을 문자열로 바꿈
    print(Stringify(arrayA) == "1 3 5 7 9");   // int배열을 문자열로 바꿈

    s = "1000,2000,3000";
    string[] prices = s.Split(',');    // ','를 기준으로 나눠서 배열에 담음
    print(String.Join(" ", prices) == "1000 2000 3000");

    s = "1000, 2000, 3000";           // ','를 기준으로 나누는데 공백도 제거
    prices = s.Replace(" ", "").Split(',');    
    print(String.Join(" ", prices) == "1000 2000 3000");    
    print("" == String.Empty && "" == string.Empty);   

    s = "ABCDEF";
    print(s.Substring(1, 3) == "BCD");     // 문자열의 1번부터 3개를 가져옴
    // print(s.Substring(7, 3));           OutOfRangeException 에러

  }

  public static string Stringify(int[] list)
  {
    return String.Join(" ", list);
  }
}