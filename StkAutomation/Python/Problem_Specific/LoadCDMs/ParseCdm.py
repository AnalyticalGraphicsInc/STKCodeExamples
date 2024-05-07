# cdm parsing part of the LoadCDM script 

class ParseCdm():
    def ParseCdmFromFile(filePath):
        print(filePath)

        allCdmData = []

        import xml.etree.ElementTree as ET
        tree = ET.parse(filePath)
        root = tree.getroot()

        for cdm in root.iter('cdm'):
            thisCdmData = CdmData()

            header = cdm.find('header')
            thisCdmData.ID = header.find('COMMENT').text
            

            body = cdm.find('body')
            relativeMetadataData = body.find('relativeMetadataData')
            
            thisCdmData.TCA = relativeMetadataData.find('TCA').text.replace("Z", "")
            thisCdmData.MISS_DISTANCE_VALUE = relativeMetadataData.find('MISS_DISTANCE').text
            thisCdmData.MISS_DISTANCE_UNITS = relativeMetadataData.find('MISS_DISTANCE').attrib['units']
            thisCdmData.RELATIVE_SPEED_VALUE = relativeMetadataData.find('RELATIVE_SPEED').text
            thisCdmData.RELATIVE_SPEED_UNITS = relativeMetadataData.find('RELATIVE_SPEED').attrib['units']
            
            for segment in body.iter('segment'):
                thisSegment = Segment()
                metadata = segment.find('metadata')
                thisSegment.OBJECT_DESIGNATOR = metadata.find('OBJECT_DESIGNATOR').text
                thisSegment.OBJECT_NAME = metadata.find('OBJECT_NAME').text
                thisSegment.UniqueName = thisCdmData.ID.replace('CDM_ID:', '') + '_' + thisSegment.OBJECT_NAME.replace(' ', '_').replace("(", "").replace(")", "").replace("/", "")
                thisSegment.INTERNATIONAL_DESIGNATOR = metadata.find('INTERNATIONAL_DESIGNATOR').text
                thisSegment.REF_FRAME = metadata.find('REF_FRAME').text
                thisSegment.GRAVITY_MODEL = metadata.find('GRAVITY_MODEL').text
                thisSegment.N_BODY_PERTURBATIONS = metadata.find('N_BODY_PERTURBATIONS').text
                thisSegment.SOLAR_RAD_PRESSURE = metadata.find('SOLAR_RAD_PRESSURE').text
                thisSegment.EARTH_TIDES = metadata.find('EARTH_TIDES').text


                data = segment.find('data')
                stateVectorData = data.find('stateVector')
                thisStateVector = StateVector() 
                thisStateVector.X_Value = stateVectorData.find('X').text
                thisStateVector.X_Units = stateVectorData.find('X').attrib['units']
                thisStateVector.Y_Value = stateVectorData.find('Y').text
                thisStateVector.Y_Units = stateVectorData.find('Y').attrib['units']
                thisStateVector.Z_Value = stateVectorData.find('Z').text
                thisStateVector.Z_Units = stateVectorData.find('Z').attrib['units']
                thisStateVector.X_DOT_Value = stateVectorData.find('X_DOT').text
                thisStateVector.X_DOT_Units = stateVectorData.find('X_DOT').attrib['units']
                thisStateVector.Y_DOT_Value = stateVectorData.find('Y_DOT').text
                thisStateVector.Y_DOT_Units = stateVectorData.find('Y_DOT').attrib['units']
                thisStateVector.Z_DOT_Value = stateVectorData.find('Z_DOT').text
                thisStateVector.Z_DOT_Units = stateVectorData.find('Z_DOT').attrib['units']
                thisSegment.StateVector = thisStateVector


                covarianceMatrixData = data.find('covarianceMatrix')
                thisCovariance = Covariance() 
                thisCovariance.CR_R_Value = covarianceMatrixData.find('CR_R').text
                thisCovariance.CR_R_Units = covarianceMatrixData.find('CR_R').attrib['units']
                thisCovariance.CT_R_Value = covarianceMatrixData.find('CT_R').text
                thisCovariance.CT_R_Units = covarianceMatrixData.find('CT_R').attrib['units']
                thisCovariance.CT_T_Value = covarianceMatrixData.find('CT_T').text
                thisCovariance.CT_T_Units = covarianceMatrixData.find('CT_T').attrib['units']
                thisCovariance.CN_R_Value = covarianceMatrixData.find('CN_R').text
                thisCovariance.CN_R_Units = covarianceMatrixData.find('CN_R').attrib['units']
                thisCovariance.CN_T_Value = covarianceMatrixData.find('CN_T').text
                thisCovariance.CN_T_Units = covarianceMatrixData.find('CN_T').attrib['units']
                thisCovariance.CN_N_Value = covarianceMatrixData.find('CN_N').text
                thisCovariance.CN_N_Units = covarianceMatrixData.find('CN_N').attrib['units']
                thisCovariance.CRDot_R_Value = covarianceMatrixData.find('CRDOT_R').text
                thisCovariance.CRDot_R_Units = covarianceMatrixData.find('CRDOT_R').attrib['units']
                thisCovariance.CRDot_T_Value = covarianceMatrixData.find('CRDOT_T').text
                thisCovariance.CRDot_T_Units = covarianceMatrixData.find('CRDOT_T').attrib['units']
                thisCovariance.CRDot_N_Value = covarianceMatrixData.find('CRDOT_N').text
                thisCovariance.CRDot_N_Units = covarianceMatrixData.find('CRDOT_N').attrib['units']
                thisCovariance.CRDot_RDot_Value = covarianceMatrixData.find('CRDOT_RDOT').text
                thisCovariance.CRDot_RDot_Units = covarianceMatrixData.find('CRDOT_RDOT').attrib['units']
                thisCovariance.CTDot_R_Value = covarianceMatrixData.find('CTDOT_R').text
                thisCovariance.CTDot_R_Units = covarianceMatrixData.find('CTDOT_R').attrib['units']
                thisCovariance.CTDot_T_Value = covarianceMatrixData.find('CTDOT_T').text
                thisCovariance.CTDot_T_Units = covarianceMatrixData.find('CTDOT_T').attrib['units']
                thisCovariance.CTDot_N_Value = covarianceMatrixData.find('CTDOT_N').text
                thisCovariance.CTDot_N_Units = covarianceMatrixData.find('CTDOT_N').attrib['units']
                thisCovariance.CTDot_RDot_Value = covarianceMatrixData.find('CTDOT_RDOT').text
                thisCovariance.CTDot_RDot_Units = covarianceMatrixData.find('CTDOT_RDOT').attrib['units']
                thisCovariance.CTDot_TDot_Value = covarianceMatrixData.find('CTDOT_TDOT').text
                thisCovariance.CTDot_TDot_Units = covarianceMatrixData.find('CTDOT_TDOT').attrib['units']
                thisCovariance.CNDot_R_Value = covarianceMatrixData.find('CNDOT_R').text
                thisCovariance.CNDot_R_Units = covarianceMatrixData.find('CNDOT_R').attrib['units']
                thisCovariance.CNDot_T_Value = covarianceMatrixData.find('CNDOT_T').text
                thisCovariance.CNDot_T_Units = covarianceMatrixData.find('CNDOT_T').attrib['units']
                thisCovariance.CNDot_N_Value = covarianceMatrixData.find('CNDOT_N').text
                thisCovariance.CNDot_N_Units = covarianceMatrixData.find('CNDOT_N').attrib['units']
                thisCovariance.CNDot_RDot_Value = covarianceMatrixData.find('CNDOT_RDOT').text
                thisCovariance.CNDot_RDot_Units = covarianceMatrixData.find('CNDOT_RDOT').attrib['units']
                thisCovariance.CNDot_TDot_Value = covarianceMatrixData.find('CNDOT_TDOT').text
                thisCovariance.CNDot_TDot_Units = covarianceMatrixData.find('CNDOT_TDOT').attrib['units']
                thisCovariance.CNDot_NDot_Value = covarianceMatrixData.find('CNDOT_NDOT').text
                thisCovariance.CNDot_NDot_Units = covarianceMatrixData.find('CNDOT_NDOT').attrib['units']
                thisCovariance.CDrag_R_Value = covarianceMatrixData.find('CDRG_R').text
                thisCovariance.CDrag_R_Units = covarianceMatrixData.find('CDRG_R').attrib['units']
                thisCovariance.CDrag_T_Value = covarianceMatrixData.find('CDRG_T').text
                thisCovariance.CDrag_T_Units = covarianceMatrixData.find('CDRG_T').attrib['units']
                thisCovariance.CDrag_N_Value = covarianceMatrixData.find('CDRG_N').text
                thisCovariance.CDrag_N_Units = covarianceMatrixData.find('CDRG_N').attrib['units']
                thisCovariance.CDrag_RDot_Value = covarianceMatrixData.find('CDRG_RDOT').text
                thisCovariance.CDrag_RDot_Units = covarianceMatrixData.find('CDRG_RDOT').attrib['units']
                thisCovariance.CDrag_TDot_Value = covarianceMatrixData.find('CDRG_TDOT').text
                thisCovariance.CDrag_TDot_Units = covarianceMatrixData.find('CDRG_TDOT').attrib['units']
                thisCovariance.CDrag_NDot_Value = covarianceMatrixData.find('CDRG_NDOT').text
                thisCovariance.CDrag_NDot_Units = covarianceMatrixData.find('CDRG_NDOT').attrib['units']
                thisCovariance.CDrag_Drag_Value = covarianceMatrixData.find('CDRG_DRG').text
                thisCovariance.CDrag_Drag_Units = covarianceMatrixData.find('CDRG_DRG').attrib['units']
                thisCovariance.CSrp_R_Value = covarianceMatrixData.find('CSRP_R').text
                thisCovariance.CSrp_R_Units = covarianceMatrixData.find('CSRP_R').attrib['units']
                thisCovariance.CSrp_T_Value = covarianceMatrixData.find('CSRP_T').text
                thisCovariance.CSrp_T_Units = covarianceMatrixData.find('CSRP_T').attrib['units']
                thisCovariance.CSrp_N_Value = covarianceMatrixData.find('CSRP_N').text
                thisCovariance.CSrp_N_Units = covarianceMatrixData.find('CSRP_N').attrib['units']
                thisCovariance.CSrp_RDot_Value = covarianceMatrixData.find('CSRP_RDOT').text
                thisCovariance.CSrp_RDot_Units = covarianceMatrixData.find('CSRP_RDOT').attrib['units']
                thisCovariance.CSrp_TDot_Value = covarianceMatrixData.find('CSRP_TDOT').text
                thisCovariance.CSrp_TDot_Units = covarianceMatrixData.find('CSRP_TDOT').attrib['units']
                thisCovariance.CSrp_NDot_Value = covarianceMatrixData.find('CSRP_NDOT').text
                thisCovariance.CSrp_NDot_Units = covarianceMatrixData.find('CSRP_NDOT').attrib['units']
                thisCovariance.CSrp_Drag_Value = covarianceMatrixData.find('CSRP_DRG').text
                thisCovariance.CSrp_Drag_Units = covarianceMatrixData.find('CSRP_DRG').attrib['units']
                thisCovariance.CSrp_Srp_Value = covarianceMatrixData.find('CSRP_SRP').text
                thisCovariance.CSrp_Srp_Units = covarianceMatrixData.find('CSRP_SRP').attrib['units']
                thisSegment.Covariance = thisCovariance
                
                something = 7

                thisCdmData.Segment.append(thisSegment)

            allCdmData.append(thisCdmData)
        return allCdmData



