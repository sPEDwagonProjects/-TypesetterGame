using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	/// Логика взаимодействия для GameWindow.xaml
	/// </summary>
	
	public enum Mode
	{ 
			Random,
			Standart,
	}
	public partial class GameWindow : Window
	{
		string OriginalWord = "";
		List<string> Data;
		int LevelLength = 3;
		int LevelGame = 1;
		public bool IsError { get; set; } = false;
		private Mode GameMode{ get; set; } 
		public GameWindow()
		{
			InitializeComponent();
			GameMode = Mode.Random;
			InitGame();
		}
		public GameWindow(string Word)
		{
			OriginalWord = Word.ToUpper();
			GameMode = Mode.Standart;
			InitializeComponent();
			NewWordButton.Visibility = Visibility.Collapsed;
			LevelShow.Visibility = Visibility.Collapsed;
			InitGame();
			
		}
		private void InitGame()
		{
			try
			{
				if (GameMode == Mode.Random)
				{
					LevelShow.Text = LevelGame.ToString();
					string LevelPath = "length" + LevelLength.ToString() + ".txt";
					OriginalWord = WordHelp.GetRandomWord(LevelPath).ToUpper();
				}
				else
				{
					ContinueGameButton.Visibility = Visibility.Collapsed;
				}
				Data = WordHelp.Combine(OriginalWord);
				OriginalWordList.Items.Clear();
				ResWordList.Items.Clear();
				Update();
				NewWordButton.IsEnabled = true;
				media.Play();												 
			}
			catch(Exception)
			{
				MessageBox.Show("Возникла проблема при составлении слов\nПопробуйте другое слово");
				IsError = true;
				Close();
			}
		}
		private void Update()
		{
			OriginalWordList.Items.Clear();
			for (int i = 0; i < OriginalWord.Length; i++) OriginalWordList.Items.Add(OriginalWord[i]);
			WordsCountTextBlock.Text = $"Количество слов: {Data.Count.ToString()}";
			if (Data.Count == 0) WinPanel.Visibility = Visibility.Visible;
		}
		private void OriginalItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				var item = sender as ListViewItem;
				if (item != null && item.IsSelected)
				{
					ResultText.Text = string.Empty;
					char selectedSymbol = (char)item.Content;
					ResWordList.Items.Add(selectedSymbol);
					OriginalWordList.Items.Remove(item.Content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
		private void ResItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			/*
			try
			{
				var item = sender as ListViewItem;
				if (item != null && item.IsSelected)
				{
					char? selectedSymbol = (char)item.Content;
					OriginalWordList.Items.Add(selectedSymbol);
					ResWordList.Items.Remove(item.Content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			*/
		}
		private int CheckWord()
		{
			string text = string.Empty;
			foreach (char symbol in ResWordList.Items) text += symbol;
			return Data.FindIndex(word => word == text);

		}
		private void CheckButton_Click(object sender, RoutedEventArgs e)
		{
			int checkRes = CheckWord();
			if (checkRes != -1)
			{
				ResWordList.Items.Clear();
				Data.RemoveAt(checkRes);
				Update();
				ResultText.Text = "Вы правильно нашли слово";
			}
			else ResultText.Text = "Слово не найдено";
		}
		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			ResWordList.Items.Clear();
			ResultText.Text = string.Empty;
			Update();
		}
		private void NewGameButton_Click(object sender, RoutedEventArgs e)
		{
			WinPanel.Visibility = Visibility.Collapsed;
			ResultText.Text = string.Empty;
			LevelLength = 3;
			LevelGame = 1;
			InitGame();
		}
		private void NewWordButton_Click(object sender, RoutedEventArgs e)
		{
			NewWordButton.IsEnabled = false;
			InitGame();
		}
		private void HelpButton_Click(object sender, RoutedEventArgs e)
		{
			new Help().ShowDialog();
		}
		private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
		{
			media.Position = TimeSpan.Zero;
			media.Play();
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			media.Stop();
		}
		private void ExitButtonFromGame_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void ContinueGameButton_Click(object sender, RoutedEventArgs e)
		{
			WinPanel.Visibility = Visibility.Collapsed;
			ResultText.Text = string.Empty;
			if (LevelLength == 28)
			{
				LevelLength = 3;
				LevelGame = 1;
			}
			else
			{
				LevelLength++;
				LevelGame++;
			}
			InitGame();
		}
	}
}
