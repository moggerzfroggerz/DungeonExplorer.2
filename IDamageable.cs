using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Used for damageable objects: 
    public interface IDamageable
    {
        void DamageTaken(int damage);
        bool IsAlive();
    }
}