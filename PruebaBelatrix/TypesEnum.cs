using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaBelatrix
{
    public class TypesEnum
    {
        public enum LogTo
        {
            File = 1,
            Console = 2,
            Database = 3,
            Nothing = 4
        }

        public enum TypeMessage
        {
            Info = 1,
            Warning = 2,
            Error = 3
        }
    }
}
