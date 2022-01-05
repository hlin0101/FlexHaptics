using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Point = System.Drawing.Point;

namespace FlexHapticsApp
{
    public class DrawPlottingComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DrawPlottingComponent class.
        /// </summary>
        public DrawPlottingComponent()
          : base("DrawPlottingComponent", "DP",
              "Draw the plot with input x and y",
              "FlexHaptics", "Utilities")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("xInput", "X", "A series of data points for the x axis", GH_ParamAccess.list);
            pManager.AddNumberParameter("yInput", "Y", "A series of data points for the y axis", GH_ParamAccess.list);
            pManager.AddTextParameter("PlotWidth", "W", "The width of the plot area", GH_ParamAccess.item);
            pManager.AddTextParameter("PlotHeight", "H", "The height of the plot area", GH_ParamAccess.item);
            pManager.AddTextParameter("xCap", "CAPX", "The caption of the x axis", GH_ParamAccess.item);
            pManager.AddTextParameter("yCap", "CAPY", "The caption of the y axis", GH_ParamAccess.item);
            pManager.AddTextParameter("imageName", "IName", "The name of the plotting image", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("PlotImageSrc", "PDir", "The directory of the plot image", GH_ParamAccess.item);
            pManager.AddIntegerParameter("PlotWidth", "PW", "The width of the plot", GH_ParamAccess.item);
            pManager.AddIntegerParameter("PlotHeight", "PH", "The height of the plot", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<double> x_input = new List<double>();
            List<double> y_input = new List<double>();
            string x_cap = "";
            string y_cap = "";
            string plot_width = "";
            string plot_height = "";
            string plot_name = "";

            string plot_src = "";
            int plot_width_output = 0;
            int plot_height_output = 0;
            

            #region read the inputs

            if (!DA.GetDataList(0, x_input))
                return;
            if (!DA.GetDataList(1, y_input))
                return;
            if (!DA.GetData(2, ref plot_width))
                return;
            if (!DA.GetData(3, ref plot_height))
                return;
            if (!DA.GetData(4, ref x_cap))
                return;
            if (!DA.GetData(5, ref y_cap))
                return;
            if (!DA.GetData(6, ref plot_name))
                return;


            #endregion

            if (x_input.Count > 0 && y_input.Count > 0 && x_cap!="" && y_cap!="" && !plot_width.Equals("0") && !plot_height.Equals("0") && !plot_name.Equals(""))
            {
                // draw the plot and export it as an image
                const int dataCount = 301;
                int plot_w_int = Int32.Parse(plot_width);
                int plot_h_int = Int32.Parse(plot_height);

                plot_width_output = plot_w_int;
                plot_height_output = plot_h_int;

                // Create Bitmap object
                Bitmap bitmap = new Bitmap(plot_w_int, plot_h_int, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                // Create and initialize Graphics
                Graphics graphics = Graphics.FromImage(bitmap);
                // Create Pen
                Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 0.5f);

                //List<PointF> pts = new List<PointF>();
                PointF[] pts = new PointF[dataCount];

                for (int i = 0; i < x_input.Count; ++i)
                {
                    double x_coor = x_input[i] + 3f;
                    double y_coor = (float)plot_h_int - y_input[i] - 3f;

                    PointF temp_pt = new PointF((float)x_coor, (float)y_coor);

                    pts[i] = temp_pt;

                    graphics.DrawEllipse(pen, (float)x_coor - 0.5f, (float)y_coor - 0.5f, 1f, 1f);
                }

                // draw the x-axis
                Pen pen_axis = new Pen(Color.FromKnownColor(KnownColor.Black), 1f);
                graphics.DrawLine(pen_axis, 3f, (float)plot_h_int - 3f, (float)plot_w_int, (float)plot_h_int - 3f);
                Point[] x_arrow = new Point[3];
                x_arrow[0] = new Point(plot_w_int - 10, plot_h_int);
                x_arrow[1] = new Point(plot_w_int, plot_h_int - 3);
                x_arrow[2] = new Point(plot_w_int - 10, plot_h_int - 6);

                Font font = new Font("Times New Roman", 7.0f);
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
                graphics.DrawPolygon(pen_axis, x_arrow);
                graphics.FillPolygon(solidBrush, x_arrow);
                graphics.DrawString(x_cap, font, solidBrush, (float)plot_w_int - 9f * x_cap.Length, (float)plot_h_int - 30f);

                // draw the y-axis
                graphics.DrawLine(pen_axis, 3f, (float)plot_h_int - 3f, 3f, 0f);
                Point[] y_arrow = new Point[3];
                y_arrow[0] = new Point(3, 0);
                y_arrow[1] = new Point(0, 10);
                y_arrow[2] = new Point(6, 10);

                graphics.DrawPolygon(pen_axis, y_arrow);
                graphics.FillPolygon(solidBrush, y_arrow);
                graphics.DrawString(y_cap, font, solidBrush, 10f, 10f);


                // Save the drawing into desired image format
                string filename = "plotting-" + plot_name + ".png";
                bitmap.Save(filename);

                string path = Directory.GetCurrentDirectory();

                plot_src = path + @"\" + filename;

                DA.SetData(0, plot_src);
                DA.SetData(1, plot_width_output);
                DA.SetData(2, plot_height_output);
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
            get { return new Guid("81fa0428-8603-4745-abec-349e8d9c7eb9"); }
        }
    }
}