using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
[CompilerGenerated]
internal sealed class PrivateImplementationDetails
{
    // Fields

    // Methods
    internal static uint ComputeStringHash(string s)
    {
        uint num = 0;
        if (s != null)
        {
            num = 0x811c9dc5;
            for (int i = 0; i < s.Length; i++)
            {
                num = (s[i] ^ num) * 0x1000193;
            }
        }
        return num;

    }

    // Nested Types
    [StructLayout(LayoutKind.Explicit, Size = 6, Pack = 1)]
    private struct Struct0
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x10, Pack = 1)]
    private struct Struct1
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x100, Pack = 1)]
    private struct Struct10
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x180, Pack = 1)]
    private struct Struct11
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x200, Pack = 1)]
    private struct Struct12
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x480, Pack = 1)]
    private struct Struct13
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x1800, Pack = 1)]
    private struct Struct14
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x13, Pack = 1)]
    private struct Struct2
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x1a, Pack = 1)]
    private struct Struct3
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x44, Pack = 1)]
    private struct Struct4
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x4c, Pack = 1)]
    private struct Struct5
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x74, Pack = 1)]
    private struct Struct6
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 120, Pack = 1)]
    private struct Struct7
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x7c, Pack = 1)]
    private struct Struct8
    {
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x80, Pack = 1)]
    private struct Struct9
    {
    }
}



