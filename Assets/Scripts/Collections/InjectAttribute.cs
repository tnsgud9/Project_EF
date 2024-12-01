namespace Collections
{
    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    class Inject : System.Attribute
    {
    }
    
    class InjectChild : System.Attribute
    {
    }
}