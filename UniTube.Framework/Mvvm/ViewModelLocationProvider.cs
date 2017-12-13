using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace UniTube.Framework.Mvvm
{
    public static class ViewModelLocationProvider
    {
        private static Dictionary<string, Func<object>> _factories = new Dictionary<string, Func<object>>();

        private static Dictionary<string, Type> _typeFactories = new Dictionary<string, Type>();

        private static Func<Type, object> _defaultViewModelFactory = (type) => Activator.CreateInstance(type);

        private static Func<object, Type, object> _defaultViewModelFactoryWithViewParameter;

        private static Func<Type, Type> _defaultViewTypeToViewModelTypeResolver = (viewType) =>
        {
            var viewName = viewType.FullName;
            viewName = viewName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}{1}, {2}", viewName, suffix, viewAssemblyName);
            return Type.GetType(viewModelName);
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModelFactory"></param>
        public static void SetDefaultViewModelFactory(Func<Type, object> viewModelFactory)
            => _defaultViewModelFactory = viewModelFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModelFactory"></param>
        public static void SetDefaultViewModelFactory(Func<object, Type, object> viewModelFactory)
            => _defaultViewModelFactoryWithViewParameter = viewModelFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewTypeToViewModelTypeResolver"></param>
        public static void SetDefaultViewTypeToViewModelTypeResolver(Func<Type, Type> viewTypeToViewModelTypeResolver)
            => _defaultViewTypeToViewModelTypeResolver = viewTypeToViewModelTypeResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="setDataContextCallback"></param>
        public static void AutoWireViewModelChanged(object view, Action<object, object> setDataContextCallback)
        {
            var viewModel = GetViewModelForView(view);

            if (viewModel == null)
            {
                var viewModelType = GetViewModelTypeForView(view.GetType());

                if (viewModelType == null)
                    viewModelType = _defaultViewTypeToViewModelTypeResolver(view.GetType());

                if (viewModelType == null)
                    return;

                viewModel = _defaultViewModelFactoryWithViewParameter != null ? _defaultViewModelFactoryWithViewParameter(view, viewModelType) : _defaultViewModelFactory(viewModelType);
            }

            setDataContextCallback(view, viewModel);
        }

        private static object GetViewModelForView(object view)
        {
            var viewKey = view.GetType().ToString();

            if (_factories.ContainsKey(viewKey))
                return _factories[viewKey]();

            return null;
        }

        private static Type GetViewModelTypeForView(Type view)
        {
            var viewKey = view.ToString();

            if (_typeFactories.ContainsKey(viewKey))
                return _typeFactories[viewKey];

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public static void Register<T>(Func<object> factory) => Register(typeof(T).ToString(), factory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewTypeName"></param>
        /// <param name="factory"></param>
        public static void Register(string viewTypeName, Func<object> factory) => _factories[viewTypeName] = factory;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="VM"></typeparam>
        public static void Register<T, VM>() => Register(typeof(T).ToString(), typeof(VM));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewTypeName"></param>
        /// <param name="viewModelType"></param>
        public static void Register(string viewTypeName, Type viewModelType)
            => _typeFactories[viewTypeName] = viewModelType;
    }
}
