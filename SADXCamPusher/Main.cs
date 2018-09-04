using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

using SADXCamLib;

namespace SADXCamPusher
{
    public partial class Main : Form
    {
        #region WINAPI stuff
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UInt32 nSize, ref UInt32 lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;
        #endregion

        Process targetProc;
        IntPtr procHandle;
        bool playModeActivated = false;
        bool isValidProcess = false;

        // pointer calc stuff
        IntPtr camObjPointer = (IntPtr)0x3b2cbb0; // this is the pointer location
        IntPtr timerPointer = (IntPtr)0x1E0BE5C; // this is the pointer location
        IntPtr sonicData1Pointer = (IntPtr)0x3B42E10;
        IntPtr interactWithObjectsPtr1 = (IntPtr)0x3B42E10;
        IntPtr interactWithObjectsPtr;
        IntPtr moduleBaseAddress;
        float last_timer = 0.0f;
        uint pedantry;

        // File Exporting vars
        bool fileIsLoaded = false;

        // stored data
        CameraData cameraData;
        byte gameState = 0;
        int currentPlaybackFrame = 0;

        // code patch restoration
        byte[] originalCamCode = new byte[5];
        byte[] originalHUDCode = new byte[7];
        IntPtr displayCodeAddress = (IntPtr)0x4948C0;
        byte[] originalSonicDisplayCode = new byte[4] {0x8B, 0x44, 0x24, 0x04};

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            processLabel.Text = "No process loaded.";
            processLabel.ForeColor = Color.Red;
            cameraData = new CameraData();

            LockControls();
        }

        /*-----------------------------------------------------------------
         *  Pulse()
         *  -----------------
         *      This is where most of the action is. The function will have two branches, one for 'debug' mode, and one for 
         *  'record' mode. 'Debug' Mode is after the user has selected a process, but has not initiated recording yet. It will
         *  output values to the coordinate windows so the user can ascertain for themselves if they have attached to the correct
         *  process. At no time should Pulse() be called before a process is loaded.
         *  ---------------------------------------------------------------*/
        public void Pulse()
        {
            Vector3 currentPosition = new Vector3();
            EulerBAMSRotation currentRotation = new EulerBAMSRotation();
            IntPtr camDataAddress;
            IntPtr sonicObjAddress;

            #region Camera Raw Storage
            byte[] rawCamDataAddress = new Byte[4];
            byte[] rawXBuffer = new Byte[4];
            byte[] rawYBuffer = new Byte[4];
            byte[] rawZBuffer = new Byte[4];
            byte[] rawXRotBuffer = new Byte[2];
            byte[] rawYRotBuffer = new Byte[2];
            byte[] rawZRotBuffer = new Byte[2];
            #endregion

            byte[] gameStateBuf = new byte[1];
            gameStateBuf = Read(procHandle, (IntPtr)0x3B22DE4, 1, ref pedantry);
            gameState = gameStateBuf[0];

            byte[] rawTBuffer = new Byte[4];
            byte[] rawTPointer = new Byte[4];

            uint floatSize = sizeof(float);
            rawCamDataAddress = Read(procHandle, (IntPtr)(camObjPointer.ToInt32()), 4, ref floatSize);
            camDataAddress = (IntPtr)BitConverter.ToUInt32(rawCamDataAddress, 0);

            byte[] rawSonicObjAddress = new byte[4];
            rawSonicObjAddress = Read(procHandle, sonicData1Pointer, 4, ref floatSize);
            sonicObjAddress = (IntPtr)BitConverter.ToUInt32(rawSonicObjAddress, 0);

            /*------------------------read timer info-----------------------*/
            // todo: possibly put timer info here. This is a holdover from the generations memory reader, so we might not add it back.

            /*-----------------------------Playback Mode---------------------------*/
            if (playModeActivated == true) // push camera frame onto memory
            {
                if (gameState == 15) // 15 is runtime. Anything else is not. Users will probably be in 16 (pause)
                {
                   // push sonic data
                    if (lockSonicToCamCheckBox.Checked)
                    {
                        Write(procHandle, (IntPtr)(sonicObjAddress.ToInt32()), 0x1, BitConverter.GetBytes((Byte)87)); // set sonic to free movement mode

                        Write(procHandle, (IntPtr)(sonicObjAddress.ToInt32() + 0x20), 0x4, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Position.X));
                        Write(procHandle, (IntPtr)(sonicObjAddress.ToInt32() + 0x24), 0x4, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Position.Y));
                        Write(procHandle, (IntPtr)(sonicObjAddress.ToInt32() + 0x28), 0x4, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Position.Z));
                    }

