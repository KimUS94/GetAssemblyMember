using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAssemblyMember
{
    class Program
    {
        static void Main(string[] args)
        {
            string asmPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "ClassLibrary.dll");

            MashallLoad(asmPath);
        }

        private static void MashallLoad(string asmPath)
        {
            try
            {
                AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
                AppDomain newDomain = AppDomain.CreateDomain("NewDom", AppDomain.CurrentDomain.Evidence, setup);

                System.Runtime.Remoting.ObjectHandle obj = newDomain.CreateInstance(typeof(AssemblyLoader).Assembly.FullName, typeof(AssemblyLoader).FullName);

                AssemblyLoader loader = (AssemblyLoader)obj.Unwrap();
                loader.Load(asmPath);


                loader.GetAsmMembers();
                // main AppDomain에서 Type형 변수에 Read한 Assembly의 Type객체를 대입하게되면
                // 내부적으로 Assembly Load가 발생하게 된다.
                //Type type = loader.GetType();
            }
            catch (AppDomainUnloadedException ex)
            {
                //
            }
        }
    }
}
