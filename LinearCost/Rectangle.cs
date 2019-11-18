using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LinearCost {
    internal class Rectangle {
        public string Name { get; set; }
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }
        public Point P4 { get; set; }

        public List<Point> PointsAsList =>
            new List<Point> {
                P1, P2, P3, P4
            };


        public Rectangle(string name, Point p1, Point p2, Point p3, Point p4) {
            Name = name;
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }


        public static Point FindMinXAndMinY(Rectangle rectangle1, Rectangle rectangle2) {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            var allPoints = rectangle1.PointsAsList;
            allPoints.AddRange(rectangle2.PointsAsList);

            foreach (var point in allPoints) {
                if (minimal.X >= point.X && minimal.Y >= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public static Point FindMinXAndMaxY(Rectangle rectangle1, Rectangle rectangle2) {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            var allPoints = rectangle1.PointsAsList;
            allPoints.AddRange(rectangle2.PointsAsList);

            foreach (var point in allPoints) {
                if (minimal.X >= point.X && minimal.Y <= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public static Point FindMaxXAndMinY(Rectangle rectangle1, Rectangle rectangle2) {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            var allPoints = rectangle1.PointsAsList;
            allPoints.AddRange(rectangle2.PointsAsList);

            foreach (var point in allPoints) {
                if (minimal.X <= point.X && minimal.Y >= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public static Point FindMaxXAndMaxY(Rectangle rectangle1, Rectangle rectangle2) {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            var allPoints = rectangle1.PointsAsList;
            allPoints.AddRange(rectangle2.PointsAsList);

            foreach (var point in allPoints) {
                if (minimal.X <= point.X && minimal.Y <= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public Point FindMinXAndMinY() {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            foreach (var point in PointsAsList) {
                if (minimal.X >= point.X && minimal.Y >= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public Point FindMinXAndMaxY() {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            foreach (var point in PointsAsList) {
                if (minimal.X >= point.X && minimal.Y <= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public Point FindMaxXAndMinY() {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            foreach (var point in PointsAsList) {
                if (minimal.X <= point.X && minimal.Y >= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public Point FindMaxXAndMaxY() {
            var minimal = new Point(int.MaxValue, int.MaxValue);

            foreach (var point in PointsAsList) {
                if (minimal.X <= point.X && minimal.Y <= point.Y) {
                    minimal = point;
                }
            }

            return minimal;
        }


        public int FindMaxX() {
            return PointsAsList.Max(x => x.X);
        }


        public int FindMinX() {
            return PointsAsList.Min(x => x.X);
        }


        public int FindMaxY() {
            return PointsAsList.Max(x => x.Y);
        }


        public int FindMinY() {
            return PointsAsList.Min(x => x.Y);
        }
    }
}
