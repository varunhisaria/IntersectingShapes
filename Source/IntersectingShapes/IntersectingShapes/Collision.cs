using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    class Collision
    {
        public List<Shape> ProvidedShapes { get; set; }
        public bool[,] IsAnIntersectingShape { get; set; }
        public bool AnyIntersectionFound { get; set; }
        public void FindIntersecingShapes()
        {
            int totalNoOfShapes = ProvidedShapes.Count;
            IsAnIntersectingShape = new bool[totalNoOfShapes, totalNoOfShapes];
            AnyIntersectionFound = false;
            for (int i = 0; i < totalNoOfShapes; i++)
            {                
                for (int j = 0; j < totalNoOfShapes; j++)
                {
                    if(i == j)
                    {
                        IsAnIntersectingShape[i, j] = false;
                    }
                    else if (j > i)
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
        }
        private bool AreShapesIntersecting(Shape shape1, Shape shape2)
        {
            foreach (var edge in shape1.Edges)
            {
                Vector axis = new Vector(-edge.Y, edge.X);
                double minA = double.MaxValue, minB = double.MaxValue, maxA = double.MinValue, maxB = double.MinValue;
                ProjectShape(axis, shape1, ref minA, ref maxA);
                ProjectShape(axis, shape2, ref minB, ref maxB);
                if (!AreProjectionsOverrlapping(minA, maxA, minB, maxB))
                {
                    return false;
                }
            }
            return true;
        }
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
        public Collision()
        {
            ProvidedShapes = new List<Shape>();
        }
    }
}
