using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_DB.DataModel
{
    public class TaxRequestResult<T>
    {
        public bool Success { get; set; }

        public T? Result { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
