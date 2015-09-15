using System;
using System.Diagnostics;

/*
The general result seems to be that abstract/virtual incur the same amount of overhead.
Hiding a non-virtual method doesn't seem to impact performance.
*/

namespace MethodPerformanceTest
{
    internal class Program
    {
        private const int TestIterations = 1000000000;

        private static readonly Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
#region Setup
            InterfaceTesterImpl directInterfaceTester = new InterfaceTesterImpl();
            IInterfaceTester indirectInterfaceTester = new InterfaceTesterImpl();

            AbstractTesterImpl directAbstractTester = new AbstractTesterImpl();
            AbstractTester indirectAbstractTester = new AbstractTesterImpl();

            VirtualTester baseVirtualTester = new VirtualTester();
            VirtualTesterImpl directVirtualTester = new VirtualTesterImpl();
            VirtualTester indirectVirtualTester = new VirtualTesterImpl();
#endregion

            Console.WriteLine("Running tests, this may take a while...");

            long concreteTime = TestConcrete();

#region Interface
            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                directInterfaceTester.Method();
            }
            long directInterfaceTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                indirectInterfaceTester.Method();
            }
            long indirectInterfaceTime = stopwatch.ElapsedMilliseconds;
#endregion

#region Abstract
            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                directAbstractTester.Method();
            }
            long directAbstractTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                indirectAbstractTester.Method();
            }
            long indirectAbstractTime = stopwatch.ElapsedMilliseconds;
#endregion

#region Virtual
            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                baseVirtualTester.Method();
            }
            long baseVirtualTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                baseVirtualTester.Method2();
            }
            long baseVirtual2Time = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                directVirtualTester.Method();
            }
            long directVirtualTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                directVirtualTester.Method2();
            }
            long directVirtual2Time = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                indirectVirtualTester.Method();
            }
            long indirectVirtualTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                indirectVirtualTester.Method2();
            }
            long indirectVirtual2Time = stopwatch.ElapsedMilliseconds;
#endregion

#region Results
            Console.WriteLine($"Concrete tester took {concreteTime}ms");
            Console.WriteLine();
            Console.WriteLine($"Direct interface tester took {directInterfaceTime}ms");
            Console.WriteLine($"Indirect interface tester took {indirectInterfaceTime}ms");
            Console.WriteLine();
            Console.WriteLine($"Direct abstract tester took {directAbstractTime}ms");
            Console.WriteLine($"Indirect abstract tester took {indirectAbstractTime}ms");
            Console.WriteLine();
            Console.WriteLine($"Base virtual tester took {baseVirtualTime}ms");
            Console.WriteLine($"Base virtual tester 2 took {baseVirtual2Time}ms");
            Console.WriteLine($"Direct virtual tester took {directVirtualTime}ms");
            Console.WriteLine($"Direct virtual tester 2 took {directVirtual2Time}ms");
            Console.WriteLine($"Indirect virtual tester took {indirectVirtualTime}ms");
            Console.WriteLine($"Indirect virtual tester 2 took {indirectVirtual2Time}ms");
        }
#endregion

        private static long TestConcrete()
        {
            ConcreteTester concreteTester = new ConcreteTester();

            stopwatch.Reset();
            stopwatch.Start();
            for(int i=0; i<TestIterations; ++i) {
                concreteTester.Method();
            }
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
