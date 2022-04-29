using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Lab5 {

	public partial class MainWindow : Window {

		public MainWindow() {

			InitializeComponent();

		}

		private void Calculate(object sender, RoutedEventArgs e) {

			List<int> number1 = Number1.GetNumber();
			List<int> number2 = Number2.GetNumber();

			#region Union

			List<int> union = number1.Concat(number2).ToList();

			union.RemoveRange(number1.Count, 2);
			union.RemoveAt(1);
			union.Insert(1, union.Count - 1);

			Union.Content = NumberToString(union);

			#endregion

			#region Comparison

			int comparison = CompareNumbers(number1, number2);

			Comparison.Content = NumberToString(number1) + ((comparison == 0) ? " = " : ((comparison > 0) ? " > " : " < ")) + NumberToString(number2);

			#endregion

			#region Sorting

			SortNumber(union);

			Sorting.Content = NumberToString(union);

			#endregion

			#region Simplification/Addition

			Simplify(union);

			Simplification.Content = NumberToString(union);

			#endregion

			#region Subtraction

			Subtraction.Content = NumberToString(Subtract(number1, number2));

			#endregion

			#region Multiplication

			Multiplication.Content = NumberToString(Multiply(number1, number2));

			#endregion

			#region Division

			if(number2.Count == 2) {

				Division.Content = "-";

			}

			else if(number1.Count == 2) {

				Division.Content = "0.0.";

			}

			else {

				List<int> o = new List<int>(number1), result = new List<int>() {
				
					0,
					number1.Count - 2

				};

				int b = number2[2];

				for(int i = 0; i < number1.Count - 3; i++) {

					int subtracted = o[2] - b;

					List<int> multiplied = Multiply(number2, new List<int>() {

						0,
						1,
						subtracted
					
					});

					o = Subtract(o, multiplied);

					result.Add(subtracted);

				}

				Division.Content = NumberToString(o);

			}

			#endregion

		}

		private int CompareNumbers(List<int> number1, List<int> number2) {

			SortNumber(number1);
			SortNumber(number2);

			for(int i = 2; i < Math.Min(number1.Count, number2.Count); i++) {

				int digit1 = number1[i];
				int digit2 = number2[i];

				if(digit1 != digit2) {

					return digit1 > digit2 ? 1 : -1;

				}

			}

			return (number1.Count > number2.Count) ? 1 : (number1.Count < number2.Count ? -1 : 0);

		}

		private List<int> Subtract(List<int> number1, List<int> number2) {

			List<int> equalRemoved1 = new List<int>(number1);
			List<int> equalRemoved2 = new List<int>(number2);

			RemoveEqual(equalRemoved1, equalRemoved2);

			if(equalRemoved2.Count == 2) {

				return equalRemoved1;

			}

			else if(equalRemoved1.Count == 2) {

				equalRemoved2[0] = 1;

				return equalRemoved1;

			}

			else {

				int sign = 0;

				if(CompareNumbers(number1, number2) < 0) {

					sign = 1;

					(equalRemoved2, equalRemoved1) = (equalRemoved1, equalRemoved2);

				}

				int upperBound = equalRemoved1[2],
					lowerBound = equalRemoved2[equalRemoved2.Count - 1] + 1;

				List<int> x = Enumerable.Range(lowerBound, upperBound - lowerBound).Reverse().ToList();

				x.Insert(0, x.Count);
				x.Insert(0, sign);

				RemoveEqual(x, equalRemoved2);

				x.Add(equalRemoved2.Last());

				x.RemoveRange(0, 2);
				equalRemoved1.RemoveRange(0, 3);
				x.InsertRange(x.Count, equalRemoved1);

				x.Insert(0, x.Count);
				x.Insert(0, sign);

				SortNumber(x);
				Simplify(x);

				return x;

			}

		}

		private List<int> Multiply(List<int> number1, List<int> number2) {

			List<int> multiplied = new List<int> {

				0,
				0

			};

			if(number1.Count != 2 && number2.Count != 2) {

				for(int i = 2; i < number1.Count; i++) {

					for(int j = 2; j < number2.Count; j++) {

						multiplied.Add(number1[i] + number2[j]);

					}

				}

			}

			int length = multiplied.Count - 2;

			multiplied.RemoveAt(1);
			multiplied.Insert(1, length);

			SortNumber(multiplied);
			Simplify(multiplied);

			return multiplied;

		}

		private void SortNumber(List<int> input) {

			List<int> prefix = input.GetRange(0, 2);
			input.RemoveRange(0, 2);

			input.Sort();
			input.Reverse();
			input.InsertRange(0, prefix);

		}

		private void RemoveEqual(List<int> number1, List<int> number2) {

			int sign1 = number1[0];
			int sign2 = number2[0];

			number1.RemoveRange(0, 2);
			number2.RemoveRange(0, 2);

			for(int i = 0; i < number1.Count; i++) {

				if(number2.Remove(number1[i])) {

					number1.RemoveAt(i--);

				}

			}

			number1.Insert(0, number1.Count);
			number2.Insert(0, number2.Count);

			number1.Insert(0, sign1);
			number2.Insert(0, sign2);

		}

		private void Simplify(List<int> number) {

			bool changed = true;

			while(changed) {

				changed = false;

				SortNumber(number);

				for(int i = number.Count - 1; i > 2; i--) {

					if(number[i] == number[i - 1]) {

						number.RemoveAt(i);
						number[i - 1]++;

						changed = true;

					}

				}

			}

			number.RemoveAt(1);
			number.Insert(1, number.Count - 1);

		}

		private string NumberToString(List<int> input) {

			string result = "";

			input.ForEach(number => {

				result += number + ".";

			});

			return result;

		}

	}

}