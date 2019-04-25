using System;

class MainClass 
{
  class Novel
  {
    public string title;
    public string genre;

    public string writer;
    public void Print()
    {
      Console.WriteLine($"{title}, {genre}, {writer}");
    }
  }

  class Magazine
  {
    public string title;
    public string genre;

    public int releaseDay;
    public void Print()
    {
      Console.WriteLine($"{title}, {genre}, {releaseDay}");
    }    
  }

  public static void Main (string[] args) 
  {
    Novel nov = new Novel();
    nov.title = "The Hobbit"; nov.genre = "Fanatasy";
    nov.writer = "J.R.R. Tolkien";
    nov.Print();
    Console.WriteLine();

    Magazine mag = new Magazine();
    mag.title = "Hello Computer Magazine";
    mag.genre = "Computer";
    mag.releaseDay = 1;
    mag.Print();
  }
}