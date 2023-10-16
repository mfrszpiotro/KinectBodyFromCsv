using CsvHelper;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    internal static class CsvBodyHelpers
    {
        public static void InitFile(string fileName)
        {
            using (var writer = new StreamWriter("saved\\" + fileName + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                IEnumerable<CsvBody> emptyList = new List<CsvBody>();
                csv.WriteRecords(emptyList);
            }
        }

        public static Dictionary<float, IReadOnlyDictionary<JointType, Joint>> ReadFromFile(string fileName)
        {
            List<CsvBody> records = null;
            using (var reader = new StreamReader("saved\\" + fileName + ".csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<CsvBody>().ToList();

            }
            Dictionary<float, IReadOnlyDictionary<JointType, Joint>> jointsInTime = new Dictionary<float, IReadOnlyDictionary<JointType, Joint>>();
            List<PropertyInfo> properties = typeof(CsvBody).GetProperties().ToList();
            foreach (CsvBody record in records)
            {
                Dictionary<JointType, Joint> joints = new Dictionary<JointType, Joint>();
                float timeInstant = (float)properties.First().GetValue(record);
                float x = float.NaN;
                float y = float.NaN;
                foreach (PropertyInfo property in properties.Skip(1))
                {
                    string[] hp = property.Name.Split('_');
                    Tuple<string, string> header = new Tuple<string, string>(hp[0], hp[1]);
                    float value = (float)property.GetValue(record);
                    if (header.Item2 == "x")
                    {
                        x = value;
                    }
                    else if (header.Item2 == "y")
                    {
                        y = value;
                    }
                    else if (header.Item2 == "z")
                    {
                        if (x == float.NaN || y == float.NaN) throw new FileFormatException("Columns should be provided in x, y, z order.");
                        JointType jt = (JointType)Enum.Parse(typeof(JointType), header.Item1);
                        joints.Add(jt, InitJointFromCsvPoint(jt, x, y, value));
                        x = float.NaN;
                        y = float.NaN;
                    }
                }
                jointsInTime.Add(timeInstant, joints);
            }
            return jointsInTime;
        }
        public static Dictionary<float, IReadOnlyDictionary<JointType, Joint>> ReadFromFile(string fileName, bool withHelper=true)
        {
            Dictionary<float, IReadOnlyDictionary<JointType, Joint>> jointsInTime = new Dictionary<float, IReadOnlyDictionary<JointType, Joint>>();
            using (var reader = new StreamReader(fileName + ".csv"))
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
                        float value = float.Parse(values[i], CultureInfo.InvariantCulture);
                        string[] hp = headers[i].Split('_');
                        Tuple<string, string> header = new Tuple<string, string>(hp[0], hp[1]);
                        // if header.Item1 is not in JointTypes then throw FileFormatException
                        if (header.Item2 == "x")
                        {
                            x = value;
                        }
                        else if (header.Item2 == "y")
                        {
                            y = value;
                        }
                        else if (header.Item2 == "z")
                        {
                            if (x == float.NaN || y == float.NaN) throw new FileFormatException("Columns should be provided in x, y, z order.");
                            JointType jt = (JointType)Enum.Parse(typeof(JointType), header.Item1);
                            joints.Add(jt, InitJointFromCsvPoint(jt, x, y, value));
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