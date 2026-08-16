using System;
using System.Runtime.InteropServices;

namespace WinTuner.App;

/// <summary>
/// Thin wrapper around the Win32 Common Item Dialog (IFileOpenDialog /
/// IFileSaveDialog). This replaces Windows.Storage.Pickers because WinTuner
/// ships unpackaged (no MSIX identity), where the WinRT pickers are unreliable
/// and routinely throw or return null. The native dialog works in both
/// packaged and unpackaged contexts and needs no window-handle broker.
/// </summary>
internal static class NativeFileDialog
{
    private const uint CLSCTX_INPROC_SERVER = 1;

    private static readonly Guid CLSID_FileOpenDialog =
        new(0xDC1C5A9C, 0xE88A, 0x4DDE, 0xA1, 0x9A, 0x99, 0x10, 0xEC, 0x0E, 0x20, 0xD5);
    private static readonly Guid CLSID_FileSaveDialog =
        new(0xC0B4E2F3, 0xBA21, 0x4773, 0x8C, 0x79, 0x42, 0x09, 0xEA, 0xD4, 0x0F, 0x09);
    private static readonly Guid IID_IShellItem =
        new(0x43826D1E, 0xE718, 0x42EE, 0xBC, 0x55, 0xA1, 0xE2, 0x61, 0xC3, 0x7B, 0xFE);

    private enum SIGDN : uint
    {
        FILESYSPATH = 0x80058000,
    }

    private enum FDAP
    {
        BOTTOM = 0,
        TOP = 1,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct COMDLG_FILTERSPEC
    {
        [MarshalAs(UnmanagedType.LPWStr)] public string? pszName;
        [MarshalAs(UnmanagedType.LPWStr)] public string? pszSpec;
    }

