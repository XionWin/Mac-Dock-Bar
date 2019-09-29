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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DockViewer.Lib
{
    public class DockItem : ContentControl, IAnimation
    {
        public DockItem()
        {
            this.RenderTransformOrigin = new Point(.5, 1);
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform());
            tg.Children.Add(new ScaleTransform());
            tg.Children.Add(new RotateTransform());

            this.RenderTransform = tg;

            
            
        }
        private bool isDownOnSurface = false;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.isDownOnSurface = true;
            this.CaptureMouse();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            this.ReleaseMouseCapture();
            
            //for Click Event
            if (isDownOnSurface)
            {
                if (this.IsMouseOver)
                {
                    OnClick();
                }
            }
            isDownOnSurface = false;
        }

        internal void ChangeVisualState(bool useTransitions)
        {
            if (this.IsSelected)
            {
                VisualStateManager.GoToState(this, "Selected", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "UnSelected", useTransitions);
            }
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DockItem));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(args);
        }


        public static readonly DependencyProperty IsSelectedProperty =
      DependencyProperty.Register("IsSelected", typeof(bool), typeof(DockItem), new UIPropertyMetadata(false, new PropertyChangedCallback((o, e) => {

      }))
      );

        public bool IsSelected
        {
            get
            {
                return Convert.ToBoolean(GetValue(IsSelectedProperty));
            }
            set
            {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    SetValue(IsSelectedProperty, value);
                    ChangeVisualState(true);
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
                    {
                        System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
                        System.Threading.Thread.Sleep(1);

                        ChangeSelectedStatus(value);
                    }));
                }));
            }
        }

        private Effect.Lib.ParticleSystem particleSystem = null;
        public Effect.Lib.ParticleSystem ParticleSystem
        {
            get
            {
                if (this.particleSystem == null)
                {
                    this.particleSystem = (Effect.Lib.ParticleSystem)this.Template.FindName("smokeParticleSystem", this);
                }
                return this.particleSystem;
            }
        }

        private void ChangeSelectedStatus(bool isSelect)
        {
            if (this.ParticleSystem != null)
            {
                if (isSelect && !this.ParticleSystem.IsRunding)
                {

                    this.ParticleSystem.Start();
                }
                else if(!isSelect && this.ParticleSystem.IsRunding)
                {
                    this.ParticleSystem.Stop();
                }
            }
        }
        
        public new static readonly DependencyProperty FontFamilyProperty;
        public new static readonly DependencyProperty FontWeightProperty;

        static DockItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockItem), new FrameworkPropertyMetadata(typeof(DockItem)));


            FontFamilyProperty = ImageViewer.FontFamilyProperty.AddOwner(typeof(DockItem));

            FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();
            meta.Inherits = true;

            FontFamilyProperty.OverrideMetadata(typeof(DockItem), meta);


            FontWeightProperty = ImageViewer.FontWeightProperty.AddOwner(typeof(DockItem));

            FrameworkPropertyMetadata metaW = new FrameworkPropertyMetadata();
            metaW.Inherits = true;

            FontWeightProperty.OverrideMetadata(typeof(DockItem), metaW);
        }

        //set item space value
        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DockItem), new PropertyMetadata("123"));
        public string Title
        {
            get
            {
                return GetValue(TitleProperty).ToString();
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }


        internal Point Original
        {
            get;
            set;
        }

        public void AnimationCompleted(object sender, EventArgs e)
        {

        }

        public DoubleAnimation MakeAnimation(double to, double duration)
        {
            return MakeAnimation(to, duration, null);
        }

        public DoubleAnimation MakeAnimation(double to, double duration, EventHandler endEvent)
        {
            DoubleAnimation anim = new DoubleAnimation(to, TimeSpan.FromMilliseconds(duration));
            anim.AccelerationRatio = 0.2;
            anim.DecelerationRatio = 0.7;
            if (endEvent != null)
                anim.Completed += endEvent;
            return anim;
        }



        private FrameworkElement scalepresenter = null;
        private FrameworkElement ScalePresenter
        {
            get
            {
                if (this.scalepresenter == null)
                {
                    ControlTemplate template = this.Template;
                    this.scalepresenter = (FrameworkElement)template.FindName("scalePresenter", this);

                    this.scalepresenter.RenderTransformOrigin = new Point(.5, 1);
                    TransformGroup tgS = new TransformGroup();
                    tgS.Children.Add(new TranslateTransform());
                    tgS.Children.Add(new ScaleTransform());
                    tgS.Children.Add(new RotateTransform());

                    this.scalepresenter.RenderTransform = tgS;
                }
                return this.scalepresenter;
            }
        }

        public void AnimateTo(double x, double y, double sx, double sy, double r, double duration)
        {
            TransformGroup group = (TransformGroup)this.RenderTransform;
            TranslateTransform trans = (TranslateTransform)group.Children[0];
            //ScaleTransform scale = (ScaleTransform)group.Children[1];
            //RotateTransform rot = (RotateTransform)group.Children[2];

            TransformGroup groupS = (TransformGroup)this.ScalePresenter.RenderTransform;
            //TranslateTransform trans = (TranslateTransform)groupS.Children[0];
            ScaleTransform scale = (ScaleTransform)groupS.Children[1];
            RotateTransform rot = (RotateTransform)groupS.Children[2];

            if (duration == 0)
            {
                trans.BeginAnimation(TranslateTransform.XProperty, null);
                trans.BeginAnimation(TranslateTransform.YProperty, null);
                scale.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, null);
                rot.BeginAnimation(RotateTransform.AngleProperty, null);
                trans.X = x;
                trans.Y = y;
                scale.ScaleX = sx;
                scale.ScaleY = sy;
                rot.Angle = r;
                AnimationCompleted(null, null);
            }
            else
            {
                trans.BeginAnimation(TranslateTransform.XProperty, MakeAnimation(x, duration, AnimationCompleted));
                trans.BeginAnimation(TranslateTransform.YProperty, MakeAnimation(y, duration));
                scale.BeginAnimation(ScaleTransform.ScaleXProperty, MakeAnimation(sx, duration));
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, MakeAnimation(sy, duration));
                rot.BeginAnimation(RotateTransform.AngleProperty, MakeAnimation(r, duration));
            }
        }
    }
}
