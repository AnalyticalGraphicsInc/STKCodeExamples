using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stk12.UiPlugin.Articulations
{ 
    public class Section
    {
        public string sectionName { get; set; }
        public string startTimeValue { get; set; }
        public string durationValue { get; set; }
        public string startValue { get; set; }
        public string endValue { get; set; }
        public string deadbandValue { get; set; }
        public string accelValue { get; set; }
        public string decelValue { get; set; }
        public string dutyValue { get; set; }
        public string periodValue { get; set; }
        public string sectionText { get; set; }
        public string articName { get; set; }
        public string objectName { get; set; }
        public int sectionNumber { get; set; }
        public string linkString { get; set; }
        public string linkType { get; set; }
        public string linkTimeInstanceName { get; set; }
        public string linkRelativePath { get; set; }
        public bool isLinked { get; set; }
        public string timeInstantName { get; set; }
        public bool linkedToList { get; set; }
        public bool linkedToStart { get; set; }
        public bool linkedToStop { get; set; }
        public List<string> linkedToListStrings { get; set; }
        public List<string> linkedToListInstantNames { get; set; }
        public List<LinkedListSection> linkedListSections { get; set; }
        public bool isIncremented { get; set; }
        public bool linkedToAttitude { get; set; }

        public Section()
        {
            linkedToListInstantNames = new List<string>();
            linkedToListStrings = new List<string>();
            linkedListSections = new List<LinkedListSection>();

        }
        public Section Clone(Section original)
        {
            Section cloneSection = new Section();
            cloneSection.sectionName = original.sectionName;
            cloneSection.startTimeValue= original.startTimeValue;
            cloneSection.durationValue= original.durationValue;
            cloneSection.startValue= original.startValue;
            cloneSection.endValue= original.endValue;
            cloneSection.deadbandValue= original.deadbandValue;
            cloneSection.accelValue= original.accelValue;
            cloneSection.decelValue= original.decelValue;
            cloneSection.dutyValue= original.dutyValue;
            cloneSection.periodValue= original.periodValue;
            cloneSection.sectionText= original.sectionText;
            cloneSection.articName= original.articName;
            cloneSection.objectName= original.objectName;
            cloneSection.sectionNumber= original.sectionNumber;
            cloneSection.linkString= original.linkString;
            cloneSection.linkType= original.linkType;
            cloneSection.linkTimeInstanceName= original.linkTimeInstanceName;
            cloneSection.linkRelativePath= original.linkRelativePath;
            cloneSection.isLinked= original.isLinked;
            cloneSection.timeInstantName= original.timeInstantName;
            cloneSection.linkedToList= original.linkedToList;
            cloneSection.linkedToStart= original.linkedToStart;
            cloneSection.linkedToStop= original.linkedToStop;
            cloneSection.linkedToListStrings= original.linkedToListStrings;
            cloneSection.linkedToListInstantNames= original.linkedToListInstantNames;
            cloneSection.linkedListSections= original.linkedListSections;
            cloneSection.isIncremented= original.isIncremented;
            cloneSection.linkedToAttitude= original.linkedToAttitude;


            return cloneSection;

        }
    }
}
