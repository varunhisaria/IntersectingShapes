using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectingShapes
{
    class Program
    {
        static Intersection intersection;
        static void Main(string[] args)
        {
            try
            {
                intersection = new Intersection();
                string input;
                do
                {
                    Console.Clear();
                    if (intersection.ProvidedShapes.Count > 0)
                    {
                        Console.WriteLine("Added shapes:");
                        for (int i = 0; i < intersection.ProvidedShapes.Count; i++)
                        {
                            Console.Write("Shape {0} -> ", i + 1);
                            intersection.ProvidedShapes[i].PrintCoordinates();
                        }
                        Console.WriteLine("----------------------------------------");
                    }
                    Console.WriteLine("1. Add a shape");
                    if (intersection.ProvidedShapes.Count > 0)
                        Console.WriteLine("2. Delete a shape");
                    if (intersection.ProvidedShapes.Count > 1)
                        Console.WriteLine("3. Check intersecion of added shapes");
                    if (intersection.ProvidedShapes.Count > 0)
                        Console.WriteLine("4. Restart");
                    Console.WriteLine("0. Exit");
                    Console.Write("Please enter your choice: ");
                    input = Console.ReadLine();
                    int choice;
                    if (int.TryParse(input, out choice))
                    {
                        switch (choice)
                        {
                            case 0: //Exit
                                break;
                            case 1: //Add shape
                                InputShape();
                                break;
                            case 2: //Delete shape
                                if(intersection.ProvidedShapes.Count > 0)
                                    DeleteShape();
                                break;
                            case 3: //check intersection
                                if(intersection.ProvidedShapes.Count > 1)
                                    intersection.FindIntersecingShapes(true);
                                break;
                            case 4: //Reset
                                if (intersection.ProvidedShapes.Count > 0)
                                    intersection.ProvidedShapes.Clear();
                                break;
                            default:
                                Console.WriteLine("Invalid choice! Please try again.");
                                break;
                        }
                        if (choice == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice! Please try again.");
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                } while (true);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Oops! Something went wrong. Share the error message with us so that we can improve the application.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadLine();
            }
        }
        static void InputShape()
        {
            Shape shape = new Rectangle();
            string input;
            bool isChoiceValid = false;
            Console.WriteLine("1. 2D Rectangle");
            Console.WriteLine("2. 2D Triangle");
            Console.Write("Please select a shape: ");
            input = Console.ReadLine();
            int choice;
            if (int.TryParse(input, out choice))
            {
                switch (choice)
                {
                    case 1:
                        isChoiceValid = true;
                        shape = new Rectangle(2);
                        break;
                    case 2:
                        isChoiceValid = true;
                        shape = new Trinagle(2);
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
                if (isChoiceValid)
                {
                    var isInputSuccess = InputCoordinates(ref shape);
                    if (isInputSuccess)
                    {
                        Console.WriteLine(string.Format("{0} added successfully.", shape.ShapeName));
                        intersection.ProvidedShapes.Add(shape);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("The provided coordinates do not form a {0}", shape.ShapeName));
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid choice! Please try again.");
            }
        }
        static void DeleteShape()
        {
            string input;
            Console.Write("Enter the index of the shape to be deleted: ");
            input = Console.ReadLine();
            int index;
            if (int.TryParse(input, out index))
            {
                if(index > 0 && index <= intersection.ProvidedShapes.Count)
                {
                    intersection.ProvidedShapes.RemoveAt(index - 1);
                    Console.WriteLine("Shape deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid index.");
                }
            }
            else
            {
                Console.WriteLine("Invalid index! Please try again.");
            }
        }
        static bool InputCoordinates(ref Shape shape)
        {
            string input;
            double providedCoordinate;
            for (int i = 0; i < shape.NoOfSides; i++)
            {
                Console.Write(string.Format("Enter the x-coordinate of the vertex {0}: ", i + 1));
                input = Console.ReadLine();
                if (!double.TryParse(input, out providedCoordinate))
                    return false;
                shape.Coordinates[i].x = providedCoordinate;
                Console.Write(string.Format("Enter the y-coordinate of the vertex {0}: ", i + 1));
                input = Console.ReadLine();
                if (!double.TryParse(input, out providedCoordinate))
                    return false;
                shape.Coordinates[i].y = providedCoordinate;
                if(shape.Dimension == 3)
                {
                    Console.Write(string.Format("Enter the z-coordinate of the vertex {0}: ", i + 1));
                    input = Console.ReadLine();
                    if (!double.TryParse(input, out providedCoordinate))
                        return false;
                    shape.Coordinates[i].z = providedCoordinate;
                }
                else
                {
                    shape.Coordinates[i].z = 0;
                }
            }
            if (shape.IsShapeValid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }        
    }
}
