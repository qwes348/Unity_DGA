using System;
using System.Text.RegularExpressions;

class MainClass 
{
  public static void Main (string[] args) 
  {
    string passWord1 = "aaabbbc";  // 연속중복문자
    string passWord2 = "123!abcd";  // no 대문자  
    string passWord3 = "123  abc";  // 공백
    string passWord4 = "123!Abc";   
    string passWord5 = "h1B@3;Qa";
    string passWord6 = "48abcd;"; // no 소문자
    string passWord7 = "!@#Av$%^&*'.>";

    Console.WriteLine(CheckPassword(passWord1) == false);
    Console.WriteLine(CheckPassword(passWord2) == false);
    Console.WriteLine(CheckPassword(passWord3) == false);
    Console.WriteLine(CheckPassword(passWord4) == true);
    Console.WriteLine(CheckPassword(passWord5) == true);
    Console.WriteLine(CheckPassword(passWord6) == false);
    Console.WriteLine(CheckPassword(passWord7) == true);


  }


  public static bool CheckPassword(string passWord)
  {
    if(passWord.Length < 6)
    {
      Console.WriteLine("passWord is Too Short!");
      return false;
    }
    else if(passWord.Length > 15)
    {
      Console.WriteLine("passWord is Too Long!");
      return false;
    }
    else
    {
      if(CheckContinuity(passWord)) // 연속문자 검사
      {
        // Console.WriteLine("true");
        if(CheckCapitalSmall(passWord)) // 적어도 하나의 소,대문자 검사
        {
          // Console.WriteLine("true");
          if(CheckSpecialnWhite(passWord)) // 공백, 특수문자 검사
          {
            // Console.WriteLine("true");
            return true;
          }
          else
          {
            // Console.WriteLine("false");
            return false;
          }
        }
        else
        {
          // Console.WriteLine("false");
          return false;
        }
      }
      else
      {
        // Console.WriteLine("false");
        return false;
      }
    }
  }

  public static bool CheckContinuity(string passWord) // 연속문자 검사
  {
    Regex regex = new Regex(@"(.)\1+");
    
      // if(regex.IsMatch(passWord))
      //   return false;
      // else
      //   return true;

    return !(regex.IsMatch(passWord));
  }

  public static bool CheckCapitalSmall(string passWord) // 적어도 하나의 소,대문자 검사
  {
    Regex regexSmall = new Regex(@"[a-z]");
    Regex regexCapital = new Regex(@"[A-Z]");
    bool matchSmall = regexSmall.IsMatch(passWord);
    bool matchCapital = regexCapital.IsMatch(passWord);

    if(matchSmall && matchCapital)
      return true;
    else
      return false;

    // Regex smallCapital = new Regex(@"^*[a-zA-z]$");
    // bool match = smallCapital.IsMatch(passWord);

    // return match;
  }

  public static bool CheckSpecialnWhite(string passWord) // 공백, 특수문자 검사
  {
    Regex regexSpecial = new Regex(@"[!@#\$%\^&\*\(\)\?/>.<,:;'\\\|\}\]\{\[_~`+=\-""]");
    // Regex regexWhithe = new Regex(@"^\S");

    bool matchSpecial = regexSpecial.IsMatch(passWord);
    // bool matchWhite = regexWhithe.IsMatch(passWord);

    // if(matchSpecial && matchWhite)
    //   return true;
    // else
    //   return false;

    return matchSpecial;
  }
}


/* 
1. Check passWord
2. 주어진 password 문자열이 다음 규칙을 만족하는지 알려주는 CheckPassword(string password) 작성
https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/tokens/verbatim
*/

  