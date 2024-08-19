using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace TRRandoTracker.Core.Extensions;

public static class ProcessExtensions
{
    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(int hProcess, long lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

    [DllImport("psapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb, out uint lpcbNeeded, uint dwFilterFlag);

    [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern uint GetModuleFileNameExW(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);

    [DllImport("psapi.dll", CharSet = CharSet.Unicode)]
    private static extern uint GetModuleBaseNameW(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);

    [DllImport("psapi.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, [Out] out MODULEINFO lpmodinfo, uint cb);

    [StructLayout(LayoutKind.Sequential)]
    public struct MODULEINFO
    {
        public IntPtr lpBaseOfDll;
        public uint SizeOfImage;
        public IntPtr EntryPoint;
    }

    public static T Read<T>(this Process process, int address) where T : struct
    {
        byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
        int bytesRead = 0;
        address = (process.MainModule.BaseAddress + address).ToInt32();
        ReadProcessMemory((int)process.Handle, address, buffer, buffer.Length, ref bytesRead);
        return ByteArrayToStructure<T>(buffer);
    }

    public static T Read<T>(this Process process, IntPtr baseAddress, long address) where T : struct
    {
        byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
        int bytesRead = 0;
        address = baseAddress.ToInt64() + address;
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

    public static IntPtr GetDLLBase(this Process process, string dllName)
    {
        if (!EnumProcessModulesEx(process.Handle, null, 0, out uint cbNeeded, 3))
            throw new Win32Exception();

        uint numMods = cbNeeded / (uint)IntPtr.Size;
        var hModules = new IntPtr[(int)numMods];
        if (!EnumProcessModulesEx(process.Handle, hModules, cbNeeded, out _, 3))
            throw new Win32Exception();

        var sb = new StringBuilder(260);
        for (int i = 0; i < numMods; i++)
        {
            sb.Clear();
            if (GetModuleFileNameExW(process.Handle, hModules[i], sb, (uint)sb.Capacity) == 0)
                throw new Win32Exception();

            sb.Clear();
            if (GetModuleBaseNameW(process.Handle, hModules[i], sb, (uint)sb.Capacity) == 0)
                throw new Win32Exception();
            string baseName = sb.ToString();
            if (baseName.ToLower() != dllName.ToLower())
            {
                continue;
            }

            MODULEINFO moduleInfo = new();
            uint size = (uint)Marshal.SizeOf(moduleInfo);
            if (!GetModuleInformation(process.Handle, hModules[i], out moduleInfo, size))
                throw new Win32Exception();

            return moduleInfo.lpBaseOfDll;
        }

        return IntPtr.Zero;
    }
}