using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flash.Tests {

    public class SimpleLensTest {

        public record TestPoint(int X, int Y);
        public record TestNamedPoint(TestPoint Point, string Name);

        [Fact]
        public void AggregateLensTest() {
            // Arrange
            var pointX =
                new Lens<TestPoint, int>(
                    Get: (point) => point.X,
                    Set: (i, point) => point with { X = i });

            var namedPointPoint =
                new Lens<TestNamedPoint, TestPoint>(
                    Get: (namedPoint) => namedPoint.Point,
                    Set: (point, namedPoint) => namedPoint with { Point = point });

            var namedPointX = namedPointPoint.Compose(pointX);

            var testPoint = new TestPoint(10, 20);
            var testNamedPoint = new TestNamedPoint(testPoint, "ten twenty");

            var expectedSet = new TestNamedPoint(new TestPoint(1020, 20), "ten twenty");

            Assert.Equal(10, pointX.Get(testPoint));
            Assert.Equal(testPoint, namedPointPoint.Get(testNamedPoint));
            Assert.Equal(10, namedPointX.Get(testNamedPoint));
            Assert.Equal(expectedSet, namedPointX.Set(1020, testNamedPoint));
        }

        [Fact]
        public void WithSyntaxTest() {
            var originalPoint = new TestPoint(100, 200);
            var originalNamed = new TestNamedPoint(originalPoint, "neat point");

            var expectedPoint = new TestPoint(1000, 200);
            var expectedNamed = new TestNamedPoint(expectedPoint, "neat point");

            var otherExpected = new TestNamedPoint(new TestPoint(2000, 200), "neat point");

            var actual = originalNamed with { Point = originalPoint with { X = 1000 } };

            var otherActual = 
                originalNamed with { 
                    Point = originalNamed.Point with { 
                        X = 2000 
                    } 
                };

            Assert.Equal(expectedNamed, actual);
            Assert.Equal(otherExpected, otherActual);
        }
    }
}
