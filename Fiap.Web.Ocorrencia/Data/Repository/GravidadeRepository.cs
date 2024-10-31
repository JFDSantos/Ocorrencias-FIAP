using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public class GravidadeRepository : IGravidadeRepository
    {
        private readonly DatabaseContext _context;

        public GravidadeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<GravidadeModel> GetAll() => _context.Gravidades.ToList();

        public IEnumerable<GravidadeModel> GetAll(int page, int size)
        {
            return _context.Gravidades
                            .Skip((page - 1) * page)
                            .Take(size)
                            .AsNoTracking()
                            .ToList();
        }

        public IEnumerable<GravidadeModel> GetAllReference(int lastReference, int size)
        {
            var gravidade = _context.Gravidades
                                .Where(c => c.id_gravidade > lastReference)
                                .OrderBy(c => c.id_gravidade)
                                .Take(size)
                                .AsNoTracking()
                                .ToList();

            return gravidade;
        }

        public GravidadeModel GetById(int id) => _context.Gravidades.Find(id);

        public void Add(GravidadeModel gravidade)
        {
            _context.Gravidades.Add(gravidade);
            _context.SaveChanges();
        }
        public void Update(GravidadeModel gravidade)
        {
            _context.Update(gravidade);
            _context.SaveChanges();
        }

        public void Delete(GravidadeModel gravidade)
        {
            _context.Gravidades.Remove(gravidade);
            _context.SaveChanges();
        }
    }

}
