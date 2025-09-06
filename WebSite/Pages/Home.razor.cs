using Subleq.Hardware;
using WebSite.Components;

namespace WebSite.Pages;


public partial class Home
{
    private CPU? _cpu;
    private Memory? _memory;
    private MemoryView? _memoryView;
    protected int _memorySize = 32;
    protected string? Message { get; set; } = "Ready";


    protected override void OnInitialized()
    {
        _memory = new Memory(32);
        _cpu = new CPU(_memory);
        Console.WriteLine("Home initialized");
    }

    private void StoredPrograms_OnLoad(int[] program)
    {
        _memory?.Reset();
        _memory?.Load(program);
        _cpu?.ClearInstructionPointer();
        _memoryView?.UpdateView();
    }

    private void ResetInstructionPointer()
    {
        _cpu?.ClearInstructionPointer();
        Message = "Instruction pointer reset.";
    }

    private void InitialiseToMemorySize(int newMemorySize)
    {
        _cpu = null;
        _memory = null;
        _memory = new Memory(newMemorySize);
        _cpu = new CPU(_memory);
        Message = $"Initialized memory to size {newMemorySize}.";

        _memoryView?.SetMemoryObject(_memory);
    }

    private void Step()
    {
        if (_cpu is null)
            return;

        try
        {
            _cpu.ExecuteNextInstruction();
            Message = $"Executed instruction successfully.";
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Message = $"Error occurred while executing instruction: {ex.Message}";
        }
        finally
        {
            _memoryView?.UpdateView();
        }
    }
}