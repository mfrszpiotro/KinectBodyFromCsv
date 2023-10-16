using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Globalization;
using CsvHelper;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    internal class CsvBody
    {
        public float Time { get; set; }
        public float SpineBase_x { get; set; }      public float SpineBase_y { get; set; }      public float SpineBase_z { get; set; }
        public float SpineMid_x { get; set; }       public float SpineMid_y { get; set; }       public float SpineMid_z { get; set; }
        public float Neck_x { get; set; }           public float Neck_y { get; set; }           public float Neck_z { get; set; }
        public float Head_x { get; set; }           public float Head_y { get; set; }           public float Head_z { get; set; }
        public float ShoulderLeft_x { get; set; }   public float ShoulderLeft_y { get; set; }   public float ShoulderLeft_z { get; set; }
        public float ElbowLeft_x { get; set; }      public float ElbowLeft_y { get; set; }      public float ElbowLeft_z { get; set; }
        public float WristLeft_x { get; set; }      public float WristLeft_y { get; set; }      public float WristLeft_z { get; set; }
        public float HandLeft_x { get; set; }       public float HandLeft_y { get; set; }       public float HandLeft_z { get; set; }
        public float ShoulderRight_x { get; set; }  public float ShoulderRight_y { get; set; }  public float ShoulderRight_z { get; set; }
        public float ElbowRight_x { get; set; }     public float ElbowRight_y { get; set; }     public float ElbowRight_z { get; set; }
        public float WristRight_x { get; set; }     public float WristRight_y { get; set; }     public float WristRight_z { get; set; }
        public float HandRight_x { get; set; }      public float HandRight_y { get; set; }      public float HandRight_z { get; set; }
        public float HipLeft_x { get; set; }        public float HipLeft_y { get; set; }        public float HipLeft_z { get; set; }
        public float KneeLeft_x { get; set; }       public float KneeLeft_y { get; set; }       public float KneeLeft_z { get; set; }
        public float AnkleLeft_x { get; set; }      public float AnkleLeft_y { get; set; }      public float AnkleLeft_z { get; set; }
        public float FootLeft_x { get; set; }       public float FootLeft_y { get; set; }       public float FootLeft_z { get; set; }
        public float HipRight_x { get; set; }       public float HipRight_y { get; set; }       public float HipRight_z { get; set; }
        public float KneeRight_x { get; set; }      public float KneeRight_y { get; set; }      public float KneeRight_z { get; set; }
        public float AnkleRight_x { get; set; }     public float AnkleRight_y { get; set; }     public float AnkleRight_z { get; set; }
        public float FootRight_x { get; set; }      public float FootRight_y { get; set; }      public float FootRight_z { get; set; }
        public float SpineShoulder_x { get; set; }  public float SpineShoulder_y { get; set; }  public float SpineShoulder_z { get; set; }
        public float HandTipLeft_x { get; set; }    public float HandTipLeft_y { get; set; }    public float HandTipLeft_z { get; set; }
        public float ThumbLeft_x { get; set; }      public float ThumbLeft_y { get; set; }      public float ThumbLeft_z { get; set; }
        public float HandTipRight_x { get; set; }   public float HandTipRight_y { get; set; }   public float HandTipRight_z { get; set; }
        public float ThumbRight_x { get; set; }     public float ThumbRight_y { get; set; }    public float ThumbRight_z { get; set; }

        public List<CsvBody> ReadFromFile(string fileName)
        {
            using (var reader = new StreamReader("saved\\"+fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvBody>();
            }
            return records;
        }

        public void InitFile(string fileName)
        {
            using (var writer = new StreamWriter("saved\\"+fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                List<CsvBody> emptyList = new List<CsvBody>()
                csv.WriteRecords(emptyList);
            }
        }

        public void AppendToFile(string fileName, List<CsvBody> records)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var stream = File.Open("saved\\"+fileName, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
