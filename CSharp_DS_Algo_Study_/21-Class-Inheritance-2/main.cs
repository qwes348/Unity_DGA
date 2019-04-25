using System;

class MainClass 
{
  class Book
  {
    public string title;
    public string genre;

    public virtual void Print()  // 상위클래스는 virtual 붙여줌
    {
      Console.Write($"{title}, {genre}, ");
    }
  }
  class Novel : Book  // Book 클래스를 상속받음
  {
    public string writer;
    public override void Print()
    {
      base.Print();
      Console.WriteLine($"{writer}");
    }
  }

  class Magazine : Book  // Book 클래스를 상속받음
  {
    public int releaseDay;
    public override void Print() // 하위클래스는 override 붙여줌
    {
      base.Print();  // 이줄을 빼면 기능추가가 아닌 재정의하는 함수가 됨
      Console.WriteLine($"{releaseDay}");
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

    Book[] books = {nov, mag};
    // 부모클래스의 타입으로 자식클래스의 객체를 담을수 있다!
    foreach(var b in books)
    {
      b.Print(); // Book클래스의 Print 함수가 아닌 자식클래스의 Print함수가 호출된다.
      Console.WriteLine();
    }
  }
}