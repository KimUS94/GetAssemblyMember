using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GetAssemblyMember
{
    public class AssemblyLoader : MarshalByRefObject
    {
        private System.Reflection.Assembly m_Assembly;
        private Type m_Type;
        private object m_instance;

        public AssemblyLoader()
        {
            m_Assembly = null;
            m_Type = null;
            m_instance = null;
        }

        ~AssemblyLoader()
        {
            m_Assembly = null;
            m_Type = null;
            m_instance = null;
        }

        public void Load(string path)
        {
            this.m_Assembly = System.Reflection.Assembly.Load(AssemblyName.GetAssemblyName(path));
        }

        public object ExecuteMethod(string strModule, string methodName, params object[] parameters)
        {
            foreach (System.Type type in this.m_Assembly.GetTypes())
            {
                if (String.Compare(type.Name, strModule, true) == 0)
                {
                    this.m_Type = type;
                    this.m_instance = m_Assembly.CreateInstance(type.FullName);
                    break;
                }
            }

            //MethodInfo MyMethod = MyType.GetMethod(methodName, new Type[] { typeof(int), typeof(string), typeof(string), typeof(string) });
            //MyMethod.Invoke(inst, BindingFlags.InvokeMethod, null, parameters, null);

            MethodInfo MyMethod = this.m_Type.GetMethod(methodName);
            object obj = MyMethod.Invoke(this.m_instance, BindingFlags.InvokeMethod, null, parameters, null);

            return obj;
        }

        public void GetAsmMembers()
        {
            Type[] types = this.m_Assembly.GetTypes();

            List<string> constructors = new List<string>();
            List<string> events = new List<string>();
            List<string> fields = new List<string>();

            List<string> usrMethods = new List<string>();
            List<string> sysMethods = new List<string>();


            List<string> propertys = new List<string>();




            foreach (Type type in types)
            {
                if (type.IsClass)
                {

                    foreach (System.Reflection.MemberInfo member in type.GetMembers())
                    {
                        switch (member.MemberType)
                        {
                            case MemberTypes.Constructor:
                                constructors.Add(member.Name);
                                break;

                            case MemberTypes.Event:
                                events.Add(member.Name);
                                break;

                            case MemberTypes.Field:
                                fields.Add(member.Name);
                                break;

                            case MemberTypes.Method:
                                //isStatic
                                //isVirtur
                                //isSpecialName                                
                                if (member.DeclaringType.FullName != "System.Object")
                                {
                                    //User Function
                                    usrMethods.Add(member.Name);
                                }
                                else
                                {
                                    //Sysmtem Function
                                    sysMethods.Add(member.Name);
                                }
                                break;

                            case MemberTypes.Property:
                                propertys.Add(member.Name);
                                break;
                        }
                    }
                }
            
            }
            
        }
    }
}
