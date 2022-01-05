using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace FlexHapticsApp
{
    public class FlexHapticsAppComponent : GH_Component
    {
        int modualType;
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public FlexHapticsAppComponent()
          : base("InterfaceLogic", "IL",
              "decide which FlexHaptics module panel should be shown",
              "FlexHaptics", "Utilities")
        {
            modualType = -1;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("LinearResisBtnClicked", "LiRes", "Linear resistance button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("LinearDetentBtnClicked", "LiDet", "Linear detent button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("LinearBounceBtnClicked", "LiBounce", "Linear bounce button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("OrthoPlanarBounceBtnClicked", "OrthoPlanarBounce", "Ortho-planar bounce button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RotaryResisBtnClicked", "RotRes", "Rotary resistance button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RotaryDetentBtnClicked", "RotDet", "Rotary detent button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RotaryBounceClicked", "RotBounce", "Rotary bounce button clicked", GH_ParamAccess.item);
            pManager.AddBooleanParameter("PlanarBounceClicked", "PlaBounce", "Planar bounce button clicked", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("LinearResistancePanel", "LiResPanel", "Linear resistance panel actvated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("LinearDetentPanel", "LiDetPanel", "Linear detent panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("LinearBouncePanel", "LiBouncePanel", "Linear bounce panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("OrthoPlanarBouncePanel", "OrthoPlanarBouncePanel", "Ortho-planar bounce panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RotaryResisPanel", "RotResPanel", "Rotary resistance panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RotaryDetentPanel", "RotDetPanel", "Rotary detent panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RotaryBouncePanel", "RotBouncePanel", "Rotary bounce panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("PlanarBouncePanel", "PlaBouncePanel", "Planar bounce panel activated", GH_ParamAccess.item);
            pManager.AddBooleanParameter("DefaultPanel", "DefaultPanel", "Default panel activated", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool btnClick_lires = false;
            bool btnClick_lidet = false;
            bool btnClick_libou = false;
            bool btnClick_ortho = false;
            bool btnClick_rotres = false;
            bool btnClick_rotdet = false;
            bool btnClick_rotbou = false;
            bool btnClick_planar = false;

            #region read the button clicks

            if (!DA.GetData(0, ref btnClick_lires))
                return;

            if (!DA.GetData(1, ref btnClick_lidet))
                return;

            if (!DA.GetData(2, ref btnClick_libou))
                return;

            if (!DA.GetData(3, ref btnClick_ortho))
                return;

            if (!DA.GetData(4, ref btnClick_rotres))
                return;

            if (!DA.GetData(5, ref btnClick_rotdet))
                return;

            if (!DA.GetData(6, ref btnClick_rotbou))
                return;

            if (!DA.GetData(7, ref btnClick_planar))
                return;

            #endregion

            if (modualType == 1 && !btnClick_lires)
            {
                DA.SetData(0, true);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (btnClick_lires && !btnClick_lidet && !btnClick_libou && !btnClick_ortho &&
                !btnClick_rotres && !btnClick_rotdet && !btnClick_rotbou && !btnClick_planar)
            {
                modualType = 1;
            }

            if (modualType == 2 && !btnClick_lidet)
            {
                DA.SetData(0, false);
                DA.SetData(1, true);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && btnClick_lidet && !btnClick_libou && !btnClick_ortho &&
                !btnClick_rotres && !btnClick_rotdet && !btnClick_rotbou && !btnClick_planar)
            {
                modualType = 2;
            }

            if (modualType == 3 && !btnClick_libou)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, true);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && !btnClick_lidet && btnClick_libou && !btnClick_ortho &&
                !btnClick_rotres && !btnClick_rotdet && !btnClick_rotbou && !btnClick_planar)
            {
                modualType = 3;
            }

            if (modualType == 4 && !btnClick_ortho)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, true);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && !btnClick_lidet && !btnClick_libou && btnClick_ortho &&
                !btnClick_rotres && !btnClick_rotdet && !btnClick_rotbou && !btnClick_planar)
            {
                modualType = 4;
            }

            if (modualType == 5 && !btnClick_rotres)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, true);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && !btnClick_lidet && !btnClick_libou && !btnClick_ortho &&
                btnClick_rotres && !btnClick_rotdet && !btnClick_rotbou && !btnClick_planar)
            {
                modualType = 5;
            }

            if (modualType == 6 && !btnClick_rotdet)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, true);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && !btnClick_lidet && !btnClick_libou && !btnClick_ortho &&
                !btnClick_rotres && btnClick_rotdet && !btnClick_rotbou && !btnClick_planar)
            {
                modualType = 6;
            }

            if (modualType == 7 && !btnClick_rotbou)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, true);
                DA.SetData(7, false);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && !btnClick_lidet && !btnClick_libou && !btnClick_ortho &&
                !btnClick_rotres && !btnClick_rotdet && btnClick_rotbou && !btnClick_planar)
            {
                modualType = 7;
            }

            if (modualType == 8 && !btnClick_planar)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, true);
                DA.SetData(8, false);
            }
            else if (!btnClick_lires && !btnClick_lidet && !btnClick_libou && !btnClick_ortho &&
                !btnClick_rotres && !btnClick_rotdet && !btnClick_rotbou && btnClick_planar)
            {
                modualType = 8;
            }


            if (modualType == -1)
            {
                DA.SetData(0, false);
                DA.SetData(1, false);
                DA.SetData(2, false);
                DA.SetData(3, false);
                DA.SetData(4, false);
                DA.SetData(5, false);
                DA.SetData(6, false);
                DA.SetData(7, false);
                DA.SetData(8, true);
            }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f78d608a-5423-495b-a734-0dea9b1d54cd"); }
        }
    }
}
