using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly DatabaseContext _context;

        public VeiculoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<VeiculoModel> GetAll() => _context.Veiculos.Include(c => c.ServicoEmergencia).ToList();

        public VeiculoModel GetById(int id) => _context.Veiculos.AsNoTracking().FirstOrDefault(v => v.id_veic == id);

        public void Add(VeiculoModel veiculos)
        {
            _context.Veiculos.Add(veiculos);
            _context.SaveChanges();
        }
        public void Update(VeiculoModel veiculos)
        {
            _context.Update(veiculos);
            _context.SaveChanges();
        }

        public void Delete(VeiculoModel veiculos)
        {
            _context.Veiculos.Remove(veiculos);
            _context.SaveChanges();
        }
    }
}
