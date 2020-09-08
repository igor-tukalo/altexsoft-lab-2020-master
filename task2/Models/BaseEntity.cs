using System;
using System.Collections.Generic;
using System.Text;

namespace task2.Models
{
    class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
