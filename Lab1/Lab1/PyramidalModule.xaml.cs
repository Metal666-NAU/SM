using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Lab1.Compute;
using static Lab1.WindowHelper;

namespace Lab1 {

	public partial class PyramidalModule : Window {

		private const int GRID_CELL_SIZE = 40;

		public PyramidalModule() {

			InitializeComponent();

			List<bool> input = InitInput(6);

			input.ForEach(value => InputPanel.Children.Add(new Label() { Content = (value ? 1 : 0) + "\n" + value.ToString() }));

			CreateModules(input);

		}

		private void CreateModules(List<bool> input) {

			for(int i = 0; i < input.Count; i++) {

				ModulesVisualisation.Height += GRID_CELL_SIZE;
				ModulesVisualisation.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });

			}

			for(int i = 0; i < 3; i++) {

				ModulesVisualisation.Width += GRID_CELL_SIZE * 3;

				ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });
				ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });
				ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });

				for(int j = 0; j < input.Count; j++) {

					CreateLabel(ModulesVisualisation, input[j] ? 1 : 0, j, i * 3);

				}

				ModuleA module1 = new ModuleA(input[0], input[1]);
				ModuleA module2 = new ModuleA(input[2], input[3]);
				ModuleA module3 = new ModuleA(input[4], input[5]);

				CreateLabel(ModulesVisualisation, "A", 1, (i * 3) + 1);
				CreateLabel(ModulesVisualisation, "A", 3, (i * 3) + 1);
				CreateLabel(ModulesVisualisation, "A", 5, (i * 3) + 1);

				input.Clear();

				input.Add(module1.GetOutput2());
				input.Add(module1.GetOutput1());
				input.Add(module2.GetOutput2());
				input.Add(module2.GetOutput1());
				input.Add(module3.GetOutput2());
				input.Add(module3.GetOutput1());

				for(int j = 0; j < input.Count; j++) {

					CreateLabel(ModulesVisualisation, input[j] ? 1 : 0, j, (i * 3) + 2);

				}

				if(i == 2) {

					break;

				}

				Swap(input, 1, 2);
				Swap(input, 3, 4);

			}

			ModulesVisualisation.Width += GRID_CELL_SIZE * 4;

			ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });
			ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });
			ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });
			ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });

			ModuleA module21 = new ModuleA(input[1], input[2]);
			ModuleA module22 = new ModuleA(input[3], input[4]);

			input.RemoveRange(1, 4);

			input.Insert(1, module21.GetOutput2());
			input.Insert(2, module21.GetOutput1());
			input.Insert(3, module22.GetOutput2());
			input.Insert(4, module22.GetOutput1());

			CreateLabel(ModulesVisualisation, "A", 1, 9);
			CreateLabel(ModulesVisualisation, "A", 4, 9);

			CreateLabel(ModulesVisualisation, module21.GetOutput2() ? 1 : 0, 1, 10);
			CreateLabel(ModulesVisualisation, module21.GetOutput1() ? 1 : 0, 2, 10);
			CreateLabel(ModulesVisualisation, module21.GetOutput2() ? 1 : 0, 3, 10);
			CreateLabel(ModulesVisualisation, module22.GetOutput1() ? 1 : 0, 4, 10);

			ModuleA module31 = new ModuleA(input[2], input[3]);

			input.RemoveRange(2, 2);

			input.Insert(2, module31.GetOutput2());
			input.Insert(3, module31.GetOutput1());

			CreateLabel(ModulesVisualisation, "A", 2, 11);

			CreateLabel(ModulesVisualisation, module31.GetOutput2() ? 1 : 0, 2, 12);
			CreateLabel(ModulesVisualisation, module31.GetOutput1() ? 1 : 0, 3, 12);

			for(int i = 0; i < input.Count; i++) {

				bool result = input[i];

				ModulesResult.Height += GRID_CELL_SIZE;

				ModulesResult.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });

				CreateLabel(ModulesResult, result ? 1 : 0, i, 0);

			}

			ProcessResults(input, (input, notEqual, i) => {

				Results.Height += GRID_CELL_SIZE;
				Results.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });

				CreateLabel(Results, input[i] ? 1 : 0, i, 0);
				CreateLabel(Results, input[i + 1] ? 1 : 0, i, 1);
				CreateLabel(Results, notEqual[0] ? 1 : 0, i, 2);

			}, (results, iterations) => {

				CreateLabel(Results, results[iterations] ? 1 : 0, iterations * 6, 4);
				CreateLabel(Results, results[iterations + 1] ? 1 : 0, (iterations * 6) + 2, 4);
				CreateLabel(Results, results[iterations + 2] ? 1 : 0, (iterations * 6) + 4, 4);

			}, results => {

				results.ForEach(result => OutputPanel.Children.Add(new Label() { Content = (result ? 1 : 0) + "\n" + result.ToString() }));

			});

			static void Swap<T>(List<T> list, int index1, int index2) {

				T temp = list[index1];
				list[index1] = list[index2];
				list[index2] = temp;

			}

		}

	}

}