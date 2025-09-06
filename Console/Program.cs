using System;
using Subleq.Hardware;

namespace Subleq
{
    class Program
    {
        static void Main(string[] args)
        {
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