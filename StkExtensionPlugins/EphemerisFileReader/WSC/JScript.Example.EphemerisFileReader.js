//=====================================================
//  Copyright 2012, Analytical Graphics, Inc.          
//=====================================================

//==========================================
// AgELogMsgType Enumeration
//==========================================
var eLogMsgDebug      = 0;
var eLogMsgInfo       = 1;
var eLogMsgForceInfo  = 2;
var eLogMsgWarning    = 3;
var eLogMsgAlarm      = 4;

//==========================================
// AgEAsEphemFileDistanceUnit
//==========================================
var eAsEphemFileDistanceUnitUnknown   = -1;
var eAsEphemFileDistanceUnitMeter     = 0;
var eAsEphemFileDistanceUnitKilometer = 1;
var eAsEphemFileDistanceUnitKiloFeet  = 2;
var eAsEphemFileDistanceUnitFeet      = 3;
var eAsEphemFileDistanceUnitNautMile  = 4;

//==========================================
// AgEAsEphemFileTimeUnit
//==========================================
var eAsEphemFileTimeUnitUnknown       = -1;
var eAsEphemFileTimeUnitSecond        = 0;
var eAsEphemFileTimeUnitMinute        = 1;
var eAsEphemFileTimeUnitHour          = 2;
var eAsEphemFileTimeUnitDay           = 3;

//==========================================
// AgEAsEphemInterpolationMethod
//==========================================
var eAsEphemInterpolationMethodUnknown     = -1;
var eAsEphemInterpolationMethodLagrange    = 0;
var eAsEphemInterpolationMethodHermite     = 1;
var eAsEphemInterpolationMethodLagrangeVOP = 2;

//==========================================
// AgEAsCovRep
//==========================================
var eAsCovRepUnknown  = -1;
var eAsCovRepStandard = 0;
var eAsCovRepRIC      = 1;

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite = null;
var m_extension = ".txt";

//=======================
// Init method
//=======================
function Init(Site)
{
    m_AgUtPluginSite = Site
    return true;
}

//===========================
// Register method
//===========================
function Register(Result)
{
    if (Result)
    {
        Result.FormatID = "JScript.Example.EphemerisFileReader";
        Result.Name = "JScript ECF Report Reader";
        Result.AddFileExtension(m_extension);
    }
}

//===========================
// ReadEphemeris method
//===========================
function ReadEphemeris(Result)
{
    if (Result)
    {
        Result.Centralbody = "Earth";
        Result.Coordinatesystem = "Fixed";
        Result.InterpolationMethod = eAsEphemInterpolationMethodLagrange;
        Result.InterpolationOrder = 5;
        Result.CovarianceRepresentation = eAsCovRepRIC;

        Result.SetUnits( eAsEphemFileDistanceUnitKilometer, eAsEphemFileTimeUnitSecond );

        var fileSystemObject = new ActiveXObject("Scripting.FileSystemObject");
        var textReader = fileSystemObject.OpenTextFile( Result.Filename );
        var fileLines = textReader.ReadAll().split("\n");

        var lineNumber = 0;
        var numPointsRead = 0;
        var added;

        // Go through each line in the data report and parse it
        var i;
        for (i = 0; i < fileLines.length; i++)
        {
            var elements = fileLines[i].split("    ");
            lineNumber = lineNumber + 1;
            if (lineNumber > 6 && elements.length >= 6)
            {
                var d = elements[0];
                var x = parseFloat(elements[1]);
                var y = parseFloat(elements[2]);
                var z = parseFloat(elements[3]);
                var vx = parseFloat(elements[4]);
                var vy = parseFloat(elements[5]);
                var vz = parseFloat(elements[6]);

                added = Result.AddEphemerisAtEpoch("UTCG", d, x, y, z, vx, vy, vz, new ActiveXObject("Scripting.Dictionary").items());
                if (added)
                {
                    numPointsRead = numPointsRead + 1;
                }
                else
                {
                    m_AgUtPluginSite.Message(eUtLogMsgWarning, "Could not add point #" + numPointsRead);
                }
            }
        }

        m_AgUtPluginSite.Message( eLogMsgInfo, "JScript Data Report Reader read " + numPointsRead + " points" );
    }
}

//======================
// ReadMetaData Method
//======================
function ReadMetaData(Result)
{
    if (Result)
    {
        Result.AddMetaData("My Metadata", "Custom");
    }
}

//======================
// Verify Method
//======================
function Verify(Result)
{
    if (Result)
    {
        var fileSystemObject = new ActiveXObject("Scripting.FileSystemObject");
        if (!(new RegExp(m_extension + "$").test(Result.Filename)))
        {
            Result.IsValid = false;
            Result.Message = "File must have a .txt extension";
        }

        // For instance, let's reject text files that we don't recognize as the ECF Position & Velocity Data Report
        var textReader = fileSystemObject.OpenTextFile(Result.Filename);
        textReader.ReadLine();
        var secondLine = textReader.ReadLine();
        textReader.close();

        if (secondLine.indexOf("ECF Position & Velocity") == -1)
        {
            Result.IsValid = false;
            Result.Message = "File doesn't seem to be an ECF Position & Velocity Data Report";
        }
    }
}

//======================
// Free Method
//======================
function Free(Result)
{
    m_AgUtPluginSite = null;
}

//=====================================================
//  Copyright 2012, Analytical Graphics, Inc.          
//=====================================================
