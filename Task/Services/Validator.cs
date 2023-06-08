using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Helpers;
using Task.Models;

namespace Task.Services
{
    public class Validator
    {

        public static List<HousingMismatch> ValidateHousing(Harness_drawing harness_drawing1, Harness_drawing harness_drawing2)
        {
            Dictionary<string, Harness_wire> wires_dictionary = new Dictionary<string, Harness_wire>();
            List<HousingMismatch> housingMismatches= new List<HousingMismatch>();

            foreach( Harness_wire wire in harness_drawing1.harness_wires)
            {
                if (wire.housing_1 != null)
                    wires_dictionary.Add(wire.housing_1,wire);

                if (wire.housing_2 != null)
                    wires_dictionary.Add(wire.housing_2, wire);
            }
            foreach (Harness_wire wire in harness_drawing2.harness_wires)
            {
                if(wire.housing_1 != null && wires_dictionary.ContainsKey(wire.housing_1))
                {
                    housingMismatches.Add(new HousingMismatch(wires_dictionary[wire.housing_1], wire, "Dublis jungties " + wire.housing_1));
                }
                if (wire.housing_2 != null && wires_dictionary.ContainsKey(wire.housing_2))
                {
                    housingMismatches.Add(new HousingMismatch(wires_dictionary[wire.housing_2], wire, "Dublis jungties " + wire.housing_2));

                }
            }
            return housingMismatches;
        }
    }
}
