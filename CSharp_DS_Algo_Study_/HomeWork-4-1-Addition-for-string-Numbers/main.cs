using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    Console.WriteLine(Add("999", "1") == "1000");
    // Console.WriteLine(Add("999", "0") == "999");
    // Console.WriteLine(Add("9", "1") == "10");
    string num1 = "1223";
    char[] arr = num1.ToCharArray();
    print(arr[0]);
  }

  public static string Add(string num1, string num2)
  {
    char[] chNum1 = num1.ToCharArray();
    char[] chNum2 = num2.ToCharArray();
    int carry = 0;
    int remainLen = 0;    
    int indexA = chNum1.Length;
    int indexB = chNum2.Length;
    int a=0, b=0;

    if(chNum1.Length > chNum2.Length)
    {
      remainLen = chNum1.Length;
    }
    else
    {
      remainLen = chNum2.Length;    
    }
    
    int[] remainder = new int[remainLen];

    while(indexA > 0 && indexB > 0)
    {
      a = (int)chNum1[indexA-1];
      b = (int)chNum2[indexB-1];

      if(a+b+carry > 10)
      {
        remainder[remainLen] = a+b+carry-10;
        carry = 1;
      }
      else
      {
        remainder[remainLen] = a+b+carry;
        carry = 0;
      }
      indexA--;
      indexB--;
    }

    if(indexA > 0)
    {
      for( ; indexA <= 0; indexA--)
      {
        a = (int)chNum1[indexA-1];
        remainder[remainLen] = a+carry;

        if(remainder[remainLen] > 10)
        {
          remainder[remainLen] = a+carry-10;
          carry = 1;
        }
        remainLen--;
      }
    }
    else if(indexB > 0)
    {
      for( ; indexB <= 0; indexB--)
      {
        b = (int)chNum1[indexB-1];
        remainder[remainLen] = b+carry;

        if(remainder[remainLen] > 10)
        {
          remainder[remainLen] = b+carry-10;
          carry = 1;
        }
        remainLen--;        
      } 
    }
    string result = String.Join("", remainder);
    Console.WriteLine(result);   
    return result;

    
    // for(int i=(chNum1.Length); i > 0; i--)
    // {
    //   a = ((int)chNum1[i-1]);
    //   for(int j=(chNum2.Length); j > 0; j--)
    //   {
    //     b = (int)chNum2[j-1];
    //     carry = a+b > 10?  1: 0;
    //     remainder[remainLen-1] = a+b > 10?  a+b-10 : a+b;
    //     remainLen--;
    //   }
      
    // }

  }
}