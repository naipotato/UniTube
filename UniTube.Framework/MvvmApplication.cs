using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI.Xaml;

namespace UniTube.Framework
{
    /// <summary>
    /// Base class that provides MVVM patterns to your UWP application.
    /// </summary>
    public abstract class MvvmApplication : Application
    {
        private object _pageKeys;

        /// <summary>
        /// Initialize a new instance of the <see cref="MvvmApplication"/> class.
        /// </summary>
        public MvvmApplication() => Current = this;

        public new static MvvmApplication Current { get; private set; }

        public Dictionary<T, Type> PageKeys<T>() where T : struct, IConvertible
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (_pageKeys != null && _pageKeys is Dictionary<T, Type>)
            {
                return _pageKeys as Dictionary<T, Type>;
            }

            return (_pageKeys = new Dictionary<T, Type>()) as Dictionary<T, Type>;
        }
    }
}
