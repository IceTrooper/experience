using System.Collections.Generic;
using UnityEngine;

namespace Atoms
{
    public abstract class AtomList<T> : ScriptableObject
    {
        // Made a wrapper because of annoying 'Type Mismatch' at the runtime. Class could be expanded to provide more secure way of accessing objects.
        public List<T> Items
        {
            get
            {
                return items;
            }
            set
            {
                Items = value;
            }
        }
        protected List<T> items;
    }

}
