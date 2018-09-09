using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Runtime
{
    /// <summary>
    /// This class loads external libraries, and hopefully doesn't tank the performance.
    /// TODO: Test executing methods with () and delete execute methods.
    /// TODO: Check if there's anything more to do with arguments, delegates are pretty rigid so anything can be helpful.
    /// </summary>
    class ModuleLoader
    {
        //As many delegates as possibilities.
        public delegate void TemporaryDelegate();
        public delegate void TemporaryDelegate<T>(T[] args);
        public delegate void TemporaryDelegate<T, Y>(T[] args, ref Y[] results);

        public static Assembly LoadModule(string dllName)
        {
            return Assembly.LoadFrom(dllName);
        }

        public static Type ExtractClass(Assembly module, string className)
        {
            Type pluginClass = module.GetType(String.Format("{0}.{1}", module.GetName().Name, className));
            if (pluginClass == null) throw new Exception(String.Format("Class {0} not found in {1}.dll.", className, module.FullName)); //TODO: Put string into Logger/ErrorHandler? Translation purposes.
            return pluginClass;
        }

        //todo: override? (huh?)
        public static TemporaryDelegate ExtractMethod(Type module, string methodName)
        {
            MethodInfo method = module.GetMethod(methodName);
            if (method.Equals(null))
            {
                Common.ErrorHandler.Report(new Exception(String.Format("Method {0} not found in {1}.", methodName, module.Name)));
            }
            TemporaryDelegate tempDelegate = (TemporaryDelegate)Delegate.CreateDelegate(typeof(TemporaryDelegate), method);
            return tempDelegate;
        }

        public static TemporaryDelegate<T> ExtractMethod<T>(Type module, string methodName)
        {
            MethodInfo method = module.GetMethod(methodName);
            if (method.Equals(null))
            {
                Common.ErrorHandler.Report(new Exception(String.Format("Method {0} not found in {1}.", methodName, module.Name)));
            }
            TemporaryDelegate<T> tempDelegate = (TemporaryDelegate<T>)Delegate.CreateDelegate(typeof(TemporaryDelegate<T>), method);

            return tempDelegate;
        }

        public static TemporaryDelegate<T, Y> ExtractMethod<T, Y>(Type module, string methodName)
        {
            MethodInfo method = module.GetMethod(methodName);
            if (method.Equals(null))
            {
                Common.ErrorHandler.Report(new Exception(String.Format("Method {0} not found in {1}.", methodName, module.Name)));
            }
            TemporaryDelegate<T, Y> tempDelegate = (TemporaryDelegate<T, Y>)Delegate.CreateDelegate(typeof(TemporaryDelegate<T, Y>), method);

            return tempDelegate;
        }

        public static Type LoadModuleAndExtractClass(string dllName, string className)
        {
            return ExtractClass(LoadModule(dllName), className);
        }

        /*
        public static void ExtractMethodAndExecute(Type module, string methodName)
        {
            ExtractMethod(module, methodName)();
        }

        public static void ExtractMethodAndExecute<T>(Type module, string methodName, T[] args)
        {
            ExtractMethod<T>(module, methodName)(args);
        }

        public static void ExtractMethodAndExecute<T, Y>(Type module, string methodName, T[] args, Y[] results)
        {
            ExtractMethod<T, Y>(module, methodName)(args, ref results);
        }*/
        
    }
    /// <summary>
    /// This class/type does absolutely nothing, and only serves to shut up VS when no arguments are providen.
    /// Workaround, sure, can't find a way to have result-only function with same signature.
    /// TODO: What did he mean by this?
    /// </summary>
   /* class Nil
    {
        //hack: obamaCmon
    }*/
}
