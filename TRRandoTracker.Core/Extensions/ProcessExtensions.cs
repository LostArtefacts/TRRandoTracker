using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TRRandoTracker.Core.Extensions;

public static class ProcessExtensions
{
    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

    public static T Read<T>(this Process process, int address) where T : struct
    {
        byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
        int bytesRead = 0;
        address = (process.MainModule.BaseAddress + address).ToInt32();
        ReadProcessMemory((int)process.Handle, address, buffer, buffer.Length, ref bytesRead);
        return ByteArrayToStructure<T>(buffer);
    }

    public static byte[] ReadBytes(this Process process, int address, int length)
    {
        byte[] buffer = new byte[length];
        int bytesRead = 0;
        address = (process.MainModule.BaseAddress + address).ToInt32();
        ReadProcessMemory((int)process.Handle, address, buffer, buffer.Length, ref bytesRead);
        return buffer;
    }

    private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try
        {
            return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            handle.Free();
        }
    }
}