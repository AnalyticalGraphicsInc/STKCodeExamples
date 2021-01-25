using AGI.STKUtil;
using AGI.STKVgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsToolbox
{
    public static class AWBFunctions
    {
        public static IAgCrdnAngleToPlane GetCreateAngleToPlane(IAgCrdnProvider vgtPrv, IAgCrdnPlane plane, IAgCrdnVector vector, string angleName, string description)
        {
            IAgCrdnAngleToPlane planeAngle;
            if (vgtPrv.Angles.Contains(angleName))
            {
                planeAngle = (IAgCrdnAngleToPlane)vgtPrv.Angles[angleName];
            }
            else
            {
                planeAngle = (IAgCrdnAngleToPlane)vgtPrv.Angles.Factory.Create(angleName,
                    description,
                    AgECrdnAngleType.eCrdnAngleTypeToPlane);
            }
            planeAngle.ReferencePlane.SetPlane(plane);
            planeAngle.ReferenceVector.SetVector(vector);
            planeAngle.Signed = AgECrdnSignedAngleType.eCrdnSignedAngleNone;

            return planeAngle;
        }

        public static IAgCrdnCalcScalarAngle GetCreateAngleCalcScalar(IAgCrdnProvider vgtPrv, IAgCrdnAngle inputAngle, string scalarName)
        {
            IAgCrdnCalcScalarAngle calcScalarAngle;
            if (vgtPrv.CalcScalars.Contains(scalarName))
            {
                calcScalarAngle = (IAgCrdnCalcScalarAngle)vgtPrv.CalcScalars[scalarName];
            }
            else
            {
                calcScalarAngle =
                    (IAgCrdnCalcScalarAngle)vgtPrv.CalcScalars.Factory.CreateCalcScalarAngle(scalarName, "");
            }
            calcScalarAngle.InputAngle = inputAngle;
            return calcScalarAngle;
        }

        public static IAgCrdnEventArrayExtrema GetCreateEventArrayExtrema(IAgCrdnProvider vgtPrv, IAgCrdnCalcScalar cScalar, string extremaName, AgECrdnExtremumConstants extremaType, bool isGlobal)
        {
            IAgCrdnEventArrayExtrema extrema;
            if (vgtPrv.EventArrays.Contains(extremaName))
            {
                extrema = (IAgCrdnEventArrayExtrema)vgtPrv.EventArrays[extremaName];
            }
            else
            {
                extrema =
                    (IAgCrdnEventArrayExtrema)vgtPrv.EventArrays.Factory.CreateEventArrayExtrema(extremaName,
                        "");
            }
            extrema.Calculation = cScalar;
            extrema.ExtremumType = extremaType;
            extrema.IsGlobal = isGlobal;

            return extrema;
        }

        public static IAgCrdnVectorDisplacement GetCreateDisplacementVector(IAgCrdnProvider vgtPrv, IAgCrdnPoint OrginPoint, IAgCrdnPoint DestPoint, string VectorName)
        {
            IAgCrdnVectorDisplacement dispVector;
            if (vgtPrv.Vectors.Contains(VectorName))
            {
                dispVector = vgtPrv.Vectors[VectorName] as IAgCrdnVectorDisplacement;
                dispVector.Origin.SetPoint(OrginPoint);
                dispVector.Destination.SetPoint(DestPoint);
            }
            else
            {
                dispVector = vgtPrv.Vectors.Factory.CreateDisplacementVector(VectorName, OrginPoint, DestPoint);
            }

            return dispVector;
        }

        public static IAgCrdnAngleBetweenVectors GetCreateAngleBetweenVectors(IAgCrdnProvider vgtPrv, IAgCrdnVector FromVector, IAgCrdnVector ToVector, string AngleName, string description)
        {
            IAgCrdnAngleBetweenVectors angle;
            if (vgtPrv.Angles.Contains(AngleName))
            {
                angle = vgtPrv.Angles[AngleName] as IAgCrdnAngleBetweenVectors;
            }
            else
            {
                angle = (IAgCrdnAngleBetweenVectors)vgtPrv.Angles.Factory.Create(AngleName, description, AgECrdnAngleType.eCrdnAngleTypeBetweenVectors);
            }
            angle.FromVector.SetVector(FromVector);
            angle.ToVector.SetVector(ToVector);

            return angle;
        }

        public static IAgCrdnConditionScalarBounds GetCreateConditionScalarBounds(IAgCrdnProvider vgtPrv, IAgCrdnCalcScalar cScalar, string conditionName, AgECrdnConditionThresholdOption option)
        {
            IAgCrdnConditionScalarBounds condition;
            //create condition
            if (vgtPrv.Conditions.Contains(conditionName))
            {
                condition = (IAgCrdnConditionScalarBounds)vgtPrv.Conditions[conditionName];
            }
            else
            {
                condition = (IAgCrdnConditionScalarBounds)vgtPrv.Conditions.Factory
                    .CreateConditionScalarBounds(conditionName, "");
            }
            condition.Operation = option;
            condition.Scalar = cScalar;

            return condition;
        }

        public static void SetAngleConditionScalarBounds(IAgCrdnConditionScalarBounds condition, double angleLB, double angleUB)
        {
            IAgQuantity quant = CommonData.StkRoot.ConversionUtility.NewQuantity("Angle", "deg", angleLB);
            condition.SetMinimum(quant);
            quant = CommonData.StkRoot.ConversionUtility.NewQuantity("Angle", "deg", angleUB);
            condition.SetMaximum(quant);
        }

        public static IAgCrdnPlaneNormal GetCreatePlaneNormal(IAgCrdnProvider vgtPrv, IAgCrdnPoint Orgin, IAgCrdnVector RefVector, IAgCrdnVector NormalVector, string PlaneName, string description)
        {
            IAgCrdnPlaneNormal plane;
            if (vgtPrv.Planes.Contains(PlaneName))
            {
                plane = (IAgCrdnPlaneNormal)vgtPrv.Planes[PlaneName];
            }
            else
            {
                plane = (IAgCrdnPlaneNormal)vgtPrv.Planes.Factory.Create(PlaneName,
                    description, AgECrdnPlaneType.eCrdnPlaneTypeNormal);
            }
            plane.NormalVector.SetVector(NormalVector);
            plane.ReferencePoint.SetPoint(Orgin);
            plane.ReferenceVector.SetVector(RefVector);

            return plane;
        }
    }
}
