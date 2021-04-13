VO_V110		
ShowBoresight		 No
BEGIN GeneralVectorAxes
    PersistentLineWidth		 2
    Scale		 15.8489
END
BEGIN VectorAxes
    BEGIN CrdnDef
        IsVector		 Yes
        IsCentralBodyFrame		 No
        Name		 "Boresight"
    END
    BEGIN RefCrdn
        IsVector		 No
        IsCentralBodyFrame		 No
        Name		 "Body"
        Object		 _Default
    END
    Duration		 600
    IsShowing		 No
    IsPersistent		 No
    IsTransparent		 No
    UseTrueScale		 No
    Label		 Name
    DrawAtCentralBody		 No
    ConnectType		 Sweep
    ColorIndex		 #ffa500
    IntervalType		 AlwaysOn
    Thickness		 10
    AngleUnit		 deg
END
BEGIN VectorAxes
    BEGIN CrdnDef
        IsVector		 Yes
        IsCentralBodyFrame		 No
        Name		 "Sun"
        MagUnit		 km
    END
    BEGIN RefCrdn
        IsVector		 No
        IsCentralBodyFrame		 No
        Name		 "Body"
        Object		 _Default
    END
    Duration		 600
    IsShowing		 No
    IsPersistent		 No
    IsTransparent		 No
    UseTrueScale		 No
    Label		 Name
    DrawAtCentralBody		 No
    ConnectType		 Sweep
    ColorIndex		 #ffff00
    IntervalType		 AlwaysOn
    Thickness		 10
    AngleUnit		 deg
END
BEGIN VectorAxes
    BEGIN CrdnDef
        IsVector		 Yes
        IsCentralBodyFrame		 No
        Name		 "Up"
    END
    BEGIN RefCrdn
        IsVector		 No
        IsCentralBodyFrame		 No
        Name		 "Body"
        Object		 _Default
    END
    Duration		 600
    IsShowing		 No
    IsPersistent		 No
    IsTransparent		 No
    UseTrueScale		 No
    Label		 Name
    DrawAtCentralBody		 No
    ConnectType		 Sweep
    ColorIndex		 #4169e1
    IntervalType		 AlwaysOn
    Thickness		 10
    AngleUnit		 deg
END
BEGIN VectorAxes
    BEGIN CrdnDef
        IsVector		 No
        IsCentralBodyFrame		 No
        Name		 "Body"
    END
    BEGIN RefCrdn
        IsVector		 No
        IsCentralBodyFrame		 No
        Name		 "Body"
    END
    Duration		 600
    IsShowing		 No
    IsPersistent		 No
    IsTransparent		 No
    UseTrueScale		 No
    Label		 Name
    DrawAtCentralBody		 No
    ConnectType		 Sweep
    ColorIndex		 #00ff00
    IntervalType		 AlwaysOn
    Thickness		 10
    AngleUnit		 deg
END
BEGIN VectorAngle
    FractionalScale		 1
    SupportingDihedralArcLineWidth		 1
    ArcLineWidth		 2
    PixelThreshold		 0.5
    BEGIN VectorAngleData
        Name		 "Sun"
        Show		 No
        ShowLabel		 Yes
        ShowAngle		 Yes
        ShowDihedralAngleSupportingArcs		 Yes
        ColorIndex		 #ba55d3
        IntervalType		 AlwaysOn
        Unit		 deg
    END
END
BEGIN Plane
END
