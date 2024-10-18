using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class StrengthWorkout : Workout
    {
        public int Repetitions;

        public int CalculateCaloriesBurned()
        {
            return 0; // tillfällig
        }
    }
}
