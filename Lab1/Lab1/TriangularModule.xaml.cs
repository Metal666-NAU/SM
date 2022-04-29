using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Lab1.Compute;
using static Lab1.WindowHelper;

namespace Lab1 {

	public partial class TriangularModule : Window {

		private const int INPUTS = 6, GRID_CELL_SIZE = 40;

		public TriangularModule() {

			InitializeComponent();

			List<bool> input = InitInput(INPUTS);

			input.ForEach(value => InputPanel.Children.Add(new Label() { Content = (value ? 1 : 0) + "\n" + value.ToString() }));

			CreateModules(input);

		}

		public void CreateModules(List<bool> input) {

			List<bool> results = new List<bool>();

			int iterations = input.Count - 1;

			for(int i = 0; i < iterations; i++) {

				ModulesVisualisation.Height += GRID_CELL_SIZE * 2;
				ModulesVisualisation.Width += GRID_CELL_SIZE * 2;

				ModulesVisualisation.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });
				ModulesVisualisation.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });

				ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });
				ModulesVisualisation.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(GRID_CELL_SIZE) });

				for(int j = 0; j < iterations - i; j++) {

					GenerateGridSegment(i, j);

					ModuleA module = new ModuleA(input[0], input[1]);

					input.RemoveRange(0, 2);

					input.Insert(0, module.GetOutput2());
					input.Add(module.GetOutput1());

				}

				bool finalBool = input[0];

				results.Add(finalBool);

				input.RemoveAt(0);

			}

			ModulesVisualisation.Height += GRID_CELL_SIZE * 2;

			ModulesVisualisation.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });
			ModulesVisualisation.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });

			CreateLabel(ModulesVisualisation, input[0] ? 1 : 0, 0, iterations * 2);

			results.Add(input[0]);

			results.Reverse();

			for(int i = 0; i < results.Count; i++) {

				bool result = results[i];

				ModulesResult.Height += GRID_CELL_SIZE * 2;

				ModulesResult.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });
				ModulesResult.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(GRID_CELL_SIZE) });

				CreateLabel(ModulesResult, result ? 1 : 0, i * 2, 0);

			}

			ProcessResults(results, (input, notEqual, i) => {

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

			void GenerateGridSegment(int i, int j) {

				CreateLabel(ModulesVisualisation, "A", (iterations * 2) - (i * 2), ((iterations - 1) * 2) - (j * 2) + 1);
				CreateLabel(ModulesVisualisation, input[0] ? 1 : 0, (iterations * 2) - (i * 2), (i * 2) + (j * 2));
				CreateLabel(ModulesVisualisation, input[1] ? 1 : 0, (iterations * 2) - (i * 2) + 1, (i * 2) + (j * 2) + 1);

			}

		}

	}

}