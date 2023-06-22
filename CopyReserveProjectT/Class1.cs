using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal abstract class Storage
    {
        private string _name;
        private string _model;
        public string Name
        {
            get { return _name; }
            private set
            {
                try
                {
                    _name = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(value));
                }

            }
        }
        public string Model
        {
            get { return _model; }
            private set
            {
                try
                {
                    _model = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(value));
                }
            }
        }

        public Storage(string n, string m)
        {
            Name = n;
            Model = m;
        }

        public abstract double CopyData(double memoryFor);
        public abstract double FreeSpace(double size);
        public abstract double FreeSpace(double[] size);
        public abstract string GetGeneralInfo();
    }

    internal class HDD : Storage
    {
        private double _memoryInSection;
        private int _sections;
        private double _speedUSB;
        private double _memory;

        public double MemoryInSection
        {
            get
            {
                return _memoryInSection;
            }
            private set
            {
                try
                {
                    if (value <= 0)
                        throw new FormatException();
                    _memoryInSection = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(_memoryInSection));
                }

            }
        }
        public int Sections
        {
            get
            {
                return _sections;
            }
            private set
            {
                try
                {
                    if (value <= 0)
                        throw new FormatException();
                    _sections = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(Sections));
                }

            }
        }
        public double SpeedUSB
        {
            get { return _speedUSB; }
            private set
            {
                if (value < 0)
                    throw new FormatException(nameof(SpeedUSB));
                _speedUSB = value;
            }
        }
        public double Memory
        {
            get { return _memory; }
            private set
            {
                if (value < 0)
                    throw new FormatException(nameof(Memory));
                _memory = value;
            }
        }

        public HDD(double sUBS, int s, double mIS, string n, string mod) : base(n, mod)
        {
            SpeedUSB = sUBS;
            Sections = s;
            MemoryInSection = mIS;
            Memory = Sections * MemoryInSection;
        }

        public override double CopyData(double memoryFor)
        {
            if (memoryFor < 0)
                throw new FormatException(nameof(memoryFor));
            return Math.Round(memoryFor / Memory);
        }

        public override double FreeSpace(double size)
        {
            if (size < 0)
                throw new FormatException(nameof(size));
            double freeSpace = Memory - size;
            if (freeSpace < 0)
            {
                throw new FormatException(nameof(freeSpace));
            }
            return freeSpace;
        }
        public override double FreeSpace(double[] size)
        {
            double freeSpace = 0;
            for (int i = 0; i < size.Length; i++)
            {
                if (size[i] < 0)
                    throw new FormatException(nameof(size));
                freeSpace += size[i];
            }

            freeSpace = Memory - freeSpace;

            if (freeSpace < 0)
                throw new FormatException(nameof(freeSpace));

            return freeSpace;
        }

        public override string GetGeneralInfo()
        {
            return $"Name - {Name}\nModel - {Model}\nSpeed USB 2.0 - {SpeedUSB}\nMemory - {Memory}";
        }

    }


    internal class DVD : Storage
    {
        private double _speedReading = 0;
        private double _speedWriting = 0;
        private string _type;
        private double _memory = 0;

        public double SpeedReading
        {
            get { return _speedReading; }
            private set
            {
                try
                {
                    if (value <= 0)
                        throw new FormatException();
                    _speedReading = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(SpeedReading));
                }
            }
        }
        public double SpeedWriting
        {
            get { return _speedWriting; }
            private set
            {
                try
                {
                    if (value <= 0)
                        throw new FormatException();
                    _speedWriting = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(SpeedWriting));
                }
            }
        }
        public string Type
        {
            get { return _type; }
            private set
            {
                try
                {
                    _type = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(Type));
                }
            }
        }
        public double Memory
        {
            get { return _memory; }
            private set
            {
                try
                {
                    if (value <= 0)
                        throw new FormatException();
                    _memory = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(Memory));
                }
            }
        }


        public DVD(double SpeedReading, double SpeedWriting, string Type, string Name, string Model) : base(Name, Model)
        {
            this.SpeedReading = SpeedReading;
            this.SpeedWriting = SpeedWriting;
            this.Type = Type;

            if (Type == "one_sided")
            {
                Memory = 4.7;
            }
            else
            {
                Memory = 9;
            }
        }

        public override double CopyData(double memoryFor)
        {
            if (memoryFor < 0)
                throw new FormatException(nameof(memoryFor));

            return Math.Round(memoryFor / Memory);
        }

        public override double FreeSpace(double size)
        {

            if (size < 0) throw new FormatException(nameof(size));

            double freeSpace = Memory - size;

            if (freeSpace < 0) throw new FormatException(nameof(freeSpace));

            return freeSpace;
        }
        public override double FreeSpace(double[] size)
        {
            double freeSpace = 0;

            for (int i = 0; i < size.Length; i++)
            {
                if (size[i] < 0)
                    throw new FormatException(nameof(size));
                freeSpace += size[i];
            }

            freeSpace = Memory - freeSpace;

            if (freeSpace < 0) throw new FormatException(nameof(freeSpace));

            return freeSpace;
        }

        public override string GetGeneralInfo()
        {
            return $"Name - {Name}\nModel - {Model}\nSpeed Reading - {SpeedReading}\n Speed Writing - {SpeedWriting} \nMemory - {Memory}";
        }

    }


    internal class Flash : Storage
    {
        private double _speedUSB;
        private double _memory;
        public double SpeedUSB
        {
            get
            {
                return _speedUSB;
            }
            private set
            {
                try
                {
                    if (value < 0)
                        throw new FormatException();
                    _speedUSB = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(SpeedUSB));
                }
            }
        }
        public double Memory
        {
            get
            {
                return _memory;
            }
            private set
            {
                try
                {
                    if (value < 0)
                        throw new FormatException();
                    _memory = value;
                }
                catch (FormatException)
                {
                    throw new FormatException(nameof(Memory));
                }
            }
        }

        public Flash(double sUBS, double m, string n, string mod) : base(n, mod)
        {
            SpeedUSB = sUBS;
            Memory = m;
        }

        public override double CopyData(double memoryFor)
        {
            if (memoryFor < 0)
                throw new FormatException(nameof(memoryFor));
            return Math.Round(memoryFor / Memory);
        }

        public override double FreeSpace(double size)
        {
            if (size < 0)
                throw new FormatException(nameof(size));
            double freeSpace = Memory - size;
            if (freeSpace < 0)
            {
                throw new FormatException(nameof(freeSpace));
            }
            return freeSpace;
        }
        public override double FreeSpace(double[] size)
        {
            double freeSpace = 0;
            for (int i = 0; i < size.Length; i++)
            {
                if (size[i] < 0)
                    throw new FormatException(nameof(size));
                freeSpace += size[i];
            }

            freeSpace = Memory - freeSpace;

            if (freeSpace < 0)
            {
                throw new FormatException(nameof(freeSpace));
            }

            return freeSpace;
        }

        public override string GetGeneralInfo()
        {
            return $"Name - {Name}\nModel - {Model}\nSpeed USB 3.0 - {SpeedUSB}\nMemory - {Memory}";
        }
    }


    internal class CopyReserve
    {
        private Storage[] storages = new Storage[0];




        private void New()
        {
            char k = ' ';
            Console.WriteLine("1 - Flash\n2 - DVD\n3 - HDD\n0 - выход");
            k = Console.ReadKey().KeyChar;
            if (k != '1' && k != '2' && k != '3' && k != '0')
            {
                Console.WriteLine("k - неверный аргумент");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
            }

            Console.Clear();
            switch (k)
            {
                case '1':
                    NewFlash();
                    break;
                case '2':
                    NewDVD();
                    break;
                case '3':
                    NewHDD();
                    break;
                case '0':
                    break;
                default:
                    Console.ReadKey();
                    break;
            }
        }

        private void NewFlash()
        {
            string name;
            string model;
            double speedUSB;
            double memory;
            Console.WriteLine("Новый Flash");
            Console.WriteLine("Введите имя устройства: "); name = Console.ReadLine();
            Console.WriteLine("Введите модель устройства: "); model = Console.ReadLine();
            Console.WriteLine("Введите скорость USB устройства Мб/c: "); speedUSB = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите память устройства Мб: "); memory = double.Parse(Console.ReadLine());

            try
            {
                Flash tmp = new Flash(speedUSB, memory, name, model);
                Array.Resize(ref storages, storages.Length + 1);
                storages[storages.Length - 1] = tmp;
            }
            catch (FormatException)
            {
                throw;
            }
        }

        private void NewDVD()
        {
            string name;
            string model;
            double speedReading, speedWriting;
            string type;
            Console.WriteLine("Новый DVD");
            Console.WriteLine("Введите имя устройства: "); name = Console.ReadLine();
            Console.WriteLine("Введите модель устройства: "); model = Console.ReadLine();
            Console.WriteLine("Введите скорость считывания Мб/c: "); speedReading = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите скорость записи Мб/c: "); speedWriting = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите тип устройства (one_sided или другие): "); type = Console.ReadLine();

            try
            {
                DVD tmp = new DVD(speedReading, speedWriting, type, name, model);
                Array.Resize(ref storages, storages.Length + 1);
                storages[storages.Length - 1] = tmp;
            }
            catch (FormatException)
            {
                throw;
            }
        }

        private void NewHDD()
        {
            string name;
            string model;
            int sections;
            double memoryInSection;
            double speedUSB;
            Console.WriteLine("Новый HDD");
            Console.WriteLine("Введите имя устройства: "); name = Console.ReadLine();
            Console.WriteLine("Введите модель устройства: "); model = Console.ReadLine();
            Console.WriteLine("Введите скорость USB устройства Мб/c: "); speedUSB = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество секций устройства: "); sections = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите память в одной секции Мб: "); memoryInSection = double.Parse(Console.ReadLine());

            try
            {
                HDD tmp = new HDD(speedUSB, sections, memoryInSection, name, model);
                Array.Resize(ref storages, storages.Length + 1);
                storages[storages.Length - 1] = tmp;
            }
            catch (FormatException)
            {
                throw;
            }
        }


        private void Delete()
        {
            int index;
            Console.WriteLine(Show());
            Console.WriteLine("Введите номер устройства");
            try
            {
                index = int.Parse(Console.ReadLine()) - 1;
                if (index < 0 || index > storages.Length)
                    throw new FormatException();
            }
            catch (FormatException)
            {
                throw new FormatException(nameof(index));
            }

            storages = storages.Where(val => val != storages[index]).ToArray();
        }

        private string Show()
        {
            string str = "";
            int i = 1;
            foreach (Storage s in storages)
            {
                str += $"{i} - ";
                i++;
                if (s is Flash)
                    str += nameof(Flash) + '\n';
                if (s is DVD)
                    str += nameof(DVD) + '\n';
                if (s is HDD)
                    str += nameof(HDD) + '\n';

                str += s.GetGeneralInfo();
                str += '\n';
            }
            return str;
        }

        private double GeneralMemory()
        {
            double generalMemory = 0;
            foreach (Storage s in storages)
            {
                if (s is Flash)
                    generalMemory += (s as Flash).Memory;
                else if (s is DVD)
                    generalMemory += (s as DVD).Memory;
                else if (s is HDD)
                    generalMemory += (s as HDD).Memory;
            }
            return generalMemory;
        }


        private double TimeForCopy()
        {
            Console.WriteLine(Show());
            Console.WriteLine("Введите номер устройства:");
            int index;
            try
            {
                index = int.Parse(Console.ReadLine()) - 1;
                if (index < 0 || index > storages.Length)
                    throw new FormatException();
            }
            catch (FormatException)
            {
                throw new FormatException(nameof(index));
            }

            Console.WriteLine("Введите объем информации (в Мб):");
            int size;
            try
            {
                size = int.Parse(Console.ReadLine());
                if (size < 0)
                    throw new FormatException();
            }
            catch (FormatException)
            {
                throw new FormatException(nameof(size));
            }


            if (storages[index] is Flash)
                return size / (storages[index] as Flash).SpeedUSB;
            else if (storages[index] is DVD)
                return size / (storages[index] as DVD).SpeedWriting;
            else if (storages[index] is HDD)
                return size / (storages[index] as HDD).SpeedUSB;
            return 0;
        }

        private int NumbersOfMedia()
        {
            Console.WriteLine(Show());
            Console.WriteLine("Введите номер носителя");
            int index;
            try
            {
                index = int.Parse(Console.ReadLine()) - 1;
                if (index < 0 || index > storages.Length)
                    throw new FormatException();
            }
            catch (FormatException)
            {
                throw new FormatException(nameof(index));
            }

            if (storages[index] is Flash)
                return (int)(578560 / (storages[index] as Flash).Memory) + 1;
            if (storages[index] is DVD)
                return (int)(578560 / (storages[index] as DVD).Memory) + 1;
            if (storages[index] is HDD)
                return (int)(578560 / (storages[index] as HDD).Memory) + 1;
            return 0;


        }

        public void Main()
        {
            char k = ' ';
            while (k != '0')
            {
                Console.Clear();
                Console.WriteLine($"1||Расчет общего количества памяти на всех " +
                    $"устройствах (текущее количество - {storages.Length})");    //+
                Console.WriteLine("2||Копирование информации на устройства");  //+
                Console.WriteLine("3||Расчет времени необходимого для копирование");  //+
                Console.WriteLine("4||расчет необходимого количества носителей " +
                    "информации представленных типов для переноса информации."); //+
                Console.WriteLine("5||Добавить новое устройство");   //+
                Console.WriteLine("6||Удалить устройство");     //+
                Console.WriteLine("7||Показать все устройства");     //+
                Console.WriteLine("0||Завершить программу и выйти");

                k = Console.ReadKey().KeyChar;
                if (k != '1' && k != '2' && k != '3' && k != '4' && k != '5' && k != '6' && k != '0')
                {
                    Console.WriteLine("k - неверный аргумент");
                    Console.WriteLine("Попробуйте снова\nНажмите любую клавишу для продолжения...");
                }
                try
                {
                    Console.Clear();
                    switch (k)
                    {
                        case '1':
                            Console.WriteLine("Обшая память всех устройств - " + GeneralMemory() + " Мб");
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.WriteLine("Время для копирования - " + TimeForCopy() / 60 + " минут");
                            Console.ReadKey();
                            break;
                        case '3':
                            Console.WriteLine("Время для копирования - " + TimeForCopy() + " секунд");
                            Console.ReadKey();
                            break;
                        case '4':
                            Console.WriteLine("Необходимое количество устройств для переноса информации 565 Гб - " + NumbersOfMedia() + " единиц");
                            Console.ReadKey();
                            break;
                        case '5':
                            New();
                            break;
                        case '6':
                            Delete();
                            break;
                        case '7':
                            Console.WriteLine(Show());
                            Console.ReadKey();
                            break;
                        case '0':
                            break;
                        default:
                            Console.ReadKey();
                            break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("ОШИБКА ФОРМАТА" + $"{e.Message}");
                    Console.ReadKey();
                }

            }

        }



    }

}
