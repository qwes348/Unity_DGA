using System;

public class Program
{
	public static void Main()
	{
		Book lor = new Book("The Load of the Rings", 30000, "J.R.R. Tolkien");
		Book hobbit = new Book("The Hobbit", 40000, "J.R.R. Tolkien");
    // lor, hobbit == 객체, object, 인스턴스
    // class Book은 객체를 만들기위한 틀

    Console.WriteLine(Book.count == 2);

    Book[] books = new Book[2];
    books[0] = lor;
    books[1] = hobbit;

    foreach(var b in books)
      Console.WriteLine(b.title + ", " + b.price + ", " + 
      b.author + ", " + b.DiscountedPrice(0.1));
	}
}

public class Book
{
	public string title;    // private string title == string title (생략하면 기본 private)
	public int price;       // 멤버변수 혹은 필드 3개
	public string author;
  public static int count = 0;  // 용도 = 초기화값을 줄 때 and 상수로 쓰고싶을때
	
	public Book(string _title, int _price, string _author)
	{
		this.title = _title;   // 생성자 3개 ctor==(Constructer)
		this.price = _price;
		this.author = _author;
    count++;
	}

  public double DiscountedPrice(double discount)  // 멤버메소드 혹은 멤버함수
  {
    return(price - price * discount);
  }
}