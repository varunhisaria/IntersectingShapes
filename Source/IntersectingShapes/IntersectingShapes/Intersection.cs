using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    public class Intersection
    {
        public List<Shape> ProvidedShapes { get; set; }
        public bool[,] IsAnIntersectingShape { get; set; }
        public bool AnyIntersectionFound { get; set; }
        //populate IsAnIntersectingShape; it holds intersection for every pair of shapes
        public void FindIntersecingShapes(bool printOnConsole = false)
        {
            int totalNoOfShapes = ProvidedShapes.Count;
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
    }
}
