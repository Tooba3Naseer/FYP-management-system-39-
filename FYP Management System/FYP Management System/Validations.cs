using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP_Management_System
{
    class Validations
    {
        // data members
        private String tit;
        private String des;
        
        // constructor without parameter
        public Validations()
        {
            tit = "dfgh";
            des = "fgh";
        }
        public String title
        {
            get { return title; }
            set
            {
                if (isalphaTest(title))
                { }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        public String description
        {
            get { return description; }
            set
            {
                if (isalphaTest(description))
                { }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }
        private bool isalphaTest(String name)
        {
            if (String.IsNullOrWhiteSpace(name))  // built-in function that checks that is it just null or white space in string
                return false;
            for (int i = 0; i < name.Length; i++)
            {
                if ((Char.IsLetter(name[i])) || (name[i] == ' ' && Char.IsLetter(name[i - 1]) && Char.IsLetter(name[i + 1])))
                    continue;
                else
                    return false;
            }
            return true;
        }

    }
}
