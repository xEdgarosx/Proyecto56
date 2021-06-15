using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_unidad_5_y_6
{
    class Comentario
    {
        public int  id { get; set; }
        public string autor { get; set; }
        public DateTime fecha { get; set; }
        public string comentario { get; set; }
        public string ip { get; set; }
        public int inapropiado { get; set; }
        public int likes { get; set; }
        

        public override string ToString()
        {
            if (inapropiado > 50)
            {
                return String.Format($"Comentario inapropiado");
            }
            else
            {
                return String.Format($"{id} / {autor} /{fecha} /{comentario} /{ip} / Dislikes:{inapropiado} / Likes:{likes}  ");
               
            }
        }
    }
    class ComentarioDB
    {
        private List<Comentario> comens = new List<Comentario>();
        public static void SaveToFile(List<Comentario> comen, string path)
        {
            StreamWriter Comentout = null;
            try
            {
                Comentout = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comens in comen)
                {
                    Comentout.Write(comens.id + "|");
                    Comentout.Write(comens.autor + "|");
                    Comentout.Write(comens.fecha + "|");
                    Comentout.Write(comens.comentario + "|");
                    Comentout.Write(comens.ip + "|");
                    Comentout.Write(comens.inapropiado + "|");
                    Comentout.Write(comens.likes + "|");
                    Comentout.WriteLine();
                 }
            }
            catch (IOException)
            {
                Console.WriteLine("Ya existe el archivo");
            }
            catch(Exception)
            {
                Console.WriteLine("Error");
            }
            finally
            {
                if (Comentout != null)
                    Comentout.Close();
            }
        }

        public static List<Comentario> ReadFromFile(string path)
        {
            List<Comentario> comens = new List<Comentario>();
            StreamReader ComentIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));

            while(ComentIn.Peek()!= -1)
            {
                string row = ComentIn.ReadLine();
                string[] columns = row.Split('|');
                Comentario C = new Comentario();
                C.id = int.Parse(columns[0]);
                C.autor = columns[1];
                C.fecha = DateTime.Parse(columns[2]);
                C.comentario = (columns[3]);
                C.ip = columns[4];
                C.inapropiado = int.Parse(columns[5]);
                C.likes = int.Parse(columns[6]);

                comens.Add(C);
            }
            ComentIn.Close();
            return comens;
        }
        public static void GetLike(string path)
        {
            List<Comentario> comens;
            
            comens = ReadFromFile(path);

            var Com_likes = from C in comens
                                   orderby C.likes descending
                                   select C;

            foreach (var C in Com_likes)
                Console.WriteLine(C);

        }
        public static void GetTime(string path)
        {
            List<Comentario> comens;

            comens = ReadFromFile(path);

            var Com_Time = from C in comens
                            orderby C.fecha descending
                            select C;

            foreach (var C in Com_Time)
                Console.WriteLine(C);

        }

    }

    class Program
    {
        static void Main(string[] args)
        {

             /*List<Comentario> comens = new List<Comentario>();

            
            comens.Add(new Comentario() { id = 0, autor = "Brandon", fecha = new DateTime(2020,10,20), comentario = "Hola, que onda?", ip = "38515", inapropiado = 10, likes = 15 });

             comens.Add(new Comentario() { id = 1 , autor = "Raul", fecha = new DateTime(2018, 8, 5), comentario = "Papas fritas?", ip = "316545", inapropiado = 100, likes = 50 });

             comens.Add(new Comentario() { id = 2, autor = "Alan", fecha = new DateTime(2019, 5, 10), comentario = "Se vende casas", ip = "356185", inapropiado = 0, likes = 2 });


            ComentarioDB.SaveToFile(comens, @"C:\Users\edgar\OneDrive\Documentos\poo\comen.txt");*/


            List<Comentario> comens = ComentarioDB.ReadFromFile(@"C:\Users\edgar\OneDrive\Documentos\poo\comen.txt");

            foreach (var c in comens)
                Console.WriteLine(c);
            Console.WriteLine("Por likes");
            ComentarioDB.GetLike(@"C:\Users\edgar\OneDrive\Documentos\poo\comen.txt");

            Console.WriteLine("Por fecha");
            ComentarioDB.GetTime(@"C:\Users\edgar\OneDrive\Documentos\poo\comen.txt");

            Console.WriteLine("Deseas agregar un comentario?");
            Console.WriteLine("Presione y o n");
            string pre = Console.ReadLine();
            int al = 0;
            while (pre == "y")
            {
                try
                {
                    Console.Write("Escribe tu nombre: ");
                    string nombres = Console.ReadLine();
                    Console.WriteLine("Escribe el comentario:");
                    string coments = Console.ReadLine();
                    Console.WriteLine("Escribe la ip:");
                    int ips = int.Parse(Console.ReadLine());

                    comens.Add(new Comentario() { id = 3 + al, autor = nombres, comentario = coments, fecha = DateTime.Now, ip = ips.ToString(), inapropiado = 0, likes = 0 });
                    al = al + 1;
                    Console.WriteLine("=================================================================================");
                    foreach (var c in comens)
                        Console.WriteLine(c);

                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("Deseas escribir otro comentario?");
                    pre = Console.ReadLine();
                }
                catch(OverflowException)
                {

                    Console.WriteLine("Error, OverflowException");
                    Console.WriteLine("Escriba una ip De -2.147.483.648 a 2.147.483.647");
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error, FormatException");
                    Console.WriteLine("Escriba el nombre y mensaje nuevamente");
                    Console.WriteLine("Escriba números en la ip");
                }
            }

            Console.WriteLine("Quieres borrar un comentario?");
            Console.WriteLine("Presione y o n");
            string pres = Console.ReadLine();
            while (pres == "y")
            {
                try
                {
                    Console.WriteLine("id del que desea borrar?");
                    int a = int.Parse(Console.ReadLine());
                    comens.RemoveAt(a);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Error, ArgumentOutOfRangeException");
                    Console.WriteLine("Escriba un numero valido");
                }
                catch(FormatException)
                {
                    Console.WriteLine("Error, FormatException");
                    Console.WriteLine("Escriba un valor valido");
                }
                catch(OverflowException)
                {
                    Console.WriteLine("Error, OverflowException");
                    Console.WriteLine("Escriba un valor valido, De -2.147.483.648 a 2.147.483.647");
                }
                Console.WriteLine("=================================================================================");
                foreach (var c in comens)
                        Console.WriteLine(c);
                Console.WriteLine("=================================================================================");
                Console.WriteLine("desea borrar otro comentario?");
                pres = Console.ReadLine();
            }
        }
    }
}

