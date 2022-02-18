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
	/// Логика взаимодействия для Menu.xaml
	/// </summary>
	public partial class Menu : Window
	{
		public Menu()
		{
			InitializeComponent();
		}
		private void RandomGame_Click(object sender, RoutedEventArgs e)
		{
			StartGame(Mode.Random,false);
		}
		private void GameWindow_Closed(object sender, EventArgs e)
		{
			(sender as GameWindow).Closed -= GameWindow_Closed;
			GC.SuppressFinalize(sender);
			this.Visibility = Visibility.Visible;
		}
		public void StartGame(Mode mode, bool InputWord = false)
		{
			GameWindow gameWindow = null;
			if (InputWord)
			{
				InputWordDialog inputWordDialog = new InputWordDialog();
				if ((bool)inputWordDialog.ShowDialog()) gameWindow = new GameWindow(inputWordDialog.Word);
			}
			else gameWindow = new GameWindow();
			if (gameWindow != null)
			{
				if (gameWindow.IsError==false)
				{
					this.Visibility = Visibility.Collapsed;
					gameWindow.Show();
					gameWindow.Closed += GameWindow_Closed;
				}
			}
		}
		private void StandartGame_Click(object sender, RoutedEventArgs e)
		{
				StartGame(Mode.Standart,true);
		}
	}
}
