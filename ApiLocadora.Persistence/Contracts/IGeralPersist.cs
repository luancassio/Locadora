using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence.Contracts
{
    public interface IGeralPersist
    {

        //GERAL - TODA ALTERAÇÃO SERÁ FEITA UTILIZANDO ESSES METODOS, 
        //SOMENTE OS GETS SERAM DIFERENTES
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
