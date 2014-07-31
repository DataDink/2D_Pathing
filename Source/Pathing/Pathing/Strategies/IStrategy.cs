using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathing.Math;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Strategies
{
    public interface IStrategy
    {
        Node Evaluate(Map map, Coord from, Coord to);
    }
}
