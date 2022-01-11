using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Exceptions
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class UnsupportedColourException:Exception
    {
        public UnsupportedColourException(string code):base($"Colour \"{code}\" is unsupported.")
        {

        }
    }
}
