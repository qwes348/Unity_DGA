using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    for(int i=0; i<9; i++)
    {
      for(int j=0; j<(8-i); j++)
      {
        Console.Write(" ");        
      }
      for(int x=(8-i); x+1<9; x++)
      {
        Console.Write("*");
      }

      Console.WriteLine();
    }

  }
}