class CdmData():
    def __init__(self) -> None:
        self.ID = None
        self.TCA = ''
        self.MISS_DISTANCE_VALUE = ''
        self.MISS_DISTANCE_UNITS = ''
        self.RELATIVE_SPEED_VALUE = ''
        self.RELATIVE_SPEED_UNITS = ''
        self.Segment = []

class Segment():
    def __init__(self) -> None:
        self.OBJECT = None
        self.OBJECT_DESIGNATOR = None
        self.OBJECT_NAME = None
        self.UniqueName = None
        self.INTERNATIONAL_DESIGNATOR = None
        self.REF_FRAME = None
        self.GRAVITY_MODEL = None
        self.N_BODY_PERTURBATIONS = None
        self.SOLAR_RAD_PRESSURE = None
        self.EARTH_TIDES = None
        self.StateVector = None
        self.Covariance = None

class StateVector():
    def __init__(self) -> None:
        self.X_Value = ''
        self.X_Units = ''
        self.Y_Value = ''
        self.Y_Units = ''
        self.Z_Value = ''
        self.Z_Units = ''
        self.X_DOT_Value = ''
        self.X_DOT_Units = ''
        self.Y_DOT_Value = ''
        self.Y_DOT_Units = ''
        self.Z_DOT_Value = ''
        self.Z_DOT_Units = ''

