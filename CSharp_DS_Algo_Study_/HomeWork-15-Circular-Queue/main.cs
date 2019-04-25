using System;

// https://lktprogrammer.tistory.com/59

namespace Circular_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<object> print = Console.WriteLine;
            var queue1 = new CircularQueue(5);
            queue1.Enqueue(1);
            queue1.Enqueue(2);
            queue1.Enqueue(3);
            queue1.Enqueue(4);
            queue1.Enqueue(5);
            print(queue1.Enqueue(6) == false);

            print(queue1.Dequeue() == 1);
            print(queue1.Dequeue() == 2);
            print(queue1.Dequeue() == 3);
            print(queue1.Dequeue() == 4);
            print(queue1.Dequeue() == 5);
            print(queue1.Dequeue() == -1);

            queue1.Enqueue(1);
            queue1.Enqueue(2);
            queue1.Enqueue(3);
            queue1.Enqueue(4);
            queue1.Enqueue(5);
            print(queue1.Enqueue(6) == false);            
        }
    }
}

public class CircularQueue
{
    int front = 0;
    int rear = 0;
    int[] cQueue;
    
    public CircularQueue(int size)
    {
        cQueue = new int[size+1];
    }

    public bool IsFull()
    {
        if((rear+1) % cQueue.Length == front)
            return true;
        else
            return false;
    }

    public bool IsEmpty()
    {
        if(rear == front)
            return true;
        else
            return false;
    }

    public bool Enqueue(int num)
    {
        if(IsFull())
        {
            Console.WriteLine("Is Full!!");
            return false;
        }
        else
        {
            rear = (rear+1) % cQueue.Length;
            // Console.WriteLine(rear); // test
            cQueue[rear] = num;
            return true;
        }

    }

    public int Dequeue()
    {
        if(IsEmpty())
        {
            Console.WriteLine("Is Empty!!");
            return -1;
        }
        else
        {
            front = (front+1) % cQueue.Length;
            return cQueue[front];
        }
    }
}
