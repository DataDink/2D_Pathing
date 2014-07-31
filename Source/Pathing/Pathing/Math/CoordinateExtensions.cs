using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc = System.Math;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Math
{
    public static class CoordinateExtensions
    {
        public static int Round(this double doubleValue) { return (int)Calc.Round(doubleValue); }
        public static double ToRadians(this double degrees) { return degrees * Calc.PI / 180d; }
        public static double ToDegrees(this double radians) { return radians * 180d / Calc.PI; }
        public static double Direction(this Coord to) { return (Calc.Atan2(to.Y, to.X).ToDegrees() + 450d) % 360d; }
        public static double DirectionTo(this Coord from, Coord to) { return (to - from).Direction(); }
        public static double Magnitude(this Coord to) { return Calc.Sqrt(to.X * to.X + to.Y * to.Y); }
        public static double MagnitudeTo(this Coord from, Coord to) { return (to - from).Magnitude(); }
        public static double AngleSize(this double magnitude) { return 45d / magnitude; }
        
        public static Coord Plot(double direction, double magnitude)
        {
            var r = ((direction + 270d) % 360).ToRadians(); 
            return new Coordinate((Calc.Cos(r) * magnitude).Round(), (Calc.Sin(r) * magnitude).Round());
        }
        
        public static Coord PlotTo(this Coord from, double direction, double magnitude)
        {
            return Plot(direction, magnitude) + from;
        }
        
        public static IEnumerable<Coord> Path(this Coord from, double direction)
        {
            var distance = 0d; 
            while (distance < double.MaxValue) yield return from.PlotTo(direction, distance++);
        } 

        public static IEnumerable<Coord> PathTo(this Coord from, double direction, double distance)
        {
            var d = 0d;
            while (d < distance) yield return from.PlotTo(direction, d++);
        } 
        
        public static IEnumerable<Coord> PathTo(this Coord from, Coord to)
        {
            return from.PathTo(from.DirectionTo(to), from.MagnitudeTo(to));
        }
    }
}
