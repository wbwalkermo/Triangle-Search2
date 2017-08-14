# triangle-search

The following is a little coonsole app I threw together to solve a triangle coordinate problem and generates a set of triangles in a grid pattern.  You can run the utility by just entering the executable name (after building the .net console app written in C#):
 CoordTest.exe

This will generate a dump of all of the triangles in the grid (which is 6 row by 6 column grid with a pair of triangles each 10x10 pixels filling each cell).  Note that you can pass 3 pairs of x/y coords on the command line to specify the coordinates of a specific triangle and this utility will then do a lookup after the grid dump (telling you which triangle it is in the set - if it exists). Note also that the order you specify the coordinates is irrelevant if you want to do a lookup. 

For example:
 CoordTest.exe "10,10" "10,0" "0,0"
 CoordTest.exe "20,60" "20,50" "10,50"

[Note also that you can see sample output in the Misc folder].

Bill Walker 8/13/2017 
