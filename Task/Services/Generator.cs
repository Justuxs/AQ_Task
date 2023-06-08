using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Task.Models;

namespace Task.Services
{
    internal class Generator
    {
        private const int lower_size = 3;
        private const int upper_size = 4;

        public static List<Harness_drawing>? GenerateRandomSets(List<Harness_drawing> harness_drawings) {
            if (harness_drawings == null || harness_drawings.Count< upper_size) { return null; }
            Random random = new Random();
            int size = random.Next(lower_size, upper_size + 1);
            List<Harness_drawing> randomDrawings = harness_drawings.OrderBy(d => random.Next()).Take(size).ToList();
            return randomDrawings;
        }

        public static List<HarnessDrawingMatrixRow>? GenerateDrawingMatrix(List<Harness_drawing> harness_drawings)
        {
            if (harness_drawings == null) { return null; }
            List<HarnessDrawingMatrixRow> matrix = new List<HarnessDrawingMatrixRow>();

            for(int i=0;i< harness_drawings.Count; i++)
            {

                for (int j = i+1; j < harness_drawings.Count; j++)
                {
                    matrix.Add(new HarnessDrawingMatrixRow(i+1,j+1,harness_drawings[i], harness_drawings[j]));
                }
            }
            return matrix;
        }

    }
}
