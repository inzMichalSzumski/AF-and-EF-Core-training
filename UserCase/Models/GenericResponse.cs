using System;
using System.Collections.Generic;
using System.Text;

namespace UserCase.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse(T value, long miliseconds)
        {
            ResponseValue = value;
            MilisecondsOfExecutionTime = miliseconds;
        }

        public T ResponseValue { get; set; }
        public long MilisecondsOfExecutionTime { get; set; }
    }
}
