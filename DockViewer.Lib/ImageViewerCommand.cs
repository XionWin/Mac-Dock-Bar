using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DockViewer.Lib
{
    public class MoveLeftCommand : RoutedUICommand
    {
        public event EventHandler CanExecuteChanged;

        public MoveLeftCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            ImageViewer imageViewer = parameter as ImageViewer;

            return imageViewer.CanMoveLeft();
        }

        public void Execute(object parameter)
        {
            ImageViewer imageViewer = parameter as ImageViewer;

            imageViewer.MoveLeft();
        }
    }
    public class MoveRightCommand : RoutedUICommand
    {
        public event EventHandler CanExecuteChanged;

        public MoveRightCommand()
        {

        }


        public bool CanExecute(object parameter)
        {
            ImageViewer imageViewer = parameter as ImageViewer;

            return imageViewer.CanMoveRight();
        }

        public void Execute(object parameter)
        {
            ImageViewer imageViewer = parameter as ImageViewer;

            imageViewer.MoveRight();
        }
    }
}
