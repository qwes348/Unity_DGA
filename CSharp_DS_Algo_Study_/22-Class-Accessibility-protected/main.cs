using System;

class Point
{
  protected int x;
  protected int y;
  // protected = 상속관계인 클래스만 접근가능한 변수
}

class DerivedPoint : Point 
{
  public static void Main (string[] args) 
  {
    DerivedPoint dpoint = new DerivedPoint();
    dpoint.x = 10; 
  }
}