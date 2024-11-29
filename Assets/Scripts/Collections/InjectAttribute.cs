using System;

namespace Collections.DependencyInject
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    class InjectComponent : System.Attribute
    {
    }
}