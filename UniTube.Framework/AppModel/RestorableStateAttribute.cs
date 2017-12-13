using System;

namespace UniTube.Framework.AppModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RestorableStateAttribute : Attribute
    {

    }
}
