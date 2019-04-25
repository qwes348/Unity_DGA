using System;
using System.Collections;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Tokens f = new Tokens("This is a sample sentence.", new char[] {' ', '-'});
  }
}

public class Tokens : IEnumerable<Tokens>
{
  public string sentence;
  public char[] ch;
  
  public Tokens(string sentence, char[] ch)
  {
    this.sentence = sentence;
    this.ch = ch;
  }
  public IEnumerator<Tokens> GetEnumerator() { return new TokensEnumerator(list);}
  IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }    
  
  public List<Tokens> list = sentence.Split(ch[0]);

  class TokensEnumerator : IEnumerator<Tokens>
  {
    int position = -1;
    List<Tokens> list;

    public TokensEnumerator(List<Tokens> list)
    {
      this.list = list;
    }
    
    public Tokens Current
    {
      get
      {
        return list[position];
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return this.Current;
      }
    }

    public bool MoveNext()
    {
      if(position < list.Count-1)
      {
        position++;
        return true;
      }
      else
        return false;
    }

    public void Reset()
    {
        position = -1;
    }

    public void Dispose()
    {

    }
  }
}