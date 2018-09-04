using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SADXCamLib
{
    public class CameraData
    {
        private List<CameraState> cameraStates = new List<CameraState>();

        public Int32 StateCount 
        {
            get
            {
                if (cameraStates == null) return 0;
                else return cameraStates.Count;
            }
        }
        public List<CameraState> CameraStates { get { return cameraStates; } set { cameraStates = value; } }

        public CameraData()
        {
        }

        public static CameraData Load(String filePath, out bool errorState, out string errorString)
        {
            CameraData output = new CameraData();

            if (File.Exists(filePath))
            {
                FileStream inputStream = File.Open(filePath, FileMode.Open);
                BinaryReader inputReader = new BinaryReader(inputStream);

                if (inputStream.Length % 0x12 != 0)
                {
                    errorState = true;
                    errorString = "Error - camera playback file is invalid.";
                    return output;
                }

                int entryCount = (int)inputStream.Length / 0x12;

                for (int i = 0; i < entryCount; i++)
                {
                    try
                    {
                        float camXPos, camYPos, camZPos;
                        ushort camXRotation, camYRotation, camZRotation;

                        camXPos = inputReader.ReadSingle();
                        camYPos = inputReader.ReadSingle();
                        camZPos = inputReader.ReadSingle();

                        camXRotation = inputReader.ReadUInt16();
                        camYRotation = inputReader.ReadUInt16();
                        camZRotation = inputReader.ReadUInt16();

                        CameraState newState = new CameraState(i, new Vector3(camXPos, camYPos, camZPos), new EulerBAMSRotation(camXRotation, camYRotation, camZRotation));
                        output.cameraStates.Add(newState);
                    }
                    catch (EndOfStreamException)
                    {
                        errorState = true;
                        errorString = "Stream ended unexpectedly... good luck!";
                        return output;
                    }
                }

                inputReader.Close();
            }
            else
            {
                errorState = true;
                errorString = "Camera Playback Data load failed!";
                return output;
            }

            errorState = false;
            errorString = "Loading completed successfully!";
            return output;
        }
    }

    public struct CameraState
    {
        private int frameIndex;
        private Vector3 position;
        private EulerBAMSRotation rotation;

        public float FrameIndex { get { return frameIndex; } }
        public Vector3 Position { get { return position; } }
        public EulerBAMSRotation Rotation { get { return rotation; } }

        public CameraState(int frameIndex, Vector3 nodePosition, EulerBAMSRotation nodeRotation)
        {
            this.frameIndex = frameIndex;
            position = nodePosition;
            rotation = nodeRotation;
        }
    }

    public struct Vector3
    {
        private float x;
        private float y;
        private float z;

        public float X { get { return x; } }
        public float Y { get { return y; } }
        public float Z { get { return z; } }

        public Vector3(float xCoord, float yCoord, float zCoord)
        {
            x = xCoord;
            y = yCoord;
            z = zCoord;
        }
    }

    public struct EulerBAMSRotation
    {
        private ushort x,y,z;
        public ushort X { get { return x; } set { x = value;}}
        public ushort Y { get { return y; } set { y = value;}}
        public ushort Z { get { return z; } set { z= value;}}

        public EulerBAMSRotation(ushort x, ushort y, ushort z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
