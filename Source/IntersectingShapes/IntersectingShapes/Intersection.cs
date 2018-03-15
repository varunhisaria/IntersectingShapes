using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    public class Intersection
    {
        private const int axisX = 101;
        private const int axisY = 21;        
        private int minX, minY, maxX, maxY;
        private ConsoleColor[,] px = new ConsoleColor[axisY, axisX];
        public List<Shape> ProvidedShapes { get; set; }
        public bool[,] IsAnIntersectingShape { get; set; }
        private bool[] IntersectingIndex { get; set; }
        public bool AnyIntersectionFound { get; set; }
        //populate IsAnIntersectingShape; it holds intersection for every pair of shapes
        public void FindIntersecingShapes(bool printOnConsole = false)
        {            
            int totalNoOfShapes = ProvidedShapes.Count;
            IntersectingIndex = new bool[totalNoOfShapes];
            IsAnIntersectingShape = new bool[totalNoOfShapes, totalNoOfShapes];
            AnyIntersectionFound = false;
            //checking intersection for every pair of shapes
            for (int i = 0; i < totalNoOfShapes; i++)
            {
                for (int j = 0; j < totalNoOfShapes; j++)
                {
                    if(i == j)
                    {
                        IsAnIntersectingShape[i, j] = false;
                    }
                    else if (j > i) //need to check for upper diagonal matrix only
                    {
                        IsAnIntersectingShape[i, j] = AreShapesIntersecting(ProvidedShapes[i], ProvidedShapes[j]);
                        if (IsAnIntersectingShape[i, j])
                        {
                            IntersectingIndex[i] = true;
                            IntersectingIndex[j] = true;
                            AnyIntersectionFound = true;
                        }
                    }
                    else
                    {
                        IsAnIntersectingShape[i, j] = IsAnIntersectingShape[j, i];
                    }
                }
            }
            //print result
            if(printOnConsole)
                PrintIntersections();
        }
        //check if given two shapes intersect, will work for any pair of convex polygons
        //2D implementation
        private bool AreShapesIntersecting(Shape shape1, Shape shape2)
        {
            Vector[] allEdges = new Vector[shape1.Edges.Length + shape2.Edges.Length];
            Array.Copy(shape1.Edges, allEdges, shape1.Edges.Length);
            Array.Copy(shape2.Edges, 0, allEdges, shape1.Edges.Length, shape2.Edges.Length);
            foreach (var edge in allEdges)
            {
                //perpedicular axis to edge in 2D
                Vector axis = new Vector(-edge.Y, edge.X);
                double minA = double.MaxValue, minB = double.MaxValue, maxA = double.MinValue, maxB = double.MinValue;
                //get min and max of projections of vertices on the perpendicular axis obtained above
                ProjectShape(axis, shape1, ref minA, ref maxA);
                ProjectShape(axis, shape2, ref minB, ref maxB);
                if (!AreProjectionsOverrlapping(minA, maxA, minB, maxB))
                {
                    return false;
                }
            }
            return true;
        }
        //get min and max of projections of vertices on the perpendicular axis
        private void ProjectShape(Vector axis, Shape shape, ref double min, ref double max)
        {
            for (int i = 0; i < shape.Coordinates.Length; i++)
            {
                double dotProduct = axis.DotProduct(shape.Coordinates[i]);
                if (dotProduct < min)
                {
                    min = dotProduct;
                }
                if (dotProduct > max)
                {
                    max = dotProduct;
                }
            }
        }
        //check if min and max projections of two shapes are overlapping
        private bool AreProjectionsOverrlapping(double minA, double maxA, double minB, double maxB)
        {
            double gap;
            if (minA < minB)
            {
                gap = minB - maxA;
            }
            else
            {
                gap = minA - maxB;
            }
            if (gap < 0.0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void PrintIntersections()
        {
            for (int i = 0; i < ProvidedShapes.Count; i++)
            {
                bool flag = false;
                Console.Write("Shapes intersecting with shape {0}: ", i + 1);
                for (int j = 0; j < ProvidedShapes.Count; j++)
                {
                    if (IsAnIntersectingShape[i, j])
                    {
                        Console.Write("{0} ", j + 1);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    Console.Write("No intersections!");
                }
                Console.WriteLine();
            }
        }
        public Intersection()
        {
            ProvidedShapes = new List<Shape>();
        }
        public void Draw()
        {
            FindMinMax();
            if (!(minX >= -(axisX / 2) && maxX <= (axisX / 2) && minY >= -(axisY / 2) && maxY <= (axisY / 2)))
            {
                Console.WriteLine("This range of coordinates cannot be plotted");
                return;
            }
            for (int i = 0; i < axisY; i++)
            {
                for (int j = 0; j < axisX; j++)
                {
                    px[i, j] = Console.BackgroundColor;
                }
            }
            FindIntersecingShapes();
            for (int i = 0; i < ProvidedShapes.Count; i++)
            {
                ConsoleColor color = ConsoleColor.White;
                if (IntersectingIndex[i])
                {
                    color = ConsoleColor.Red;
                }
                for (int j = 0; j < ProvidedShapes[i].Segments.Length; j++)
                {
                    DrawLineSegment((int)ProvidedShapes[i].Segments[j][0].x, (int)ProvidedShapes[i].Segments[j][0].y, (int)ProvidedShapes[i].Segments[j][1].x, (int)ProvidedShapes[i].Segments[j][1].y, color);
                }
            }
            DrawOnConsole();
        }
        private void FindMinMax()
        {
            minX = int.MaxValue;
            maxX = int.MinValue;
            minY = int.MaxValue;
            maxY = int.MinValue;
            for (int i = 0; i < this.ProvidedShapes.Count; i++)
            {
                for (int j = 0; j < ProvidedShapes[i].Coordinates.Length; j++)
                {
                    if (minX > ProvidedShapes[i].Coordinates[j].x)
                        minX = (int)(ProvidedShapes[i].Coordinates[j].x);
                    if (maxX < ProvidedShapes[i].Coordinates[j].x)
                        maxX = (int)(ProvidedShapes[i].Coordinates[j].x);
                    if (minY > ProvidedShapes[i].Coordinates[j].y)
                        minY = (int)(ProvidedShapes[i].Coordinates[j].y);
                    if (maxY < ProvidedShapes[i].Coordinates[j].y)
                        maxY = (int)(ProvidedShapes[i].Coordinates[j].y);
                }
            }
        }
        private void DrawLineSegment(int x1, int y1, int x2, int y2, ConsoleColor color)
        {
            int cfactor = ((x2 - x1) * y1) - ((y2 - y1) * x1);
            for (int i = Math.Min(y1, y2); i <= Math.Max(y1, y2); i++)
            {
                for (int j = Math.Min(x1, x2); j <= Math.Max(x1, x2); j++)
                {
                    if (((x2 - x1) * i) == (((y2 - y1) * j) + (cfactor)))
                    {
                        px[(i+10), (j+50)] = color;
                    }
                }
            }
        }
        private void DrawOnConsole()
        {
            for (int i = axisY-1; i >= 0; i--)
            {
                for (int j = 0; j < axisX; j++)
                {
                    Console.ForegroundColor = px[i, j];
                    Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
