using System;
using System.Collections.Generic;
using System.Text;

namespace CheckList
{
    abstract class Listable
    {
        public string name;
        public bool done;
    }

    class List : Listable
    {
        public List<ListElement> elements;

        public List(string name)
        {
            this.name = name;
            this.done = false;
            elements = new List<ListElement>();
        }

        public void UpdateDone ()
        {
            for(int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].done)
                {
                    done = false;
                    return;
                }
            }

            done = true;
        }
    }

    class ListElement : Listable
    {
        public ListElement(string name)
        {
            this.name = name;
            this.done = false;
        }
    }
}
