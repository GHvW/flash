using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flash.Tests {

    public class SimpleLensTest {

        public record TestPoint(int X, int Y);
        public record TestNamedPoint(TestPoint Point, string Color);

        [Fact]
        public void AggregateLensTest() {
            // Arrange
            var pointX =
                new Lens<TestPoint, int>(
                    (point) => point.X,
                    (i, point) => point with { X = i });

            var namedPointPoint =
                new Lens<TestNamedPoint, TestPoint>(
                    (namedPoint) => namedPoint.Point,
                    (point, namedPoint) => namedPoint with { Point = point });

            var namedPointX = namedPointPoint.Compose(pointX);

            var testPoint = new TestPoint(10, 20);
            var testNamedPoint = new TestNamedPoint(testPoint, "ten twenty");

            Assert.Equal(10, pointX.Get(testPoint));
            Assert.Equal(testPoint, namedPointPoint.Get(testNamedPoint));
            Assert.Equal(10, namedPointX.Get(testNamedPoint));
        }
    }
}
