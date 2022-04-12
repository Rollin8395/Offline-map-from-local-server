using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap;

using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
//using MapTiler.GlobalMapTiles;
//using MBTilesSupport.Helpers;

//public abstract class MBTilesMapProviderBase : GMapProvider;







namespace Map
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Lattitude_Click(object sender, EventArgs e)
        {

        }

        private void Loadmap1_Click(object sender, EventArgs e)
        {

            Map.Zoom = 14;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            Map.CacheLocation = Application.StartupPath + "\\Cache";
            //Map.CacheLocation = @"cache";
            //Map.MapProvider = GMapProviders.GoogleSatelliteMap;
            //Map.MapProvider = new MBTilesMapProviderBase;

             //Map.MapProvider = GMapProviders.GoogleMap;
            Map.MapProvider = GMapProviders.CustomMap;
            GMapProviders.CustomMap.CustomServerUrl = "D:\\commtel\\Map\\Commtel_Map_Png\\{z}/{x}/{y}.png";
           //GMapProviders.CustomMap.CustomServerUrl = "http://localhost/Test/{z}/{x}/{y}.png";
            //GMapProviders.CustomMap.CustomServerUrl = "http://{l}.localhost/osm_tiles/{x}/{y}/{z}.png";
            // GMapProviders.CustomMap.CustomServerLetters = "abc";



            Map.DragButton = MouseButtons.Left;
            double lat = Convert.ToDouble(txtlat.Text);
            double lon = Convert.ToDouble(txtlon.Text);

            Map.Position = new PointLatLng(lat, lon);
            //Map.Zoom = 8;


            PointLatLng point = new PointLatLng(lat, lon);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);


            // creating overlay to display loacation

            GMapOverlay markers = new GMapOverlay("markers");

            


            markers.Markers.Add(marker);

            Map.Overlays.Add(markers);









        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Map.Zoom = 14;

            //Application.DoEvents();
            Map.MinZoom = 10;
            Map.MaxZoom = 19;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Password = Pids@cnpl; Database = PIDS; Persist Security Info = True; User ID = sa; Data Source = SWAPNILP-PC\\SQLEXPRESS";
            cn.Open();



            DataTable dt = new DataTable();
            string strqry;
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            int i;

            strqry = "select  top 100 * from [PIDS].[dbo].[reddata] order by eventtime desc";
            sqlcmd = new SqlCommand(strqry, cn);
            da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);


            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {

                try

                {
                    Map.MapProvider = GMapProviders.GoogleSatelliteMap;
                    double dblat, dblong;
                    dblat = System.Convert.ToDouble(dt.Rows[i]["Latitude"].ToString());

                    dblong = System.Convert.ToDouble(dt.Rows[i]["Longitude"].ToString());

                    Map.Position = new PointLatLng(dblat, dblong);
                    Map.MinZoom = 1;
                    Map.MaxZoom = 100;

                    Map.Zoom = 8;
                    Map.MarkersEnabled = true;



                    PointLatLng Point = new PointLatLng(dblat, dblong);
                    GMapMarker Marker = new GMarkerGoogle(Point, GMarkerGoogleType.red_pushpin);


                    GMapOverlay markers = new GMapOverlay("markers");


                    markers.Markers.Add(Marker);
                    Map.Overlays.Add(markers);
                    Marker.ToolTipText = i+1 +  " ID : " + dt.Rows[i]["id"].ToString() + " ,Date_Time: " + dt.Rows[i]["EventTime"].ToString() + " ,Type: " + dt.Rows[i]["EventType"].ToString();
                }
                catch (Exception ex)

                {
                }


            }
        }
    }
}









