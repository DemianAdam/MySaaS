using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaaS.Domain.Exceptions.Recipe
{
    public class RecipeWithoutComponentsException : Exception
    {
        public RecipeWithoutComponentsException() : base("A recipe must contain at least one component.")
        {
        }

        public RecipeWithoutComponentsException(string? message) : base(message)
        {
        }

        public RecipeWithoutComponentsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
