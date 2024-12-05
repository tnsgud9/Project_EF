using System;

namespace Collections
{
    /// <summary>
    ///     Inject은 GetComponent와 같은 역할이며 해당 컴포넌트가 존재해야한다.
    ///     값을 설정한 컴포넌트를 가져올때 사용
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal class Inject : Attribute
    {
    }

    /// <summary>
    ///     Inject은 GetChildrenComponent와 같은 역할이며 해당 컴포넌트가 존재해야한다.
    ///     값을 설정한 자식 컴포넌트를 가져올때 사용
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal class InjectChild : Attribute
    {
    }

    /// <summary>
    ///     Inject은 GetComponent,AddCompoent와 같은 역할이며 해당 컴포넌트가 존재해야한다.
    ///     값이 없는 경우 Add 있는 경우 설정한 컴포넌트를 가져온다.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal class InjectAdd : Attribute
    {
    }
}