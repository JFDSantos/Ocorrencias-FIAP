using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public class OcorrenciaRepository : IOcorrenciaRepository
    {
        private readonly DatabaseContext _context;

        public OcorrenciaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<OcorrenciaModel> GetAll() => _context.Ocorrencias.Include(a => a.Localizacao).Include(b => b.Gravidade).ToList();

        public IEnumerable<OcorrenciaModel> GetAll(int page, int size)
        {
            return _context.Ocorrencias.Include(a => a.Localizacao).Include(b => b.Gravidade)
                            .Skip((page - 1) * page)
                            .Take(size)
                            .AsNoTracking()
                            .ToList();
        }

        public IEnumerable<OcorrenciaModel> GetAllReference(int lastReference, int size)
        {
            var atendimento = _context.Ocorrencias.Include(a => a.Localizacao).Include(b => b.Gravidade)
                                .Where(c => c.id_ocorrencia > lastReference)
                                .OrderBy(c => c.id_ocorrencia)
                                .Take(size)
                                .AsNoTracking()
                                .ToList();

            return atendimento;
        }

        public OcorrenciaModel GetById(int id) => _context.Ocorrencias.Find(id);

        public void Add(OcorrenciaModel ocorrencia)
        {
            _context.Ocorrencias.Add(ocorrencia);
            _context.SaveChanges();
        }
        public void Update(OcorrenciaModel ocorrencia)
        {
            _context.Update(ocorrencia);
            _context.SaveChanges();
        }

        public void Delete(OcorrenciaModel ocorrencia)
        {
            _context.Ocorrencias.Remove(ocorrencia);
            _context.SaveChanges();
        }
    }
}
