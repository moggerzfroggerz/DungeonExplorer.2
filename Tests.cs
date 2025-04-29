using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Tests
    {
        // Tests values for validation: 
        public static void TestForPositiveInteger(int value)
        {
            Debug.Assert(value > 0, "Test failure. Value is not a positive integer");
        }
        public static void TestForZeroOrAbove(int value)
        {
            Debug.Assert(value >= 0, "Test failure. Value is not a positive integer or equal to 0");
        }

    }
}