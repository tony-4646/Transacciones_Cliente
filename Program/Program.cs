using ClienteTransacciones.Data;
using ClienteTransacciones.Services;
using Microsoft.EntityFrameworkCore;

namespace ClienteTransacciones.Program
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var servicio = new ClienteService();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==== MENU DE CLIENTES ====");
                Console.WriteLine("1) Ver todos los clientes");
                Console.WriteLine("2) Transferir saldo");
                Console.WriteLine("3) Eliminar cliente");
                Console.WriteLine("4) Salir");
                Console.Write("Seleccione una opción: ");

                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        await MostrarClientes();
                        break;

                    case "2":
                        Console.Write("ID del cliente origen: ");
                        int idOrigen = int.Parse(Console.ReadLine()!);

                        Console.Write("ID del cliente destino: ");
                        int idDestino = int.Parse(Console.ReadLine()!);

                        Console.Write("Monto a transferir: ");
                        decimal monto = decimal.Parse(Console.ReadLine()!);

                        await servicio.TransferirSaldo(idOrigen, idDestino, monto);
                        break;

                    case "3":
                        Console.Write("ID del cliente a eliminar: ");
                        int idEliminar = int.Parse(Console.ReadLine()!);

                        await servicio.EliminarCliente(idEliminar);
                        break;

                    case "4":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static async Task MostrarClientes()
        {
            using var context = new ClientesContext();
            var clientes = await context.Clientes.ToListAsync();

            Console.WriteLine("\n==== Lista de Clientes ====");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.Id}, Nombre: {cliente.Nombre}, Saldo: {cliente.Saldo:C}");
            }
        }
    }
}
