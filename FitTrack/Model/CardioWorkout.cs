using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class CardioWorkout : Workout
    {
        private int distance;

        public int Distance
        {
            get { return distance; }
            set 
            { 
                distance = value; 
            }
        }

        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException(); // tillfälligt
            /*
            MET-värdet för aktiviteten
            MET-värden är standardvärden för olika aktiviteter, till exempel:

            Gå i normal takt: ca 3,5 MET
            Springa (5 km/h): ca 7 MET
            Springa (10 km/h): ca 10 MET
            Styrketräning (måttlig): ca 4 MET
            Städa eller dammsuga: ca 3 MET
            MET-tabeller finns ofta på träningssidor eller hälsosajter om du vill ha fler exempel.

            Formeln för att räkna ut kaloriförbrukning är:

            Kalorier forbrukade=MET-varde × vikt i kg × tid i timmar
            */
        }
    }
}
