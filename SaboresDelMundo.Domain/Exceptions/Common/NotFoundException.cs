using System;
using System.Collections.Generic;
using System.Text;

namespace MySaaS.Domain.Exceptions.Common
{
    public abstract class NotFoundException : Exception
    {
        public int Id { get; init; }
        protected NotFoundException(string message) : base(message)
        {
        }
    }
    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException() : base($"{typeof(T).Name} not found.")
        {
        }
        public NotFoundException(int id) : base($"{typeof(T).Name} with Id {id} not found.")
        {
            Id = id;
        }
    }
}