class Covariance():
    def __init__(self) -> None:
        self.CR_R_Value = '0.0'
        self.CR_R_Units = '0.0'
        self.CT_R_Value = '0.0'
        self.CT_R_Units = '0.0'
        self.CT_T_Value = '0.0'
        self.CT_T_Units = '0.0'
        self.CN_R_Value = '0.0'
        self.CN_R_Units = '0.0'
        self.CN_T_Value = '0.0'
        self.CN_T_Units = '0.0'
        self.CN_N_Value = '0.0'
        self.CN_N_Units = '0.0'
        self.CRDot_R_Value = '0.0'
        self.CRDot_R_Units = '0.0'
        self.CRDot_T_Value = '0.0'
        self.CRDot_T_Units = '0.0'
        self.CRDot_N_Value = '0.0'
        self.CRDot_N_Units = '0.0'
        self.CRDot_RDot_Value = '0.0'
        self.CRDot_RDot_Units = '0.0'
        self.CTDot_R_Value = '0.0'
        self.CTDot_R_Units = '0.0'
        self.CTDot_T_Value = '0.0'
        self.CTDot_T_Units = '0.0'
        self.CTDot_N_Value = '0.0'
        self.CTDot_N_Units = '0.0'
        self.CTDot_RDot_Value = '0.0'
        self.CTDot_RDot_Units = '0.0'
        self.CTDot_TDot_Value = '0.0'
        self.CTDot_TDot_Units = '0.0'
        self.CNDot_R_Value = '0.0'
        self.CNDot_R_Units = '0.0'
        self.CNDot_T_Value = '0.0'
        self.CNDot_T_Units = '0.0'
        self.CNDot_N_Value = '0.0'
        self.CNDot_N_Units = '0.0'
        self.CNDot_RDot_Value = '0.0'
        self.CNDot_RDot_Units = '0.0'
        self.CNDot_TDot_Value = '0.0'
        self.CNDot_TDot_Units = '0.0'
        self.CNDot_NDot_Value = '0.0'
        self.CNDot_NDot_Units = '0.0'
        self.CDrag_R_Value = '0.0'
        self.CDrag_R_Units = '0.0'
        self.CDrag_T_Value = '0.0'
        self.CDrag_T_Units = '0.0'
        self.CDrag_N_Value = '0.0'
        self.CDrag_N_Units = '0.0'
        self.CDrag_RDot_Value = '0.0'
        self.CDrag_RDot_Units = '0.0'
        self.CDrag_TDot_Value = '0.0'
        self.CDrag_TDot_Units = '0.0'
        self.CDrag_NDot_Value = '0.0'
        self.CDrag_NDot_Units = '0.0'
        self.CDrag_Drag_Value = '0.0'
        self.CDrag_Drag_Units = '0.0'
        self.CSrp_R_Value = '0.0'
        self.CSrp_R_Units = '0.0'
        self.CSrp_T_Value = '0.0'
        self.CSrp_T_Units = '0.0'
        self.CSrp_N_Value = '0.0'
        self.CSrp_N_Units = '0.0'
        self.CSrp_RDot_Value = '0.0'
        self.CSrp_RDot_Units = '0.0'
        self.CSrp_TDot_Value = '0.0'
        self.CSrp_TDot_Units = '0.0'
        self.CSrp_NDot_Value = '0.0'
        self.CSrp_NDot_Units = '0.0'
        self.CSrp_Drag_Value = '0.0'
        self.CSrp_Drag_Units = '0.0'
        self.CSrp_Srp_Value = '0.0'
        self.CSrp_Srp_Units = '0.0'





#cdmPath = r'C:\temp\1year\LoadCDMs\SomeCDM.xml'
#cdmData = ParseCdm(cdmPath)
#something = 7