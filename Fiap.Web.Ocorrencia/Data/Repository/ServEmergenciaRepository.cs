using Fiap.Web.Ocorrencias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public class ServEmergenciaRepository : IServEmergenciaRepository
    {
        private readonly DatabaseContext _context;

        public ServEmergenciaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ServicoEmergenciaModel> GetAll() => _context.ServicoEmergencias.Include(c => c.Gravidade).ToList();

        public ServicoEmergenciaModel GetById(int id) => _context.ServicoEmergencias.AsNoTracking().FirstOrDefault(a => a.id_serv_emergencia == id);

        public void Add(ServicoEmergenciaModel servicoEmergencia)
        { 
                _context.ServicoEmergencias.Add(servicoEmergencia);
                _context.SaveChanges();
        }
        public void Update(ServicoEmergenciaModel servicoEmergencia)
        {
            _context.Update(servicoEmergencia);
            _context.SaveChanges();
        }

        public void Delete(ServicoEmergenciaModel servicoEmergencia)
        {
            _context.ServicoEmergencias.Remove(servicoEmergencia);
            _context.SaveChanges();
        }
    }
}
