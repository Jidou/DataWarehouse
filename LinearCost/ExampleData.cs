using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace LinearCost {
    internal static class ExampleData {
        public static void Init() {
            Data.RectangleNames = new[] {"R1", "R2", "R3", "R4", "R5"};

            Data.Rectangles = new List<Rectangle> {
                new Rectangle(
                    "R1",
                    new Point(3, 2),
                    new Point(6, 2),
                    new Point(6, 4),
                    new Point(3, 4)
                ),
                new Rectangle(
                    "R2",
                    new Point(2, 7),
                    new Point(5, 7),
                    new Point(5, 11),
                    new Point(2, 11)
                ),
                new Rectangle(
                    "R3",
                    new Point(8, 6),
                    new Point(10, 6),
                    new Point(8, 9),
                    new Point(10, 9)
                ),
                new Rectangle(
                    "R4",
                    new Point(14, 9),
                    new Point(18, 9),
                    new Point(18, 14),
                    new Point(14, 14)
                ),
                new Rectangle(
                    "R5",
                    new Point(12, 2),
                    new Point(20, 2),
                    new Point(20, 8),
                    new Point(12, 8)
                )
            };

            Data.UnassignedRectangles = Data.Rectangles.ToArray().ToList();
            Data.SemiResults = new List<List<List<Rectangle>>> {
                new List<List<Rectangle>>(),
                new List<List<Rectangle>>()
            };
        }
    }
}
