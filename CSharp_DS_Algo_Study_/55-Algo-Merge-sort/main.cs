using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] list = {6, 8, 1, 4, 3, 2, 5, 7, 9};
    MergeSort(list, 0, list.Length-1);
    print(list.Stringify() == "1 2 3 4 5 6 7 8 9");

    list = new int[] {6, 8, 1, 4, 3, 2, 5, 7, 9};
    Conbine(list, 0, 0, 1, 1);
    print(list.Stringify() == "6 8 1 4 3 2 5 7 9"); 
    Conbine(list, 0, 1, 2, 3);
    print(list.Stringify() == "1 4 6 8 3 2 5 7 9");
    Conbine(list, 5, 6, 7, 8);
    print(list.Stringify() == "1 4 6 8 3 2 5 7 9");
  }

  public static void MergeSort(int[] list, int left, int right)
  {
    if(left == right)
      return;
    int mid = (left+right) / 2;
    MergeSort(list, left, mid);
    MergeSort(list, mid+1, right);
    Conbine(list, left, mid, mid+1, right);
  }

  // not-inplace algo, offline algo
  public static void Conbine(int[] list, int leftBegin, int leftEnd, int rightBegin,
  int rightEnd)
  {
    int i = leftBegin;
    int j = rightBegin;
    var C = new int[(rightEnd-leftBegin)+1];

    for(int k=0; k<C.Length; k++)
    {
      if(i > leftEnd || j > rightEnd)
      {
        if(i > leftEnd)
          C[k] = list[j++];
        else
          C[k] = list[i++];
      }
      else
      {
        if(list[i] <= list[j])
          C[k] = list[i++];
        else
          C[k] = list[j++];
      }
    }
    i = leftBegin;
    for(int k=0; k<C.Length; k++)
    {
      list[i++] = C[k];
    }
  }                 
  
}

public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  {
    return String.Join(" ", list);
  }
}