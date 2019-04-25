using System;
using System.Collections;


class MainClass 
{
  public static void Main (string[] args)
  {
    Tokens tokens = new Tokens("This is a sample sentence.", new char[] {' ', '-'});
    foreach(string t in tokens)
      Console.WriteLine(t);
  }
}

public class Tokens : IEnumerable
{
  string[] elements;

  public Tokens(string source, char[] delimiters)
  {
    elements = source.Split(delimiters);
  }

  public IEnumerator GetEnumerator()
  {
    // return elements.GetEnumerator(); // string[]의 GetEnumerator을 가져옴
    return new TokenEnumerator(this);
  }

  class TokenEnumerator : IEnumerator
  {
    Tokens t;
    int position = -1;

    public TokenEnumerator(Tokens t)
    {
      this.t = t;
    }

    public bool MoveNext()
    {
      if(position < t.elements.Length-1)
      {
        position++;
        return true;
      }
      else
        return false;
    }

    public object Current
    {
      get
      {
        return t.elements[position];
      }
    }

    public void Reset()
    {
      position = -1;
    }
  }
}