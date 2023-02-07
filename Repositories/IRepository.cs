using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TechSupportHelpSystem.Repositories
{
    interface IRepository<T> where T : class
    {
        List<T> GetAll(DbContextOptions clientOptions);
        T Get(DbContextOptions clientOptions, int id);
        T Create(DbContextOptions clientOptions, T item);
        T Update(DbContextOptions clientOptions, T item);
        T Delete(DbContextOptions clientOptions, int id);

    }
}
