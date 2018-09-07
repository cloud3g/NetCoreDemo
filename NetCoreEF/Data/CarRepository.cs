using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using NewApi.Models;


namespace NewApi.Data {
    public class CarRepository : Repository<Car>
    {
        public CarRepository(NewApiContext context) 
            : base(context)
        {
        }
        
        public Car Get(Guid id)
        {
           return FindById(id);
        }

        public Car Add(Car obj)
        {
            return Insert(obj);

        }

        public IEnumerable<Car> ListAll()
        {
            return List();
        }

        public void Delete(Car obj)
        {
            Remove(obj);
        }

        public IEnumerable<Car> SearchByCriteria(Expression<Func<Car, bool>> predicate)
        {
            return Search(predicate);
        }

        public Car UpdateData(Car obj)
        {
            return Update(obj);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PessoaRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}