using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace TrafficLights.Controls
{
    public partial class TrafficLigthsControl : UserControl
    {

        #region Settings

        /// <summary>
        /// Border width
        /// </summary>
        private const int BorderWidth = 5;

        /// <summary>
        /// Border color
        /// </summary>
        private static readonly Color BorderColor = Colors.Black;

        /// <summary>
        /// Case color
        /// </summary>
        private static readonly Color CaseColor = new Color(255, 40, 40, 40);


        #endregion

        #region Readonly stuff

        /// <summary>
        /// Brush to fill traffic lights case
        /// </summary>
        private static readonly IBrush CaseFillBrush = new SolidColorBrush(CaseColor);

        private static readonly IBrush BorderBrush = new SolidColorBrush(BorderColor);

        private static readonly IPen CaseBorderPen = new Pen(BorderBrush, BorderWidth);

        #endregion

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
        /// Ширина контрола в пикселях
        /// </summary>
        private int _width;

        /// <summary>
        /// Высота контрола в пикселях
        /// </summary>
        private int _height;

        /// <summary>
        /// Прямоугольник, описывающий корпус светофора
        /// </summary>
        private Rect _caseRect;

        public TrafficLigthsControl()
        {
            InitializeComponent();

            // Подписываемся на изменение свойств контрола
            PropertyChanged += OnPropertyChangedListener;

            IsRedLightOnProperty.Changed.Subscribe(x => HandleRedLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsYellowLightOnProperty.Changed.Subscribe(x => HandleYellowLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsGreenLightOnProperty.Changed.Subscribe(x => HandleGreenLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
        }

        /// <summary>
        /// Этот метод реагирует на изменение состояния красного огня
        /// </summary>
        private void HandleRedLightStateChanged(AvaloniaObject sender, bool isRedLightOn)
        {
            InvalidateVisual(); // Встроенный в авалонию метод "перерисовать содержимое контрола"
        }

        /// <summary>
        /// Этот метод реагирует на изменение состояния жёлтого огня
        /// </summary>
        private void HandleYellowLightStateChanged(AvaloniaObject sender, bool isYellowLightOn)
        {
            InvalidateVisual(); // Встроенный в авалонию метод "перерисовать содержимое контрола"
        }

        /// <summary>
        /// Этот метод реагирует на изменение состояния зелёного огня
        /// </summary>
        private void HandleGreenLightStateChanged(AvaloniaObject sender, bool isGreenLightOn)
        {
            InvalidateVisual(); // Встроенный в авалонию метод "перерисовать содержимое контрола"
        }

        /// <summary>
        /// Этот метод вызывается при изменении размеров контрола
        /// </summary>
        /// <param name="bounds">Прямоугольник, соответствующий контролу. 0, 0 - верхний левый угол контрола</param>
        private void OnResize(Rect bounds)
        {
            _width = (int)bounds.Width;
            _height = (int)bounds.Height;

            _caseRect = new Rect(0, 0, _width, _height);
        }

        /// <summary>
        /// Этот метод вызывается когда меняется какое-либо ваще свойство контрола
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // Если меняется свойство Bounds (границы контрола)
            {
                // То вызвать OnResize() с новыми границами
                OnResize((Rect)e.NewValue);
            }
        }

        /// <summary>
        /// Переопределяем метод рисования, пришедший нам от предка
        /// </summary>
        /// <param name="context">Холст, на котором мы будем рисовать</param>
        public override void Render(DrawingContext context)
        {
            base.Render(context); // Вызов метода отрисовки предка (рисует фон, границы и т.п.)

            // Рисуем корпус светофора
            context.DrawRectangle
            (
                CaseFillBrush, // Кисть для заливки
                CaseBorderPen, // Перо для рисования границы (оно состоит из кисти и ширины)
                _caseRect // Прямоугольник, в данном случае заданный верхним левым углом и размерами
            );
        }

    }
}
