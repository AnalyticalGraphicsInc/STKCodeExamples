stk.v.12.0
WrittenBy    STK_v12.0.0

BEGIN Facility

    Name		 Baghdad

    BEGIN CentroidPosition

        CentralBody		 Earth
        DisplayCoords		 Geodetic
        EcfLatitude		  3.3334037780761697e+01
        EcfLongitude		  4.4397834777832003e+01
        EcfAltitude		  0.0000000000000000e+00
        HeightAboveGround		  0.0000000000000000e+00
        ComputeTrnMaskAsNeeded		 Off
        SaveTrnMaskDataInBinary		 Off
        LightingGrazingType		 GroundModel
        DisplayAltRef		 Ellipsoid
        UseTerrainInfo		 Off
        NumAzRaysInMask		 360
        TerrainNormalMode		 UseCbShape

    END CentroidPosition

    BEGIN Extensions

        BEGIN LaserCAT
            Mode		 TargetObject
            StartTime		 31 Oct 2003 00:00:00.000000000
            StopTime		 1 Nov 2003 00:00:00.000000000
            RangeConstraint		 500000000
            MinElevationAng		 0.34907
            Duration		 0
            ExclHalfAng		 0.08727
            MaxPVtoScenario		 10
            CenterFrequency		 14000000000
            BandWidth		 20000000
            Linear_PowerFlux/EIRP		  1.0000000000000000e+14
            Linear_PowerThreshold		  6.3095734448019298e-04
            TransmitOn		 1
            ReceiveOn		 0
            PVDataBase		 stkAllComm.tce
            RFIDataBase		 stkAllComm.rfi
            UseGeomFilters		 Yes
            UseOutOfDate		 Yes
            NearEarthOutOfDate		  9.9999999999998099e+00
            DeepSpaceOutOfDate		  3.9999999999999240e+01
            LoadPotVictims		 No
            UsePotVictimList		 No
        END LaserCAT

        BEGIN ExternData
        END ExternData

        BEGIN RFI
            Mode		 TargetObject
            StartTime		 31 Oct 2003 00:00:00.000000000
            StopTime		 1 Nov 2003 00:00:00.000000000
            RangeConstraint		 500000000
            MinElevationAng		 0.34907
            Duration		 0
            ExclHalfAng		 0.08727
            MaxPVtoScenario		 10
            CenterFrequency		 14000000000
            BandWidth		 20000000
            Linear_PowerFlux/EIRP		  1.0000000000000000e+14
            Linear_PowerThreshold		  6.3095734448019298e-04
            TransmitOn		 1
            ReceiveOn		 0
            PVDataBase		 stkAllComm.tce
            RFIDataBase		 stkAllComm.rfi
            UseGeomFilters		 Yes
            UseOutOfDate		 Yes
            NearEarthOutOfDate		  9.9999999999998099e+00
            DeepSpaceOutOfDate		  3.9999999999999240e+01
            LoadPotVictims		 No
            UsePotVictimList		 No
        END RFI

        BEGIN ADFFileData
        END ADFFileData

        BEGIN AccessConstraints
            LineOfSight IncludeIntervals

            UsePreferredMaxStep No
            PreferredMaxStep 360
        END AccessConstraints

        BEGIN ObjectCoverage
        END ObjectCoverage

        BEGIN Desc
            BEGIN LongText
City Name		Baghdad
Type			3
Province		Baghdad
Country			Iraq
Province Rank		0
Population		3841268
Latitude		33.3340377807617 deg
Longitude		44.3978347778320 deg
            END LongText
        END Desc

        BEGIN Atmosphere
<?xml version = "1.0" standalone = "yes"?>
<SCOPE>
    <VAR name = "InheritAtmosAbsorptionModel">
        <BOOL>true</BOOL>
    </VAR>
    <VAR name = "AtmosAbsorptionModel">
        <VAR name = "Simple_Satcom">
            <SCOPE Class = "AtmosphericAbsorptionModel">
                <VAR name = "Version">
                    <STRING>&quot;1.0.1 a&quot;</STRING>
                </VAR>
                <VAR name = "ComponentName">
                    <STRING>&quot;Simple_Satcom&quot;</STRING>
                </VAR>
                <VAR name = "Type">
                    <STRING>&quot;Simple Satcom&quot;</STRING>
                </VAR>
                <VAR name = "SurfaceTemperature">
                    <QUANTITY Dimension = "Temperature" Unit = "K">
                        <REAL>293.15</REAL>
                    </QUANTITY>
                </VAR>
                <VAR name = "WaterVaporConcentration">
                    <QUANTITY Dimension = "Density" Unit = "g*m^-3">
                        <REAL>7.5</REAL>
                    </QUANTITY>
                </VAR>
            </SCOPE>
        </VAR>
    </VAR>
    <VAR name = "EnableLocalRainData">
        <BOOL>false</BOOL>
    </VAR>
    <VAR name = "LocalRainIsoHeight">
        <QUANTITY Dimension = "DistanceUnit" Unit = "m">
            <REAL>0</REAL>
        </QUANTITY>
    </VAR>
    <VAR name = "LocalRainRate">
        <QUANTITY Dimension = "SlowRate" Unit = "mm*hr^-1">
            <REAL>0</REAL>
        </QUANTITY>
    </VAR>
    <VAR name = "LocalSurfaceTemp">
        <QUANTITY Dimension = "Temperature" Unit = "K">
            <REAL>293.15</REAL>
        </QUANTITY>
    </VAR>
