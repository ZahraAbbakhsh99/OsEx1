using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AbbakhshOs3
{
    internal class Program
    {
        static int ArrayCapacity = 100;
        public struct Person
        {
            public String FirstName;
            public String LastName;
            public DateTime BirthDate;
            public long Number;
            public String Address;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("HI...");
            Console.WriteLine("How Many People Do You Want To Enter?");
            int n = int.Parse(Console.ReadLine());
            Person[] People = new Person[ArrayCapacity];

            int lastIndex;
            Boolean Continue;
            char answer;

            for (int i = 0; i < n; i++)
            {
                lastIndex = findLastIndex(People);
                if (lastIndex != -1)
                {
                    Console.WriteLine("Enter FirstName For Person Number " + (i + 1) + " : ");
                    People[lastIndex].FirstName = Console.ReadLine();
                    Console.WriteLine("Enter LastName For Person Number " + (i + 1) + " : ");
                    People[lastIndex].LastName = Console.ReadLine();

                    // Set birth date correctly and provide error handling
                    Boolean ok;
                    Continue = true;
                    while (Continue)
                    {
                        Console.WriteLine("Fine,Enter Birth Date For Person Number " + (i + 1) + " : ");
                        Console.WriteLine("year(4): ");
                        int y = int.Parse(Console.ReadLine());
                        Console.WriteLine("month(2): ");
                        int m = int.Parse(Console.ReadLine());
                        Console.WriteLine("day(2): ");
                        int d = int.Parse(Console.ReadLine());

                        if (y.ToString().Length == 4 && (m.ToString().Length == 2 || m.ToString().Length == 1) && (d.ToString().Length == 2 || d.ToString().Length == 1))
                        {
                            People[lastIndex].BirthDate = new DateTime(y, m, d);
                            ok = true;
                        }
                        else
                        {
                            People[lastIndex].BirthDate = new DateTime(1111, 11, 11);
                            ok = false;
                        }

                        if (ok) { Continue = false; }
                        else
                        {
                            Console.WriteLine("The Entered Value Isn't Corret!By Default We Put 1111/11/11");
                            Console.WriteLine("Do You Want To Modify?(y/anything)");
                            answer = char.Parse(Console.ReadLine());
                            if (answer != 'y') { Continue = false; }
                        }
                    }


                    // Set number correctly and provide error handling
                    Continue = true;
                    while (Continue)
                    {
                        Console.WriteLine("Fine,Enter Number For Person Number " + (i + 1) + " (11): ");
                        long num = long.Parse(Console.ReadLine());
                        if (num.ToString().Length == 10)
                        {
                            String numTemp = Convert.ToString(num);
                            numTemp = "0" + numTemp;
                            num = long.Parse(numTemp);
                            People[lastIndex].Number = num;
                            ok = true;
                        }
                        else
                        {
                            People[lastIndex].Number = 00000000000;
                            ok = false;
                        }

                        if (ok) { Continue = false; }
                        else
                        {
                            Console.WriteLine("The Entered Value Isn't Corret!By Default We Put 00000000000");
                            Console.WriteLine("Do You Want To Modify?(y/anything)");
                            answer = char.Parse(Console.ReadLine());
                            if (answer != 'y') { Continue = false; }
                        }
                    }

                    Console.WriteLine("Fine, Enter Address For Person Number " + (i + 1) + " : ");
                    People[lastIndex].Address = Console.ReadLine();

                    Console.WriteLine("EXCELLENT...Person Number " + (i + 1) + " Information Entered Successfully!");
                    Console.WriteLine("---------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("The Array Is Filled!");
                }
            }
            Console.WriteLine("EXCELLENT....We Fineshed.");
            Console.WriteLine("---------------------------------------------------");

            Console.WriteLine("A view of the array so far");
            ShowArray(People);


            Continue = true;
            while (Continue)
            {
                try
                {
                    Console.WriteLine("Now we want to load the array of people into the json file.");

                    // Saving to JSON file
                    var jsonObjects = new JArray();
                    for (int i = 0; i < findLastIndex(People); i++)
                    {
                        var jsonObject = new JObject();
                        jsonObject["FirstName"] = People[i].FirstName;
                        jsonObject["LastName"] = People[i].LastName;
                        jsonObject["BirthDate"] = People[i].BirthDate;
                        jsonObject["Number"] = People[i].Number;
                        jsonObject["Address"] = People[i].Address;
                        jsonObjects.Add(jsonObject);
                    }

                    string filePath = @"people.json";
                    string json = jsonObjects.ToString(Formatting.Indented);
                    File.WriteAllText(filePath, json);
                    Console.WriteLine("The list of people was successfully saved in the JSON file at path " + filePath + ".");
                    Console.WriteLine("---------------------------------------------------");
                    Continue = false;
                }
                catch 
                {

                    Console.WriteLine("A problem has occurred. please check it..");
                    Console.WriteLine("Do you want to try agein?(y/anythin)");
                    answer = char.Parse(Console.ReadLine());
                    if (answer != 'y') { Continue = false; }
                    Console.WriteLine("-----------------------------------------------------------------------------------");

                }
            }

            Console.WriteLine("This time, let's try to load a content of a json file into the created Array.\r\n");
            Continue = true;
            while (Continue)
            {
                // Loading from JSON file
                try
                {
                    Console.WriteLine("Please create a json file and enter its address.");
                    String GetfilePath = Console.ReadLine();
                    string jsonFromFile = File.ReadAllText(GetfilePath);
                    JArray jsonArray = JArray.Parse(jsonFromFile);
                    foreach (var item in jsonArray)
                    {
                        Person person = item.ToObject<Person>();
                        lastIndex = findLastIndex(People);
                        if (lastIndex != -1 && lastIndex < People.Length)
                        {
                            People[lastIndex] = person;
                            lastIndex++;
                        }
                        else
                        {
                            Console.WriteLine("The array is filled. No more information can be added.");
                            Continue= false;
                            break;
                        }
                    }
                    Console.WriteLine("Values loaded from the file and added to the array.");
                    Continue= false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("It seems that there is a problem in the file address or in other parts.");
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Do you want to try agein?(y/anythin)");
                    answer = char.Parse(Console.ReadLine());
                    if (answer != 'y') { Continue = false; }
                    Console.WriteLine("-----------------------------------------------------------------------------------");

                }
            }
            Console.WriteLine("A view of the array so far");
            ShowArray(People);
            Console.WriteLine("BYE....");
        }

        public static int findLastIndex(Person[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (String.IsNullOrEmpty(a[i].FirstName))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void ShowArray(Person[] p)
        {
            for (int i = 0; i<findLastIndex(p); i++)
            {
                Console.WriteLine("Person number " + (i+1) + " : ");
                Console.WriteLine("FirstName : " + p[i].FirstName);
                Console.WriteLine("LastName : " + p[i].LastName);
                Console.WriteLine("BirthDate : " + p[i].BirthDate);
                Console.WriteLine("Number : " + p[i].Number);
                Console.WriteLine("Address : " + p[i].Address);
                Console.WriteLine("/////////////////////////////////");

            }
        }
    }
}
