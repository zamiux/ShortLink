﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Application.Interfaces
{
    public interface IPasswordHelper
    {
        string EncodePasswordMD5(string pass);
    }
}
