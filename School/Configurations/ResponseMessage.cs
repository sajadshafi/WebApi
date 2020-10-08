using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Configurations {
    public class ResponseMessage<T> {
        public bool Success { get; set; } = true;
        public T Data { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
