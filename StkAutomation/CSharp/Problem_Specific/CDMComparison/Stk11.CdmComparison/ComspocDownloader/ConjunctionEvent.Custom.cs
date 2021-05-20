using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComspocDownloader.ConjunctionAssessment
{
    public partial class Conjunction
    {
        private string analysisStartTimeField;
        private string analysisStopTimeField;

        private string conjunctionStartTimeField;
        private string conjunctionStopTimeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string AnalysisStartTime
        {
            get
            {
                return this.analysisStartTimeField;
            }
            set
            {
                this.analysisStartTimeField = value;
                this.RaisePropertyChanged("AnalysisStartTime");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string AnalysisStopTime
        {
            get
            {
                return this.analysisStopTimeField;
            }
            set
            {
                this.analysisStopTimeField = value;
                this.RaisePropertyChanged("AnalysisStopTime");
            }
        }


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string ConjunctionStartTime
        {
            get
            {
                return this.conjunctionStartTimeField;
            }
            set
            {
                this.conjunctionStartTimeField = value;
                this.RaisePropertyChanged("ConjunctionStartTime");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string ConjunctionStopTime
        {
            get
            {
                return this.conjunctionStopTimeField;
            }
            set
            {
                this.conjunctionStopTimeField = value;
                this.RaisePropertyChanged("ConjunctionStopTime");
            }
        }



    }

    public partial class ConjunctionEvent 
    {
        private string eventTimeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string EventTime
        {
            get
            {
                return this.eventTimeField;
            }
            set
            {
                this.eventTimeField = value;
                this.RaisePropertyChanged("EventTime");
            }
        }
    }


    public partial class ConjunctionSpaceObjectEphemerisDetail    
    {
        private string epochField;

        private string fitSpanStartTimeField;

        private string fitSpanStopTimeField;

        private string startTimeField;
                
        private string stopTimeField;
                
        private string vectorEpochField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 4)]
        public string Epoch
        {
            get
            {
                return this.epochField;
            }
            set
            {
                this.epochField = value;
                this.RaisePropertyChanged("Epoch");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string FitSpanStartTime
        {
            get
            {
                return this.fitSpanStartTimeField;
            }
            set
            {
                this.fitSpanStartTimeField = value;
                this.RaisePropertyChanged("FitSpanStartTime");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string FitSpanStopTime
        {
            get
            {
                return this.fitSpanStopTimeField;
            }
            set
            {
                this.fitSpanStopTimeField = value;
                this.RaisePropertyChanged("FitSpanStopTime");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
                this.RaisePropertyChanged("StartTime");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public string StopTime
        {
            get
            {
                return this.stopTimeField;
            }
            set
            {
                this.stopTimeField = value;
                this.RaisePropertyChanged("StopTime");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 19)]
        public string VectorEpoch
        {
            get
            {
                return this.vectorEpochField;
            }
            set
            {
                this.vectorEpochField = value;
                this.RaisePropertyChanged("VectorEpoch");
            }
        }
    }
    
}
