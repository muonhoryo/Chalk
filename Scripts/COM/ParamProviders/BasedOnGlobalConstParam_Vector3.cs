
using MuonhoryoLibrary;
using MuonhoryoLibrary.Unity;
using UnityEngine;

namespace Chalk
{
    public class BasedOnGlobalConstParam_Vector3 : BasedOnGlobalConstParam<Vector3,CompositeVector3>
    {
        protected override CompositeVector3 InitializeParam(Vector3 defaultValue)
        {
            return new CompositeVector3(defaultValue);
        }
    }
}
