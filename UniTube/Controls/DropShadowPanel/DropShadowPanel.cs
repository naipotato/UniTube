using System.Numerics;

using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Shapes;

namespace UniTube.Controls
{
    [TemplatePart(Name = ShadowPart, Type = typeof(Border))]
    public partial class DropShadowPanel : ContentControl
    {
        private const string ShadowPart = "Shadow";

        private DropShadow      _dropShadow;
        private SpriteVisual    _shadowVisual;
        private Border          _border;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropShadowPanel"/> class.
        /// </summary>
        public DropShadowPanel()
        {
            DefaultStyleKey = typeof(DropShadowPanel);

            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            _shadowVisual = compositor.CreateSpriteVisual();

            _dropShadow = compositor.CreateDropShadow();
            _shadowVisual.Shadow = _dropShadow;
        }

        protected override void OnApplyTemplate()
        {
            _border = GetTemplateChild(ShadowPart) as Border;

            if (_border != null)
                ElementCompositionPreview.SetElementChildVisual(_border, _shadowVisual);

            ConfigureShadowVisualForCastingElement();

            base.OnApplyTemplate();
        }

        private void OnBlurRadiusChanged(double newValue)
        {
            if (_dropShadow != null)
                _dropShadow.BlurRadius = (float)newValue;
        }

        private void OnColorChanged(Color newValue)
        {
            if (_dropShadow != null)
                _dropShadow.Color = newValue;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (oldContent != null)
                if (oldContent is FrameworkElement oldElement)
                    oldElement.SizeChanged -= OnSizeChanged;

            if (newContent != null)
                if (newContent is FrameworkElement newElement)
                    newElement.SizeChanged += OnSizeChanged;

            base.OnContentChanged(oldContent, newContent);
        }

        private void OnOffsetXChanged(double newValue)
        {
            if (_dropShadow != null)
                UpdateShadowOffset((float)newValue, _dropShadow.Offset.Y, _dropShadow.Offset.Z);
        }

        private void OnOffsetYChanged(double newValue)
        {
            if (_dropShadow != null)
                UpdateShadowOffset(_dropShadow.Offset.X, (float)newValue, _dropShadow.Offset.Z);
        }

        private void OnOffsetZChanged(double newValue)
        {
            if (_dropShadow != null)
                UpdateShadowOffset(_dropShadow.Offset.X, _dropShadow.Offset.Y, (float)newValue);
        }

        private void OnShadowOpacityChanged(double newValue)
        {
            if (_dropShadow != null)
                _dropShadow.Opacity = (float)newValue;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => UpdateShadowSize();

        private void ConfigureShadowVisualForCastingElement()
        {
            UpdateShadowMask();

            UpdateShadowSize();
        }

        private void UpdateShadowMask()
        {
            if (Content != null)
            {
                CompositionBrush mask = null;

                if (Content is Image)
                    mask = ((Image)Content).GetAlphaMask();
                else if (Content is Shape)
                    mask = ((Shape)Content).GetAlphaMask();
                else if (Content is TextBlock)
                    mask = ((TextBlock)Content).GetAlphaMask();

                _dropShadow.Mask = mask;
            }
            else
                _dropShadow.Mask = null;
        }

        private void UpdateShadowOffset(float x, float y, float z)
        {
            if (_dropShadow != null)
                _dropShadow.Offset = new Vector3(x, y, z);
        }

        private void UpdateShadowSize()
        {
            if (_shadowVisual != null)
            {
                var newSize = new Vector2(0, 0);
                if (Content is FrameworkElement contentFE)
                    newSize = new Vector2((float)contentFE.ActualWidth, (float)contentFE.ActualHeight);

                _shadowVisual.Size = newSize;
            }
        }
    }
}