using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Application.Common.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base()
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string message, System.Exception exp) : base(message, exp)
        {

        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {

        }
    }
}
