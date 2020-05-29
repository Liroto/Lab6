using Coutaq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_6
{
    class MAIN
    {
        enum Pos
        {
            Тр,
            Тс,
            А,
            None
        }



        public const int WATCH_TABLE = 1;
        public const int ADD_RAW = 2;
        public const int REMOVE_RAW = 3;
        public const int UPDATE_RAW = 4;
        public const int FIND_RAW = 5;
        public const int SHOW_LOG = 6;
        public const int EXIT = 7;

        public struct HRD
        {
            public string Transport; // Фамилия
            public string number;//Долнжость
            public int Length;//Год рождения
            public string RoadTime;//Оклад

            internal void ShowTable(string name, string number, int Length, string RoadTime)
            {
                Console.Write("{0,10}", name);
                Console.Write("{0,10}", number);
                Console.Write("{0,10}", Length);
                Console.Write("{0,10}", RoadTime);
                Console.WriteLine();
            }
        }

        //Списки
        static List<HRD> list = new List<HRD>(50);



        //Список опираций
        enum Operations
        {
            ADD,
            DELETE,
            UPDATE,
            LOOK,
            SEARCH
        };

        //ЛОГИРОВАНИЕ
        struct Logging
        {
            static List<Logging> log = new List<Logging>();
            public DateTime time;
            public Operations action;
            public String data;

            public static Logging Add(DateTime dt, Operations operation, string s)
            {
                log.Add(new Logging(dt, operation, s));
                return log[log.Count - 1];
            }

            public Logging(DateTime Time, Operations Operations, String Date)
            {
                time = Time;
                action = Operations;
                data = Date;
            }

            public static void ShowInfo()
            {

                foreach (Logging l in log)
                {
                    l.PrintLog();
                }
            }
            public void PrintLog()
            {
                Console.Write("{0,10}", time);
                Console.Write("{0,20}  ", action);
                Console.WriteLine("{0,10}", data);
            }



        }


        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Выберите пункт");
                Console.WriteLine("1 - Просмотр таблицы");
                Console.WriteLine("2 - Добавить запись");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - Обновить запись");
                Console.WriteLine("5 - Поиск записей");
                Console.WriteLine("6 - Просмотреть лог");
                Console.WriteLine("7 - Выход");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case WATCH_TABLE:
                        Console.WriteLine("{0,10} {1,10} {2,10} {3,10}", "Вид транспорта", "№ маршрута", "Протяженность маршрута (км) ", "Время в дороге (мин)");
                        for (int list_item = 0; list_item < list.Count; list_item++)
                        {
                            HRD t = list[list_item];
                            Console.WriteLine("----------------------------------------------------------------------------\n");
                            t.ShowTable(t.Transport, t.number, t.Length, t.RoadTime);

                        }
                        
                        string textFromFile = FileExpert.ReadFromRelativePath("lab.dat");
                        Console.WriteLine(textFromFile);
                        Logging.Add(DateTime.Now, Operations.LOOK, "Просмотрена таблица");
                        break;

                    case ADD_RAW:
                        HRD t1;
                        Console.WriteLine("Введите Вид транспорта");
                        t1.Transport = Console.ReadLine();
                        Console.WriteLine("Введите Номер маршрута");
                        t1.number = Console.ReadLine();

                    Found1:
                        Console.WriteLine("Введите Протяженность маршрута (км) ");
                        try
                        {
                            int enter_length = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                            t1.Length = enter_length;
                            if ((enter_length <= 0) || (enter_length >= 250))
                            {
                                Console.WriteLine("Error. (Введите повторно)");
                                goto Found1;
                            }
                        }
                        catch (FormatException)
                        {
                            t1.Length = 000;
                            Console.WriteLine("Error. (Введите повторно)");
                            goto Found1;
                        }
                        Pos pro;
                    Found3:
                        Console.WriteLine("Время в дороге (мин)");
                        try
                        {
                            string enter_length3 = Console.ReadLine();
                            t1.RoadTime = enter_length3;
                            pro = (Pos)Enum.Parse(typeof(Pos), enter_length3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error. (Введите повторно)");
                            pro = Pos.None;
                            goto Found3;
                        }

                        list.Add(t1);
                        Console.WriteLine("Строка была добавлена!");
                        Console.WriteLine();
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка добавлена в таблицу!");
                        break;
                    case REMOVE_RAW:


                        Console.WriteLine("Введите номер строки, которую хотите удалить");
                        int number = int.Parse(Console.ReadLine());
                        try
                        {
                            list.RemoveAt(number - 1);
                        }
                        catch (Exception e) { Console.WriteLine("Строки с таким номером нет!"); }
                        Console.WriteLine();
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка удалена!");
                        break;
                    case UPDATE_RAW:
                        Console.WriteLine("Введите номер строки, которую хотите изменить");
                        int UpdateIndex = int.Parse(Console.ReadLine());
                        try
                        {
                            HRD t2 = list[UpdateIndex - 1];

                            //Выводим старые значения

                            t2.ShowTable(t2.Transport, t2.number, t2.Length, t2.RoadTime);


                            //Вводим новые значения

                            Console.WriteLine("Введите новый вид транспорта");
                            t2.Transport = Console.ReadLine();
                            Console.WriteLine("Введите новый № маршрута ");
                            t2.number = Console.ReadLine();
                            Console.WriteLine("Введите новую протяженность маршрута (км) ");
                        Found2:
                            t2.Length = int.Parse(Console.ReadLine());
                            try
                            {
                                int enter_length2 = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                                t2.Length = enter_length2;
                                if ((enter_length2 < 1895) || (enter_length2 > 2030))
                                {
                                    Console.WriteLine("Error. (Введите повторно)");
                                    goto Found2;
                                }
                            }
                            catch (FormatException)
                            {
                                t2.Length = 000;
                                Console.WriteLine("Error. (Введите повторно)");
                                goto Found2;
                            }
                            Console.WriteLine("Введите новый Тип");
                            Pos pro2;
                        Found4:
                            try
                            {
                                string enter_length4 = Console.ReadLine();
                                t2.RoadTime = enter_length4;
                                pro2 = (Pos)Enum.Parse(typeof(Pos), enter_length4);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error. (Введите повторно)");
                                pro2 = Pos.None;
                                goto Found4;
                            }
                            list[UpdateIndex - 1] = t2;

                        }
                        catch (Exception e) { Console.WriteLine("Нет строки с таким номером!"); }
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка обновлена!");
                        break;
                    case FIND_RAW:
                        Console.WriteLine("Введите фамилию");
                        string text = Console.ReadLine();
                        HRD FindRaw;
                        for (int item_list = 0; item_list < list.Count; item_list++)
                        {
                            FindRaw = list[item_list];
                            if (FindRaw.Transport.ToLower().Equals(text.ToLower()))
                            {
                                Console.Write("{0,10}", FindRaw.Transport);
                                Console.Write("{0,10}", FindRaw.number);
                                Console.Write("{0,10}", FindRaw.Length);
                                Console.Write("{0,10}", FindRaw.RoadTime);
                                Console.WriteLine();
                            }
                        }
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка найдена!");
                        break;
                    case SHOW_LOG:
                        Logging.Add(DateTime.Now, Operations.ADD, "Логи просмотрены!");
                        Logging.ShowInfo();
                        break;
                    case EXIT:
                        
                        String file = string.Empty;
                        foreach (var t in list) {
                            file += "----------------------------------------------------------------------------";
                            file += t.Transport + "\t" + t.number + "\t" + t.Length + "\t" + t.RoadTime; 
                           
                        }
                        //for (int list_item = 0; list_item < list.Count; list_item++)
                        //{
                        //    HRD t = list[list_item];
                        //    write.WriteLine("----------------------------------------------------------------------------");
                        //    write.WriteLine("{0,10} {1,10} {2,10} {3,10}" , t.Transport, t.number, t.Length, t.RoadTime);
                        // }

                        FileExpert.SaveToRelativePath("lab.dat", file);
                        break;
                }
            } while (choice != 7);
        }
        
    }
}


