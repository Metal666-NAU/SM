using System.Windows;

namespace Lab1 {

	public partial class Launcher : Window {

		public Launcher() {

			InitializeComponent();

		}

		private void StartTriangularModule(object sender, RoutedEventArgs e) {

			TriangularModule module = new TriangularModule();

			module.Show();

		}

		private void StartPyramidalModule(object sender, RoutedEventArgs e) {

			PyramidalModule module = new PyramidalModule();

			module.Show();

		}

	}

}