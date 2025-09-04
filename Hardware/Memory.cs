namespace Subleq.Hardware;

public class Memory
{
    private int[] mem;

    public Memory(int size)
    {
        mem = new int[size];
    }

    public int LastWrittenAddress { get; private set; } = -1;

    public void Load(int[] program, int offset = 0)
    {
        if (program.Length + offset > mem.Length)
            throw new IndexOutOfRangeException("Program too large to fit in memory.");
        Array.Copy(program, 0, mem, offset, program.Length);
        LastWrittenAddress = -1;
    }
    public void Reset()
    {
        Array.Clear(mem, 0, mem.Length);
    }

    public int[] Dump()
    {
        return (int[])mem.Clone();
    }

    /// <summary>
    /// Reads a value from memory if the address is >= zero and less than the length of the memory.
    /// </summary>
    /// <remarks>If the address is less than zero, the address itself is returned. This allows for simple I/O port mapping.</remarks>
    /// <param name="address"></param>
    /// <returns>int</returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public int Read(int address)
    {
        if (address >= 0 && address < mem.Length)
            return mem[address];

        if (address < 0)
            return address; // Negative addresses return the address itself for now
        throw new IndexOutOfRangeException("Memory read out of bounds.");
    }

    public void Write(int address, int value)
    {
        if (address < 0 || address >= mem.Length)
            throw new IndexOutOfRangeException("Memory write out of bounds.");
        mem[address] = value;
        LastWrittenAddress = address;
    }
}