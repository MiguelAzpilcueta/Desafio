using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Service
{
    public interface IClienteGuardarService
    {
        Task<bool> guardarCienteAsync(Cliente cliente);
    }

    public class ClienteGuardarService : IClienteGuardarService
    {
        public readonly AppDbContext _dbContext;

        public ClienteGuardarService(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> guardarCienteAsync( Cliente cliente)
        {        
            _dbContext.Clientes.Add(cliente);
            _dbContext.SaveChanges();

            return true;
            
        }
    }

}
