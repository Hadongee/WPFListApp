using System;
using System.Collections.Generic;
using System.Text;

namespace CheckList
{
    abstract class Listable
    {
        public string name;
        public bool done;

        public void ToggleDone ()
        {
            done = !done;
        }

        public virtual void Save ()
        {
            throw new NotImplementedException();
        }
    }
}
