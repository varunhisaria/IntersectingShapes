using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vector(Point source, Point destination)
        {
            X = destination.x - source.x;
            Y = destination.y - source.y;
            Z = destination.z - source.z;
        }
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double DotProduct(Point p)
        {
            return (X * p.x) + (Y * p.y);
        }
    }
}
