using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    for(int i=0; i<8; i++)
    {
      for(int j=1; j<8-i; j++)
      {
        Console.Write(" ");
      }
      for(int j=1; j<=2*i-1; j++)
      {
        Console.Write("*");
      }
      Console.WriteLine();
    }

    for(int i=8; i>0; i--)
    {
      for(int j=7; j<8-i; j--)
      {
        Console.Write(" ");
      }
      for(int j=16; j>=i/2-1; j--)
      {
        Console.Write("*");
      }
      Console.WriteLine();
    }
  }
}