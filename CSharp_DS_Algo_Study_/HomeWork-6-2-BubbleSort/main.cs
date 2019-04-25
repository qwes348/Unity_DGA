using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    Action<object> print = Console.WriteLine;
    int[] arr1 = new int[] {9, 7, 6, 8, 4, 3, 5, 1, 2};
    int[] arr2 = new int[] {2, 2, 1, 1, 3, 3, 3};
    int[] arr3 = new int[] {50, 10, 20, 40, 30, 100, 60, 70, 80, 90};

    print(Stringify(BubbleSort(arr1)) == "1 2 3 4 5 6 7 8 9");
    print(Stringify(BubbleSort(arr2)) == "1 1 2 2 3 3 3");
    print(Stringify(BubbleSort(arr3)) == "10 20 30 40 50 60 70 80 90 100");
  }

  public static int[] BubbleSort(int[] arr)
  {
    int loop = arr.Length-1;
    int temp = 0;

    for(int i=0; i<loop; i++)
    {
      for(int j=0; j<(loop-i); j++)
      {
        if(arr[j] > arr[j+1])
        {
          temp = arr[j];
          arr[j] = arr[j+1];
          arr[j+1] = temp;
        }
      }
    }
    return arr;
  }

  public static string Stringify(int[] list)
  {
    return String.Join(" ", list); 
  }
}