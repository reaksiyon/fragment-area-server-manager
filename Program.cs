using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AreaServerLevelEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out int lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hObject);


        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();


        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref int lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

        private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
        private const int MEMORY_ADDRESS = 0x0EB37B3C - 4;


        public static Process process;
        public static Process[] processes;
        public static IntPtr processHandle;
        public static IntPtr baseAddressFromExe;

        [STAThread]
        static void Main()
        {
            //AllocConsole(); enable developer console

            Inject();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }


        public static void Inject()
        {
            const int PROCESS_VM_READ = 0x0010;

            string processName = "AREA SERVER";

            processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0)
            {
                MessageBox.Show("ERROR 1004 - Please Open AREA SERVER First!", "Process Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(0);

                return;
            }

            process = processes[0];
            processHandle = process.Handle;
            baseAddressFromExe = process.MainModule.BaseAddress;
        }

        public static void ReloadUI()
        {
            string serverName = GetServerName(processes);
        }

        public static void SetServerLevel(int level)
        {
            if (processes.Length > 0)
            {
                IntPtr targetAddress = (IntPtr)(baseAddressFromExe + 0x29F2EC); 

                byte[] pointerBuffer = new byte[4];
                if (ReadProcessMemory(processHandle, targetAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    int value = BitConverter.ToInt32(pointerBuffer, 0);
                    Console.WriteLine($"Value at address 0x{targetAddress:X}: {value}");

                    WriteMemoryValue(targetAddress, (byte)level);
                }
                else
                {
                    MessageBox.Show("Memory Read Error");
                }
            }

            ReloadUI();
        }

        public static int GetServerLevel()
        {
            if (processes.Length > 0)
            {
                IntPtr targetAddress = (IntPtr)(baseAddressFromExe + 0x29F2EC);

                byte[] pointerBuffer = new byte[4];
                if (ReadProcessMemory(processHandle, targetAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    int serverLevel = BitConverter.ToInt32(pointerBuffer, 0);
                    return serverLevel;
                }
                else
                {
                    MessageBox.Show("Memory Read Error");
                    return 0;
                }
            }

            ReloadUI();
            return 0;
        }

        public static void SetLandLevels(int lavaFieldLevel, int fieryFieldLevel, int sandFieldLevel, int desertFieldLevel, int woodsFieldLevel, int snowFieldLevel, int iceFieldLevel, int rockyFieldLevel, int plainsFieldLevel, int swampFieldLevel, int grassFieldLevel, int machineFieldLevel)
        {
            if (processes.Length > 0)
            {
                IntPtr baseAddress = (IntPtr)(0x0069f1c8);

                byte[] pointerBuffer = new byte[IntPtr.Size];
                if (ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    IntPtr pointerAddress = (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);

                    // Write each field level to the appropriate memory address
                    WriteMemoryValue(pointerAddress, lavaFieldLevel, 0x94);
                    WriteMemoryValue(pointerAddress, fieryFieldLevel, 0x95);
                    WriteMemoryValue(pointerAddress, sandFieldLevel, 0x96);
                    WriteMemoryValue(pointerAddress, desertFieldLevel, 0x97);
                    WriteMemoryValue(pointerAddress, woodsFieldLevel, 0x98);
                    WriteMemoryValue(pointerAddress, snowFieldLevel, 0x99);
                    WriteMemoryValue(pointerAddress, iceFieldLevel, 0x9A);
                    WriteMemoryValue(pointerAddress, rockyFieldLevel, 0x9B);
                    WriteMemoryValue(pointerAddress, plainsFieldLevel, 0x9C);
                    WriteMemoryValue(pointerAddress, swampFieldLevel, 0x9D);
                    WriteMemoryValue(pointerAddress, grassFieldLevel, 0x9E);
                    WriteMemoryValue(pointerAddress, machineFieldLevel, 0x9F);

                    MessageBox.Show("Land levels successfully set!");

                }
                else
                {
                    MessageBox.Show("Memory Read Error");
                }
            }

            ReloadUI();
        }

        public static int[] GetLandLevels()
        {
            int[] landLevels = new int[12];

            if (processes.Length > 0)
            {
                IntPtr baseAddress = (IntPtr)(0x0069f1c8);

                byte[] pointerBuffer = new byte[IntPtr.Size];
                if (ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    IntPtr pointerAddress = (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);

                    // Read each field level from the appropriate memory address
                    landLevels[0] = ReadMemoryValue(pointerAddress, 0x94);
                    landLevels[1] = ReadMemoryValue(pointerAddress, 0x95);
                    landLevels[2] = ReadMemoryValue(pointerAddress, 0x96);
                    landLevels[3] = ReadMemoryValue(pointerAddress, 0x97);
                    landLevels[4] = ReadMemoryValue(pointerAddress, 0x98);
                    landLevels[5] = ReadMemoryValue(pointerAddress, 0x99);
                    landLevels[6] = ReadMemoryValue(pointerAddress, 0x9A);
                    landLevels[7] = ReadMemoryValue(pointerAddress, 0x9B);
                    landLevels[8] = ReadMemoryValue(pointerAddress, 0x9C);
                    landLevels[9] = ReadMemoryValue(pointerAddress, 0x9D);
                    landLevels[10] = ReadMemoryValue(pointerAddress, 0x9E);
                    landLevels[11] = ReadMemoryValue(pointerAddress, 0x9F);
                }
                else
                {
                    Console.WriteLine("ERROR - Failed to read memory!");
                }
            }
            else
            {
                Console.WriteLine("ERROR - No processes found!");
            }

            return landLevels; 
        }

        public static void SetServerName(string serverName)
        {
            if (processes.Length > 0)
            {
                // Set server name at both addresses
                SetNameAtAddress(baseAddressFromExe + 0x0033A2DC, 0x2E4, serverName); //map editor address 3
                SetNameAtAddress(baseAddressFromExe + 0x0029F1A4, 0x370, serverName); //save memory address 3 
                SetNameAtAddress(baseAddressFromExe + 0x0033A18C, 0x2DC, 0xCC, 0x00, serverName); //ingame server name 4
                SetNameAtAddress(baseAddressFromExe + 0x29F1EC, serverName); //live server address 2

                MessageBox.Show("Server name successfully set!");

                ReloadUI();
            }
            else
            {
                MessageBox.Show("Server name set error!");
            }

            ReloadUI();
        }

        public static string GetServerName(Process[] processes)
        {
            if (processes.Length > 0)
            {
                IntPtr baseAddress = (IntPtr)(baseAddressFromExe + 0x29F1EC);

                return GetNameAtAddress(baseAddress);
            }
            else
            {
                return null;
            }
        }

        private static void WriteMemoryValue(IntPtr basePointer, int value, int offset)
        {
            byte[] byteValue = new byte[] { (byte)value };

            if (!WriteProcessMemory(processHandle, (IntPtr)(basePointer.ToInt32() + offset), byteValue, (uint)byteValue.Length, out _))
            {
                //Console.WriteLine($"ERROR - Failed to write value {value} to offset {offset}!");
            }
        }

        private static void WriteMemoryValue(IntPtr basePointer, byte value)
        {
            byte[] byteValue = new byte[] { value };

            if (!WriteProcessMemory(processHandle, basePointer, byteValue, (uint)byteValue.Length, out _))
            {
                int errorCode = Marshal.GetLastWin32Error();
                //Console.WriteLine($"ERROR - Failed to write value! Error Code: {errorCode}");
            }
            else
            {
                //Console.WriteLine($"Successfully wrote value {value} to address {basePointer.ToString("X")}");
            }
        }


        private static int ReadMemoryValue(IntPtr basePointer, int offset)
        {
            byte[] buffer = new byte[1];
            if (ReadProcessMemory(processHandle, (IntPtr)(basePointer.ToInt32() + offset), buffer, (uint)buffer.Length, out _))
            {
                return buffer[0]; 
            }
            else
            {
                //Console.WriteLine($"ERROR - Failed to read value from offset {offset}!");
                return 0; 
            }
        }

        private static void SetNameAtAddress(IntPtr baseAddress, int offset0, string serverName)
        {
            byte[] pointerBuffer = new byte[IntPtr.Size];


            if (ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
            {
                IntPtr pointerAddress = (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);
                IntPtr finalAddress = (IntPtr)(pointerAddress.ToInt32() + offset0);

                byte[] nameBytes = System.Text.Encoding.ASCII.GetBytes(serverName);
                byte[] stringBuffer = new byte[20]; // 20 char buffer because game server name limit
                Array.Copy(nameBytes, stringBuffer, Math.Min(nameBytes.Length, stringBuffer.Length));

                if (nameBytes.Length + 1 < stringBuffer.Length)
                {
                    stringBuffer[nameBytes.Length] = 0;
                }
                else
                {
                    MessageBox.Show("Server name too long!");
                }

                for (int i = 0; i < stringBuffer.Length; i++)
                {
                    WriteMemoryValue(finalAddress, stringBuffer[i], i);
                }
            }
            else
            {
                MessageBox.Show("Permission Error!");
            }
        }

        //public static void SetServerTag(string symbol)
        //{
        //    if (processes.Length > 0)
        //    {
        //        // Set server name at both addresses
        //        SetNameAtAddress(baseAddressFromExe + 0x0029F150, 0x44, 0xAC, symbol);

        //        MessageBox.Show("Server tag successfully set!");

        //        ReloadUI();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Server name set error!");
        //    }

        //    ReloadUI();
        //}

        //public static string GetServerTag()
        //{
        //    if (processes.Length > 0)
        //    {
        //        return GetNameAtAddress(baseAddressFromExe + 0x0029F150, 0x44, 0xAC);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Server symbol get error!");
        //        return "-";
        //    }

        //    return "-";
        //}

        public static void SetServerHelloMessage(string message)
        {
            if (processes.Length > 0)
            {
                SetNameAtAddress(baseAddressFromExe + 0x0029F1C8, 0x18C, 0x0, message);

                MessageBox.Show("Server name successfully set!");

                ReloadUI();
            }
            else
            {
                MessageBox.Show("Server name set error!");
            }

            ReloadUI();
        }

        private static void SetNameAtAddress(IntPtr baseAddress, int offset0, int offset1, int offset2, string serverName)
        {
            byte[] pointerBuffer = new byte[4];

            if (ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
            {
                IntPtr pointerAddress = (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);
                IntPtr targetAddress1 = (IntPtr)(pointerAddress.ToInt32() + offset0);

                if (!ReadProcessMemory(processHandle, targetAddress1, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    return;
                }

                IntPtr targetAddress2 = (IntPtr)(BitConverter.ToInt32(pointerBuffer, 0) + offset1);

                if (!ReadProcessMemory(processHandle, targetAddress2, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    return;
                }

                IntPtr finalAddress = (IntPtr)(BitConverter.ToInt32(pointerBuffer, 0) + offset2);

                byte[] nameBytes = System.Text.Encoding.ASCII.GetBytes(serverName);
                byte[] stringBuffer = new byte[20];
                Array.Copy(nameBytes, stringBuffer, Math.Min(nameBytes.Length, stringBuffer.Length));

                stringBuffer[Math.Min(nameBytes.Length, stringBuffer.Length - 1)] = 0;

                if (!WriteProcessMemory(processHandle, finalAddress, stringBuffer, (uint)stringBuffer.Length, out _))
                {
                    MessageBox.Show("Memory Write Error!");
                }
            }
            else
            {
                MessageBox.Show("Permission Error!");
            }
        }

        private static void SetNameAtAddress(IntPtr baseAddress, int offset0, int offset1, string serverName)
        {
            byte[] pointerBuffer = new byte[4];

            if (ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
            {
                IntPtr pointerAddress = (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);
                IntPtr targetAddress1 = (IntPtr)(pointerAddress.ToInt32() + offset0);

                if (!ReadProcessMemory(processHandle, targetAddress1, pointerBuffer, (uint)pointerBuffer.Length, out _))
                {
                    return;
                }

                IntPtr targetAddress2 = (IntPtr)(BitConverter.ToInt32(pointerBuffer, 0) + offset1);

                byte[] nameBytes = System.Text.Encoding.ASCII.GetBytes(serverName);
                byte[] stringBuffer = new byte[20];
                Array.Copy(nameBytes, stringBuffer, Math.Min(nameBytes.Length, stringBuffer.Length));

                stringBuffer[Math.Min(nameBytes.Length, stringBuffer.Length - 1)] = 0;

                if (!WriteProcessMemory(processHandle, targetAddress2, stringBuffer, (uint)stringBuffer.Length, out _))
                {
                    MessageBox.Show("Memory Write Error!");
                }
            }
            else
            {
                MessageBox.Show("Permission Error!");
            }
        }

        private static void SetNameAtAddress(IntPtr targetAddress, string serverName)
        {
            byte[] nameBytes = System.Text.Encoding.ASCII.GetBytes(serverName);
            byte[] stringBuffer = new byte[20];

            Array.Copy(nameBytes, stringBuffer, Math.Min(nameBytes.Length, stringBuffer.Length - 1));


            if (WriteProcessMemory(processHandle, targetAddress, stringBuffer, (uint)stringBuffer.Length, out _))
            {
            //   Console.WriteLine($"Successfully wrote '{serverName}' to address 0x{targetAddress:X}");
            }
            else
            {
            //    Console.WriteLine("ERROR 1001 - Failed to write memory!");
            }
        }

        private static string GetNameAtAddress(IntPtr baseAddress)
        {
            byte[] nameBuffer = new byte[20]; // Maksimum 20 karakter

            // Pointer'ı oku
            if (!ReadProcessMemory(processHandle, baseAddress, nameBuffer, (uint)nameBuffer.Length, out _))
            {
                MessageBox.Show("Memory Read Error at Base Address!");
                return string.Empty;
            }

            // ASCII string olarak çevir, null karakterden sonrasını al
            return System.Text.Encoding.ASCII.GetString(nameBuffer).Split('\0')[0];
        }

        private static string GetNameAtAddress(IntPtr baseAddress, int offset)
        {
            byte[] pointerBuffer = new byte[4]; // 32-bit için 4 byte
            byte[] nameBuffer = new byte[20]; // Maksimum 20 karakter

            // Pointer'ı oku
            if (!ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
            {
                MessageBox.Show("Memory Read Error at Base Address!");
                return string.Empty;
            }

            IntPtr targetAddress = (IntPtr)(BitConverter.ToInt32(pointerBuffer, 0) + offset);

            // İsim verisini oku
            if (!ReadProcessMemory(processHandle, targetAddress, nameBuffer, (uint)nameBuffer.Length, out _))
            {
                MessageBox.Show("Memory Read Error at Target Address!");
                return string.Empty;
            }

            // ASCII string olarak çevir, null karakterden sonrasını al
            return System.Text.Encoding.ASCII.GetString(nameBuffer).Split('\0')[0];
        }

        private static string GetNameAtAddress(IntPtr baseAddress, int offset0, int offset1)
        {
            byte[] pointerBuffer = new byte[4]; // 32-bit için 4 byte
            byte[] nameBuffer = new byte[20]; // Maksimum 20 karakter

            // İlk pointer'ı oku
            if (!ReadProcessMemory(processHandle, baseAddress, pointerBuffer, (uint)pointerBuffer.Length, out _))
            {
                MessageBox.Show("Memory Read Error at Level 0!");
                return string.Empty;
            }
            IntPtr pointerAddress = (IntPtr)BitConverter.ToInt32(pointerBuffer, 0);
            IntPtr targetAddress1 = (IntPtr)(pointerAddress.ToInt32() + offset0);

            // İkinci pointer'ı oku
            if (!ReadProcessMemory(processHandle, targetAddress1, pointerBuffer, (uint)pointerBuffer.Length, out _))
            {
                MessageBox.Show("Memory Read Error at Level 1!");
                return string.Empty;
            }
            IntPtr targetAddress2 = (IntPtr)(BitConverter.ToInt32(pointerBuffer, 0) + offset1);


            // İsim verisini oku
            if (!ReadProcessMemory(processHandle, targetAddress2, nameBuffer, (uint)nameBuffer.Length, out _))
            {
                MessageBox.Show("Memory Read Error at Target Address!");
                return string.Empty;
            }

            // ASCII string olarak çevir, null karakterden sonrasını al
            return System.Text.Encoding.ASCII.GetString(nameBuffer).Split('\0')[0];
        }
    }
}