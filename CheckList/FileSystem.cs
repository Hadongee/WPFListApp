using System;
using System.Collections.Generic;
using System.Text;

namespace CheckList
{
    class FileSystem
    {
        public static string SAVE_DIRECTORY = System.IO.Directory.GetCurrentDirectory();
        public static string FILE_NAME = @"\list.txt";

        public void Save(List list)
        {
            string[] lines = new string[list.elements.Count + 1];
            lines[0] = list.name;

            for (int i = 0; i < list.elements.Count; i++)
            {
                if (list.elements[i].done)
                {
                    lines[1 + i] = "1";
                }
                else
                {
                    lines[1 + i] = "0";
                }
                lines[1 + i] += list.elements[i].name;
            }

            System.IO.File.WriteAllLines(SAVE_DIRECTORY + FILE_NAME, lines);
        }

        public List Load ()
        {
            if (!System.IO.File.Exists(SAVE_DIRECTORY + FILE_NAME))
            {
                return new List("ListApp");
            }
            else
            {
                string[] lines = System.IO.File.ReadAllLines(SAVE_DIRECTORY + FILE_NAME);
                List list = new List(lines[0]);

                for (int i = 1; i < lines.Length; i++)
                {
                    list.elements.Add(new ListElement(lines[i].Remove(0, 1)));
                    string done = lines[i].Remove(1);

                    if (done == "1")
                    {
                        list.elements[list.elements.Count - 1].done = true;
                    }
                }

                list.UpdateDone();

                return list;
            }
        }
    }
}
