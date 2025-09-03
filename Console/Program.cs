using System;

namespace Subleq
{
    class Program
    {
        static void Main(string[] args)
        {
            // AIVersion();

            // var exampleProgramFromAI = new int[] {
            //     15, 17, -1,   // loop: sub & test       | 0,  1,  2
            //     17, -1, -1,   // output each char       | 3,  4,  5
            //     16, 1, -1,    // pointer bump           | 6,  7,  8
            //     16, 3, -1,    // loop back              | 9,  10, 11
            //     15, 15, 0,    // terminator             | 12, 13, 14
            //     0, -1,        // padding                | 15, 16
            //     72, 101, 108, 108, 111, 44, 32, 119, // | 17, 18, 19, 20, 21, 22, 23, 24
            //     111, 114, 108, 100, 33, 10, 0 //        | 25, 26, 27, 28, 29, 30, 31
            // };

            // var simpleCounter = new int[] {
            //     13, 21, 3,
            //     21, -1, 6,
            //     13, 21, 9,
            //     21, -1, 12,
            //     13, 21, 15,
            //     21, -1, 18,
            //     -1, -1, 32
            // };

            var outputACharacter = new int[] {
                12, -1, 3,
                1, 12, 6,
                12, -1, 9,
                -1, -1, -1,
                65
            };

            MyVersion(outputACharacter);
        }

        static void MyVersion(int[] program)
        {
            var memory = new Memory(256);
            memory.Load(program);
            var cpu = new CPU(memory);

            while (cpu.InstructionPointer >= 0)
                cpu.ExecuteNextInstruction();
        }
    }
}