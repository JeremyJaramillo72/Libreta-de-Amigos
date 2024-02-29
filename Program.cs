using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Globalization;
namespace Clase_de_ayudantiassssss
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int Opc = 0;
            string RutaDeArchivo = " ";
            string[] amigos = new string[10];
            do
            {
                Console.Clear();
                Console.WriteLine("1. Ingrese la ruta");
                Console.WriteLine("2. Agregar un amigo");
                Console.WriteLine("3. Desea modificar un amigo?");
                Console.WriteLine("4. Desea eliminar un amigo?");
                Console.WriteLine("5. Lista de amigos");
                Console.WriteLine("6. Cerrar");
                Console.Write("\nSeleccione opción de menú --> ");
                Opc = int.Parse(Console.ReadLine());

                switch (Opc)
                {
                    case 1:
                            Console.WriteLine("Ejemplo de ruta: C:\\Users\\Charito Acosta\\Documents\\Jeremy\\Texto.txt");
                            Console.WriteLine("Ingrese la ruta del archivo a guardar");
                            RutaDeArchivo = Console.ReadLine();
                            RutaDelArchivo(RutaDeArchivo);
                        break;
                    case 2:          
                            AgregarAmigos(RutaDeArchivo, amigos);  
                        break;

                    case 3:
                        EditarAmigo(RutaDeArchivo, amigos);
                        break;
                    case 4:
                        EliminarAmigo(RutaDeArchivo, amigos);
                        break;
                    case 5:
                        ListaDeAmigos(RutaDeArchivo, amigos);
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del programa....");
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }

            } while (Opc != 6);

            Console.ReadKey();
        }
        static void RutaDelArchivo(string ruta)
        {
            FileStream agenda = new FileStream(@ruta, FileMode.Create, FileAccess.Write);
            Console.WriteLine("La carpeta se creó en {0}", ruta);
            agenda.Close();
        }
        static void AgregarAmigos(string ruta, string[] amigos)
        {
            Console.WriteLine("Ingrese el nombre de su amigo");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el genero de su amigo (F/M)");
            string genero = Console.ReadLine();
            Console.WriteLine("Ingrese la edad de su amigo");
            int edad = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la ciudad de su amigo");
            string ciudad = Console.ReadLine();
            string amigo = nombre + ";" + genero + ";" + edad.ToString() + ";" + ciudad +":";
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null)
                {
                    amigos[i] = amigo;
                    break;
                }
            }
            using (StreamWriter writer = new StreamWriter(ruta, true))
            {
                writer.WriteLine(amigo);
            }
            Console.WriteLine("Amigo agregado correctamente.");
        }
        static void EditarAmigo(string ruta, string[] amigos)
        {
            bool encontrado = false;
            Console.WriteLine("Ingrese el nombre del amigo a editar");
            string nombreBuscar = Console.ReadLine();

            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null)
                {
                    string[] datosAmigo = amigos[i].Split(';');
                    string nombre = datosAmigo[0];

                    if (nombre.ToLower() == nombreBuscar.ToLower())
                    {
                        encontrado = true;
                        Console.WriteLine("Amigo encontrado:");
                        Console.WriteLine($"Nombre: {datosAmigo[0]}");
                        Console.WriteLine($"Género: {datosAmigo[1]}");
                        Console.WriteLine($"Edad: {datosAmigo[2]}");
                        Console.WriteLine($"Ciudad: {datosAmigo[3]}");

                        Console.WriteLine("¿Desea cambiar la edad de tu amigo? (S/N):");
                        string respuesta = Console.ReadLine();

                        if (respuesta.ToLower() == "s")
                        {
                            Console.WriteLine("Ingrese la nueva edad:");
                            int nuevaEdad = int.Parse(Console.ReadLine());
                            datosAmigo[2] = nuevaEdad.ToString();
                        }
                        Console.WriteLine("¿Desea cambiar el genero de tu amigo? (S/N):");
                        respuesta = Console.ReadLine();
                        if (respuesta.ToLower()=="s")
                        {
                            Console.WriteLine("Ingrese el nuevo género de tu amigo (F/M):");
                            string nuevoGenero = Console.ReadLine();
                            datosAmigo[1] = nuevoGenero;
                        }
                        Console.WriteLine("¿Desea cambiar la ciudad de tu amigo? (S/N):");
                        respuesta = Console.ReadLine();
                        if (respuesta.ToLower() == "s")
                        {
                            Console.WriteLine("Ingrese la nuevo ciudad de tu amigo (F/M):");
                            string nuevociudad = Console.ReadLine();
                            datosAmigo[3] = nuevociudad;
                        }
                        amigos[i] = string.Join(";", datosAmigo);
                        File.WriteAllLines(ruta, amigos);
                        Console.WriteLine("Amigo modificado correctamente.");
                        break;
                    }
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("El amigo no se encuentra en la lista.");
            }
            if (!encontrado)
            {
                Console.WriteLine("El amigo no se encuentra en la lista.");
            }
        }
        static void EliminarAmigo(string ruta, string[] amigos)
        {
            Console.WriteLine("Ingrese el nombre del amigo que desea eliminar:");
            string nombreEliminar = Console.ReadLine();
            bool encontrado = false;
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] != null)
                {
                    string[] datosAmigo = amigos[i].Split(';');
                    string nombre = datosAmigo[0];

                    if (nombre.ToLower() == nombreEliminar.ToLower())
                    {
                        encontrado = true;
                        Console.WriteLine("Amigo encontrado y eliminado:");
                        Console.WriteLine($"Nombre: {datosAmigo[0]}");
                        Console.WriteLine($"Género: {datosAmigo[1]}");
                        Console.WriteLine($"Edad: {datosAmigo[2]}");
                        Console.WriteLine($"Ciudad: {datosAmigo[3]}");
                        amigos[i] = null; 
                        Console.WriteLine("Amigo eliminado correctamente.");
                        break;
                    }
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("El amigo no se encuentra en la lista.");
            }
            else
            {
                File.WriteAllLines(ruta, amigos);
            }
        }
        static void ListaDeAmigos(string ruta, string[] amigos)
        {
            try
            {
                Console.WriteLine("Lista de amigos:");
                foreach (string amigo in amigos)
                {
                    if (amigo != null)
                    {
                        Console.WriteLine(amigo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer la lista de amigos: " + ex.Message);
            }
        }
    }
}