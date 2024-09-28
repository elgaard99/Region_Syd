using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public virtual class Repository
    {
        public virtual string _connectionString { get; private set; }

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
