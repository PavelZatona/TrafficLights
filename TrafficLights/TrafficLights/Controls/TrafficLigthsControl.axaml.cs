using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace TrafficLights.Controls
{
    public partial class TrafficLigthsControl : UserControl
    {
        #region Red light state

        /// <summary>
        /// Read light state is stored here
        /// </summary>
        public static readonly AttachedProperty<bool> IsRedLightOnProperty = AvaloniaProperty.RegisterAttached<TrafficLigthsControl, Interactive, bool>(nameof(IsRedLightOn));

        /// <summary>
        /// Proxy for red light state
        /// </summary>
        public bool IsRedLightOn
        {
            get { return GetValue(IsRedLightOnProperty); }
            set { SetValue(IsRedLightOnProperty, value); }
        }

        #endregion

        #region Yellow light state

        /// <summary>
        /// Yellow light state is stored here
        /// </summary>
        public static readonly AttachedProperty<bool> IsYellowLightOnProperty = AvaloniaProperty.RegisterAttached<TrafficLigthsControl, Interactive, bool>(nameof(IsYellowLightOn));

        /// <summary>
        /// Proxy for Yellow light state
        /// </summary>
        public bool IsYellowLightOn
        {
            get { return GetValue(IsYellowLightOnProperty); }
            set { SetValue(IsYellowLightOnProperty, value); }
        }

        #endregion

        #region Green light state

        /// <summary>
        /// Green light state is stored here
        /// </summary>
        public static readonly AttachedProperty<bool> IsGreenLightOnProperty = AvaloniaProperty.RegisterAttached<TrafficLigthsControl, Interactive, bool>(nameof(IsGreenLightOn));

        /// <summary>
        /// Proxy for Green light state
        /// </summary>
        public bool IsGreenLightOn
        {
            get { return GetValue(IsGreenLightOnProperty); }
            set { SetValue(IsGreenLightOnProperty, value); }
        }

        #endregion

        /// <summary>
        /// Ўирина контрола в пиксел€х
        /// </summary>
        private int _width;

        /// <summary>
        /// ¬ысота контрола в пиксел€х
        /// </summary>
        private int _height;

        public TrafficLigthsControl()
        {
            InitializeComponent();

            // ѕодписываемс€ на изменение свойств контрола
            PropertyChanged += OnPropertyChangedListener;

            IsRedLightOnProperty.Changed.Subscribe(x => HandleRedLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsYellowLightOnProperty.Changed.Subscribe(x => HandleYellowLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsGreenLightOnProperty.Changed.Subscribe(x => HandleGreenLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
        }

        /// <summary>
        /// Ётот метод реагирует на изменение состо€ни€ красного огн€
        /// </summary>
        private void HandleRedLightStateChanged(AvaloniaObject sender, bool isRedLightOn)
        {
            InvalidateVisual(); // ¬строенный в авалонию метод "перерисовать содержимое контрола"
        }

        /// <summary>
        /// Ётот метод реагирует на изменение состо€ни€ жЄлтого огн€
        /// </summary>
        private void HandleYellowLightStateChanged(AvaloniaObject sender, bool isYellowLightOn)
        {
            InvalidateVisual(); // ¬строенный в авалонию метод "перерисовать содержимое контрола"
        }

        /// <summary>
        /// Ётот метод реагирует на изменение состо€ни€ зелЄного огн€
        /// </summary>
        private void HandleGreenLightStateChanged(AvaloniaObject sender, bool isGreenLightOn)
        {
            InvalidateVisual(); // ¬строенный в авалонию метод "перерисовать содержимое контрола"
        }

        /// <summary>
        /// Ётот метод вызываетс€ при изменении размеров контрола
        /// </summary>
        /// <param name="bounds">ѕр€моугольник, соответствующий контролу. 0, 0 - верхний левый угол контрола</param>
        private void OnResize(Rect bounds)
        {
            _width = (int)bounds.Width;
            _height = (int)bounds.Height;
        }

        /// <summary>
        /// Ётот метод вызываетс€ когда мен€етс€ какое-либо ваще свойство контрола
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // ≈сли мен€етс€ свойство Bounds (границы контрола)
            {
                // “о вызвать OnResize() с новыми границами
                OnResize((Rect)e.NewValue);
            }
        }
    }
}
