using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace FlexHapticsApp
{
    public class FabMethodMaterialSelectionComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FabMethodMaterialSelectionComponent class.
        /// </summary>
        public FabMethodMaterialSelectionComponent()
          : base("FabMethodMaterialSelectionComponent", "FabMM",
              "Choose the fabrication method and material",
              "FlexHaptics", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Method", "Me", "The fab method", GH_ParamAccess.item);
            //pManager.AddTextParameter("Material", "Ma", "The material", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Materials", "MList", "The list of materials for a chosen fab method", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string method_index = "";
            List<string> materialList = new List<string>();

            if (!DA.GetData(0, ref method_index))
                return;

            if(method_index.Equals("3D printing"))
            {
                materialList.Add("PLA");
                materialList.Add("ABS");
            }
            else if(method_index.Equals("Laser cutting"))
            {
                materialList.Add("PET");
                materialList.Add("Acrylic");
            }

            if(materialList.Count > 0)
            {
                DA.SetDataList(0, materialList);
            }
            else
            {
                materialList.Add("...");
                DA.SetDataList(0, materialList);
            }

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
            get { return new Guid("4b4609f3-44cd-45d3-9235-97d5bdf342a8"); }
        }
    }
}