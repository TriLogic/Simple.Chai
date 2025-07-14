using Simple.Chai;

namespace Simple.ChaiTests
{


    public class AssertionTests
    {
        [Test]
        public void TestEquality()
        {
            Expect.That(42).To.Equal(42);
            Expect.That("foo").Not.Equal("bar");
        }

        [Test]
        public void TestTruthiness()
        {
            Expect.That(true).To.Be.True();
            Expect.That(false).To.Be.False();
            Expect.That("yes").To.Be.Ok();
            Expect.That("").To.Not.Be.Ok();
        }

        [Test]
        public void TestNull()
        {
            Expect.That<object>(null).To.Be.Null();
            Expect.That("something").To.Not.Be.Null();
        }

        [Test]
        public void TestLength()
        {
            Expect.That("abc").To.Have.LengthOf(3);
            Expect.That(new List<int>()).To.Be.Empty();
        }

        [Test]
        public void TestCollections()
        {
            var items = new[] { 1, 2, 3 };
            Expect.That(items).To.Include(2);
            Expect.That(items).To.Have.Members(new[] { 1, 2 });
        }

        [Test]
        public void TestCloseTo()
        {
            Expect.That(9.8).To.Be.CloseTo(10.0, 0.5);
            Expect.That(105).To.Be.WithinPercent(100, 5);
            Expect.That(95).To.Be.WithinPercent(100, 5);
        }

        /// <summary>
        /// *
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        [Test]
        public void TestThrows()
        {
            Action throwingAction = () => throw new InvalidOperationException("bad");

            Expect.That(throwingAction).To.Throws<InvalidOperationException>();
            Expect.That(throwingAction).To.Throws<InvalidOperationException>("bad");
        }
        //*/

        [Test]
        public void TestProperty()
        {
            var obj = new { Name = "Alice", Age = 30 };
            Expect.That(obj).To.Have.Property("Name");
        }
    }
}