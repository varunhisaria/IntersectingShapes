using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        //to create a vector between a given source and destination
        public Vector(Point source, Point destination)
        {
            X = destination.x - source.x;
            Y = destination.y - source.y;
            Z = destination.z - source.z;
        }
        //to create a vector by directly supplying the values
        public Vector(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double DotProduct(Point p)
        {
            return (X * p.x) + (Y * p.y) + (Z * p.z);
        }
    }
}
