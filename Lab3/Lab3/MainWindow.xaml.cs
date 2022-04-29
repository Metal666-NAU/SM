using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Lab3 {

	public partial class MainWindow : Window {

		public MainWindow() {

			InitializeComponent();

		}

		public void ProcessJson(string path) {

			InputTable.Children.Clear();
			InputTable.ColumnDefinitions.Clear();
			InputTable.RowDefinitions.Clear();

			OutputTable.Children.Clear();
			OutputTable.ColumnDefinitions.Clear();

			try {

				string json = File.ReadAllText(path);
				List<List<string>> table = JsonConvert.DeserializeObject<List<List<string>>>(json);

				for(int i = 0; i < table.Count; i++) {

					List<string> row = table[i];

					InputTable.RowDefinitions.Add(new RowDefinition() {

						Height = new GridLength(40)

					});

					OutputTable.ColumnDefinitions.Add(new ColumnDefinition() {

						Width = new GridLength(40)

					});

					Label outputNumber = new Label() {
					
						Content = i + 1

					};

					Grid.SetColumn(outputNumber, i);

					OutputTable.Children.Add(outputNumber);

					while(InputTable.ColumnDefinitions.Count < row.Count) {

						InputTable.ColumnDefinitions.Add(new ColumnDefinition() {

							Width = new GridLength(60)

						});

					}

					List<int> indeces = new List<int>();

					for(int j = 0; j < row.Count; j++) {

						string value = row[j];

						Label label = new Label() {

							Content = value,
							HorizontalAlignment = HorizontalAlignment.Center,
							VerticalAlignment = VerticalAlignment.Center

						};

						Grid.SetRow(label, i);
						Grid.SetColumn(label, j);

						InputTable.Children.Add(label);

						if(int.Parse(value[0].ToString()) == 5) {

							indeces.Add(int.Parse(value.Substring(1)));

						}

						else if(int.Parse(value[0].ToString()) == 1) {

							indeces.Add(0);

						}

					}

					Label rank = new Label() {
						
						Content = indeces.Max() + 2

					};

					Grid.SetRow(rank, 1);
					Grid.SetColumn(rank, i);

					OutputTable.Children.Add(rank);

				}

			}

			catch(Exception) {

				MessageBox.Show("Path you entered is invalid, or JSON is malformed.", "Error");

			}

		}

		private void ReloadJson(object sender, RoutedEventArgs e) {

			ProcessJson(JsonPath.Text);
		
		}

	}

}