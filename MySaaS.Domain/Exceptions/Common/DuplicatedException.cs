using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MySaaS.Domain.Exceptions.Common
{
    public class DuplicatedException : DuplicateNameException
    {
        public string Column { get; init; }
        public string Value { get; init; }

        public DuplicatedException(string value) : base("Duplicate entry found.")
        {
            Column = "";
            Value = value;
        }
        public DuplicatedException(string column,string value) : base($"Duplicate entry found. Column: {column} | Value : {value}")
        {
            Column = column;
            Value = value;
        }
    }
}
