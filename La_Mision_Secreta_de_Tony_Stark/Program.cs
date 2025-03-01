using System;
using System.IO;

class Program
{
    //  Rutas de trabajo
    static string rutaLaboratorio = @"C:\LaboratorioAvengers";
    static string rutaBackup = Path.Combine(rutaLaboratorio, "Backup");
    static string rutaClasificados = Path.Combine(rutaLaboratorio, "ArchivosClasificados");
    static string rutaProyectos = Path.Combine(rutaLaboratorio, "ProyectosSecretos");
    static string rutaArchivoInventos = Path.Combine(rutaLaboratorio, "inventos.txt");
    static void CrearCarpeta(string rutaCarpeta) // Función para  crear una carpeta si no existe
    {
        try
        {
            if (!Directory.Exists(rutaCarpeta)) // Verifica si la carpeta ya existe
            {
                Directory.CreateDirectory(rutaCarpeta); // Si no existe, la crea
                Console.WriteLine($"Carpeta creada: {rutaCarpeta}");
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: El directorio no se pudo encontrar.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al acceder al directorio.");
        }
    }

    static void CrearArchivoInventos() // Fiuncion que crea el archivo inventos.txt si no existe
    {
        try
        {
            CrearCarpeta(rutaLaboratorio); // Llama a la función CrearCarpeta para asegurar que la carpeta principal exista

            if (!File.Exists(rutaArchivoInventos)) // Verifica si el archivo ya existe
            {
                string contenido = "1. Traje Mark I\n2. Reactor Arc\n3. Inteligencia Artificial JARVIS"; // Si no existe, lo crea con un contenido inicial
                File.WriteAllText(rutaArchivoInventos, contenido);
                Console.WriteLine("Archivo inventos.txt creado.");
            }
            else
            {
                Console.WriteLine("El archivo ya existe.");  // Si el archivo ya existe, muestra un mensaje
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No tienes permisos para crear el archivo.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al crear el archivo.");
        }
    }

    static void AgregarInvento(string nuevoInvento) // Funcion para agrega un invento al archivo inventos.txt
    {
        try
        {
            if (!File.Exists(rutaArchivoInventos)) // Verifica si el archivo existe
            {
                Console.WriteLine("El archivo no existe. Creándolo..."); // Si no existe, lo crea
                CrearArchivoInventos();
            }

            File.AppendAllText(rutaArchivoInventos, "\n" + nuevoInvento); // Agrega el nuevo invento al archivo sin sobrescribir el contenido
            Console.WriteLine("Invento agregado.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No tienes permisos para modificar el archivo.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al modificar el archivo.");
        }
    }

    static void LeerInventosLineaPorLinea()  // Funcion que lee el archivo línea por línea y muestra su contenido
    {
        try
        {
            if (!File.Exists(rutaArchivoInventos)) // Verifica si el archivo existe
            {
                Console.WriteLine("Error: El archivo no existe.");
                return;
            }
            string[] lineas = File.ReadAllLines(rutaArchivoInventos); // Lee el archivo línea por línea
            Console.WriteLine("=== Inventos registrados ===");
            foreach (string linea in lineas)
            {
                Console.WriteLine(linea); // Muestra cada línea del archivo
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: El archivo no existe.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al leer el archivo.");
        }
    }

    static void LeerTodoElTexto() // Funcion que lee todo el contenido del archivo de una sola vez
    {
        try
        {
            if (!File.Exists(rutaArchivoInventos)) // Verifica si el archivo existe
            {
                Console.WriteLine("Error: El archivo no existe.");
                return;
            }
            string contenido = File.ReadAllText(rutaArchivoInventos); // Lee todo el contenido del archivo de una sola vez
            Console.WriteLine("=== Todo el contenido ===");
            Console.WriteLine(contenido); // Muestra el contenido completo del archivo
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: El archivo no existe.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al leer el archivo.");
        }
    }

    static void CopiarArchivo() // Funcion que copia el archivo inventos.txt al directorio de Backup
    {
        try
        {
            CrearCarpeta(rutaBackup); // Asegura que la carpeta de Backup exista
            string rutaRespaldo = Path.Combine(rutaBackup, "inventos_backup.txt");// Define la ruta del archivo de respaldo

            if (File.Exists(rutaArchivoInventos)) // Verifica si el archivo original existe
            {
                File.Copy(rutaArchivoInventos, rutaRespaldo, true); // Si existe el archivo, lo copia al directorio de backup
                Console.WriteLine("Archivo copiado a Backup.");
            }
            else
            {
                Console.WriteLine("Error: El archivo no existe.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No tienes permisos para copiar el archivo.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al copiar el archivo.");
        }
    }

    static void MoverArchivo() // Funcion para mover el archivo inventos.txt a ArchivosClasificados
    {
        try
        {
            CrearCarpeta(rutaClasificados); // Asegura que la carpeta ArchivosClasificados exista
            string rutaDestino = Path.Combine(rutaClasificados, "inventos.txt"); // Define la nueva ruta para el archivo

            if (File.Exists(rutaArchivoInventos))  // Verifica si el archivo original existe
            {
                File.Move(rutaArchivoInventos, rutaDestino);  // Si existe el archivo, lo mueve al nuevo directorio
                Console.WriteLine("Archivo movido a ArchivosClasificados.");
            }
            else
            {
                Console.WriteLine("Error: El archivo no existe.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No tienes permisos para mover el archivo.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al mover el archivo.");
        }
    }

    static void EliminarArchivo() // Funcion para eliminar el archivo inventos.txt después de hacer un respaldo
    {
        try
        {
            string rutaRespaldo = Path.Combine(rutaBackup, "inventos_backup.txt"); // Define la ruta del archivo respaldado

            if (File.Exists(rutaArchivoInventos)) // Verifica si el archivo original existe
            {
                if (File.Exists(rutaRespaldo)) // Verifica si hay un archivo de respaldo
                {
                    File.Delete(rutaArchivoInventos); // Elimina el archivo original si existe un respaldo
                    Console.WriteLine("Archivo inventos.txt eliminado.");
                }
                else
                {
                    Console.WriteLine("Error: No hay copia de seguridad.");
                }
            }
            else
            {
                Console.WriteLine("Error: El archivo no existe.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No tienes permisos para eliminar el archivo.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al eliminar el archivo.");
        }
    }

    static void ListarArchivosEnCarpeta(string rutaCarpeta) // Funcion para listar todos los archivos en el directorio especificado
    {
        try
        {
            if (Directory.Exists(rutaCarpeta)) // Verifica si la carpeta existe
            {
                string[] archivos = Directory.GetFiles(rutaCarpeta); // Obtiene todos los archivos en la carpeta
                Console.WriteLine("=== Archivos en la carpeta ===");
                foreach (string archivo in archivos)
                {
                    Console.WriteLine(Path.GetFileName(archivo)); // Muestra solo el nombre del archivo
                }
            }
            else
            {
                Console.WriteLine("Error: La carpeta no existe.");
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: La carpeta no se encuentra.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error: Hubo un problema al listar los archivos.");
        }
    }

    static void Main() // Menú interactivo para ejecutar las operaciones
    {
        // Asegura que las carpetas principales que se mencionan en la instrucciones de la tarea existan
        CrearCarpeta(rutaLaboratorio);
        CrearCarpeta(rutaBackup);
        CrearCarpeta(rutaClasificados);
        CrearCarpeta(rutaProyectos);

        while (true)
        {
            Console.Clear(); // Limpia la consola para mostrar el menú
            Console.WriteLine(".............................................");
            Console.WriteLine("¡Bienvenido al LaboratorioAvengers!");
            Console.WriteLine(".............................................");
            Console.WriteLine("Elige lo que deseas hacer:");
            Console.WriteLine(".............................................");
            Console.WriteLine("1. Crear archivo inventos.txt");
            Console.WriteLine("2. Agregar un invento");
            Console.WriteLine("3. Leer inventos línea por línea");
            Console.WriteLine("4. Leer todo el contenido");
            Console.WriteLine("5. Copiar archivo a Backup");
            Console.WriteLine("6. Mover archivo a ArchivosClasificados");
            Console.WriteLine("7. Eliminar archivo inventos.txt");
            Console.WriteLine("8. Listar archivos en la carpeta");
            Console.WriteLine("9. Crear la carpeta ProyectosSecretos");
            Console.WriteLine("10. Salir");
            Console.Write("Selecciona una opción: ");


            switch (Console.ReadLine())
            {
                case "1": CrearArchivoInventos(); break;
                case "2": AgregarInvento(Console.ReadLine()); break;
                case "3": LeerInventosLineaPorLinea(); break;
                case "4": LeerTodoElTexto(); break;
                case "5": CopiarArchivo(); break;
                case "6": MoverArchivo(); break;
                case "7": EliminarArchivo(); break;
                case "8": ListarArchivosEnCarpeta(rutaLaboratorio); break;
                case "9":
                    CrearCarpeta(rutaProyectos); 
                    Console.WriteLine("Carpeta 'ProyectosSecretos' creada.");
                    break;
                case "10": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
            Console.WriteLine("\nPresiona ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
