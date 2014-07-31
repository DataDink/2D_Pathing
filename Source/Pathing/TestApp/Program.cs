using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pathing.Math;
using Pathing.Strategies;

namespace TestApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            Map map;
            Coordinate start;
            Coordinate end;
            if (!Imaging.ShowOpenDialog(Color.FromArgb(0, 255, 33), Color.Red, Color.White, out start, out end, out map)) return;

            using (var frm = new Form { ClientSize = new Size(map.Width, map.Height), FormBorderStyle = FormBorderStyle.None }) 
            using (var gfx = frm.CreateGraphics()) {

                Console.WriteLine("=====================================================");
                Console.WriteLine("=             Please Select A Strategy              =");
                Console.WriteLine("=                                                   =");
                Console.WriteLine("= 1: A* Strategy                                    =");
                Console.WriteLine("= 2: Scan Strategy (low accuracy)                   =");
                Console.WriteLine("= 3: Scan Strategy (med accuracy)                   =");
                Console.WriteLine("= 4: Scan Strategy (high accuracy)                  =");
                Console.WriteLine("=                                                   =");
                Console.WriteLine("=====================================================");
                var selection = Console.ReadKey();
                frm.Show();

                IStrategy strategy;
                switch (selection.KeyChar) {
                    case '1':
                        strategy = new AStarStrategy(
                            debug: pt => {
                                var point = pt.ToPoint();
                                gfx.DrawEllipse(Pens.Red, point.X, point.Y, 1, 1);
                            });
                        break;
                    case '2':
                        strategy = new ScanStrategy(
                            accuracy: 200,
                            debug: pt => {
                                var point = pt.ToPoint();
                                gfx.DrawEllipse(Pens.Red, point.X, point.Y, 1, 1);
                            });
                        break;
                    case '3':
                        strategy = new ScanStrategy(
                            accuracy: 100,
                            debug: pt => {
                                var point = pt.ToPoint();
                                gfx.DrawEllipse(Pens.Red, point.X, point.Y, 1, 1);
                            });
                        break;
                    case '4':
                        strategy = new ScanStrategy(
                            accuracy: 10,
                            debug: pt => {
                                var point = pt.ToPoint();
                                gfx.DrawEllipse(Pens.Red, point.X, point.Y, 1, 1);
                            });
                        break;
                    default:
                        return;
                }
                
                var path = strategy.Evaluate(map, start, end);
                if (path != null) {
                    foreach (var pt in path) {
                        gfx.DrawEllipse(Pens.Green, pt.X, pt.Y, 1, 1);
                    }
                } else {
                    frm.Hide();
                    Console.WriteLine("Could not find path");
                }
                Console.ReadKey();
            }
        }
    }
}
