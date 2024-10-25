using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        private int repetitions;

        public int Repetitions
        {
            get { return repetitions; }
            set 
            { 
                repetitions = value;                
            }
        }

        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException(); // tillfällig
            // selected activity


        }
    }
}
