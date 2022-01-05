using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace FlexHapticsApp
{
    public class ButtonClickComponent : GH_Component
    {
        bool testBtnClick;
        /// <summary>
        /// Initializes a new instance of the ButtonClickComponent class.
        /// </summary>
        public ButtonClickComponent()
          : base("ButtonClickComponent", "BtnClick",
              "Detect button click",
              "FlexHaptics", "Utilities")
        {
            testBtnClick = false;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("ButtonClick", "BC", "Button click", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Execution", "Exe", "Execute the button click", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool btnClick = false;
            bool isOn = false;

            if (!DA.GetData(0, ref btnClick))
                return;

            if (!btnClick && testBtnClick)
            {
                isOn = true;
                testBtnClick = false;
            }
            else if (btnClick)
            {
                testBtnClick = true;
            }

            DA.SetData(0, isOn);

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("bb767712-05ed-43b7-803c-e18934717878"); }
        }
    }
}