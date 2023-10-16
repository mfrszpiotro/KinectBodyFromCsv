using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    internal static class CsvBodyHelpers
    {

        public static Dictionary<float, IReadOnlyDictionary<JointType, Joint>> ReadFromFile(string fileName)
        {
            Dictionary<float, IReadOnlyDictionary<JointType, Joint>> jointsInTime = new Dictionary<float, IReadOnlyDictionary<JointType, Joint>>();
            using (var reader = new StreamReader(fileName))
            {
                string headline = reader.ReadLine();
                string[] headers = headline.Split(',');
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    float timeInstant = float.Parse(values[0], CultureInfo.InvariantCulture);
                    Dictionary<JointType, Joint> joints = new Dictionary<JointType, Joint>();
                    float x = float.NaN;
                    float y = float.NaN;
                    for (uint i = 1; i < values.Length; i++)
                    {
                        string[] hp = headers[i].Split('_');
                        Tuple<string, string> header = new Tuple<string, string>(hp[0], hp[1]);
                        // if header.Item1 is not in JointTypes then throw FileFormatException
                        if (header.Item2 == "x")
                        {
                            x = float.Parse(values[i], CultureInfo.InvariantCulture);
                        }
                        else if (header.Item2 == "y")
                        {
                            y = float.Parse(values[i], CultureInfo.InvariantCulture);
                        }
                        else if (header.Item2 == "z")
                        {
                            if (x == float.NaN || y == float.NaN) throw new FileFormatException("Columns should be provided in x, y, z order.");
                            JointType jt = (JointType)Enum.Parse(typeof(JointType), header.Item1);
                            joints.Add(jt, InitJointFromCsvPoint(jt, x, y, float.Parse(values[i], CultureInfo.InvariantCulture)));
                            x = float.NaN;
                            y = float.NaN;
                        }
                        else
                        {
                            throw new FileFormatException("Header " + headers[i] + " should end with x, y or z after '_'");
                        }
                    }
                    jointsInTime.Add(timeInstant, joints);
                }
            }
            return jointsInTime;
        }

        private static Joint InitJointFromCsvPoint(JointType type, float x, float y, float z)
        {
            Joint j = new Joint();
            j.TrackingState = TrackingState.Tracked;
            j.JointType = type;
            j.Position = new CameraSpacePoint();
            j.Position.X = x;
            j.Position.Y = y;
            j.Position.Z = z;
            return j;
        }
    }
}