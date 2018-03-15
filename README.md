# IntersectingShapes
A Console Application in C# to identify intersecting convex polygons.

# About running the application
The executable under the Demo folder can be used to directly run the application. Simply download the application and execute it. It is a menu driven application.

It provides options to - 
1. Add a shape
2. Delete a shape
3. Check intersecion of added shapes
4. Restart
0. Exit

At any time, all the added shapes can be seen at the top of the Console Window.

Source folder contains the Visual Studio solution file. The application have been developed using 'Microsoft Visual Studio Professional 2015' and '.NET Framework 4.6'.

# A detailed insight
The application supports 2D convex polygons. As of now, it supports triangles and rectangles(even rotated ones) and can be easily extended to suppport any other convex ploygon. 'Separating Axis Test' has been used to check if there is an intersection between the given polygons. Although the application is implemented to support 2D convex polygons, it has the scope to be extended to suppport 3D shapes as well. The current implementation works well for integer-valued coordinaets. The decimal-valued coordinates might not work as expected as of now. ‘Intersection’ is defined as area overlap(as opposed to edge). Two common-edged shapes with no overlapping area have been considered not to intersect each other. Access modifiers used have been chosen such that the application can be later used as an assembly to be integerated with other applications.
