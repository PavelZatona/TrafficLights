using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Linq.Expressions;

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

        /// <summary>
        /// Color for off lights
        /// </summary>
        private static readonly Color LightOffColor = Colors.Brown;

        /// <summary>
        /// Color for red light in on state
        /// </summary>
        private static readonly Color RedLightOnColor = Colors.Red;

        /// <summary>
        /// Color for yellow light in on state
        /// </summary>
        private static readonly Color YellowLightOnColor = Colors.Yellow;

        /// <summary>
        /// Color for green light in on state
        /// </summary>
        private static readonly Color GreenLightOnColor = Colors.Green;

        /// <summary>
        /// LEDs radiuses
        /// </summary>
        private const int LedsRadius = 5;

        /// <summary>
        /// Step between LEDs
        /// </summary>
        private const int LedsStep = 12;

        #endregion

        #region Readonly stuff

        /// <summary>
        /// Brush to fill traffic lights case
        /// </summary>
        private static readonly IBrush CaseFillBrush = new SolidColorBrush(CaseColor);

        private static readonly IBrush TrafficLightsBorderBrush = new SolidColorBrush(BorderColor);

        private static readonly IPen BordersPen = new Pen(TrafficLightsBorderBrush, BorderWidth);

        /// <summary>
        /// Invisible pen
        /// </summary>
        private static readonly IPen InvisiblePen = new Pen(new SolidColorBrush(Colors.Transparent), 0);

        /// <summary>
        /// Brush to draw off lights
        /// </summary>
        private static readonly IBrush LightOffBrush = new SolidColorBrush(LightOffColor);

        /// <summary>
        /// Brush to draw red light in on state
        /// </summary>
        private static readonly IBrush RedLightOnBrush = new SolidColorBrush(RedLightOnColor);

        /// <summary>
        /// Brush to draw yellow light in on state
        /// </summary>
        private static readonly IBrush YellowLightOnBrush = new SolidColorBrush(YellowLightOnColor);

        /// <summary>
        /// Brush to draw green light in on state
        /// </summary>
        private static readonly IBrush GreenLightOnBrush = new SolidColorBrush(GreenLightOnColor);

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

        /// <summary>
        /// Центр светофора по горизонтали
        /// </summary>
        private double _xCenter;

        /// <summary>
        /// Радиус огня
        /// </summary>
        private double _rLight;

        /// <summary>
        /// Центр красного огня
        /// </summary>
        private Point _redLigthCenter;

        /// <summary>
        /// Центр жёлтого огня
        /// </summary>
        private Point _yellowLigthCenter;

        /// <summary>
        /// Центр зелёного огня
        /// </summary>
        private Point _greenLigthCenter;

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

            _xCenter = _width / 2.0;

            var yRed = _height / 6.0;
            var yYellow = _height / 2.0;
            var yGreen = _height * 5 / 6.0;

            var r1 = (yGreen - yRed) / 4.0 * 0.9;
            var r2 = _width / 2.0 * 0.9;
            _rLight = Math.Min(r1, r2);

            // Вычисление точек центров огней
            _redLigthCenter = new Point(_xCenter, yRed);
            _yellowLigthCenter = new Point(_xCenter, yYellow);
            _greenLigthCenter = new Point(_xCenter, yGreen);

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
                BordersPen, // Перо для рисования границы (оно состоит из кисти и ширины)
                _caseRect // Прямоугольник, в данном случае заданный верхним левым углом и размерами
            );

            // Рисуем красный огонь
            DrawLight(context, IsRedLightOn ? RedLightOnBrush : LightOffBrush, _redLigthCenter);

            // Рисуем жёлтый огонь
            DrawLight(context, IsYellowLightOn ? YellowLightOnBrush : LightOffBrush, _yellowLigthCenter);

            // Рисуем зелёный огонь
            DrawLight(context, IsGreenLightOn ? GreenLightOnBrush : LightOffBrush, _greenLigthCenter);
        }

        /// <summary>
        /// Метод для рисования огня
        /// </summary>
        private void DrawLight(DrawingContext drawingContext, IBrush lightBrush, Point lightCenter)
        {
            for (var y = lightCenter.Y - _rLight + LedsRadius; y <= lightCenter.Y + _rLight/* - LedsRadius*/; y += LedsStep) // Движение по вертикали сверху вниз с шагом 12
            {
                for (var x = lightCenter.X - _rLight + LedsRadius; x <= lightCenter.X + _rLight/* - LedsRadius*/; x += LedsStep) // Движение по строке слева направо с шагом 12
                {
                    if (Math.Pow((lightCenter.X - x), 2) + Math.Pow((lightCenter.Y - y), 2) <= Math.Pow(_rLight + LedsRadius, 2))
                    {
                        DrawLed(drawingContext, lightBrush, new Point(x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Draw LED
        /// </summary>
        private void DrawLed(DrawingContext drawingContext, IBrush ledBrush, Point ledCenter)
        {
            drawingContext.DrawEllipse
            (
                ledBrush,
                InvisiblePen,
                ledCenter,
                LedsRadius,
                LedsRadius
            );
        }
    }
}
