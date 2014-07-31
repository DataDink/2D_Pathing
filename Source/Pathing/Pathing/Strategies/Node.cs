using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pathing.Math;
using Coord = Pathing.Math.Coordinate;

namespace Pathing.Strategies
{
    public class Node : IEnumerable<Coord>
    {
        private readonly List<Coord> _plots = new List<Coord>(); 
        private Node() {}
        public Node(Coord start) { _plots.Add(start); }

        public Coord Position { get { return _plots.Last(); } }

        public double? Heading { get { return _plots.Count > 1 ? _plots[_plots.Count - 2].DirectionTo(_plots.Last()) : (double?)null; } }
        
        public double? BackHeading { get { return _plots.Count > 1 ? _plots.Last().DirectionTo(_plots[_plots.Count - 2]) : (double?)null; } }

        public Node Branch(Coord to)
        {
            var branch = new Node();
            branch._plots.AddRange(_plots);
            branch._plots.Add(to);
            return branch;
        }

        public IEnumerator<Coord> GetEnumerator()
        {
            return _plots.Skip(1)
                .Aggregate(new[] { _plots.First() }, (a, c) => a.Concat(a.Last().PathTo(c)).ToArray())
                .ToList()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return new {
                Position = Position,
                Heading = Heading,
                BackHeading = BackHeading,
                Length = this.ToArray().Length
            }.ToString();
        }
    }
}
