using Microsoft.EntityFrameworkCore;
using repositorio.Models;

namespace MVCEF.Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext _context;
        private IGenericRepository<Curso> _cursos;
        private IGenericRepository<Estudiante> _estudiantes;

        public UnitOfWork(RepositoryContext context)
        {
            _context = context;
        }

        public IGenericRepository<Curso> Cursos
        {
            get
            {
                if (_cursos == null)
                {
                    _cursos = new GenericRepository<Curso>(_context);
                }
                return _cursos;
            }
        }

        public IGenericRepository<Estudiante> Estudiantes
        {
            get
            {
                if (_estudiantes == null)
                {
                    _estudiantes = new GenericRepository<Estudiante>(_context);
                }
                return _estudiantes;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
