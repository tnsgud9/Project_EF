namespace Collections
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    class InjectComponent : System.Attribute
    {
    }
    
    class InjectChildrenComponent : System.Attribute
    {
    }
}