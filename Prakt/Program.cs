using System;

// Опис типу даних для елементу списку
enum MonthType
{
    January,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}

class Node
{
    public string Name { get; set; }
    public int BirthDay { get; set; }
    public MonthType BirthMonth { get; set; }
    public Node Next { get; set; }
    public Node Previous { get; set; }
}

class LinkedList
{
    private Node head;

    public LinkedList()
    {
        head = null;
    }
    // Метод додавання елемента на певну позицію списку
    public void AddElement(string name, int day, MonthType month, int position)
    {
        Node newNode = new Node
        {
            Name = name,
            BirthDay = day,
            BirthMonth = month
        };

        if (position <= 0)
        {
            newNode.Next = head;
            if (head != null)
                head.Previous = newNode;
            head = newNode;
        }
        else
        {
            Node current = head;
            int currentPosition = 0;

            while (current != null && currentPosition < position - 1)
            {
                current = current.Next;
                currentPosition++;
            }

            if (current != null)
            {
                newNode.Next = current.Next;
                newNode.Previous = current;
                if (current.Next != null)
                    current.Next.Previous = newNode;
                current.Next = newNode;
            }
            else
            {
                Console.WriteLine("Invalid position.");
            }
        }
    }
    // Метод видалення елемента з певним значенням списку
    public void RemoveElement(string name)
    {
        Node current = head;

        while (current != null)
        {
            if (current.Name == name)
            {
                if (current.Previous != null)
                    current.Previous.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Previous = current.Previous;

                break;
            }

            current = current.Next;
        }
    }
    // Метод ітерації по списку
    public Node GetFirstNode()
    {
        return head;
    }

    public Node GetNextNode(Node current)
    {
        return current?.Next;
    }

    public int GetLength()
    {
        int length = 0;
        Node current = head;

        while (current != null)
        {
            length++;
            current = current.Next;
        }

        return length;
    }
    // Пошук елементів списку згідно з варіантом завдання
    public LinkedList SearchBySummerBirth()
    {
        LinkedList resultList = new LinkedList();
        Node current = head;

        while (current != null)
        {
            if (current.BirthMonth >= MonthType.June && current.BirthMonth <= MonthType.August)
                resultList.AddElement(current.Name, current.BirthDay, current.BirthMonth, resultList.GetLength());

            current = current.Next;
        }

        return resultList;
    }
}
// Основний клас програми
class Program
{
    static void Main(string[] args)
    {
        LinkedList list = new LinkedList();
        int choice = 0;

        while (choice != 6)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add an element to the list");
            Console.WriteLine("2. Remove an element from the list");
            Console.WriteLine("3. Display the list");
            Console.WriteLine("4. Search for elements by summer birth");
            Console.WriteLine("5. Split the list");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter the birth day: ");
                    int day = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter the birth month (0 - January, 1 - February, etc.): ");
                    int month = Convert.ToInt32(Console.ReadLine());

                    list.AddElement(name, day, (MonthType)month, list.GetLength());
                    Console.WriteLine("Element added to the list.");
                    Console.WriteLine();
                    break;

                case 2:
                    Console.Write("Enter the name to remove: ");
                    string nameToRemove = Console.ReadLine();

                    list.RemoveElement(nameToRemove);
                    Console.WriteLine("Element removed from the list.");
                    Console.WriteLine();
                    break;

                case 3:
                    DisplayList(list);
                    Console.WriteLine();
                    break;

                case 4:
                    LinkedList summerBirthList = list.SearchBySummerBirth();
                    Console.WriteLine("People born in summer:");
                    DisplayList(summerBirthList);
                    Console.WriteLine();
                    break;


                case 5:
                    Console.Write("Enter the position to split the list: ");
                    int position = Convert.ToInt32(Console.ReadLine());

                    LinkedList secondList = SplitList(list, position);
                    Console.WriteLine("The list has been split.");
                    Console.WriteLine("First list:");
                    DisplayList(list);
                    Console.WriteLine("Second list:");
                    DisplayList(secondList);
                    Console.WriteLine();
                    break;

                case 6:
                    Console.WriteLine("Exiting the program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine();
                    break;
            }
        }
    }
    // Метод ввиведення списку
    static void DisplayList(LinkedList list)
    {
        Node current = list.GetFirstNode();

        Console.WriteLine("Name\t\tBirth Date");
        Console.WriteLine("------------------------------");

        while (current != null)
        {
            Console.WriteLine($"{current.Name}\t\t{current.BirthDay}/{(int)current.BirthMonth + 1}");
            current = list.GetNextNode(current);
        }
    }
    // Метод розбиття списку на два списки
    static LinkedList SplitList(LinkedList list, int position)
    {
        LinkedList secondList = new LinkedList();
        Node current = list.GetFirstNode();
        int currentPosition = 0;

        while (current != null && currentPosition < position)
        {
            current = list.GetNextNode(current);
            currentPosition++;
        }

        if (current != null)
        {
            Node previousNext = current.Previous?.Next;
            if (previousNext != null)
                previousNext.Previous = null;

            current.Previous = null;
            secondList.GetFirstNode().Previous = current;
            current.Next = secondList.GetFirstNode();

            secondList.GetFirstNode().Next = previousNext;
        }
        else
        {
            Console.WriteLine("Invalid position.");
        }

        return secondList;
    }
}
