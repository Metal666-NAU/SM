using System.Windows;
using System.Windows.Controls;

namespace Lab1 {

	public static class WindowHelper {

		public static void CreateLabel(Grid grid, object content, int row, int column) {

			Label label = new Label() {
				Content = content,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			Grid.SetRow(label, row);
			Grid.SetColumn(label, column);

			grid.Children.Add(label);

		}

	}

}