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
                CaseBorderPen, // ���� ��� ��������� ������� (��� ������� �� ����� � ������)
                _caseRect // �������������, � ������ ������ �������� ������� ����� ����� � ���������
            );
        }

    }
}
