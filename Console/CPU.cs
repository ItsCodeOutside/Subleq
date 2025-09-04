namespace Subleq;

public class CPU
{
    private Memory memory;

    public Action<int>? OnOutput { get; set; }
    public int Input { get; set; }

    public CPU(Memory memory)
    {
        this.memory = memory;
        InstructionPointer = 0;
    }
    public int InstructionPointer { get; private set; }
    public void ClearInstructionPointer() => InstructionPointer = 0;
    public void ExecuteNextInstruction()
    {
        // The instruction pointer is the location in memory to fetch the next 
        // instruction from

        // Fetch the instruction
        // The instruction is a list of memory addresses
        var pointer_A = memory.Read(InstructionPointer);
        var pointer_B = memory.Read(InstructionPointer + 1);
        var pointer_C = memory.Read(InstructionPointer + 2);


        switch (pointer_A, pointer_B, pointer_C)
        {
            // Input - If the minuend is -1, read from the input 'register'
            case (-1, _, _):
                memory.Write(pointer_B, Input);
                break;
            // Output - If the subtrahend is -1, write to the output 'register' (Invokes the event)
            case (_, -1, _):
                OnOutput?.Invoke(memory.Read(pointer_A));
                break;


            // Execute the instruction
            // Subtract and branch if less than or equal to zero
            default:
                Subleq(
                    pointer_A, // Address of the subtrahend (The value to subtract)
                    pointer_B, // Address of the minuend (The value to subtract from)

                    // Target is the address to branch to if the subtrahend is less than or equal to zero
                    pointer_C
                );
                // Return here because the Subleq handles the instruction pointer
                return;
        }
        InstructionPointer += 3;
    }


/// <summary>
/// Subtracts the subtrahend from the minuend and writes the result to the minuend's memory location.
/// </summary>
/// <param name="addressOfSubtrahend">The value to subtract</param>
/// <param name="addressOfMinuend">The value to subtract from</param>
/// <param name="addressOfJumpTarget">If the <see cref="minuend">minuend</see> is less than or equal to zero, this is the memory address to set as the value for the instruction pointer</param>
    private void Subleq(int addressOfSubtrahend, int addressOfMinuend, int addressOfJumpTarget)
    {
        // Subtract
        memory.Write(addressOfMinuend, memory.Read(addressOfMinuend) - memory.Read(addressOfSubtrahend));

        if (memory.Read(addressOfMinuend) <= 0)
        {
            InstructionPointer = addressOfJumpTarget;
            return;
        }

        InstructionPointer += 3;
    }
}
