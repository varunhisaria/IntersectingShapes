﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    public struct Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public double GetDistanceSquare(Point p)
        {
            return ((this.x - p.x) * (this.x - p.x)) + ((this.y - p.y) * (this.y - p.y)) + ((this.z - p.z) * (this.z - p.z));
        }
    }
    
    public abstract class Shape
    {
        public int NoOfSides { get; set; }
        public int Dimension { get; set; }
        public string ShapeName { get; set; }
        public Point[] Coordinates { get; set; }
        public Point[][] Segments { get; set; }
        public Vector[] Edges { get; set; }
        public abstract bool IsShapeValid();
        public void PrintCoordinates()
        {
            if (!IsShapeValid())
                return;
            Console.Write(string.Format("{0}: ", ShapeName));
            for (int i = 0; i < NoOfSides; i++)
            {
                Console.Write(string.Format("({0},", Coordinates[i].x));
                Console.Write(Coordinates[i].y);
                if(Dimension == 3)
                {
                    Console.Write(string.Format(",{0}", Coordinates[i].z));
                }
                Console.Write(")");
            }
            Console.WriteLine();
        }
        public Shape(int dimension = 2)
        {
            Dimension = dimension;
        }
    }
    public class Rectangle: Shape
    {
        public Rectangle(int dimension = 2)
        {
            NoOfSides = 4;
            Dimension = dimension;
            ShapeName = "Rectangle";
            Coordinates = new Point[4];            
            Edges = new Vector[4];
            Segments = new Point[4][];
        }
        //TODO: modify to check for decimal values also
        //2D implementation
        public override bool IsShapeValid()
        {
            //all points should be distinct
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
            //no of distinct distances between any two vertices will be 3 for a rectangle and 2 for a square
            Dictionary<double, bool> sizes = new Dictionary<double, bool>();
            for (int i = 0; i < Coordinates.Length; i++)
            {
                for (int j = 0; j < Coordinates.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    sizes[Coordinates[i].GetDistanceSquare(Coordinates[j])] = true;
                }
            }            
            if (sizes.Keys.Count > 3 || sizes.Keys.Count < 2)
                return false;
            //check all possible order of given input for a valid rectangle
            //populate the edges accordingly
            if (IsOrderedRectangle(Coordinates[0], Coordinates[1], Coordinates[2], Coordinates[3]))
            {
                Edges[0] = new Vector(Coordinates[0], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[2]);
                Edges[2] = new Vector(Coordinates[2], Coordinates[3]);
                Edges[3] = new Vector(Coordinates[3], Coordinates[0]);

                Segments[0] = new Point[] { Coordinates[0], Coordinates[1] };
                Segments[1] = new Point[] { Coordinates[1], Coordinates[2] };
                Segments[2] = new Point[] { Coordinates[2], Coordinates[3] };
                Segments[3] = new Point[] { Coordinates[3], Coordinates[0] };

                return true;
            }
            if (IsOrderedRectangle(Coordinates[2], Coordinates[1], Coordinates[3], Coordinates[0]))
            {
                Edges[0] = new Vector(Coordinates[2], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[3]);
                Edges[2] = new Vector(Coordinates[3], Coordinates[0]);
                Edges[3] = new Vector(Coordinates[0], Coordinates[2]);

                Segments[0] = new Point[] { Coordinates[2], Coordinates[1] };
                Segments[1] = new Point[] { Coordinates[1], Coordinates[3] };
                Segments[2] = new Point[] { Coordinates[3], Coordinates[0] };
                Segments[3] = new Point[] { Coordinates[0], Coordinates[2] };

                return true;
            }
            if (IsOrderedRectangle(Coordinates[0], Coordinates[1], Coordinates[3], Coordinates[2]))
            {
                Edges[0] = new Vector(Coordinates[0], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[3]);
                Edges[2] = new Vector(Coordinates[3], Coordinates[2]);
                Edges[3] = new Vector(Coordinates[2], Coordinates[0]);

                Segments[0] = new Point[] { Coordinates[0], Coordinates[1] };
                Segments[1] = new Point[] { Coordinates[1], Coordinates[3] };
                Segments[2] = new Point[] { Coordinates[3], Coordinates[2] };
                Segments[3] = new Point[] { Coordinates[2], Coordinates[0] };

                return true;
            }
            return false;
        }
        //check if a,b,c,d in sequence form a rectnagle - i.e. edges will be ab, bc, cd, da
        //2D implementation
        private bool IsOrderedRectangle(Point a, Point b, Point c, Point d)
        {
            List<Point> points = new List<Point> { a, b, c, d};
            //check if orthogonal using Pythagros theorem
            double ab = points[0].GetDistanceSquare(points[1]);
            double bc = points[1].GetDistanceSquare(points[2]);
            double ac = points[0].GetDistanceSquare(points[2]);
            if(ac == ab+ bc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Trinagle : Shape
    {
        public Trinagle(int dimension = 2)
        {
            NoOfSides = 3;
            Dimension = dimension;
            ShapeName = "Triangle";
            Coordinates = new Point[3];
            Edges = new Vector[3];
            Segments = new Point[3][];
        }
        //2D implementation
        public override bool IsShapeValid()
        {
            //using shoelace formula; skipped multiplication by 0.5 to simplify; area of a traingle must be greater than 0
            double area = Coordinates[0].x * (Coordinates[1].y - Coordinates[2].y) + Coordinates[1].x * (Coordinates[2].y - Coordinates[0].y) + Coordinates[2].x * (Coordinates[0].y - Coordinates[1].y);
            if (area == 0)
                return false;
            else
            {
                Edges[0] = new Vector(Coordinates[0], Coordinates[1]);
                Edges[1] = new Vector(Coordinates[1], Coordinates[2]);
                Edges[2] = new Vector(Coordinates[2], Coordinates[0]);

                Segments[0] = new Point[] { Coordinates[0], Coordinates[1] };
                Segments[1] = new Point[] { Coordinates[1], Coordinates[2] };
                Segments[2] = new Point[] { Coordinates[2], Coordinates[0] };

                return true;
            }
        }
    }
}
