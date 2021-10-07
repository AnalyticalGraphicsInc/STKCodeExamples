using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DataProviderExplorer
{
    [Serializable]
    [XmlRoot(ElementName = "ANSYS_EnggData")]
    public class ANSYS_EnggData
    {
        public ANSYS_EnggData()
        {
            LoadVariationData = new LoadVariationData
            {
                MatML_Doc = new MatML_Doc
                {
                    LoadVariation = new LoadVariation[0]
                }
            };
        }

        public void WriteLibraryFile(string path)
        {
            var serializer = new XmlSerializer(typeof(ANSYS_EnggData));
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(stream, this);
            }
        }

        public void AddProperty(string propertyName, IEnumerable<double> propertyValues,
            string parameterName, IEnumerable<double> parameterValues)
        {
            var numberOfExistingProperties = LoadVariationData.MatML_Doc.LoadVariation.Length;

            var propsList = LoadVariationData.MatML_Doc.LoadVariation.ToList();


            propsList.Add(new LoadVariation()
            {
                BulkDetails = new BulkDetails()
                {
                    Name = propertyName,
                    PropertyData = new PropertyData()
                    {
                        property = "pr" + (numberOfExistingProperties + 1),
                        Data = new Data()
                        {
                            Value = string.Join(",", propertyValues.Select(v => v.ToString())),
                            format = "float"
                        },
                        Qualifier = propertyName,
                        ParameterValue = new ParameterValue()
                        {
                            Value = string.Join(",", parameterValues.Select(v => v.ToString())),
                            format = "float",
                            parameter = "pa" + (numberOfExistingProperties + 1)
                        }
                    }
                },
                Metadata = new Metadata()
                {
                    ParameterDetails = new ParameterDetails()
                    {
                        id = "pa" + (numberOfExistingProperties + 1),
                        Name = parameterName
                    },
                    PropertyDetails = new ParameterDetails()
                    {
                        id = "pr" + (numberOfExistingProperties + 1),
                        Name = propertyName
                    }
                }
            });

            LoadVariationData.MatML_Doc.LoadVariation = propsList.ToArray();
        }

        [XmlElement(ElementName = "MaterialData")]
        public object MaterialData { get; set; }

        [XmlElement(ElementName = "ConvectionData")]
        public object ConvectionData { get; set; }
        
        [XmlElement(ElementName = "LoadVariationData")]
        public LoadVariationData LoadVariationData { get; set; }
        [XmlElement(ElementName = "BeamSectionData")]
        public object BeamSectionData { get; set; }

    }

    public class LoadVariationData
    {
        public LoadVariationData()
        {

        }
        [XmlElement(ElementName = "MatML_Doc")]
        public MatML_Doc MatML_Doc { get; set; }
    }

    public class MatML_Doc
    {
        public MatML_Doc()
        {

        }

        [XmlElement(ElementName = "LoadVariation")]
        public LoadVariation[] LoadVariation { get; set; }
    }

    public class LoadVariation
    {
        public LoadVariation()
        {

        }
        [XmlElement(ElementName = "BulkDetails")]
        public BulkDetails BulkDetails { get; set; }
        [XmlElement(ElementName = "Metadata")]
        public Metadata Metadata { get; set; }

    }

    public class Metadata
    {
        public Metadata()
        {

        }
        [XmlElement(ElementName = "ParameterDetails")]
        public ParameterDetails ParameterDetails { get; set; }
        [XmlElement(ElementName = "PropertyDetails")]
        public ParameterDetails PropertyDetails { get; set; }
    }

    public class ParameterDetails
    {
        public ParameterDetails()
        {

        }
        [XmlAttribute("id")] public string id { get; set; } = "pa1";
        //[XmlElement(ElementName = "Name")]
        public string Name { get; set; }
    }
    public class PropertyDetails
    {
        public PropertyDetails()
        {

        }
        [XmlAttribute("id")] public string id { get; set; } = "pr1";
        //[XmlElement(ElementName = "Name")]
        public string Name { get; set; }
    }

    public class BulkDetails
    {
        public BulkDetails()
        {

        }
        //[XmlElement(ElementName = "Name")]
        public string Name;
        [XmlElement(ElementName = "Form")] 
        public ansysForm Form { get; set; }
        [XmlElement(ElementName = "PropertyData")]
        public PropertyData PropertyData { get; set; }

    }

    public class ansysForm
    {
        public ansysForm()
        {

        }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
    }

    public class PropertyData
    {
        public PropertyData()
        {

        }
        [XmlAttribute("property")] public string property { get; set; } = "pr1";

        [XmlElement(ElementName = "Data")]
        public Data Data { get; set; }
        [XmlElement(ElementName = "Qualifier")]
        public string Qualifier { get; set; }

        [XmlElement(ElementName = "ParameterValue")]
        public ParameterValue ParameterValue { get; set; }


    }

    public class Data
    {
        public Data()
        {

        }
        [XmlText] public string Value;
        [XmlAttribute("format")] public string format { get; set; } = "float";
    }
    public class ParameterValue
    {
        public ParameterValue()
        {

        }
        [XmlText] public string Value;
        [XmlAttribute("format")] public string format { get; set; } = "float";
        [XmlAttribute("parameter")] public string parameter { get; set; } = "pa1";
    }
}
