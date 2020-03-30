stk.v.11.0
WrittenBy    STK_v11.7.0

BEGIN Receiver

    Name		 OneWebReceiver
<?xml version = "1.0" standalone = "yes"?>
<VAR name = "STK_Receiver_Object">
    <SCOPE Class = "CommRadarObject">
        <VAR name = "Version">
            <STRING>&quot;1.0.0 a&quot;</STRING>
        </VAR>
        <VAR name = "STKVersion">
            <INT>1170</INT>
        </VAR>
        <VAR name = "ComponentName">
            <STRING>&quot;STK_Receiver_Object&quot;</STRING>
        </VAR>
        <VAR name = "Description">
            <STRING>&quot;STK Receiver Object&quot;</STRING>
        </VAR>
        <VAR name = "Type">
            <STRING>&quot;STK Receiver Object&quot;</STRING>
        </VAR>
        <VAR name = "UserComment">
            <STRING>&quot;STK Receiver Object&quot;</STRING>
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
            <VAR name = "Simple_Receiver_Model">
                <SCOPE Class = "Receiver">
                    <VAR name = "Version">
                        <STRING>&quot;1.0.0 a&quot;</STRING>
                    </VAR>
                    <VAR name = "STKVersion">
                        <INT>1170</INT>
                    </VAR>
                    <VAR name = "ComponentName">
                        <STRING>&quot;Simple_Receiver_Model&quot;</STRING>
                    </VAR>
                    <VAR name = "Description">
                        <STRING>&quot;Simple model of a receiver&quot;</STRING>
                    </VAR>
                    <VAR name = "Type">
                        <STRING>&quot;Simple Receiver Model&quot;</STRING>
                    </VAR>
                    <VAR name = "UserComment">
                        <STRING>&quot;Simple model of a receiver&quot;</STRING>
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
                    <VAR name = "AutoSelectDemodulator">
                        <BOOL>true</BOOL>
                    </VAR>
                    <VAR name = "Demodulator">
                        <VAR name = "BPSK">
                            <SCOPE Class = "Demodulator">
                                <VAR name = "Version">
                                    <STRING>&quot;1.0.0 a&quot;</STRING>
                                </VAR>
                                <VAR name = "STKVersion">
                                    <INT>1170</INT>
                                </VAR>
                                <VAR name = "ComponentName">
                                    <STRING>&quot;BPSK&quot;</STRING>
                                </VAR>
                                <VAR name = "Description">
                                    <STRING>&quot;Demodulator capable of demodulating a BPSK modulated signal.&quot;</STRING>
                                </VAR>
                                <VAR name = "Type">
                                    <STRING>&quot;BPSK&quot;</STRING>
                                </VAR>
                                <VAR name = "UserComment">
                                    <STRING>&quot;Demodulator capable of demodulating a BPSK modulated signal.&quot;</STRING>
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
                            </SCOPE>
                        </VAR>
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
                    <VAR name = "Bandwidth">
                        <QUANTITY Dimension = "BandwidthUnit" Unit = "Hz">
                            <REAL>2000</REAL>
                        </QUANTITY>
                    </VAR>
                    <VAR name = "AutoScaleBandwidth">
                        <BOOL>true</BOOL>
                    </VAR>
                    <VAR name = "PreReceiveGainsLosses">
                        <SCOPE>
                            <VAR name = "GainLossList">
                                <LIST />
                            </VAR>
                        </SCOPE>
                    </VAR>
                    <VAR name = "PreDemodGainsLosses">
                        <SCOPE>
                            <VAR name = "GainLossList">
                                <LIST />
                            </VAR>
                        </SCOPE>
                    </VAR>
                    <VAR name = "EnableLinkMargin">
                        <BOOL>true</BOOL>
                    </VAR>
                    <VAR name = "LinkMarginType">
                        <STRING>&quot;Eb/No&quot;</STRING>
                    </VAR>
                    <VAR name = "LinkMarginThreshold">
                        <QUANTITY Dimension = "RatioUnit" Unit = "units">
                            <REAL>16.98243652461744</REAL>
                        </QUANTITY>
                    </VAR>
                    <VAR name = "RainOutagePercent">
                        <PROP name = "FormatString">
                            <STRING>&quot;%#6.3f&quot;</STRING>
                        </PROP>
                        <REAL>0.1</REAL>
                    </VAR>
                    <VAR name = "UseRain">
                        <BOOL>true</BOOL>
                    </VAR>
                    <VAR name = "GOverT">
                        <QUANTITY Dimension = "GainTempRatio" Unit = "units*K^-1">
                            <REAL>13.48962882591654</REAL>
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
                    <VAR name = "Frequency">
                        <QUANTITY Dimension = "FrequencyUnit" Unit = "Hz">
                            <REAL>14500000000</REAL>
                        </QUANTITY>
                    </VAR>
                    <VAR name = "FrequencyAutoTracking">
                        <BOOL>true</BOOL>
                    </VAR>
                </SCOPE>
            </VAR>
        </VAR>
    </SCOPE>
</VAR>
END Receiver

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
