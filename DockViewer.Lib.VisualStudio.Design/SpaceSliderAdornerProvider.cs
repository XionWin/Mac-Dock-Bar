using Microsoft.Windows.Design.Interaction;
using Microsoft.Windows.Design.Model;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DockViewer.Lib.VisualStudio.Design
{
    public class SpaceSliderAdornerProvider : PrimarySelectionAdornerProvider
    {

        private ModelItem adornedControlModel;
        private ModelEditingScope batchedChange;
        private Slider spaceSlider;
        private AdornerPanel opacitySliderAdornerPanel;

        public SpaceSliderAdornerProvider()
        {
            spaceSlider = new Slider();
        }


        // The Panel utility property demand-creates the  
        // adorner panel and adds it to the provider's  
        // Adorners collection. 
        public AdornerPanel Panel
        {
            get
            {
                if (this.opacitySliderAdornerPanel == null)
                {
                    opacitySliderAdornerPanel = new AdornerPanel();

                    opacitySliderAdornerPanel.Children.Add(spaceSlider);

                    // Add the panel to the Adorners collection.
                    Adorners.Add(opacitySliderAdornerPanel);
                }

                return this.opacitySliderAdornerPanel;
            }
        }


        protected override void Activate(ModelItem item)
        {
            // Save the ModelItem and hook into when it changes. 
            // This enables updating the slider position when  
            // a new Background value is set.
            adornedControlModel = item;
            adornedControlModel.PropertyChanged +=
                new System.ComponentModel.PropertyChangedEventHandler(
                    AdornedControlModel_PropertyChanged);

            // Setup the slider's min and max values.
            spaceSlider.Minimum = -20;
            spaceSlider.Maximum = 20;
            spaceSlider.TickFrequency = 1;
            spaceSlider.IsSnapToTickEnabled = true;

            spaceSlider.Background = new SolidColorBrush(Colors.Green);
            spaceSlider.Foreground = new SolidColorBrush(Colors.Red);

            // Setup the adorner panel. 
            // All adorners are placed in an AdornerPanel 
            // for sizing and layout support.
            AdornerPanel myPanel = this.Panel;

            AdornerPanel.SetHorizontalStretch(spaceSlider, AdornerStretch.Stretch);
            AdornerPanel.SetVerticalStretch(spaceSlider, AdornerStretch.None);

            AdornerPlacementCollection placement = new AdornerPlacementCollection();

            // The adorner's width is relative to the content. 
            // The slider extends the full width of the control it adorns.
            placement.SizeRelativeToContentWidth(1.0, 0);

            // The adorner's height is the same as the slider's.
            placement.SizeRelativeToAdornerDesiredHeight(1.0, 0);

            // Position the adorner above the control it adorns.
            placement.PositionRelativeToAdornerHeight(-2.0, 0);

            // Position the adorner up 5 pixels. This demonstrates  
            // that these placement calls are additive. These two calls 
            // are equivalent to the following single call: 
            // PositionRelativeToAdornerHeight(-1.0, -5).
            placement.PositionRelativeToAdornerHeight(0, -5);

            AdornerPanel.SetPlacements(spaceSlider, placement);

            // Initialize the slider when it is loaded.
            spaceSlider.Loaded += new RoutedEventHandler(slider_Loaded);

            // Handle the value changes of the slider control.
            spaceSlider.ValueChanged +=
                new RoutedPropertyChangedEventHandler<double>(
                    slider_ValueChanged);

            spaceSlider.PreviewMouseLeftButtonUp +=
                new System.Windows.Input.MouseButtonEventHandler(
                    slider_MouseLeftButtonUp);

            spaceSlider.PreviewMouseLeftButtonDown +=
                new System.Windows.Input.MouseButtonEventHandler(
                    slider_MouseLeftButtonDown);
            base.Activate(item);
        }

        // The following method deactivates the adorner. 
        protected override void Deactivate()
        {
            adornedControlModel.PropertyChanged -=
                new System.ComponentModel.PropertyChangedEventHandler(
                    AdornedControlModel_PropertyChanged);
            base.Deactivate();
        }

        // The following method handles the PropertyChanged event. 
        // It updates the slider control's value if the adorned control's  
        // Background property changed, 
        void AdornedControlModel_PropertyChanged(
            object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Space")
            {
                spaceSlider.Value = GetCurrentSpace();
            }
        }

        // The following method handles the Loaded event. 
        // It assigns the slider control's initial value. 
        void slider_Loaded(object sender, RoutedEventArgs e)
        {
            spaceSlider.Value = GetCurrentSpace();
        }

        // The following method handles the MouseLeftButtonDown event. 
        // It calls the BeginEdit method on the ModelItem which represents  
        // the adorned control. 
        void slider_MouseLeftButtonDown(
            object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            batchedChange = adornedControlModel.BeginEdit();
        }

        // The following method handles the MouseLeftButtonUp event. 
        // It commits any changes made to the ModelItem which represents the 
        // the adorned control. 
        void slider_MouseLeftButtonUp(
            object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            if (batchedChange != null)
            {
                batchedChange.Complete();
                batchedChange.Dispose();
                batchedChange = null;
            }
        }

        // The following method handles the slider control's  
        // ValueChanged event. It sets the value of the  
        // Background opacity by using the ModelProperty type. 
        void slider_ValueChanged(
            object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            int newSpaceValue = Convert.ToInt32(e.NewValue);

            // During setup, don't make a value local and set the opacity. 
            if (newSpaceValue == GetCurrentSpace())
            {
                return;
            }
            
            adornedControlModel.Properties["Space"].SetValue(newSpaceValue);
        }

        // This utility method gets the adorned control's 
        // Background brush by using the ModelItem. 
        private int GetCurrentSpace()
        {
            return Convert.ToInt32(adornedControlModel.Properties["Space"].ComputedValue);
        }
    }
}
