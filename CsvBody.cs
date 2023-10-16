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
using System.Reflection;

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

        public CsvBody()
        {
            List<PropertyInfo> properties = typeof(CsvBody).GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, 0);
            }
        }

        public CsvBody(double secondInstant, IReadOnlyDictionary<JointType, Joint> joints)
        {
            foreach (KeyValuePair<JointType, Joint> pair in joints)
            {
                switch (pair.Key)
                {
                    case JointType.Head:
                        this.Head_x = pair.Value.Position.X;            this.Head_y = pair.Value.Position.Y;            this.Head_z = pair.Value.Position.Z; break;
                    case JointType.Neck:
                        this.Neck_x = pair.Value.Position.X;            this.Neck_y = pair.Value.Position.Y;            this.Neck_z = pair.Value.Position.Z; break;
                    case JointType.SpineShoulder:
                        this.SpineShoulder_x = pair.Value.Position.X;   this.SpineShoulder_y = pair.Value.Position.Y;   this.SpineShoulder_z = pair.Value.Position.Z; break;
                    case JointType.ShoulderLeft:
                        this.ShoulderLeft_x = pair.Value.Position.X;    this.ShoulderLeft_y = pair.Value.Position.Y;    this.ShoulderLeft_z = pair.Value.Position.Z; break;
                    case JointType.ElbowLeft:
                        this.ElbowLeft_x = pair.Value.Position.X;       this.ElbowLeft_y = pair.Value.Position.Y;       this.ElbowLeft_z = pair.Value.Position.Z; break;
                    case JointType.WristLeft:
                        this.WristLeft_x = pair.Value.Position.X;       this.WristLeft_y = pair.Value.Position.Y;       this.WristLeft_z = pair.Value.Position.Z; break;
                    case JointType.HandLeft:
                        this.HandLeft_x = pair.Value.Position.X;        this.HandLeft_y = pair.Value.Position.Y;        this.HandLeft_z = pair.Value.Position.Z; break;
                    case JointType.HandTipLeft:
                        this.HandTipLeft_x = pair.Value.Position.X;     this.HandTipLeft_y = pair.Value.Position.Y;     this.HandTipLeft_z = pair.Value.Position.Z; break;
                    case JointType.ThumbLeft:
                        this.ThumbLeft_x = pair.Value.Position.X;       this.ThumbLeft_y = pair.Value.Position.Y;       this.ThumbLeft_z = pair.Value.Position.Z; break;
                    case JointType.ShoulderRight:
                        this.ShoulderRight_x = pair.Value.Position.X;   this.ShoulderRight_y = pair.Value.Position.Y;   this.ShoulderRight_z = pair.Value.Position.Z; break;
                    case JointType.ElbowRight:
                        this.ElbowRight_x = pair.Value.Position.X;      this.ElbowRight_y = pair.Value.Position.Y;      this.ElbowRight_z = pair.Value.Position.Z; break;
                    case JointType.WristRight:
                        this.WristRight_x = pair.Value.Position.X;      this.WristRight_y = pair.Value.Position.Y;      this.WristRight_z = pair.Value.Position.Z; break;
                    case JointType.HandRight:
                        this.HandRight_x = pair.Value.Position.X;       this.HandRight_y = pair.Value.Position.Y;       this.HandRight_z = pair.Value.Position.Z; break;
                    case JointType.HandTipRight:
                        this.HandTipRight_x = pair.Value.Position.X;    this.HandTipRight_y = pair.Value.Position.Y;    this.HandTipRight_z = pair.Value.Position.Z; break;
                    case JointType.ThumbRight:
                        this.ThumbRight_x = pair.Value.Position.X; this.ThumbRight_y = pair.Value.Position.Y; this.ThumbRight_z = pair.Value.Position.Z; break;
                    case JointType.SpineMid:
                        this.SpineMid_x = pair.Value.Position.X;        this.SpineMid_y = pair.Value.Position.Y;        this.SpineMid_z = pair.Value.Position.Z; break;
                    case JointType.SpineBase:
                        this.SpineBase_x = pair.Value.Position.X;       this.SpineBase_y = pair.Value.Position.Y;       this.SpineBase_z = pair.Value.Position.Z; break;
                    case JointType.HipLeft:
                        this.HipLeft_x = pair.Value.Position.X;         this.HipLeft_y = pair.Value.Position.Y;         this.HipLeft_z = pair.Value.Position.Z; break;
                    case JointType.KneeLeft:
                        this.KneeLeft_x = pair.Value.Position.X;        this.KneeLeft_y = pair.Value.Position.Y;        this.KneeLeft_z = pair.Value.Position.Z; break;
                    case JointType.AnkleLeft:
                        this.AnkleLeft_x = pair.Value.Position.X;       this.AnkleLeft_y = pair.Value.Position.Y;       this.AnkleLeft_z = pair.Value.Position.Z; break;
                    case JointType.FootLeft:
                        this.FootLeft_x = pair.Value.Position.X;        this.FootLeft_y = pair.Value.Position.Y;        this.FootLeft_z = pair.Value.Position.Z; break;
                    case JointType.HipRight:
                        this.HipRight_x = pair.Value.Position.X;        this.HipRight_y = pair.Value.Position.Y;        this.HipRight_z = pair.Value.Position.Z; break;
                    case JointType.KneeRight:
                        this.KneeRight_x = pair.Value.Position.X;       this.KneeRight_y = pair.Value.Position.Y;       this.KneeRight_z = pair.Value.Position.Z; break;
                    case JointType.AnkleRight:
                        this.AnkleRight_x = pair.Value.Position.X;      this.AnkleRight_y = pair.Value.Position.Y;      this.AnkleRight_z = pair.Value.Position.Z; break;
                    case JointType.FootRight:
                        this.FootRight_x = pair.Value.Position.X;       this.FootRight_y = pair.Value.Position.Y;       this.FootRight_z = pair.Value.Position.Z; break;
                    default:
                        throw new ArgumentException("Unexpected joint value");
                }
            }
            this.Time = (float)secondInstant;
        }

        public void AppendToFile(string fileName)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var stream = File.Open("saved\\"+fileName+".csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecord(this);
                csv.NextRecord();
            }
        }
    }
}
