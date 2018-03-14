using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    struct Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public double GetDistanceSquare(Point p)
        {
            return ((this.x - p.x) * (this.x - p.x)) + ((this.y - p.y) * (this.y - p.y));
        }
    }
    
    abstract class Shape
    {
        public int NoOfSides { get; set; }
        public int Dimension { get; set; }
        public string ShapeName { get; set; }
        public Point[] Coordinates { get; set; }
        public Vector[] Edges { get; set; }
        public abstract bool IsShapeValid();
        public abstract void DrawShape();
        public void PrintCoordinates()
        {
            if (!IsShapeValid())
                return;
            Console.Write(string.Format("{0}: ", ShapeName));
            for (int i = 0; i < NoOfSides; i++)
            {
                Console.Write(string.Format("({0},", Coordinates[i].x));
                Console.Write(Coordinates[i].y);
                if(Dimension == 2)
                {
                    Console.Write(")");
                }
                else
                {
                    Console.Write(string.Format(",{0})", Coordinates[i].z));
                }
            }
            Console.WriteLine();
        }
        public Shape()
        {
            Dimension = 2;
        }
    }
    class Rectangle: Shape
    {
        public Rectangle(int dimension = 2)
        {
            NoOfSides = 4;
            Dimension = dimension;
            ShapeName = "Rectangle";
            Coordinates = new Point[4];            
            Edges = new Vector[4];
        }
        public override bool IsShapeValid()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (Coordinates[i].x == Coordinates[j].x && Coordinates[i].y == Coordinates[j].y)
                        return false;
                }
            }
            if (IsOrderedRectangle(Coordinates[0], Coordinates[1], Coordinates[2], Coordinates[3]))
            {
                Edges[0] = new Vector(Coordinates[0], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[2]);
                Edges[2] = new Vector(Coordinates[2], Coordinates[3]);
                Edges[3] = new Vector(Coordinates[3], Coordinates[0]);
                return true;
            }
            if (IsOrderedRectangle(Coordinates[2], Coordinates[1], Coordinates[3], Coordinates[0]))
            {
                Edges[0] = new Vector(Coordinates[2], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[3]);
                Edges[2] = new Vector(Coordinates[3], Coordinates[0]);
                Edges[3] = new Vector(Coordinates[0], Coordinates[2]);
                return true;
            }
            if (IsOrderedRectangle(Coordinates[0], Coordinates[1], Coordinates[3], Coordinates[2]))
            {
                Edges[0] = new Vector(Coordinates[0], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[3]);
                Edges[2] = new Vector(Coordinates[3], Coordinates[2]);
                Edges[3] = new Vector(Coordinates[2], Coordinates[0]);
                return true;
            }
            return false;
        }
        private bool IsOrderedRectangle(Point a, Point b, Point c, Point d)
        {
            List<Point> points = new List<Point> { a, b, c, d};
            Dictionary<double, bool> sizes = new Dictionary<double, bool>();
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    sizes[points[i].GetDistanceSquare(points[j])] = true;
                }
            }
            if (sizes.Keys.Count > 3 || sizes.Keys.Count < 2)
                return false;
            List<double> sortedSizes = new List<double>();
            foreach (var item in sizes.Keys)
            {
                sortedSizes.Add(item);
            }
            sortedSizes.Sort();
            if (sortedSizes.Count == 2)
                return (2 * sortedSizes[0] == sortedSizes[1]);
            return (sortedSizes[0] + sortedSizes[1] == sortedSizes[2]);
        }
        public override void DrawShape()
        {

        }
    }
    class Trinagle : Shape
    {
        public Trinagle(int dimension = 2)
        {
            NoOfSides = 3;
            Dimension = dimension;
            ShapeName = "Triangle";
            Coordinates = new Point[3];
            Edges = new Vector[3];
        }
        public override bool IsShapeValid()
        {
            //using shoelace formula; skipped multiplication by 0.5
            double area = Coordinates[0].x * (Coordinates[1].y - Coordinates[2].y) + Coordinates[1].x * (Coordinates[2].y - Coordinates[0].y) + Coordinates[2].x * (Coordinates[0].y - Coordinates[1].y);
            if (area == 0)
                return false;
            else
                return true;
        }        
        public override void DrawShape()
        {

        }
    }
}