</SCOPE>        END Atmosphere

        BEGIN RadarCrossSection
<?xml version = "1.0" standalone = "yes"?>
<SCOPE>
    <VAR name = "Inherit">
        <BOOL>true</BOOL>
    </VAR>
</SCOPE>        END RadarCrossSection

        BEGIN RadarClutter
<?xml version = "1.0" standalone = "yes"?>
<SCOPE>
    <VAR name = "Inherit">
        <BOOL>true</BOOL>
    </VAR>
</SCOPE>        END RadarClutter

        BEGIN Identification
        END Identification

        BEGIN Crdn
        END Crdn

        BEGIN Graphics

            BEGIN Attributes

                MarkerColor		 #00ff00
                LabelColor		 #00ff00
                LineStyle		 0
                MarkerStyle		 5
                FontStyle		 0

            END Attributes

            BEGIN Graphics

                Show		 On
                Inherit		 On
                IsDynamic		 Off
                ShowLabel		 On
                ShowAzElMask		 Off
                ShowAzElFill		 Off
                AzElFillStyle		 7
                AzElFillAltTranslucency		 0.5
                UseAzElColor		 Off
                AzElColor		 #ffffff
                MinDisplayAlt		 0
                MaxDisplayAlt		 1000000
                NumAzElMaskSteps		 1
                ShowAzElAtRangeMask		 Off
                ShowAzElAtRangeFill		 Off
                AzElFillRangeTranslucency		 0.5
                AzElAtRangeFillStyle		 7
                UseAzElAtRangeColor		 Off
                AzElAtRangeColor		 #ffffff
                MinDisplayRange		 0
                MaxDisplayRange		 10000000
                NumAzElAtRangeMaskSteps		 1

            END Graphics

            BEGIN DisplayTimes
                DisplayType		 AlwaysOn
            END DisplayTimes
        END Graphics

        BEGIN ContourGfx
            ShowContours		 Off
        END ContourGfx

        BEGIN Contours
            ActiveContourType		 Radar Cross Section

            BEGIN ContourSet Radar Cross Section
                Altitude		 0
                ShowAtAltitude		 Off
                Projected		 On
                Relative		 On
                ShowLabels		 Off
                LineWidth		 1
                DecimalDigits		 1
                ColorRamp		 On
                ColorRampStartColor		 #ff0000
                ColorRampEndColor		 #0000ff
                BEGIN ContourDefinition
                    BEGIN CntrAntAzEl
                        CoordinateSystem		 0
                        BEGIN AzElPatternDef
                            SetResolutionTogether		 0
                            NumAzPoints		 361
                            AzimuthRes		 1
                            MinAzimuth		 -180
                            MaxAzimuth		 180
                            NumElPoints		 91
                            ElevationRes		 1
                            MinElevation		 0
                            MaxElevation		 90
                        END AzElPatternDef
                    END CntrAntAzEl
                    BEGIN RCSContour
                        Frequency		 2997924580
                        ComputeType		 0
                    END RCSContour
                END ContourDefinition
            END ContourSet
        END Contours

        BEGIN VO
        END VO

        BEGIN 3dVolume
            ActiveVolumeType		 Radar Cross Section

            BEGIN VolumeSet Radar Cross Section
                Scale		 100
                MinimumDisplayedRcs		 1
                Frequency		  1.4500000000000000e+10
                ShowAsWireframe		 0
                BEGIN AzElPatternDef
                    SetResolutionTogether		 0
                    NumAzPoints		 50
                    AzimuthRes		 7.346938775510203
                    MinAzimuth		 -180
                    MaxAzimuth		 180
                    NumElPoints		 50
                    ElevationRes		 3.673469387755102
                    MinElevation		 0
                    MaxElevation		 180
                END AzElPatternDef
                ColorMethod		 1
                MinToMaxStartColor		 #ff0000
                MinToMaxStopColor		 #0000ff
                RelativeToMaximum		 0
            END VolumeSet
            BEGIN VolumeGraphics
                ShowContours		 No
                ShowVolume		 No
            END VolumeGraphics
        END 3dVolume

    END Extensions

    BEGIN SubObjects

    END SubObjects

END Facility

