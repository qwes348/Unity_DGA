using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    print(Add("999", "1") == "1000");
    print(Add("999", "0") == "999");
    print(Add("9", "1") == "10");
    print(Add("123", "12") == "135");
    print(Add("123", "10000")== "10123");
    print(Add("0", "0") == "0");
    print(Add("1900000000000000000000000008",
              "9900000000000000000000009999")==
              "11800000000000000000000010007");

  }

  public static string Add(string num1, string num2)
  {
    int carry = 0;
    int remainder = 0;
    string result = string.Empty;
    int a=0, b=0;


    if(num1.Length > num2.Length)
    {
      while(num1.Length > num2.Length)
      {
        num2 = "0"+num2;
      }
    }
    else if(num1.Length < num2.Length)
    {
      while(num1.Length < num2.Length)
      {
        num1 = "0"+num1;
      }
    }
    // **Length To Same Test**
    // Console.WriteLine(num1.Length);
    // Console.WriteLine(num2.Length);

    for(int i=num1.Length-1; i>=0; i--)
    {
      a = int.Parse(num1[i].ToString());
      b = int.Parse(num2[i].ToString());
      a+=carry;
      carry = 0;
      carry = a+b >= 10 ? 1 : 0;
      // ** Print Carry Test **
      // Console.WriteLine("c:"+carry.ToString());
      remainder = a+b >= 10? a+b-10 : a+b;
      // ** Print Remainder Test **
      // Console.WriteLine("r:"+remainder.ToString());
      result = remainder.ToString() + result;
    }
    if(carry == 1)
    {
      result = carry.ToString() + result;
    }

    return result;
  }
}