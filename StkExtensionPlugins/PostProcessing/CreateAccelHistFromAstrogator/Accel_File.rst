stk.v.11.0
WrittenBy    STK_v11.7.1

BEGIN ReportStyle

    BEGIN ClassId
        Class		 Satellite
    END ClassId

    BEGIN Header
        StyleType		 0
        Date		 Yes
        Name		 Yes
        IsHidden		 No
        DescShort		 No
        DescLong		 No
        YLog10		 No
        Y2Log10		 No
        YUseWholeNumbers		 No
        Y2UseWholeNumbers		 No
        VerticalGridLines		 No
        HorizontalGridLines		 No
        AnnotationType		 Spaced
        NumAnnotations		 3
        NumAngularAnnotations		 5
        ShowYAnnotations		 Yes
        AnnotationRotation		 1
        BackgroundColor		 #ffffff
        ForegroundColor		 #000000
        ViewableDuration		 3600
        RealTimeMode		 No
        DayLinesStatus		 1
        LegendStatus		 1
        LegendLocation		 1
        TextPointSize		 10
        TextForegroundColor		 #ffff00
        TextBackgroundColor		 #000000

        BEGIN PostProcessor
            Destination		 0
            Use		 1
            ScriptName		 U:\STK\Plugins\PostProcessing\AccelHist\AccelHist.vbs
            Destination		 1
            Use		 1
            ScriptName		 C:\temp\delete\Accel_File_PPS.pl
            Destination		 2
            Use		 1
            ScriptName		 C:\temp\delete\Accel_File_PPS.pl
            Destination		 3
            Use		 1
            ScriptName		 C:\temp\delete\Accel_File_PPS.pl
        END PostProcessor
        NumSections		 1
    END Header

    BEGIN Section
        Name		 Section 1
        ClassName		 Satellite
        NameInTitle		 No
        ExpandMethod		 0
        PropMask		 770
        ShowIntervals		 No
        NumIntervals		 0
        NumLines		 1

        BEGIN Line
            Name		 Line 1
            NumElements		 6

            BEGIN Element
                Name		 Time
                IsIndepVar		 Yes
                IndepVarName		 Time
                Title		 Time
                NameInTitle		 No
                Service		 AstrogatorSegmentHistory
                Type		 Top
                Element		 Time
                SumAllowedMask		 0
                SummaryOnly		 No
                DataType		 0
                UnitType		 2
                LineStyle		 0
                LineWidth		 0
                PointStyle		 0
                PointSize		 0
                FillPattern		 0
                LineColor		 #000000
                FillColor		 #000000
                PropMask		 0
                UseScenUnits		 No
                BEGIN Units
                    DateFormat		 EpochSeconds
                END Units
            END Element

            BEGIN Element
                Name		 Astrogator Maneuver Ephemeris Block History-Top-Epoch
                IsIndepVar		 No
                IndepVarName		 Time
                Title		 Epoch
                NameInTitle		 Yes
                Service		 AstrogatorSegmentHistory
                Type		 Top
                Element		 Epoch
                SumAllowedMask		 1559
                SummaryOnly		 No
                DataType		 0
                UnitType		 2
                LineStyle		 0
                LineWidth		 0
                PointStyle		 0
                PointSize		 0
                FillPattern		 0
                LineColor		 #000000
                FillColor		 #000000
                PropMask		 0
                UseScenUnits		 No
                BEGIN Units
                    DateFormat		 GregorianUTC
                END Units
            END Element

            BEGIN Element
                Name		 Astrogator Maneuver Ephemeris Block History-Maneuver-Thrust_Vector_X
                IsIndepVar		 No
                IndepVarName		 Time
                Title		 Thrust_Vector_X
                NameInTitle		 Yes
                Service		 AstrogatorSegmentHistory
                Type		 Maneuver
                Element		 Thrust_Vector_X
                SumAllowedMask		 1559
                SummaryOnly		 No
                DataType		 0
                UnitType		 30
                LineStyle		 0
                LineWidth		 0
                PointStyle		 0
                PointSize		 0
                FillPattern		 0
                LineColor		 #000000
                FillColor		 #000000
                PropMask		 0
                UseScenUnits		 Yes
            END Element

            BEGIN Element
                Name		 Astrogator Maneuver Ephemeris Block History-Maneuver-Thrust_Vector_Y
                IsIndepVar		 No
                IndepVarName		 Time
                Title		 Thrust_Vector_Y
                NameInTitle		 Yes
                Service		 AstrogatorSegmentHistory
                Type		 Maneuver
                Element		 Thrust_Vector_Y
                SumAllowedMask		 1559
                SummaryOnly		 No
                DataType		 0
                UnitType		 30
                LineStyle		 0
                LineWidth		 0
                PointStyle		 0
                PointSize		 0
                FillPattern		 0
                LineColor		 #000000
                FillColor		 #000000
                PropMask		 0
                UseScenUnits		 Yes
            END Element

            BEGIN Element
                Name		 Astrogator Maneuver Ephemeris Block History-Maneuver-Thrust_Vector_Z
                IsIndepVar		 No
                IndepVarName		 Time
                Title		 Thrust_Vector_Z
                NameInTitle		 Yes
                Service		 AstrogatorSegmentHistory
                Type		 Maneuver
                Element		 Thrust_Vector_Z
                SumAllowedMask		 1559
                SummaryOnly		 No
                DataType		 0
                UnitType		 30
                LineStyle		 0
                LineWidth		 0
                PointStyle		 0
                PointSize		 0
                FillPattern		 0
                LineColor		 #000000
                FillColor		 #000000
                PropMask		 0
                UseScenUnits		 Yes
            END Element

            BEGIN Element
                Name		 Astrogator Maneuver Ephemeris Block History-Maneuver-Total_Mass
                IsIndepVar		 No
                IndepVarName		 Time
                Title		 Total_Mass
                NameInTitle		 Yes
                Service		 AstrogatorSegmentHistory
                Type		 Maneuver
                Element		 Total_Mass
                SumAllowedMask		 1559
                SummaryOnly		 No
                DataType		 0
                UnitType		 8
                LineStyle		 0
                LineWidth		 0
                PointStyle		 0
                PointSize		 0
                FillPattern		 0
                LineColor		 #000000
                FillColor		 #000000
                PropMask		 0
                UseScenUnits		 Yes
            END Element
        END Line
    END Section

    BEGIN LineAnnotations
    END LineAnnotations
END ReportStyle

