using System;

namespace Collections
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal class Inject : Attribute
    {
    }

    internal class InjectChild : Attribute
    {
    }
}