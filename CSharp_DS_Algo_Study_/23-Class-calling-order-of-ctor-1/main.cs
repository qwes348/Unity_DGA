using System;

class MainClass 
{
  class Book
  {
    public string title;
    
    public Book()
    {
      Console.WriteLine("Book ctor");
    }
  }
  class Novel : Book  
  {
    public Novel() : base()
    {
      Console.WriteLine("Novel ctor");
    }
  }

  class SFNovel : Novel  
  {
    public SFNovel() : base()
    {
      Console.WriteLine("SFNovel ctor");
    }
  }

  public static void Main (string[] args) 
  {
    SFNovel nov = new SFNovel();
    nov.title = "The Hobbit";
  }
}

// 생성자 순서는 부모-자식-자식