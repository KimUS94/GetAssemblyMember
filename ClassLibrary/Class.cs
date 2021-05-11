using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Class
    {
        public event EventHandler handler;

        public string publicMember;
        private string privateMember;
        

        public string publicProperty
        {
            get
            {
                return "property";
            }
        }

        public Class()
        {

        }

        public Class(string para1, string para2)
        {

        }

        public void PublicMethod()
        {
            //
        }

        private void PrivateMethod()
        {
            //
        }

        public virtual void VirtulaMethod()
        {

        }

        public static void StaticMethod()
        {

        }
    }
}
