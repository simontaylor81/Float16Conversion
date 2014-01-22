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

namespace Float16Conversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sourceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hexStrings = sourceTextBox.Text.Split();

			// Parse to get shorts.
			var rawValues = hexStrings.Select(s =>
				{
					ushort val;
					if (ushort.TryParse(s, System.Globalization.NumberStyles.HexNumber, null, out val))
						return val;
					return (ushort)0;
				});

			// Convert to float16.
			var float16s = rawValues.Select(val =>
				{
					var float16 = new SlimDX.Half();
					float16.RawValue = val;
					return float16;
				});

			// Convert to float32.
			var floats = SlimDX.Half.ConvertToFloat(float16s.ToArray());

			// Convert back to strings.
			destTextBox.Text = string.Join("\n", floats);
        }
    }
}
