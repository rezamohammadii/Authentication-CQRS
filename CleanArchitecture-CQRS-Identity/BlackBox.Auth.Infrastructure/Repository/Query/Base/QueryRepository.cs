using BlackBox.Auth.Domain.Repository.Query.Base;
using BlackBox.Auth.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Infrastructure.Repository.Query.Base
{
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        public QueryRepository(IConfiguration configuration) : base (configuration) { }
        
    }
}
