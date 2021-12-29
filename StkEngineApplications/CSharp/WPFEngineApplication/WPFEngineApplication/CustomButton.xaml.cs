using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace WPFEngineApplication
{
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl
    {

        public static readonly DependencyProperty IsConnectedProperty =
           DependencyProperty.Register("IsConnected", typeof(Boolean), typeof(CustomButton), 
                                       new PropertyMetadata(false, ChangeConnectedColor));

        DependencyPropertyDescriptor dpd;


        public CustomButtonType ButtonType { get; set; }
        public string Orientation { get; set; }
        public bool m_mouseDown;
        private bool m_connected;
        public bool IsConnected
        {
            set
            {
                  ChangeConnectedColor1(value);
            }
            get
            {
                return false;
            }
        }
        public string ToolTipText
        {
            set
            {
                this.ToolTip = value;
            }
        }

        public CustomButton() : base()
        {
            this.InitializeComponent();
            m_connected = false;
            this.MouseUp += new MouseButtonEventHandler(CustomButton_MouseUp);
            this.MouseDown += new MouseButtonEventHandler(CustomButton_MouseDown);
            this.MouseLeave += new MouseEventHandler(CustomButton_MouseLeave);

        }

        void CustomButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Background3.Fill = (Brush)this.FindResource("OnMouseBackground1");
            if (e.LeftButton == MouseButtonState.Released)
                if (Clicked != null)
                    this.Clicked(sender, e);
        }

        void CustomButton_MouseLeave(object sender, MouseEventArgs e)
        {
            m_mouseDown = false;
        }

        void CustomButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Background3.Fill = new SolidColorBrush(Color.FromArgb(255,255,255,255));
            m_mouseDown = true;
        }

        

        public event MouseEventHandler Clicked;

        private static void ChangeConnectedColor(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            CustomButton button = (CustomButton)obj;
            button.IsConnected = (bool)args.NewValue;
        //    SetIsConnected(obj, (bool)args.NewValue);  
        }

        private void ChangeConnectedColor1(bool green)
        {
            Color color = new Color();

            if (green)
            {
                color = Color.FromRgb(0, 255, 0);

            }
            else
            {
                color = Color.FromRgb(255, 0, 0);
            }
            Brush brush = new SolidColorBrush(color);
            Connection.Fill = brush;
            Connection.Stroke = brush;

        }

        private void SetUpIcons()
        {
            RotateTransform transform = new RotateTransform(0);

            //            RenderTransformOrigin="0.5,0.5"
            if (Orientation != null)
            {
                switch (Orientation)
                {
                    case "Top": transform.Angle = -90;
                        transform.CenterX = 0.5;
                        transform.CenterY = 0.5;
                        break;
                    case "Right": transform.Angle = 0;
                        break;
                    case "Bottom": transform.Angle = 90;
                        transform.CenterX = 0.5;
                        transform.CenterY = 0.5;
                        break;
                    case "Left": transform.Angle = 180;
                        break;
                    default:
                        break;
                }
            }
            switch (ButtonType)
            {
                case CustomButtonType.Zoom : Zoom1.Visibility = Visibility.Visible;
                    Zoom2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.ZoomTo: Zoom1.Visibility = Visibility.Visible;
                    Zoom2.Visibility = Visibility.Visible;
                    Zoom3.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Globe: Globe.Visibility = Visibility.Visible;
                    
                    break;
                case CustomButtonType.Terrain: Terrain1.Visibility = Visibility.Visible;
                    Terrain2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.RequestAlerts: RequestAlerts.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.OverlayMap: Overlay1.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Pan: Pan.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Play: Play.Visibility = Visibility.Visible;
                    Light_Play.Visibility = Visibility.Visible;
                    transform.CenterX = transform.CenterX + Play.RenderSize.Height / 2;
                    transform.CenterY = transform.CenterY + Play.RenderSize.Width / 2;
                    Play.RenderTransform = transform;
                    break;
                case CustomButtonType.Info: Info.Visibility = Visibility.Visible;
                    Light_Info1.Visibility = Visibility.Visible;
                    Light_Info2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Top: Top1.Visibility = Visibility.Visible;
                    Top2.Visibility = Visibility.Visible;
                    Light_Top1.Visibility = Visibility.Visible;
                    Light_Top2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Down: Down.Visibility = Visibility.Visible;
                    Light_Down.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Right: Right.Visibility = Visibility.Visible;
                    Light_Right.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Left: Left.Visibility = Visibility.Visible;
                    Light_Left.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Bottom: Bottom1.Visibility = Visibility.Visible;
                    Bottom2.Visibility = Visibility.Visible;
                    Light_Bottom1.Visibility = Visibility.Visible;
                    Light_Bottom2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Up: Up.Visibility = Visibility.Visible;
                    Light_Up.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Pause: Pause1.Visibility = Visibility.Visible;
                    Pause2.Visibility = Visibility.Visible;
                    Light_Pause1.Visibility = Visibility.Visible;
                    Light_Pause2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Stop: Stop.Visibility = Visibility.Visible;
                    Light_Stop.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Reverse: Reverse.Visibility = Visibility.Visible;
                    Light_Reverse.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.StepBack: StepBack1.Visibility = Visibility.Visible;
                    StepBack2.Visibility = Visibility.Visible;
                    Light_StepBack1.Visibility = Visibility.Visible;
                    Light_StepBack2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.StepForward: StepForward1.Visibility = Visibility.Visible;
                    StepForward2.Visibility = Visibility.Visible;
                    Light_StepForward1.Visibility = Visibility.Visible;
                    Light_StepForward2.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.SpeedUp: SpeedUp.Visibility = Visibility.Visible;
                    Light_SpeedUp.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.SlowDown: SlowDown.Visibility = Visibility.Visible;
                    Light_SlowDown.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.FreeView: Camera1.Visibility = Visibility.Visible;
                    Camera2.Visibility = Visibility.Visible;
                    Free.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Connect: Connect.Visibility = Visibility.Visible;
                    ConnectStatus.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Options: Options.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Data: Data.Visibility = Visibility.Visible;
                    Catalog.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Event: Event.Visibility = Visibility.Visible;
                    Catalog.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Filter: Filter.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Connection: Connection.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Folder: Folder.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Home: House.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.AGI: AGI.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Delete: Delete.Visibility = Visibility.Visible;
                    break;
                case CustomButtonType.Record: Record1.Visibility = Visibility.Visible;
                               Record2.Visibility = Visibility.Visible;
                               DoubleAnimation animation = new DoubleAnimation();
                               animation.From = 0.0;
                               animation.To = 0.4;
                               animation.AutoReverse = true;
                               animation.RepeatBehavior = RepeatBehavior.Forever;
                               animation.Duration = new Duration(TimeSpan.Parse("0:0:01"));
                               Record2.BeginAnimation(Ellipse.OpacityProperty, animation);
                               break;
                case CustomButtonType.None: Background1.Visibility = Visibility.Collapsed;
                    Background2.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }


        }

        public void TogglePlayPause()
        {
            if (Play.Visibility == Visibility.Visible)
            {
                Pause1.Visibility = Visibility.Visible;
                Pause2.Visibility = Visibility.Visible;
                Light_Pause1.Visibility = Visibility.Visible;
                Light_Pause2.Visibility = Visibility.Visible;

                Play.Visibility = Visibility.Hidden;
                Light_Play.Visibility = Visibility.Hidden;
            }
            else
            {
                Pause1.Visibility = Visibility.Hidden;
                Pause2.Visibility = Visibility.Hidden;
                Light_Pause1.Visibility = Visibility.Hidden;
                Light_Pause2.Visibility = Visibility.Hidden;

                Play.Visibility = Visibility.Visible;
                Light_Play.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpIcons();
        }
    }

    public enum CustomButtonType
    {
        Zoom,
        ZoomTo,
        Globe,
        Terrain,
        RequestAlerts,
        OverlayMap,
        Pan,
        Play,
        Info,
        Top,
        Down,
        Right,
        Left,
        Bottom,
        Up,
        Pause,
        Stop,
        Reverse,
        StepBack,
        StepForward,
        SpeedUp,
        SlowDown,
        FreeView,
        Connect,
        Options,
        Data,
        Event,
        Filter,
        Connection,
        Record,
        Folder,
        Home,
        AGI,
        Delete,
        None
    }
}
