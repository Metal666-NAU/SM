using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab5 {

	public partial class NumberInput : UserControl {

		public NumberInput() {

			InitializeComponent();

		}

		private void NumberLengthChanged(object sender, TextChangedEventArgs e) {

			if(DigitsPanel != null) {

				if(int.TryParse(NumberLength.Text, out int length)) {

					if(DigitsPanel.Children.Count / 2 > length) {

						DigitsPanel.Children.RemoveRange(length * 2, DigitsPanel.Children.Count - (length * 2));

					}

					else {

						while(DigitsPanel.Children.Count / 2 != length) {

							TextBox digit = new TextBox() {

								Width = 30,
								Text = "0",
								TextAlignment = TextAlignment.Center,
								VerticalContentAlignment = VerticalAlignment.Center

							};

							digit.GotFocus += (object source, RoutedEventArgs args) => {

								digit.SelectionStart = 0;
								digit.SelectionLength = digit.Text.Length;
							
							};

							Label dot = new Label() {

								Content = ".",
								VerticalContentAlignment = VerticalAlignment.Bottom

							};

							DigitsPanel.Children.Add(digit);
							DigitsPanel.Children.Add(dot);

						}

					}

				}

				else {

					DigitsPanel.Children.Clear();

				}

			}

		}

		public List<int> GetNumber() {

			List<int> output = new List<int>() {
				0,
				int.Parse(NumberLength.Text)
			};

			foreach(UIElement element in DigitsPanel.Children) {

				if(element is TextBox) {

					output.Add(int.Parse((element as TextBox).Text));

				}

			}

			return output;

		}

	}

}