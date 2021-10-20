using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruleta.Cache
{
    public enum Period
    {
        s, m, h, d
    }
    public class EntitySetting
    {
        public string Name { get; set; }
        public long Expiry { get; set; }
        public Period Period { get; set; }

    }
    public class CacheSettings
    {
        public List<EntitySetting> EntitySettings { get; set; }
        public string Endpoint { get; set; }

    }

}
