using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

[assembly: XmlnsDefinition("http://schemas.xion.com/dockViewer/", "DockViewer.Lib")]
[assembly: XmlnsPrefix("http://schemas.xion.com/dockViewer/", "dv")]
namespace DockViewer.Lib
{
    public partial class ImageViewer : Canvas
    {
        //animation path funtion.
        private Dock.Core.Gauss gauss = new Dock.Core.Gauss();
        public ImageViewer()
        {
            //set functin args
            gauss.Sigma = 128;
            gauss.Swing = 72;
            gauss.Offset = 88;
            
            //binding event handle form location mouse point.
            this.MouseMove += (o, e) =>
            {
                this.Transform(e.GetPosition(this));
            };

            this.MouseLeave += (o, e) =>
            {
                this.Reset();
            };

        }

        static ImageViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageViewer), new FrameworkPropertyMetadata(typeof(ImageViewer)));

        }
        
        #region DP

        public static DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(ImageViewer), new FrameworkPropertyMetadata(default(FontFamily), FrameworkPropertyMetadataOptions.Inherits));
        public FontFamily FontFamily
        {
            get
            {
                return (FontFamily)GetValue(FontFamilyProperty);
            }
            set
            {
                SetValue(FontFamilyProperty, value);
            }
        }
        public static DependencyProperty FontWeightProperty = DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(ImageViewer), new FrameworkPropertyMetadata(System.Windows.FontWeights.Normal, FrameworkPropertyMetadataOptions.Inherits));
        public FontWeight FontWeight
        {
            get
            {
                return (FontWeight)GetValue(FontWeightProperty);
            }
            set
            {
                SetValue(FontWeightProperty, value);
            }
        }

        public static DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(ImageViewer), new PropertyMetadata(default(Thickness)));
        public Thickness Padding
        {
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }


        public static DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(DockItem), typeof(ImageViewer), new PropertyMetadata(null));
        public DockItem SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty) as DockItem;
            }
            set
            {
                SetValue(SelectedItemProperty, value);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    SelectNewItem(value);
                }));
            }
        }

        public static DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ImageViewer), new PropertyMetadata(-1));
        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                if (this.SelectedIndex != value)
                {
                    SetValue(SelectedIndexProperty, value);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        SelectNewIndex(value);
                    }));
                }

            }
        }


        //set item space value
        public static DependencyProperty SpaceProperty = DependencyProperty.Register("Space", typeof(int), typeof(ImageViewer), new PropertyMetadata(0, new PropertyChangedCallback((o, e) => {

            (o as ImageViewer).InvalidateArrange();
        })
            )
            );
        public int Space
        {
            get
            {
                return Convert.ToInt32(GetValue(SpaceProperty));
            }
            set
            {
                SetValue(SpaceProperty, value);
            }
        }

        //set item's title shown location.
        public static DependencyProperty PopupDeviationProperty = DependencyProperty.Register("PopupDeviation", typeof(Point), typeof(ImageViewer), new PropertyMetadata(default(Point)));
        public Point PopupDeviation
        {
            get
            {
                return (Point)GetValue(PopupDeviationProperty);
            }
            set
            {
                SetValue(PopupDeviationProperty, value);
            }
        }

        #endregion DP

        #region RoutedEvent

        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectionChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ImageViewer));

        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        protected virtual void OnSelectionChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(SelectionChangedEvent, this);
            RaiseEvent(args);
        }
        #endregion RoutedEvent
        
        #region override

        protected override Size MeasureOverride(Size constraint)
        {
            foreach (FrameworkElement item in this.Children)
            {
                item.Width = item.Height = this.gauss.Offset;

                item.Measure(constraint);
            }
            return constraint; // base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            double needsWidth = 0;
            foreach (DockItem item in this.Children)
            {
                if (needsWidth <= 0)
                    needsWidth += item.DesiredSize.Width;
                else
                    needsWidth += this.Space + item.DesiredSize.Width;
            }

            double left = (this.DesiredSize.Width - needsWidth) / 2;


            foreach (DockItem item in this.Children)
            {
                Point topLeft = new Point(left, this.DesiredSize.Height - item.DesiredSize.Height + Padding.Top - Padding.Bottom);
                item.Original = new Point(topLeft.X + item.DesiredSize.Width / 2, topLeft.Y - item.DesiredSize.Height / 2);
                item.Arrange(new Rect(topLeft, item.DesiredSize));

                left += this.Space + item.DesiredSize.Width;
                //regist click event handle
                item.Click += Item_Click;
            }
            return arrangeBounds; // base.ArrangeOverride(arrangeBounds);
        }
        #endregion override
        
        #region Public Function 
        public bool CanMoveLeft()
        {
            if (this.SelectedIndex >= 0 + 1)
            {
                return true;
            }
            return false;
        }

        public void MoveLeft()
        {
            this.SelectedIndex--;
        }


        public bool CanMoveRight()
        {
            if (this.SelectedIndex <= this.Children.Count - 1 - 1)
            {
                return true;
            }
            return false;
        }

        public void MoveRight()
        {
            this.SelectedIndex++;
        }
        #endregion Public Function 
    }


    public partial class ImageViewer
    {

        #region animation  
        private bool wasMouseOver = false;
        public void Transform(Point p)
        {
            double duration = 0;
            if (wasMouseOver != this.IsMouseOver)
                duration = 100d;

            this.gauss.Phase = p.X;
            double ps2Max = this.gauss.IntegrateGauss(this.gauss.Phase, int.MaxValue);

            foreach (DockItem item in this.Children)
            {
                item.Dispatcher.BeginInvoke(new Action(() => {

                    double x = this.gauss.IntegrateGauss(this.gauss.Phase, item.Original.X);
                    double nx = (x / ps2Max) * this.gauss.Offset;

                    double s = (1 - Math.Abs(this.gauss.IntegrateGauss(this.gauss.Phase, item.Original.X) / ps2Max)) * 1 + 1;

                    item.AnimateTo(nx, 0, s, s, 0, duration);

                }));
            }

            wasMouseOver = this.IsMouseOver;
        }

        public void Reset()
        {
            double duration = 0;
            if (wasMouseOver != this.IsMouseOver)
                duration = 200d;
            foreach (DockItem item in this.Children)
            {
                item.Dispatcher.BeginInvoke(new Action(() =>
                {
                    item.AnimateTo(0, 0, 1, 1, 0, duration);
                }));
            }

            wasMouseOver = this.IsMouseOver;
        }
        #endregion animation

        #region Private function


        private void Item_Click(object sender, RoutedEventArgs e)
        {
            DockItem newSelected = sender as DockItem;
            SelectNewItem(newSelected);
        }

        private void SelectNewItem(DockItem item)
        {

            if (item == this.SelectedItem || item == null)
            {
                return;
            }

            if (this.Children.Contains(item))
            {
                if (this.SelectedItem != null)
                {
                    this.SelectedItem.IsSelected = false;
                }
                item.IsSelected = true;

                this.SelectedItem = item;
                this.SelectedIndex = this.Children.IndexOf(item);
                OnSelectionChanged();
            }
        }

        private void SelectNewIndex(int index)
        {
            if (index >= this.Children.Count || index < 0)
            {
                return;
            }

            DockItem item = this.Children[index] as DockItem;
            SelectNewItem(item);
        }
        #endregion private function 
    }
}
