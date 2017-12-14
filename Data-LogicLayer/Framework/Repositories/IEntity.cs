﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Framework
{
    public interface IEntity : IEntity<int> { }
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}