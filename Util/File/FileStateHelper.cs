using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Util.File
{
    /// <summary>
    /// 文件状态帮助类
    /// </summary>
    public static class FileStateHelper
    {
        /// <summary>
        /// Opens the file for reading and writing.
        /// </summary>
        private const int OF_READWRITE = 2;
        /// <summary>
        /// <para>Opens the file without denying read or write access to other processes.</para>
        /// <para>On MS-DOS-based file systems using the Win32 API, if the file has been </para>
        /// <para>opened in compatibility mode by any other process, the function fails. </para>
        /// <para>Windows NT: This flag is mapped to the CreateFile function's </para>
        /// <para>FILE_SHARE_READ | FILE_SHARE_WRITE flags.</para>
        /// </summary>
        private const int OF_SHARE_DENY_NONE = 0x40;
        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        #region window api
        /// <summary>
        /// 判断文件是否打开
        /// </summary>
        /// <param name="lpPathName">文件名称</param>
        /// <param name="iReadWrite"></param>
        /// <returns></returns>
        [DllImport(dllName: "kernel32.dll", EntryPoint = "_lopen")]
        private static extern IntPtr Lopen(string lpPathName, int iReadWrite);
        /// <summary>
        /// 关闭文件句柄
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport(dllName: "kernel32.dll", EntryPoint = "CloseHandle")]
        private static extern bool CloseHandle(IntPtr hObject);
        #endregion

        /// <summary>
        /// 判断文件是否被占用
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsTakeUp(string filename)
        {
            IntPtr vHandle = Lopen(filename, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                CloseHandle(vHandle);
                return true;
            }
            else
                return false;
        }
    }
}
