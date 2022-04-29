using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab4 {

	public partial class MainWindow : Window {

		public MainWindow() {

			InitializeComponent();

			for(int i = 2; i < 21; i++) {

				Radix.Items.Add(i);

			}

			Radix.SelectedIndex = 0;

		}

		private void Calculate(object sender, RoutedEventArgs e) {

			string input = Input.Text;
			int radix = (int) Radix.SelectedValue;

			if(double.TryParse(input, out double result)) {

				if(OutputTable.RowDefinitions.Count > 1) {

					OutputTable.RowDefinitions.RemoveRange(1, OutputTable.RowDefinitions.Count - 1);

				}

				if(OutputTable.Children.Count > 2) {

					OutputTable.Children.RemoveRange(2, OutputTable.Children.Count - 2);

				}

				Output.Text = "Output: " + Convert(result, radix, (long dividend, long remainder) => {

					OutputTable.RowDefinitions.Add(new RowDefinition() {
					
						Height = new GridLength(40)
					
					});

					Label dividentLabel = new Label {

						Content = dividend,
						VerticalAlignment = VerticalAlignment.Center

					}, remainderLabel = new Label {

						Content = remainder,
						VerticalAlignment = VerticalAlignment.Center

					};

					Grid.SetRow(dividentLabel, OutputTable.RowDefinitions.Count - 1);
					Grid.SetRow(remainderLabel, OutputTable.RowDefinitions.Count - 1);

					Grid.SetColumn(remainderLabel, 1);

					OutputTable.Children.Add(dividentLabel);
					OutputTable.Children.Add(remainderLabel);

				});

				ConversionProcess.Text = $"Conversion process: Multiply input number by {radix}^10 (to avoid decimal separator). Divide the number by {radix}. Write the remainder to the output. If the remainder is 1, subtract it from number. Continue doing so until the number is 0. Resulting output will be the representation of the given number in a numerical system with radix {radix}.";

				OutputLNS.Content = GetLNS(result);

			}

			else {

				MessageBox.Show("Are you sure the text you entered represents a number?", "Error");

			}

		}

		private readonly string[] LETTERS = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

		private string Convert(double value, int radix, Action<long, long> divide) {

			string result = "";

			long simplified = (long) Math.Round(Math.Abs(value) * Math.Pow(radix, 10));

			while(simplified != 0) {

				long remainder = simplified % radix;

				result = (remainder >= 10 ? LETTERS[remainder - 10] : remainder.ToString()) + result;

				divide?.Invoke(simplified, remainder);

				simplified = (simplified - remainder) / radix;

			}

			while(result.Length < 10) {

				result += "0";

			}

			return (value < 0 ? "-" : "") + result.Insert(result.Length - 10, ".");
		
		}

		private string GetLNS(double value) {

			string binary = Convert(value, 2, null);

			string result = binary[0] == '-' ? "1." : "0.";
			int numbers = 0;

			string[] binarySplit = (binary[0] == '-' ? binary.Substring(1) : binary).Split('.');

			for(int i = 0; i < binarySplit[0].Length; i++) {

				if(binarySplit[0][i].Equals('1')) {

					numbers++;

					result += binarySplit[0].Length - 1 - i + ".";

				}

			}

			if(binarySplit.Length > 1) {

				for(int i = 0; i < binarySplit[1].Length; i++) {

					if(binarySplit[1][i].Equals('1')) {

						numbers++;

						result += "-" + (i + 1) + ".";

					}

				}

			}

			return "LNS representation: " + result.Insert(2, numbers + ".");

		}

	}

}