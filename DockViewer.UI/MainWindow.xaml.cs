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

namespace DockViewer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.imageViewer.MouseMove += (o, e) =>
            {
                this.testViewer.Dispatcher.BeginInvoke(new Action(() => {
                    this.testViewer.RenderView(e.GetPosition(this.imageViewer));
                }));
            };

            this.imageViewer.MouseLeave += (o, e) =>
            {
                this.testViewer.Dispatcher.BeginInvoke(new Action(() => {
                    this.testViewer.ReSetView();
                }));
            };

            this.imageViewer.SelectionChanged += (o, e) =>
             {
                 Lib.ImageViewer imageViewer = o as Lib.ImageViewer;
                 this.Title = "Selected Item : " + imageViewer.SelectedItem.Title + ", selected index: " + imageViewer.SelectedIndex.ToString();
             };
        }
        

        private void CommandBinding_CanExecuted_MoveLeft(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.imageViewer.CanMoveLeft();
        }

        private void CommandBinding_Executed_MoveLeft(object sender, ExecutedRoutedEventArgs e)
        {
            this.imageViewer.MoveLeft();
        }

        private void CommandBinding_CanExecuted_MoveRight(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.imageViewer.CanMoveRight();
        }

        private void CommandBinding_Executed_MoveRight(object sender, ExecutedRoutedEventArgs e)
        {
            this.imageViewer.MoveRight();
        }
    }
}
