stk.v.11.0
WrittenBy    STK_v11.7.0

BEGIN Transmitter

    Name		 StarLinkTransmitter
<?xml version = "1.0" standalone = "yes"?>
<VAR name = "STK_Transmitter_Object">
    <SCOPE Class = "CommRadarObject">
        <VAR name = "Version">
            <STRING>&quot;1.0.0 a&quot;</STRING>
        </VAR>
        <VAR name = "STKVersion">
            <INT>1170</INT>
        </VAR>
        <VAR name = "ComponentName">
            <STRING>&quot;STK_Transmitter_Object&quot;</STRING>
        </VAR>
        <VAR name = "Description">
            <STRING>&quot;STK Transmitter Object&quot;</STRING>
        </VAR>
        <VAR name = "Type">
            <STRING>&quot;STK Transmitter Object&quot;</STRING>
        </VAR>
        <VAR name = "UserComment">
            <STRING>&quot;STK Transmitter Object&quot;</STRING>
        </VAR>
        <VAR name = "ReadOnly">
            <BOOL>false</BOOL>
        </VAR>
        <VAR name = "Clonable">
            <BOOL>true</BOOL>
        </VAR>
        <VAR name = "Category">
            <STRING>&quot;&quot;</STRING>
        </VAR>
        <VAR name = "Model">
            <VAR name = "Simple_Transmitter_Model">
                <SCOPE Class = "Transmitter">
                    <VAR name = "Version">
                        <STRING>&quot;1.0.1 a&quot;</STRING>
                    </VAR>
                    <VAR name = "STKVersion">
                        <INT>1170</INT>
                    </VAR>
                    <VAR name = "ComponentName">
                        <STRING>&quot;Simple_Transmitter_Model&quot;</STRING>
                    </VAR>
                    <VAR name = "Description">
                        <STRING>&quot;Simple model of a transmitter&quot;</STRING>
                    </VAR>
                    <VAR name = "Type">
                        <STRING>&quot;Simple Transmitter Model&quot;</STRING>
                    </VAR>
                    <VAR name = "UserComment">
                        <STRING>&quot;Simple model of a transmitter&quot;</STRING>
                    </VAR>
                    <VAR name = "ReadOnly">
                        <BOOL>false</BOOL>
                    </VAR>
                    <VAR name = "Clonable">
                        <BOOL>true</BOOL>
                    </VAR>
                    <VAR name = "Category">
                        <STRING>&quot;&quot;</STRING>
                    </VAR>
                    <VAR name = "UseFilter">
                        <BOOL>false</BOOL>
                    </VAR>
                    <VAR name = "Filter">
                        <VAR name = "Butterworth">
                            <SCOPE Class = "Filter">
                                <VAR name = "Version">
                                    <STRING>&quot;1.0.0 a&quot;</STRING>
                                </VAR>
                                <VAR name = "STKVersion">
                                    <INT>1170</INT>
                                </VAR>
                                <VAR name = "ComponentName">
                                    <STRING>&quot;Butterworth&quot;</STRING>
                                </VAR>
                                <VAR name = "Description">
                                    <STRING>&quot;General form of nth order Butterworth filter with flat passband and stopband regions&quot;</STRING>
                                </VAR>
                                <VAR name = "Type">
                                    <STRING>&quot;Butterworth&quot;</STRING>
                                </VAR>
                                <VAR name = "UserComment">
                                    <STRING>&quot;General form of nth order Butterworth filter with flat passband and stopband regions&quot;</STRING>
                                </VAR>
                                <VAR name = "ReadOnly">
                                    <BOOL>false</BOOL>
                                </VAR>
                                <VAR name = "Clonable">
                                    <BOOL>true</BOOL>
                                </VAR>
                                <VAR name = "Category">
                                    <STRING>&quot;&quot;</STRING>
                                </VAR>
                                <VAR name = "LowerBandwidthLimit">
                                    <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                                        <REAL>-20000000</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "UpperBandwidthLimit">
                                    <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                                        <REAL>20000000</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "InsertionLoss">
                                    <QUANTITY Dimension = "RatioUnit" Unit = "units">
                                        <REAL>1</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "Order">
                                    <INT>4</INT>
                                </VAR>
                                <VAR name = "CutoffFrequency">
                                    <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                                        <REAL>10000000</REAL>
                                    </QUANTITY>
                                </VAR>
                            </SCOPE>
                        </VAR>
                    </VAR>
                    <VAR name = "PostTransmitGainsLosses">
                        <SCOPE>
                            <VAR name = "GainLossList">
                                <LIST />
                            </VAR>
                        </SCOPE>
                    </VAR>
                    <VAR name = "Frequency">
                        <QUANTITY Dimension = "FrequencyUnit" Unit = "Hz">
                            <REAL>13500000000</REAL>
                        </QUANTITY>
                    </VAR>
                    <VAR name = "Modulator">
                        <VAR name = "16PSK">
                            <SCOPE Class = "Modulator">
                                <VAR name = "Version">
                                    <STRING>&quot;1.0.1 a&quot;</STRING>
                                </VAR>
                                <VAR name = "STKVersion">
                                    <INT>1170</INT>
                                </VAR>
                                <VAR name = "ComponentName">
                                    <STRING>&quot;16PSK&quot;</STRING>
                                </VAR>
                                <VAR name = "Description">
                                    <STRING>&quot;Modulator capable of modulating a signal using 16PSK modulation&quot;</STRING>
                                </VAR>
                                <VAR name = "Type">
                                    <STRING>&quot;16PSK&quot;</STRING>
                                </VAR>
                                <VAR name = "UserComment">
                                    <STRING>&quot;Modulator capable of modulating a signal using 16PSK modulation&quot;</STRING>
                                </VAR>
                                <VAR name = "ReadOnly">
                                    <BOOL>false</BOOL>
                                </VAR>
                                <VAR name = "Clonable">
                                    <BOOL>true</BOOL>
                                </VAR>
                                <VAR name = "Category">
                                    <STRING>&quot;@Top&quot;</STRING>
                                </VAR>
                                <VAR name = "UseSignalPSD">
                                    <BOOL>false</BOOL>
                                </VAR>
                                <VAR name = "NumPSDNulls">
                                    <INT>15</INT>
                                </VAR>
                                <VAR name = "UseCDMASpread">
                                    <BOOL>false</BOOL>
                                </VAR>
                                <VAR name = "ChipsPerBit">
                                    <INT>1</INT>
                                </VAR>
                                <VAR name = "AutoScaleBandwidth">
                                    <BOOL>true</BOOL>
                                </VAR>
                                <VAR name = "SymmetricBandwidth">
                                    <BOOL>true</BOOL>
                                </VAR>
                                <VAR name = "UpperBandwidthLimit">
                                    <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                                        <REAL>126425000</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "LowerBandwidthLimit">
                                    <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                                        <REAL>-126425000</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "Bandwidth">
                                    <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                                        <REAL>252850000</REAL>
                                    </QUANTITY>
                                </VAR>
                            </SCOPE>
                        </VAR>
                    </VAR>
                    <VAR name = "DataRate">
                        <QUANTITY Dimension = "DataRate" Unit = "b*sec^-1">
                            <REAL>505700000</REAL>
                        </QUANTITY>
                    </VAR>
                    <VAR name = "EIRP">
                        <QUANTITY Dimension = "PowerUnit" Unit = "W">
                            <REAL>4677.351412871986</REAL>
                        </QUANTITY>
                    </VAR>
                    <VAR name = "UsePolarization">
                        <BOOL>false</BOOL>
                    </VAR>
                    <VAR name = "Polarization">
                        <VAR name = "Linear">
                            <SCOPE Class = "Polarization">
                                <VAR name = "ReferenceAxis">
                                    <STRING>&quot;X Axis&quot;</STRING>
                                </VAR>
                                <VAR name = "TiltAngle">
                                    <QUANTITY Dimension = "AngleUnit" Unit = "rad">
                                        <REAL>0</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "CrossPolLeakage">
                                    <QUANTITY Dimension = "RatioUnit" Unit = "units">
                                        <REAL>1e-06</REAL>
                                    </QUANTITY>
                                </VAR>
                                <VAR name = "Type">
                                    <STRING>&quot;Linear&quot;</STRING>
                                </VAR>
                            </SCOPE>
                        </VAR>
                    </VAR>
                </SCOPE>
            </VAR>
        </VAR>
    </SCOPE>
