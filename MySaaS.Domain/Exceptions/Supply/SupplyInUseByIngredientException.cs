using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Domain.Exceptions.Supply
{
    public class SupplyInUseByIngredientException : Exception
    {
        public SupplyInUseByIngredientException()
       : base("Cannot delete supply because it is used by an ingredient.")
        {
        }
        public SupplyInUseByIngredientException(string message)
            : base(message)
        {
        }
        public SupplyInUseByIngredientException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
