using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathing.Math;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Strategies
{
    public class ScanStrategy : BaseStrategy
    {
        protected double Accuracy { get; private set; }

        public ScanStrategy(int concurrency = 1000, double accuracy = 100, Action<Coord> debug = null) : base(concurrency, debug) { Accuracy = accuracy; }

        protected override IEnumerable<Coordinate> Scan(Map map, Node from)
        {
            var anchor = from.Position;
            var startScan = from.BackHeading.GetValueOrDefault(0d) + Accuracy;
            var endScan = startScan + Accuracy + 360d;
            for (var angle = startScan; angle < endScan; angle += Accuracy) {
                yield return map.Path(anchor, angle);
            }
        }
    }
}
