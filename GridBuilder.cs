using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleCoordTest
{
    public class GridBuilder
    {
        private int _maxRows;
        private int _maxCols;
        List<Triad> _grid = null;

        public GridBuilder(int maxRows=10, int maxCols=10)
        {
            _maxRows = maxRows;
            _maxCols = maxCols;
            _grid = new List<Triad>();

            Load();
        }

        private void Load(int shapeWidth=10, int shapeHeight=10)
        {
            for (int curRow = 0; curRow < _maxRows; curRow++)
            {
                string rowLabel = RowLabelBuilder(curRow + 1);
                int seqNo = 1;

                for (int curCol = 0; curCol < _maxCols; curCol++)
                {
                    int baseX = curCol * shapeWidth;
                    int baseY = curRow * shapeHeight;

                    string firstColLabel = seqNo.ToString();
                    seqNo++;
                    Triad shape1 = new Triad();
                    shape1.StartingPt = GetStartPointCoords(baseX, baseY, curRow, curCol, true);
                    shape1.MidPt = GetMidPointCoords(baseX, baseY, curRow, curCol, true);
                    shape1.EndingPt = GetEndingPointCoords(baseX, baseY, curRow, curCol, true);
                    shape1.Label = rowLabel + firstColLabel;
                    _grid.Add(shape1);

                    string secondColLabel = (seqNo).ToString();
                    seqNo++;
                    Triad shape2 = new Triad();
                    shape2.StartingPt = GetStartPointCoords(baseX, baseY, curRow, curCol, false);
                    shape2.MidPt = GetMidPointCoords(baseX, baseY, curRow, curCol, false);
                    shape2.EndingPt = GetEndingPointCoords(baseX, baseY, curRow, curCol, false);
                    shape2.Label = rowLabel + secondColLabel;
                    _grid.Add(shape2);

                }
            }
        }


        public Triad FindPointSet(Triad triangleToFind)
        {
            Triad foundShape = null;

            if (VerifyUniquePts(triangleToFind))
            {
                foreach (Triad triangle in _grid)
                {
                    bool isPtAPresent = TriangleContainsPt(triangle, triangleToFind.StartingPt);
                    bool isPtBPresent = TriangleContainsPt(triangle, triangleToFind.MidPt);
                    bool isPtCPresent = TriangleContainsPt(triangle, triangleToFind.EndingPt);

                    if (isPtAPresent && isPtBPresent && isPtCPresent)
                    {
                        foundShape = new Triad()
                        {
                            StartingPt = triangle.StartingPt,
                            MidPt = triangle.MidPt,
                            EndingPt = triangle.EndingPt,
                            Label = triangle.Label
                        };
                        break;
                    }
                }
            }

            return foundShape;
        }


        // convert a row label from 1..n to A..Z
        // NOTE: if we ever have rows beyond 26 then we should probably go to an AA..ZZ type format (but no need for now)
        public static string RowLabelBuilder(int rowNum)
        {
            string result = "";
            char charLabel = (char)((int)'A' + (rowNum - 1));
            return result + charLabel;
        }


        private static Point GetStartPointCoords(int baseX, int baseY, int row, int col, bool isOdd, int shapeWidth=10, int shapeHeight=10)
        {
            Point result = new Point();
            result.x = baseX;
            result.y = baseY;
            return result;
        }


        private static Point GetMidPointCoords(int baseX, int baseY, int row, int col, bool isOdd, int shapeWidth=10, int shapeHeight=10)
        {
            Point result = new Point();

            if (isOdd)
            {
                result.x = baseX;       // only change height
                result.y = baseY + shapeHeight;
            }
            else
            {
                result.x = baseX + shapeWidth;
                result.y = baseY;       // only change width
            }

            return result;
        }


        private static Point GetEndingPointCoords(int baseX, int baseY, int row, int col, bool isOdd, int shapeWidth=10, int shapeHeight=10)
        {
            Point result = new Point();
            result.x = baseX + shapeWidth;
            result.y = baseY + shapeHeight;
            return result;
        }



        private static bool TriangleContainsPt(Triad triangle, Point pt)
        {
            return CoordsMatch(triangle.StartingPt, pt) || CoordsMatch(triangle.MidPt, pt) || CoordsMatch(triangle.EndingPt, pt);
        }


        private static bool VerifyUniquePts(Triad triangle)
        {
            bool dupe1 = CoordsMatch(triangle.StartingPt, triangle.MidPt);
            bool dupe2 = CoordsMatch(triangle.StartingPt, triangle.EndingPt);
            bool dupe3 = CoordsMatch(triangle.MidPt, triangle.EndingPt);

            return !dupe1 && !dupe2 && !dupe3;
        }


        private static bool CoordsMatch(Point firstPt, Point secondPt)
        {
            return (firstPt.x == secondPt.x) && (firstPt.y == secondPt.y);
        }


#region dump utilities

        public void Lookup(Triad triangle)
        {
            Console.WriteLine("\n\nLooking up triangle: " +
                            " (" + triangle.StartingPt.x.ToString() + "," + triangle.StartingPt.y.ToString() + ")," +
                            " (" + triangle.MidPt.x.ToString() + "," + triangle.MidPt.y.ToString() + ")," +
                            " (" + triangle.EndingPt.x.ToString() + "," + triangle.EndingPt.y.ToString() + ")");

            if (!VerifyUniquePts(triangle))
            {
                Console.WriteLine("Warning: the points entered are not three unique set of coordinates (so no search will be done)");
            }
            else
            {
                Triad foundShape = FindPointSet(triangle);

                if (foundShape == null)
                    Console.WriteLine("Triangle not found!");
                else
                    Console.WriteLine("Triangle found: " + foundShape.Label);
            }
        }


        public void GridDump()
        {
            Console.WriteLine("Dump of triangles");

            if ((_grid == null) || (_grid.Count == 0))
                Console.WriteLine("Triangle grid is empty");
            else
            {
                foreach (Triad triangle in _grid)
                {
                    DumpTriangle(triangle);
                }
            }
        }


        private static void DumpTriangle(Triad triangle)
        {
            Console.WriteLine("Triangle: " + triangle.Label +
                            " (" + triangle.StartingPt.x.ToString() + "," + triangle.StartingPt.y.ToString() + ")," +
                            " (" + triangle.MidPt.x.ToString() + "," + triangle.MidPt.y.ToString() + ")," +
                            " (" + triangle.EndingPt.x.ToString() + "," + triangle.EndingPt.y.ToString() + ")");
        }

#endregion

    }
}


