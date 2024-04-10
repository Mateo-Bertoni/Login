using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
    private static Dictionary<string, (string Contrasenia, string Rol)> users = new Dictionary<string, (string, string)>();

    public static void Main(string[] args)
    {
        string opcion = "0";
        while (opcion != "3")
        {
            Console.WriteLine("Bienvenido al sistema de login. Elige una opción:");
            Console.WriteLine("1. Registrarse");
            Console.WriteLine("2. Iniciar Sesion");
            Console.WriteLine("3. Salir");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Registro();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }
        }
    }

    private static void Registro()
    {
        Console.WriteLine("Introduce tu email:");
        string email = Console.ReadLine();
        if (!EmailValido(email))
        {
            Console.WriteLine("Email Invalido");
            return;
        }

        string opcion2 = "0";
        string contrasenia="123";
        while (opcion2 != "S" && opcion2 != "s")
        { 
            Console.WriteLine("Introduce tu contraseña:");
            contrasenia = Console.ReadLine();
            if (!ContraseniaValida(contrasenia, email))
            {
                Console.WriteLine("Contraseña invalida");
                return;
            }

            if (contrasenia.Length > 16)
            {
                Console.WriteLine("Su contraseña es muy segura");
            }
            else if (contrasenia.Length > 10)
            {
                Console.WriteLine("Su contraseña es segura");
            } 
            else
            {
                Console.WriteLine("Su contraseña poco segura");
            }

            Console.WriteLine("Desea continuar S/N: ");
            opcion2 = Console.ReadLine();
        }
        
        Console.WriteLine("Elige tu rol: (usuario, invitado, administrador)");
        string rol = Console.ReadLine().ToLower();
        if (rol != "usuario" && rol != "invitado" && rol != "administrador")
        {
            Console.WriteLine("Rol invalido");
            return;
        }
        users.Add(email, (Contrasenia: contrasenia, Rol: rol));
        Console.WriteLine("Registro exitoso.");
    }
    private static void Login()
    {
        Console.WriteLine("Introduce tu email:");
        string email = Console.ReadLine();
        Console.WriteLine("Introduce tu contraseña:");
        string contrasenia = Console.ReadLine();

        if (users.TryGetValue(email, out var userDetails))
        {
            if (userDetails.Contrasenia == contrasenia)
            {
                Console.WriteLine($"Inicio de sesión exitoso como {userDetails.Rol}");
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta");
            }
        }
        else
        {
            Console.WriteLine("Usuario no encontrado");
        }
    }

    private static bool EmailValido(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+.[^@\s]+$");
    }

    private static bool ContraseniaValida(string contrasenia, string email)
    {
        return contrasenia.Length > 8 && !contrasenia.Equals(email, StringComparison.OrdinalIgnoreCase) && Regex.IsMatch(contrasenia, @"^[a-zA-Z0-9]*$");
    }
}