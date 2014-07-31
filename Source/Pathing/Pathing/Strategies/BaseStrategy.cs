using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathing.Math;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Strategies
{
    public abstract class BaseStrategy : IStrategy
    {
        protected int Concurrency { get; private set; }
        protected Action<Coord> Debug { get; private set; }

        protected BaseStrategy(int concurrency, Action<Coord> debug)
        {
            if (debug != null) { Debug = coord => { try { debug(coord); } catch { } }; }
            Concurrency = concurrency;
        }

        public Node Evaluate(Map map, Coordinate @from, Coordinate to)
        {
            var history = new List<Coord>();
            var nodes = new[] { new Node(from) };
            while (nodes.Any())
            {
                var activeNodes = nodes
                    .OrderBy(n => n.Position.MagnitudeTo(to))
                    .Take(Concurrency)
                    .ToArray();
                var newNodes = new List<Node>();
                foreach (var node in activeNodes)
                {
                    if (map.CanPath(node.Position, to)) return node.Branch(to);
                    var scan = Scan(map, node).Where(s => !history.Contains(s) && map.Exists(s) && map[s]).ToArray();
                    history.AddRange(scan);
                    newNodes.AddRange(scan.Select(node.Branch));
                }
                nodes = nodes.Where(n => !activeNodes.Contains(n)).Concat(newNodes).ToArray();
                if (Debug != null) { nodes.Select(n => n.Position).ToList().ForEach(Debug); }
            }
            return null;
        }

        protected abstract IEnumerable<Coord> Scan(Map map, Node from);
    }
}
