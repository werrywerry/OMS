﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Domain
{
    public class InvalidOrderStateException : Exception
    {
        public InvalidOrderStateException(string message)
            :base(message)
        {

        }
    }
}
