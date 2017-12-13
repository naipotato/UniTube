using Windows.UI.Xaml;

namespace UniTube.Framework.Mvvm
{
    public class ViewModelLocator
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached(
            "AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, OnAutoWireViewModelChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetAutoWireViewModel(DependencyObject obj) => (bool)obj.GetValue(AutoWireViewModelProperty);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
            => obj.SetValue(AutoWireViewModelProperty, value);

        private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                ViewModelLocationProvider.AutoWireViewModelChanged(d, Bind);
        }

        private static void Bind(object view, object viewModel)
        {
            if (view is FrameworkElement element)
                element.DataContext = viewModel;
        }
    }
}
