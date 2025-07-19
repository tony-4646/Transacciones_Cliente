using ClienteTransacciones.Data;

namespace ClienteTransacciones.Services
{
    public class ClienteService
    {
        public async Task TransferirSaldo(int idOrigen, int idDestino, decimal monto)
        {
            using var context = new ClientesContext();
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var origen = await context.Clientes.FindAsync(idOrigen);
                var destino = await context.Clientes.FindAsync(idDestino);

                if (origen == null || destino == null)
                    throw new Exception("Uno de los clientes no existe.");

                if (monto <= 0)
                    throw new Exception("El monto debe ser mayor que cero.");

                if (origen.Saldo < monto)
                    throw new Exception("Saldo insuficiente.");

                origen.Saldo -= monto;
                destino.Saldo += monto;

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                Console.WriteLine($"Transferencia exitosa de {monto:C} de {origen.Nombre} a {destino.Nombre}.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error en la transferencia: {ex.Message}");
            }
        }

        public async Task EliminarCliente(int idCliente)
        {
            using var context = new ClientesContext();
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await context.Clientes.FindAsync(idCliente);

                if (cliente == null)
                    throw new Exception("Cliente no encontrado.");

                if (cliente.Saldo != 0)
                    throw new Exception("El saldo debe ser 0 para eliminar el cliente.");

                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                Console.WriteLine($"Cliente {cliente.Nombre} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
            }
        }
    }
}