                    // push camera data
                    Write(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x20), 0x4, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Position.X));
                    Write(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x24), 0x4, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Position.Y));
                    Write(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x28), 0x4, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Position.Z));

                    Write(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x14), 0x2, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Rotation.X));
                    Write(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x16), 0x2, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Rotation.Y));
                    Write(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x18), 0x2, BitConverter.GetBytes(cameraData.CameraStates[currentPlaybackFrame].Rotation.Z));

                    if (currentPlaybackFrame < cameraData.StateCount - 1) currentPlaybackFrame++;
                    else
                    {
                        currentPlaybackFrame = 0;
                        playModeActivated = false;
                        //pulseTimer.Stop();
                        statusLabel.Text = "Stopped";
                        return;
                    }
                }
            }
            else
            {
                rawXBuffer = Read(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x20), 4, ref floatSize);
                rawYBuffer = Read(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x24), 4, ref floatSize);
                rawZBuffer = Read(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x28), 4, ref floatSize);

                uint shortSize = sizeof(UInt16);
                rawXRotBuffer = Read(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x14), 2, ref shortSize);
                rawYRotBuffer = Read(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x16), 2, ref shortSize);
                rawZRotBuffer = Read(procHandle, (IntPtr)(camDataAddress.ToInt32() + 0x18), 2, ref shortSize);

                // create vector3 from saved data
                currentPosition = new Vector3(BitConverter.ToSingle(rawXBuffer, 0), BitConverter.ToSingle(rawYBuffer, 0), BitConverter.ToSingle(rawZBuffer, 0));
                currentRotation = new EulerBAMSRotation(BitConverter.ToUInt16(rawXRotBuffer, 0), BitConverter.ToUInt16(rawYRotBuffer, 0), BitConverter.ToUInt16(rawZRotBuffer, 0));
            }

            xCoordBox.Text = String.Format("{0:g}", currentPosition.X);
            yCoordBox.Text = String.Format("{0:g}", currentPosition.Y);
            zCoordBox.Text = String.Format("{0:g}", currentPosition.Z);

            xRotText.Text = String.Format("{0:x}", currentRotation.X);
            yRotText.Text = String.Format("{0:x}", currentRotation.Y);
            zRotText.Text = String.Format("{0:x}", currentRotation.Z);

            gameStateBox.Text = String.Format("{0}", gameState);
        }

        public byte[] Read(IntPtr handle, IntPtr address, UInt32 size, ref UInt32 bytes)
        {
            byte[] buffer = new byte[size];

            ReadProcessMemory(handle, address, buffer, size, ref bytes);

            Debug.Assert(bytes == size);

            return buffer;
        }

        public void Write(IntPtr handle, IntPtr address, UInt32 size, byte[] bytes)
        {
            int numBytes = (int)size;
            WriteProcessMemory((int)handle, (int)address, bytes, (int)size, ref numBytes);
        }

        public void LockControls() // Lock all the controls as not to playback null data
        {
            playButton.Enabled = false;
            stopButton.Enabled = false;
            detatchButton.Enabled = false;
        }

        public void UnlockControls() // Unlock so user can play
        {
            playButton.Enabled = true;
            detatchButton.Enabled = true;
            lockSonicToCamCheckBox.Enabled = true;
        }

        void GetFinalInteractPointer()
        {
            if (isValidProcess)
            {
                byte[] rawInteractPointer1 = Read(procHandle, interactWithObjectsPtr1, 4, ref pedantry);
                IntPtr interactWithObjectsPtr2 = (IntPtr)BitConverter.ToUInt32(rawInteractPointer1, 0);

                byte[] rawInteractPointer3 = Read(procHandle, (IntPtr)(interactWithObjectsPtr2.ToInt32() + 0x38), 4, ref pedantry);
                IntPtr interactWithObjectsPtr3 = (IntPtr)BitConverter.ToUInt32(rawInteractPointer3, 0);

                interactWithObjectsPtr = (IntPtr)(interactWithObjectsPtr3.ToInt32() + 0x6);
            }
        }

        private void loadProcessButton_Click(object sender, EventArgs e)
        {
            ProcessSelect processMgr = new ProcessSelect();
            DialogResult diagEval = new DialogResult();
            diagEval = processMgr.ShowDialog();

            // process selection should be complete
            if (diagEval == System.Windows.Forms.DialogResult.OK)
            {
                targetProc = processMgr.returnProcess;
                processLabel.Text = String.Format("{0:G} | {1:S}", targetProc.Id, targetProc.ProcessName);
                processLabel.ForeColor = Color.Black;
                procHandle = targetProc.Handle;
                moduleBaseAddress = targetProc.MainModule.BaseAddress;
                isValidProcess = true;
                pulseTimer.Start();
                pulseTimer.Interval = 30;
                UnlockControls();

                // patch memory to disable camera movements
                byte[] camDisablePatch = new byte[5];
                camDisablePatch[0] = 0xC3;
                camDisablePatch[1] = 0x90;
                camDisablePatch[2] = 0x90;
                camDisablePatch[3] = 0x90;
                camDisablePatch[4] = 0x90;

                byte[] hudDisablePatch = new byte[7];
                hudDisablePatch[0] = 0xC3;
                hudDisablePatch[1] = 0x90;
                hudDisablePatch[2] = 0x90;
                hudDisablePatch[3] = 0x90;
                hudDisablePatch[4] = 0x90;
                hudDisablePatch[5] = 0x90;
                hudDisablePatch[6] = 0x90;

                originalCamCode = Read(procHandle, (IntPtr)(0x437A20), 5, ref pedantry);
                originalHUDCode = Read(procHandle, (IntPtr)(0x00413D9E), 7, ref pedantry);
                Write(procHandle, (IntPtr)(0x437A20), (uint)5, camDisablePatch);
                Write(procHandle, (IntPtr)(0x00413D9E), (uint)7, hudDisablePatch);
                byte[] hudDisableVar = new byte[1];
                hudDisableVar[0] = 0xFF;
                Write(procHandle, (IntPtr)(0x3B0EF40), (uint)1, hudDisableVar);

                GetFinalInteractPointer();
            }
            processMgr.Close();
        }

        private void DetatchProcess()
        {
            if (isValidProcess)
            {
                Write(procHandle, (IntPtr)(0x437A20), (uint)5, originalCamCode); // restore the original behaviors
                Write(procHandle, (IntPtr)(0x00413D9E), (uint)7, originalHUDCode);
                Write(procHandle, displayCodeAddress, (uint)4, originalSonicDisplayCode);
                byte[] hudDisableVar = new byte[1];
                hudDisableVar[0] = 0x00;
                Write(procHandle, (IntPtr)(0x3B0EF40), (uint)1, hudDisableVar);
                UInt16 interactValue = 2;
                Write(procHandle, interactWithObjectsPtr, 2, BitConverter.GetBytes(interactValue));
                isValidProcess = false;
                pulseTimer.Stop();
                LockControls();
                processLabel.Text = "No process loaded.";
                processLabel.ForeColor = Color.Red;
                lockSonicToCamCheckBox.Checked = false;
            }
        }

        private void pulseTimer_Tick(object sender, EventArgs e)
        {
            if (targetProc.HasExited == false) // check to see if detatching is necessary
            {
                Pulse();
            }
            else // program has terminated, stop any attempts to read memory
            {
                DetatchProcess();
            }
        }

        private void detatchButton_Click(object sender, EventArgs e)
        {
            DetatchProcess();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Idle";
            statusLabel.ForeColor = Color.Black;
            recordingExistLabel.Text = "Recording Loaded";
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            AboutBox1 infoBox = new AboutBox1();

            infoBox.ShowDialog();

            infoBox.Dispose();
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            DialogResult showDialogResult = openFileDialog1.ShowDialog();

            if (showDialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!File.Exists(openFileDialog1.FileName))
                {
                    MessageBox.Show("Error opening file - file does not exist");
                    return;
                }

                bool errorFlag = false;
                string errorMessage = "";
                cameraData = CameraData.Load(openFileDialog1.FileName, out errorFlag, out errorMessage);

                if (errorFlag)
                {
                    MessageBox.Show(errorMessage);
                }
                else fileIsLoaded = true;

                recordingExistLabel.Text = "Recording Loaded";
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (fileIsLoaded)
            {
                playModeActivated = true;
                pulseTimer.Start();
                statusLabel.Text = "Playing...";
            }
            else
            {
                MessageBox.Show("Error: No playback data loaded, cannot play.");
            }
        }

        private void stopButton_Click_1(object sender, EventArgs e)
        {
            playModeActivated = false;
            currentPlaybackFrame = 0;
            statusLabel.Text = "Stopped";
        }

        private void lockSonicToCamCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (lockSonicToCamCheckBox.Checked)
            {
                if (isValidProcess)
                {
                    byte[] disableRenderPatch = new byte[4] { 0xC3, 0x90, 0x90, 0x90 };
                    Write(procHandle, displayCodeAddress, (uint)4, disableRenderPatch);

                    UInt16 interactValue = 0;

                    Write(procHandle, interactWithObjectsPtr, 2, BitConverter.GetBytes(interactValue));
                }
            }
            else
            {
                if (isValidProcess)
                {
                    Write(procHandle, displayCodeAddress, (uint)4, originalSonicDisplayCode);
                    UInt16 interactValue = 2;

                    Write(procHandle, interactWithObjectsPtr, 2, BitConverter.GetBytes(interactValue));
                }
            }
        }
        // end of class
    }
}
