﻿using Fiap.Web.Ocorrencias.Data;
using Fiap.Web.Ocorrencias.Models;

namespace Fiap.Web.Ocorrencia.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DatabaseContext _context;

        public UsuarioRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioModel> GetAll() => _context.Usuarios.ToList();

        public UsuarioModel GetById(int id) => _context.Usuarios.Find(id);

        public void Add(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
        public void Update(UsuarioModel usuario)
        {
            _context.Update(usuario);
            _context.SaveChanges();
        }

        public void Delete(UsuarioModel usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