</VAR>
END Transmitter

BEGIN Extensions

    BEGIN ExternData
    END ExternData

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
    END Desc

    BEGIN Refraction
        RefractionModel		 Effective Radius Method

        UseRefractionInAccess		 No

        BEGIN ModelData
            RefractionCeiling		  5.0000000000000000e+03
            MaxTargetAltitude		  1.0000000000000000e+04
            EffectiveRadius		  1.3333333333333299e+00

            UseExtrapolation		 Yes


        END ModelData
    END Refraction

    BEGIN Crdn
    END Crdn

    BEGIN Graphics

        BEGIN Graphics

            ShowGfx		 On
            Relative		 Off
            ShowBoresight		 On
            BoresightMarker		 4
            BoresightColor		 #ffffff

        END Graphics

        BEGIN DisplayTimes
            DisplayType		 AlwaysOn
        END DisplayTimes
    END Graphics

    BEGIN ContourGfx
        ShowContours		 Off
    END ContourGfx

    BEGIN Contours
        ActiveContourType		 Antenna Gain

        BEGIN ContourSet Antenna Gain
            Altitude		 0
            ShowAtAltitude		 Off
            Projected		 On
            Relative		 On
            ShowLabels		 Off
            LineWidth		 1
            DecimalDigits		 1
            ColorRamp		 On
            ColorRampStartColor		 #0000ff
            ColorRampEndColor		 #ff0000
            BEGIN ContourDefinition
                BEGIN CntrAntAzEl
                    CoordinateSystem		 0
                    BEGIN AzElPatternDef
                        SetResolutionTogether		 0
                        NumAzPoints		 181
                        AzimuthRes		 2
                        MinAzimuth		 -180
                        MaxAzimuth		 180
                        NumElPoints		 91
                        ElevationRes		 1
                        MinElevation		 0
                        MaxElevation		 90
                    END AzElPatternDef
                END CntrAntAzEl
            END ContourDefinition
        END ContourSet

        BEGIN ContourSet EIRP
            Altitude		 0
            ShowAtAltitude		 Off
            Projected		 On
            Relative		 On
            ShowLabels		 Off
            LineWidth		 1
            DecimalDigits		 1
            ColorRamp		 On
            ColorRampStartColor		 #0000ff
            ColorRampEndColor		 #ff0000
            BEGIN ContourDefinition
                BEGIN CntrAntAzEl
                    CoordinateSystem		 0
                    BEGIN AzElPatternDef
                        SetResolutionTogether		 0
                        NumAzPoints		 181
                        AzimuthRes		 2
                        MinAzimuth		 -180
                        MaxAzimuth		 180
                        NumElPoints		 91
                        ElevationRes		 1
                        MinElevation		 0
                        MaxElevation		 90
                    END AzElPatternDef
                END CntrAntAzEl
            END ContourDefinition
        END ContourSet

        BEGIN ContourSet Flux Density
            Altitude		 0
            ShowAtAltitude		 Off
            Projected		 On
            Relative		 On
            ShowLabels		 Off
            LineWidth		 1
            DecimalDigits		 1
            ColorRamp		 On
            ColorRampStartColor		 #0000ff
            ColorRampEndColor		 #ff0000
            BEGIN ContourDefinition
                BEGIN CntrAntLatLon
                    SetResolutionTogether		 true
                    Resolution		 1 1
                    ElevationAngleConstraint		 90
                    BEGIN LatLonSphGrid
                        Centroid		 0 0
                        ConeAngle		 0
                        NumPts		 181 91
                        Altitude		 0
                    END LatLonSphGrid
                END CntrAntLatLon
            END ContourDefinition
        END ContourSet

        BEGIN ContourSet RIP
            Altitude		 0
            ShowAtAltitude		 Off
            Projected		 On
            Relative		 On
            ShowLabels		 Off
            LineWidth		 1
            DecimalDigits		 1
            ColorRamp		 On
            ColorRampStartColor		 #0000ff
            ColorRampEndColor		 #ff0000
            BEGIN ContourDefinition
                BEGIN CntrAntLatLon
                    SetResolutionTogether		 true
                    Resolution		 1 1
                    ElevationAngleConstraint		 90
                    BEGIN LatLonSphGrid
                        Centroid		 0 0
                        ConeAngle		 0
                        NumPts		 181 91
                        Altitude		 0
                    END LatLonSphGrid
                END CntrAntLatLon
            END ContourDefinition
        END ContourSet

        BEGIN ContourSet Spectral Flux Density
            Altitude		 0
            ShowAtAltitude		 Off
            Projected		 On
            Relative		 On
            ShowLabels		 Off
            LineWidth		 1
            DecimalDigits		 1
            ColorRamp		 On
            ColorRampStartColor		 #0000ff
            ColorRampEndColor		 #ff0000
            BEGIN ContourDefinition
                BEGIN CntrAntLatLon
                    SetResolutionTogether		 true
                    Resolution		 1 1
                    ElevationAngleConstraint		 90
                    BEGIN LatLonSphGrid
                        Centroid		 0 0
                        ConeAngle		 0
                        NumPts		 181 91
                        Altitude		 0
                    END LatLonSphGrid
                END CntrAntLatLon
            END ContourDefinition
        END ContourSet
    END Contours

    BEGIN VO
    END VO

    BEGIN 3dVolume
        ActiveVolumeType		 Antenna Beam

        BEGIN VolumeSet Antenna Beam
            Scale		 4
            MinimumDisplayedGain		 1
            Frequency		 14500000000
            ShowAsWireframe		 0
            CoordinateSystem		 0
            BEGIN AzElPatternDef
                SetResolutionTogether		 0
                NumAzPoints		 181
                AzimuthRes		 2
                MinAzimuth		 -180
                MaxAzimuth		 180
                NumElPoints		 91
                ElevationRes		 1
                MinElevation		 0
                MaxElevation		 90
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
