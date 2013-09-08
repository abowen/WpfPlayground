using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class UniformGridViewModel : PropertyChangedBase
    {

        public UniformGridViewModel()
        {
            var grades = new byte[] { 0, 63, 127, 191, 255 };
            var items = from redGrade in grades
                        from greenGrade in grades
                        from blueGrade in grades
                        select Color.FromRgb(redGrade, greenGrade, blueGrade);

            Rows = 25;
            Columns = 5;
        }

        private int _rows;

        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                NotifyOfPropertyChange(() => Rows);
            }
        }

        private int _columns;

        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                NotifyOfPropertyChange(() => Columns);
            }
        }

        private IEnumerable<Color> _colors;
        public IEnumerable<Color> Colors
        {
            get { return _colors; }
            set { _colors = value; NotifyOfPropertyChange(() => Colors); }
        }


    }
}
