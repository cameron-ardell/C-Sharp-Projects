using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class Dictionary
    {
        private ArrayList dictionary = new ArrayList();

        public Dictionary()
        {
            readDictionary();
        }

        public void readDictionary()
        {
            Console.WriteLine("Enter dictionary name without file extension: ");
            String fileName = Console.ReadLine();
            String path = @"c:\Users\sarde\Desktop\C-Sharp-Projects\Boggle\Boggle\files\" + fileName + ".txt";
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                dictionary.Add(line);
            }

            file.Close();
        }

        public bool binarySearch(String word)
        {

            int lowIndex = 0;
            int highIndex = dictionary.Count - 1;
            int middleIndex = (lowIndex + highIndex) / 2;

            while (lowIndex <= highIndex)
            {
                middleIndex = (lowIndex + highIndex) / 2;
                String middleWord = get(middleIndex);
                if (middleWord.Equals(word))
                {
                    return true;
                }
                if (middleWord.CompareTo(word) < 0)
                {
                    lowIndex = middleIndex + 1;
                }
                else
                {
                    highIndex = middleIndex - 1;
                }
            }

            return false;
        }

        public String get(int index)
        {
            return (String)dictionary[index];
        }

        public int size()
        {
            return dictionary.Count;
        }
    }
}
