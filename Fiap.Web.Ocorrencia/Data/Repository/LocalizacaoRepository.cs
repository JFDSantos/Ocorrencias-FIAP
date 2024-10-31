using Fiap.Web.Ocorrencias.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Ocorrencias.Data.Repository
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly DatabaseContext _context;

        public LocalizacaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<LocalizacaoModel> GetAll() => _context.Localizacaos.ToList();

        public LocalizacaoModel GetById(int id) => _context.Localizacaos.Find(id);

        public void Add(LocalizacaoModel localizacao)
        {
            _context.Localizacaos.Add(localizacao);
            _context.SaveChanges();
        }
        public void Update(LocalizacaoModel localizacao)
        {
            _context.Update(localizacao);
            _context.SaveChanges();
        }

        public void Delete(LocalizacaoModel localizacao)
        {
            _context.Localizacaos.Remove(localizacao);
            _context.SaveChanges();
        }
    }
}
