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
using System.Windows.Shapes;

namespace GenWords
{
	/// <summary>
	/// Логика взаимодействия для InputWordDialog.xaml
	/// </summary>
	public partial class InputWordDialog : Window
	{
		public string Word{ get; set; }
		public InputWordDialog()
		{
			InitializeComponent();
		}
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}

		private void GoButton_Click(object sender, RoutedEventArgs e)
		{
			Word = WordBox.Text;
			if(string.IsNullOrEmpty(Word))
				DialogResult = false;
			else
				DialogResult = true;
			Close();
		}
	}
}
