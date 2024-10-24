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
        }
    }
}
