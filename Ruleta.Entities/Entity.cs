using Ruleta.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruleta.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
