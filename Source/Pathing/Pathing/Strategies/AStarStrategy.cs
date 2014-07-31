using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathing.Math;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Strategies
{
    public class AStarStrategy : BaseStrategy
    {
        public AStarStrategy(int concurrency = 1000, Action<Coord> debug = null) : base(concurrency, debug) { }

        protected override IEnumerable<Coord> Scan(Map map, Node from)
        {
            var anchor = from.Position;
            yield return anchor + new Coord(0, -1);
            yield return anchor + new Coord(1, -1);
            yield return anchor + new Coord(1, 0);
            yield return anchor + new Coord(1, 1);
            yield return anchor + new Coord(0, 1);
            yield return anchor + new Coord(-1, 1);
            yield return anchor + new Coord(-1, 0);
            yield return anchor + new Coord(-1, -1);
        } 
    }
}
