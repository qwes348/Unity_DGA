using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;

    int[] arr1 = new int[] {4, 9, 7, 5, 1, 3, 2, 6, 8};
    int[] arr2 = new int[] {1, 2, 10, 4, 9, 8, 7, 7, 6, 3, 5};
    int[] arr3 = new int[] {4, 4 ,4 ,4 ,1 ,1 ,1 ,3 ,3 ,9 ,9 ,8};
    int[] arr4 = new int[] {6, 136, 248, 28, 22, 57, 10, 4, 14, 13, 10};
    int[] arr5 = new int[] {1};

    Quicksort(arr1, 0, arr1.Length-1);
    print(arr1.Stringify() == "1 2 3 4 5 6 7 8 9");
    Quicksort(arr2, 0, arr2.Length-1);
    print(arr2.Stringify() == "1 2 3 4 5 6 7 7 8 9 10");
    Quicksort(arr3, 0, arr3.Length-1);
    print(arr3.Stringify() == "1 1 1 3 3 4 4 4 4 8 9 9");
    Quicksort(arr4, 0, arr4.Length-1);
    print(arr4.Stringify() == "4 6 10 10 13 14 22 28 57 136 248");
    Quicksort(arr5, 0, arr5.Length-1);
    print(arr5.Stringify() == "1");   
  }

  public static void Quicksort(int[] arr, int start, int end)
  {
    int left = start+1;
    int right = end;
    int pivot = arr[start];
    int temp = 0;
    // Console.WriteLine("Pivot: "+pivot.ToString());
    
    if(start+1 < end)
    {
      while(left < right)
      {
        while(arr[left] <= pivot && arr[left] <= arr[right])
        {
          left++;
          // Console.WriteLine($"Left: {left}");
        }
        while(arr[right] >= pivot && arr[right] >= arr[left])
        {
          right--;
          // Console.WriteLine($"Right: {right}");
        }
        if(right==left && right>start)
        {
          right--;
        }
        if(arr[left] > arr[right] && left < right)
        {
          temp = arr[left];
          arr[left] = arr[right];
          arr[right] = temp;
          // Console.WriteLine(arr.Stringify());
        }
      }
      temp = arr[start];
      arr[start] = arr[right];
      arr[right] = temp;
      // Console.WriteLine("pivotChange: "+arr.Stringify());
      Quicksort(arr, start, right-1);
      Quicksort(arr, right+1, end);
    }
    else
      return;
  }


}
public static class ClassExtension
{
  public static string Stringify<T>(this IEnumerable<T> list) 
  {
      return String.Join(" ", list);
  }
}
