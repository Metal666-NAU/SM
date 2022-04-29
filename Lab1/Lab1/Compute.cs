using System;
using System.Collections.Generic;

namespace Lab1 {

	public static class Compute {

		public static List<bool> InitInput(int length) {

			Random random = new Random();
			List<bool> result = new List<bool>();

			for(int i = 0; i < length; i++) {

				result.Add(random.Next(0, 2) == 1);

			}

			return result;

		}

		public class ModuleA {

			private readonly bool x1, x2;

			public ModuleA(bool x1, bool x2) {

				this.x1 = x1;
				this.x2 = x2;

			}

			public bool GetOutput1() {

				return x1 && x2;

			}

			public bool GetOutput2() {

				return x1 || x2;

			}

		}

		public static void ProcessResults(List<bool> input, Action<List<bool>, List<bool>, int> update1, Action<List<bool>, int> update2, Action<List<bool>> update3) {

			if(input.Count % 6 == 0) {

				input.Insert(0, false);

			}

			else {

				return;

			}

			List<bool> notEqual = new List<bool>();

			for(int i = 0; i < input.Count - 1; i++) {

				notEqual.Insert(0, input[i] != input[i + 1]);

				update1(input, notEqual, i);

			}

			List<bool> results = new List<bool>();

			int iterations = 0;

			while(notEqual.Count / 6 > 0) {

				results.Insert(0, notEqual[2] || notEqual[4] || notEqual[0]);
				results.Insert(0, notEqual[5] || notEqual[1] || notEqual[2]);
				results.Insert(0, notEqual[5] || notEqual[4] || notEqual[3]);

				update2(results, iterations);

				iterations++;

				notEqual.RemoveRange(0, 6);

			}

			update3(results);

		}

	}

}