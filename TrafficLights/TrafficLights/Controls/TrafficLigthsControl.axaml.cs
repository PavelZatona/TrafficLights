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
        /// ������ �������� � ��������
        /// </summary>
        private int _width;

        /// <summary>
        /// ������ �������� � ��������
        /// </summary>
        private int _height;

        /// <summary>
        /// �������������, ����������� ������ ���������
        /// </summary>
        private Rect _caseRect;

        /// <summary>
        /// ����� ��������� �� �����������
        /// </summary>
        private double _xCenter;

        /// <summary>
        /// ������ ����
        /// </summary>
        private double _rLight;

        /// <summary>
        /// ����� �������� ����
        /// </summary>
        private Point _redLigthCenter;

        /// <summary>
        /// ����� ������ ����
        /// </summary>
        private Point _yellowLigthCenter;

        /// <summary>
        /// ����� ������� ����
        /// </summary>
        private Point _greenLigthCenter;

        public TrafficLigthsControl()
        {
            InitializeComponent();

            // ������������� �� ��������� ������� ��������
            PropertyChanged += OnPropertyChangedListener;

            IsRedLightOnProperty.Changed.Subscribe(x => HandleRedLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsYellowLightOnProperty.Changed.Subscribe(x => HandleYellowLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            IsGreenLightOnProperty.Changed.Subscribe(x => HandleGreenLightStateChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
        }

        /// <summary>
        /// ���� ����� ��������� �� ��������� ��������� �������� ����
        /// </summary>
        private void HandleRedLightStateChanged(AvaloniaObject sender, bool isRedLightOn)
        {
            InvalidateVisual(); // ���������� � �������� ����� "������������ ���������� ��������"
        }

        /// <summary>
        /// ���� ����� ��������� �� ��������� ��������� ������ ����
        /// </summary>
        private void HandleYellowLightStateChanged(AvaloniaObject sender, bool isYellowLightOn)
        {
            InvalidateVisual(); // ���������� � �������� ����� "������������ ���������� ��������"
        }

        /// <summary>
        /// ���� ����� ��������� �� ��������� ��������� ������� ����
        /// </summary>
        private void HandleGreenLightStateChanged(AvaloniaObject sender, bool isGreenLightOn)
        {
            InvalidateVisual(); // ���������� � �������� ����� "������������ ���������� ��������"
        }

        /// <summary>
        /// ���� ����� ���������� ��� ��������� �������� ��������
        /// </summary>
        /// <param name="bounds">�������������, ��������������� ��������. 0, 0 - ������� ����� ���� ��������</param>
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

            // ���������� ����� ������� �����
            _redLigthCenter = new Point(_xCenter, yRed);
            _yellowLigthCenter = new Point(_xCenter, yYellow);
            _greenLigthCenter = new Point(_xCenter, yGreen);

        }

        /// <summary>
        /// ���� ����� ���������� ����� �������� �����-���� ���� �������� ��������
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // ���� �������� �������� Bounds (������� ��������)
            {
                // �� ������� OnResize() � ������ ���������
                OnResize((Rect)e.NewValue);
            }
        }

        /// <summary>
        /// �������������� ����� ���������, ��������� ��� �� ������
        /// </summary>
        /// <param name="context">�����, �� ������� �� ����� ��������</param>
        public override void Render(DrawingContext context)
        {
            base.Render(context); // ����� ������ ��������� ������ (������ ���, ������� � �.�.)

            // ������ ������ ���������
            context.DrawRectangle
            (
                CaseFillBrush, // ����� ��� �������
                BordersPen, // ���� ��� ��������� ������� (��� ������� �� ����� � ������)
                _caseRect // �������������, � ������ ������ �������� ������� ����� ����� � ���������
            );

            // ������ ������� �����
            DrawLight(context, IsRedLightOn ? RedLightOnBrush : LightOffBrush, _redLigthCenter);

            // ������ ����� �����
            DrawLight(context, IsYellowLightOn ? YellowLightOnBrush : LightOffBrush, _yellowLigthCenter);

            // ������ ������ �����
            DrawLight(context, IsGreenLightOn ? GreenLightOnBrush : LightOffBrush, _greenLigthCenter);
        }

        /// <summary>
        /// ����� ��� ��������� ����
        /// </summary>
        private void DrawLight(DrawingContext drawingContext, IBrush lightBrush, Point lightCenter)
        {
            for (var y = lightCenter.Y - _rLight + LedsRadius; y <= lightCenter.Y + _rLight/* - LedsRadius*/; y += LedsStep) // �������� �� ��������� ������ ���� � ����� 12
            {
                for (var x = lightCenter.X - _rLight + LedsRadius; x <= lightCenter.X + _rLight/* - LedsRadius*/; x += LedsStep) // �������� �� ������ ����� ������� � ����� 12
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
