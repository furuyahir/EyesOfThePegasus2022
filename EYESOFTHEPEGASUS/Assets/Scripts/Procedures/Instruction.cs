using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction
{
    public string ID;
    public CompletionState CompletionState;
    public Priority Priority;
    public InstructionItem[] InstructionItems;
    public string Text;
    public string Title;
}
