using BlackBox.Auth.Domain.Repository.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackBox.Auth.Infrastructure.Repository.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        public CommandRepository()
        {
            
        }
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
