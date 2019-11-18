using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinearCost {
    class Program {

        private static readonly Random Random = new Random();

        static void Main(string[] args) {
            ExampleData.Init();

            LinearSplit();

            PrintDataForR();
        }

        private static void PrintDataForR() {
            var rectangleString = "Rectangle = c(";
            var pointString = "Point = c(";
            var xCoordString = "XCoord = c(";
            var yCoordString = "YCoord = c(";

           var pointNr = 1;

            foreach (var rectangle in Data.Rectangles) {
                pointNr = 1;
                foreach (var point in rectangle.PointsAsList) {
                    rectangleString += $"\"{rectangle.Name}\", ";
                    pointString += $"\"P{pointNr}\", ";
                    xCoordString += $"{point.X}, ";
                    yCoordString += $"{point.Y}, ";
                    pointNr++;
                }
            }

            foreach (var srSemiResults in Data.SemiResults) {
                foreach (var srSemiResult in srSemiResults) {

                    if (srSemiResult.Count == 1) {
                        continue;
                    }

                    var rectangleName = srSemiResult.Aggregate(string.Empty, (current, rectangle) => current + (rectangle.Name + " "));
                    var blah = CalculatePointsOfNewRectangle(srSemiResult, rectangleName.TrimEnd());
                        pointNr = 1;
                        foreach (var point in blah.PointsAsList) {
                            rectangleString += $"\"{blah.Name}\", ";
                            pointString += $"\"P{pointNr}\", ";
                            xCoordString += $"{point.X}, ";
                            yCoordString += $"{point.Y}, ";
                            pointNr++;
                    }
                }
            }

            rectangleString = rectangleString.TrimEnd(',', ' ') + ")";
            pointString = pointString.TrimEnd(',', ' ') + ")";
            xCoordString = xCoordString.TrimEnd(',', ' ') + ")";
            yCoordString = yCoordString.TrimEnd(',', ' ') + ")";

            Console.WriteLine(rectangleString);
            Console.WriteLine(pointString);
            Console.WriteLine(xCoordString);
            Console.WriteLine(yCoordString);
        }


        private static int CalculateAreaOfNewRectangle(List<Rectangle> rectangles) {
            var tmpRectangle = CalculatePointsOfNewRectangle(rectangles, "TMP");
            return CalculateArea(tmpRectangle.P1, tmpRectangle.P2, tmpRectangle.P3, tmpRectangle.P4);
        }


        private static int CalculateAreaDiff(Rectangle rectangle1, Rectangle rectangle2) {
            var areaCombined = CalculateArea(Rectangle.FindMinXAndMinY(rectangle1, rectangle2),
                Rectangle.FindMaxXAndMinY(rectangle1, rectangle2), Rectangle.FindMaxXAndMaxY(rectangle1, rectangle2),
                Rectangle.FindMinXAndMaxY(rectangle1, rectangle2));

            var areaRectangle1 = CalculateArea(rectangle1.FindMinXAndMinY(), rectangle1.FindMaxXAndMinY(),
                rectangle1.FindMaxXAndMaxY(),
                rectangle1.FindMinXAndMaxY());

            var areaRectangle2 = CalculateArea(rectangle2.FindMinXAndMinY(), rectangle2.FindMaxXAndMinY(),
                rectangle2.FindMaxXAndMaxY(),
                rectangle2.FindMinXAndMaxY());

            return areaCombined - areaRectangle1 - areaRectangle2;
        }


        private static int CalculateArea(Point point1, Point point2, Point point3, Point point4) {
            var minX = Math.Min(Math.Min(Math.Min(point1.X, point2.X), point3.X), point4.X);
            var maxX = Math.Max(Math.Max(Math.Max(point1.X, point2.X), point3.X), point4.X);
            var minY = Math.Min(Math.Min(Math.Min(point1.Y, point2.Y), point3.Y), point4.Y);
            var maxY = Math.Max(Math.Max(Math.Max(point1.Y, point2.Y), point3.Y), point4.Y);
            return (maxX - minX) * (maxY - minY);
        }


        private static void LinearSplit() {
            var seed = PickSeeds();
            Console.WriteLine($"SR1:{{{seed.Item1.Name}}}, SR2:{{{seed.Item2.Name}}}");

            Data.UnassignedRectangles.Remove(seed.Item1);
            Data.UnassignedRectangles.Remove(seed.Item2);

            var sr1 = new List<Rectangle>{seed.Item1 };
            var sr2 = new List<Rectangle>{seed.Item2 };

            Data.SemiResults[0].Add(sr1.ToList());
            Data.SemiResults[1].Add(sr2.ToList());

            do {
                var nextRectangle = PickNext();

                var newSr1 = new List<Rectangle>(sr1) {nextRectangle};
                var newSr2 = new List<Rectangle>(sr2) {nextRectangle};

                var newSr1Area = CalculateAreaOfNewRectangle(newSr1);
                var oldSr1Area = CalculateAreaOfNewRectangle(sr1);
                var newSr2Area = CalculateAreaOfNewRectangle(newSr2);
                var oldSr2Area = CalculateAreaOfNewRectangle(sr2);

                if (Data.UnassignedRectangles.Count == 1) {
                    if (sr1.Count == 1) {
                        sr1.Add(nextRectangle);
                        Data.SemiResults[0].Add(sr1.ToList());
                    } else if (sr2.Count == 1) {
                        sr2.Add(nextRectangle);
                        Data.SemiResults[1].Add(sr2.ToList());
                    } else {
                        if (newSr1Area - oldSr1Area > newSr2Area - oldSr2Area) {
                            sr2.Add(nextRectangle);
                            Data.SemiResults[1].Add(sr2.ToList());
                        } else {
                            sr1.Add(nextRectangle);
                            Data.SemiResults[0].Add(sr1.ToList());
                        }
                    }
                } else {
                    if (newSr1Area - oldSr1Area > newSr2Area - oldSr2Area) {
                        sr2.Add(nextRectangle);
                        Data.SemiResults[1].Add(sr2.ToList());
                    } else {
                        sr1.Add(nextRectangle);
                        Data.SemiResults[0].Add(sr1.ToList());
                    }
                }

                Data.UnassignedRectangles.Remove(nextRectangle);

                Console.WriteLine($"SR1:{{{PrintRectanglesInSr(sr1)}}}, SR2:{{{PrintRectanglesInSr(sr2)}}}");
            } while (Data.UnassignedRectangles.Count > 0);
        }


        private static string PrintRectanglesInSr(List<Rectangle> sr1) {
            var allNames = sr1.Aggregate(string.Empty, (current, rectangle) => current + (rectangle.Name + ","));
            return allNames.TrimEnd(',');
        }


        private static Rectangle PickNext() {
            return Data.UnassignedRectangles[Random.Next(Data.UnassignedRectangles.Count - 1)];
        }


        private static Tuple<Rectangle, Rectangle> PickSeeds() {
            var absoluteMinX = int.MaxValue;
            var absoluteMaxX = int.MinValue;
            var absoluteMinY = int.MaxValue;
            var absoluteMaxY = int.MinValue;

            var minX = new int[5];
            var maxX = new int[5];
            var minY = new int[5];
            var maxY = new int[5];
            for (var i = 0; i < Data.Rectangles.Count; i++) {
                minX[i] = Data.Rectangles[i].FindMinX();
                maxX[i] = Data.Rectangles[i].FindMaxX();
                minY[i] = Data.Rectangles[i].FindMinY();
                maxY[i] = Data.Rectangles[i].FindMaxY();
            }

            var maxMinX = int.MinValue;
            var minMaxX = int.MaxValue;
            var maxMinY = int.MinValue;
            var minMaxY = int.MaxValue;

            Rectangle maxMinXRect = null;
            Rectangle minMaxXRect = null;
            Rectangle maxMinYRect = null;
            Rectangle minMaxYRect = null;

            for (var i = 0; i < minX.Length; i++) {
                if (maxMinX <= minX[i]) {
                    maxMinX = minX[i];
                    maxMinXRect = Data.Rectangles[i];
                }

                if (minMaxX >= maxX[i]) {
                    minMaxX = maxX[i];
                    minMaxXRect = Data.Rectangles[i];
                }

                if (maxMinY <= minY[i]) {
                    maxMinY = minY[i];
                    maxMinYRect = Data.Rectangles[i];
                }

                if (minMaxY >= maxY[i]) {
                    minMaxY = maxY[i];
                    minMaxYRect = Data.Rectangles[i];
                }

                if (absoluteMinX >= minX[i]) {
                    absoluteMinX = minX[i];
                }

                if (absoluteMaxX >= maxX[i]) {
                    absoluteMaxX = maxX[i];
                }

                if (absoluteMinY >= minY[i]) {
                    absoluteMinY = minY[i];
                }

                if (absoluteMaxY >= maxY[i]) {
                    absoluteMaxY = maxY[i];
                }
            }

            var xDimWith = absoluteMaxX - absoluteMinX;
            var yDimWith = absoluteMaxY - absoluteMinY;

            var diffX = maxMinX - minMaxX;
            var diffY = maxMinY - minMaxY;

            var xNorm = diffX / (double) (xDimWith);
            var yNorm = diffY / (double) (yDimWith);

            Console.WriteLine($"Dimension | min(max(.)) | max(min(.)) | Diff | Norm |");
            Console.WriteLine(
                $" x | {minMaxX}({minMaxXRect.Name}) | {maxMinX}({maxMinXRect.Name}) | {diffX} | {xNorm} |");
            Console.WriteLine(
                $" y | {minMaxY}({minMaxYRect.Name}) | {maxMinY}({maxMinYRect.Name}) | {diffY} | {yNorm} |");

            if (xNorm >= yNorm) {
                return new Tuple<Rectangle, Rectangle>(minMaxXRect, maxMinXRect);
            } else {
                return new Tuple<Rectangle, Rectangle>(minMaxYRect, maxMinYRect);
            }
        }


        private static Rectangle CalculatePointsOfNewRectangle(List<Rectangle> subRectangles, string nameOfNewRectangle) {
            var absoluteMinX = int.MaxValue;
            var absoluteMaxX = int.MinValue;
            var absoluteMinY = int.MaxValue;
            var absoluteMaxY = int.MinValue;

            foreach (var rectangle in subRectangles) {
                var minX = rectangle.FindMinX();
                var maxX = rectangle.FindMaxX();
                var minY = rectangle.FindMinY();
                var maxY = rectangle.FindMaxY();

                if (absoluteMinX >= minX) {
                    absoluteMinX = minX;
                }

                if (absoluteMaxX <= maxX) {
                    absoluteMaxX = maxX;
                }

                if (absoluteMinY >= minY) {
                    absoluteMinY = minY;
                }

                if (absoluteMaxY <= maxY) {
                    absoluteMaxY = maxY;
                }
            }

            return new Rectangle(nameOfNewRectangle, new Point(absoluteMinX, absoluteMinY),
                new Point(absoluteMaxX, absoluteMinY), new Point(absoluteMaxX, absoluteMaxY),
                new Point(absoluteMinX, absoluteMaxY));
        }
    }
}
