using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Math
{
    public static class MapExtensions
    {
        public static bool Exists(this Map map, Coord coordinate)
        {
            return coordinate.X >= 0 && coordinate.Y >= 0
                && coordinate.X < map.Width && coordinate.Y < map.Height;
        }

        public static Coord Path(this Map map, Coord from, double direction)
        {
            return from.Path(direction).TakeWhile(p => map.Exists(p) && map[p]).Last();
        }

        public static bool CanPath(this Map map, Coord from, Coord to)
        {
            return from.PathTo(to).All(p => map.Exists(p) && map[p]);
        }
    }
}
