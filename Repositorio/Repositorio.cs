using System.Linq.Expressions;
using MagicVillaAPI.Modelos.DTO;
using MagicVillaAPI.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db ;
            this.dbSet = _db.Set<T>();
        }
        public async Task Crear(T entidad)
        {
            await _db.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if(filtro != null)
            {
                query = query.Where(filtro);
            }
            return query.ToListAsync();
        }

        public Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            return Grabar();
        }
    }
}