    [ComImport, Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IShellItem
    {
        [PreserveSig] int BindToHandler(IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid bhid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);
        [PreserveSig] int GetParent(out IShellItem ppsi);
        [PreserveSig] int GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
        [PreserveSig] int GetAttributes(uint sfgaoMask, out uint psfgaoAttributes);
        [PreserveSig] int Compare(IShellItem psi, uint hint, out int piOrder);
    }

    [ComImport, Guid("B4DB1657-70D7-485E-8E3A-FA2B8F1D2EA8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IModalWindow
    {
        [PreserveSig] int Show(IntPtr hwndOwner);
    }

    [ComImport, Guid("42F85136-DB0F-4A9E-8B8F-54E5475A120C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IFileDialog : IModalWindow
    {
        [PreserveSig] int SetFileTypes(uint cFileTypes, [MarshalAs(UnmanagedType.LPArray)] COMDLG_FILTERSPEC[] rgFilterSpec);
        [PreserveSig] int SetFileTypeIndex(uint iFileType);
        [PreserveSig] int GetFileTypeIndex(out uint piFileType);
        [PreserveSig] int SetDefaultFolder(IShellItem psi);
        [PreserveSig] int SetFolder(IShellItem psi);
        [PreserveSig] int GetFolder(out IShellItem ppsi);
        [PreserveSig] int GetCurrentSelection(out IShellItem ppsi);
        [PreserveSig] int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        [PreserveSig] int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);
        [PreserveSig] int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
        [PreserveSig] int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);
        [PreserveSig] int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
        [PreserveSig] int GetResult(out IShellItem ppsi);
        [PreserveSig] int AddPlace(IShellItem psi, FDAP fdap);
        [PreserveSig] int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);
        [PreserveSig] int Close(int hr);
        [PreserveSig] int SetClientGuid([MarshalAs(UnmanagedType.LPStruct)] Guid guid);
        [PreserveSig] int ClearClientData();
        [PreserveSig] int SetFilter(IntPtr pFilter);
    }

    [ComImport, Guid("D57C7288-D4AD-4768-BE67-0C329E570235"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IFileOpenDialog : IFileDialog
    {
        [PreserveSig] int GetResults(out IntPtr ppenum);
        [PreserveSig] int GetSelectedItems(out IntPtr ppsai);
    }

    [ComImport, Guid("84BCCD23-5FDE-4CDB-AACB-8A11688A7344"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IFileSaveDialog : IFileDialog
    {
        [PreserveSig] int SetSaveAsItem(IShellItem psi);
        [PreserveSig] int SetProperties(IntPtr pstore);
        [PreserveSig] int SetCollectedProperties(IntPtr pList, [MarshalAs(UnmanagedType.Bool)] bool fAppendDefault);
        [PreserveSig] int GetProperties(IntPtr ppStore);
        [PreserveSig] int ApplyProperties(IShellItem psi, IntPtr pStore, IntPtr hwnd, IntPtr pSink);
    }

    [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = true)]
    private static extern int CoCreateInstance(
        [MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
        IntPtr pUnkOuter,
        uint dwClsContext,
        [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true)]
    private static extern int SHCreateItemFromParsingName(
        [MarshalAs(UnmanagedType.LPWStr)] string pszPath,
        IntPtr pbc,
        [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
        out IShellItem ppv);

    private static IShellItem? GetDesktopShellItem()
    {
        try
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            int hr = SHCreateItemFromParsingName(desktop, IntPtr.Zero, IID_IShellItem, out var item);
            return hr == 0 ? item : null;
        }
        catch
        {
            return null;
        }
    }

    public static string? ShowOpenFileDialog(IntPtr owner, string title)
    {
        try
        {
            int hr = CoCreateInstance(CLSID_FileOpenDialog, IntPtr.Zero, CLSCTX_INPROC_SERVER, typeof(IFileOpenDialog).GUID, out var obj);
            if (hr != 0 || obj is not IFileOpenDialog dlg)
            {
                return null;
            }

            try
            {
                dlg.SetTitle(title);
                dlg.SetFileTypes(1, new[] { new COMDLG_FILTERSPEC { pszName = "WinTuner files", pszSpec = "*.json" } });
                dlg.SetFileTypeIndex(1);
                var desktop = GetDesktopShellItem();
                if (desktop is not null)
                {
                    dlg.SetDefaultFolder(desktop);
                }

                if (dlg.Show(owner) != 0)
                {
                    return null; // user cancelled
                }

                if (dlg.GetResult(out var item) != 0 || item is null)
                {
                    return null;
                }

                item.GetDisplayName(SIGDN.FILESYSPATH, out var path);
                Marshal.ReleaseComObject(item);
                return path;
            }
            finally
            {
                Marshal.ReleaseComObject(dlg);
            }
        }
        catch
        {
            return null;
        }
    }

    public static string? ShowSaveFileDialog(IntPtr owner, string title, string defaultName)
    {
        try
        {
            int hr = CoCreateInstance(CLSID_FileSaveDialog, IntPtr.Zero, CLSCTX_INPROC_SERVER, typeof(IFileSaveDialog).GUID, out var obj);
            if (hr != 0 || obj is not IFileSaveDialog dlg)
            {
                return null;
            }

            try
            {
                dlg.SetTitle(title);
                dlg.SetFileTypes(1, new[] { new COMDLG_FILTERSPEC { pszName = "WinTuner files", pszSpec = "*.json" } });
                dlg.SetFileTypeIndex(1);
                dlg.SetDefaultExtension("json");
                dlg.SetFileName(defaultName);
                var desktop = GetDesktopShellItem();
                if (desktop is not null)
                {
                    dlg.SetDefaultFolder(desktop);
                }

                if (dlg.Show(owner) != 0)
                {
                    return null; // user cancelled
                }

                if (dlg.GetResult(out var item) != 0 || item is null)
                {
                    return null;
                }

                item.GetDisplayName(SIGDN.FILESYSPATH, out var path);
                Marshal.ReleaseComObject(item);
                return path;
            }
            finally
            {
                Marshal.ReleaseComObject(dlg);
            }
        }
        catch
        {
            return null;
        }
    }
}
