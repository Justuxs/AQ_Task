using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task.Helpers;

namespace Task.Models
{
    public class HarnessDrawingMatrixRow
    {
        private const string defaultName = "Pynė ";
        public string name1 { get; set; }
        public string name2 { get; set; }

        public Harness_drawing harness_drawing1 { get; set; } 
        public Harness_drawing harness_drawing2 { get; set; }

        public bool housing { get; set; } = true;
        public List<HousingMismatch> housingMismatches { get; set; } 


        public HarnessDrawingMatrixRow(int id1, int id2, Harness_drawing harness_drawing1, Harness_drawing harness_drawing2)
        {
            name1 = defaultName +id1+" "+ harness_drawing1.ID;
            name2 = defaultName + id2 + " " + harness_drawing2.ID;

            this.harness_drawing1 = harness_drawing1;
            this.harness_drawing2 = harness_drawing2;

            housingMismatches = Services.Validator.ValidateHousing(harness_drawing1, harness_drawing2);
            if (housingMismatches.Count > 0)
            {
                housing = false;

            }
        }

        public override string ToString()
        {
            return $"Name1: {name1}, Name2: {name2}, Housing: {housing}";
        }
    }
}
