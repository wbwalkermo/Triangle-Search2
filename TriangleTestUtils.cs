using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleCoordTest
{
    static class TriangleTestUtils
    {
        // dig out 3 pairs of points which define a triangle that is to be looked up
        public static Triad ProcessArgs(string[] args)
        {
            bool ok = false;
            Triad tmpShape = new Triad();
            int xCoord, yCoord, i = 0;

            if (args.Length == 3)
            {
                if ((args[i] != null) && (args[i].Length >= 3) && (args[i].IndexOf(",") > 0))
                {
                    if (ExtractPts(args[i], out xCoord, out yCoord))
                    {
                        i++;
                        tmpShape.StartingPt = new Point() { x = xCoord, y = yCoord };

                        if ((args[i] != null) && (args[i].Length >= 3) && (args[i].IndexOf(",") > 0))
                        {
                            if (ExtractPts(args[i], out xCoord, out yCoord))
                            {
                                i++;
                                tmpShape.MidPt = new Point() { x = xCoord, y = yCoord };

                                if ((args[i] != null) && (args[i].Length >= 3) && (args[i].IndexOf(",") > 0))
                                {
                                    if (ExtractPts(args[i], out xCoord, out yCoord))
                                    {
                                        tmpShape.EndingPt = new Point() { x = xCoord, y = yCoord };

                                        tmpShape.Label = "";
                                        ok = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (ok ? tmpShape : null);
        }


        // carve out the points from a string containing a pair of numbers separated by a delimeter
        private static bool ExtractPts(string flatPointSet, out int xCoord, out int yCoord, char sep=',')
        {
            bool ok = false;
            xCoord = -1;
            yCoord = -1;

            if (!string.IsNullOrEmpty(flatPointSet))
            {
                string[] parts = flatPointSet.Split(sep);

                if ((parts != null) && (parts.Length == 2))
                {
                    if (!string.IsNullOrEmpty(parts[0]) && (!string.IsNullOrEmpty(parts[1])))
                    {
                        int xVal, yVal;
                        if (int.TryParse(parts[0], out xVal) && (xVal >= 0))
                        {
                            if (int.TryParse(parts[1], out yVal) && (yVal >= 0))
                            {
                                ok = true;
                                xCoord = xVal;
                                yCoord = yVal;
                            }
                        }
                    }
                }
            }
            return ok;
        }


    }
}


