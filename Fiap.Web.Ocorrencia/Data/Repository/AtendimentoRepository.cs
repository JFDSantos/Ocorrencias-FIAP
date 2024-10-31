using Fiap.Web.Ocorrencias.Data;
using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly DatabaseContext _context;

        public AtendimentoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<AtendimentoModel> GetAll() => _context.Atendimentos.Include(a => a.Veiculo).Include(B => B.Ocorrencia).Include(c => c.ServicoEmergencia).ToList();

        public IEnumerable<AtendimentoModel> GetAll(int page, int size)
        {
            return _context.Atendimentos.Include(a => a.Veiculo).Include(B => B.Ocorrencia).Include(c => c.ServicoEmergencia)
                            .Skip((page - 1) * page)
                            .Take(size)
                            .AsNoTracking()
                            .ToList();
        }

        public IEnumerable<AtendimentoModel> GetAllReference(int lastReference, int size)
        {
            var atendimento = _context.Atendimentos.Include(a => a.Veiculo).Include(B => B.Ocorrencia).Include(c => c.ServicoEmergencia)
                                .Where(c => c.id_atendimento > lastReference)
                                .OrderBy(c => c.id_atendimento)
                                .Take(size)
                                .AsNoTracking()
                                .ToList();

            return atendimento;
        }
        public AtendimentoModel GetById(int id) => _context.Atendimentos.AsNoTracking().FirstOrDefault(c => c.id_atendimento == id);

        public void Add(AtendimentoModel atendimento)
        {
            _context.Atendimentos.Add(atendimento);
            _context.SaveChanges();
        }
        public void Update(AtendimentoModel atendimento)
        {
            _context.Update(atendimento);
            _context.SaveChanges();
        }

        public void Delete(AtendimentoModel atendimento)
        {
            _context.Atendimentos.Remove(atendimento);
            _context.SaveChanges();
        }
    }
}
