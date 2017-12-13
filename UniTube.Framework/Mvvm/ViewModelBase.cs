using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniTube.Framework.AppModel;
using UniTube.Framework.Navigation;

namespace UniTube.Framework.Mvvm
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="viewModelState"></param>
        public virtual void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            if (viewModelState != null)
            {
                RestoreViewModel(viewModelState, this);
            }
        }

        public void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            if (viewModelState != null)
            {
                FillStateDictionary(viewModelState, this);
            }
        }

        private static T RetrieveEntityStateValue<T>(string entityStateKey, IDictionary<string, object> viewModelState)
        {
            if (viewModelState != null && viewModelState.ContainsKey(entityStateKey))
                return (T)viewModelState[entityStateKey];

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModelStateKey"></param>
        /// <param name="viewModelStateValue"></param>
        /// <param name="viewModelState"></param>
        public static void AddEntityStateValue(string viewModelStateKey, object viewModelStateValue, IDictionary<string, object> viewModelState)
        {
            if (viewModelState != null)
                viewModelState[viewModelStateKey] = viewModelStateValue;
        }

        private static void FillStateDictionary(IDictionary<string, object> viewModelState, object viewModel)
        {
            var viewModelProperties = viewModel.GetType().GetRuntimeProperties().Where((c) => c.GetCustomAttribute(typeof(RestorableStateAttribute)) != null);

            foreach (var propertyInfo in viewModelProperties)
            {

            }
        }

        private static void RestoreViewModel(IDictionary<string, object> viewModelState, object viewModel)
        {
            var viewModelProperties = viewModel.GetType().GetRuntimeProperties().Where((c) => c.GetCustomAttribute(typeof(RestorableStateAttribute)) != null);

            foreach (var propertyInfo in viewModelProperties)
            {
                if (viewModelState.ContainsKey(propertyInfo.Name))
                {
                    propertyInfo.SetValue(viewModel, viewModelState[propertyInfo.Name]);
                }
            }
        }
    }
}
