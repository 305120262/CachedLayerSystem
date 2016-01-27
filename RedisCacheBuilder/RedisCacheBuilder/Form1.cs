using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SOESupport;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using StackExchange.Redis;
using stdole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace RedisCacheBuilder
{
    public partial class Form1 : Form
    {

        private IName m_fcName = null;
        private ConnectionMultiplexer m_redis;

        public Form1()
        {
            InitializeComponent();
        }

        public void ReleaseCOMObject(object o)
        {
            if ((o != null) && Marshal.IsComObject(o))
            {
                while (Marshal.ReleaseComObject(o) > 0)
                {
                }
            }
        }

        /// <summary>
        /// Build Cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            IDatabase client = m_redis.GetDatabase();
            string ns = "poi:" + this.tbxCacheName.Text + ":";
            try
            {
                BuildMeta();
                if(this.cbxMetaOnly.Checked)
                {
                    MessageBox.Show("Build Meta Successfully!");
                    ListCaches();
                    this.button1.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Build Meta Failed!");
                this.button1.Enabled = true;
                return;
            }

            if(!this.cbxMetaOnly.Checked)
            {
                int size = int.Parse(client.StringGet(ns + "size"));
                int xmin = int.Parse(client.StringGet(ns + "xmin"));
                int ymin = int.Parse(client.StringGet(ns + "ymin"));
                int xmax = int.Parse(client.StringGet(ns + "xmax"));
                int ymax = int.Parse(client.StringGet(ns + "ymax"));
                progressBar1.Minimum = 0;
                double volumn = ((xmax + size - xmin) / size) * ((ymax + size - ymin) / size);
                progressBar1.Maximum = (int)Math.Ceiling(volumn);
                backgroundWorker1.RunWorkerAsync();
            }
            
        }


        /// <summary>
        /// Build Cache Backgroud worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            int size = int.Parse(this.tbxSize.Text);
            IFeatureClass fc_poi = m_fcName.Open() as IFeatureClass;
            IGeoDataset ds_poi = fc_poi as IGeoDataset;
            int xmin = (int)Math.Floor(ds_poi.Extent.XMin);
            int ymin = (int)Math.Floor(ds_poi.Extent.YMin);
            int xmax = (int)Math.Ceiling(ds_poi.Extent.XMax);
            int ymax = (int)Math.Ceiling(ds_poi.Extent.YMax);

            List<JsonObject> ls_fields_cache = new List<JsonObject>();
            if(!(fc_poi.Extension is IAnnotationClassExtension))
            {
                for (int i = 0; i < fc_poi.Fields.FieldCount; i++)
                {
                    IField field = fc_poi.Fields.get_Field(i);
                    JsonObject js_f = new JsonObject();
                    js_f.AddString("name", field.Name);
                    js_f.AddString("type", Enum.GetName(typeof(esriFieldType), field.Type));
                    js_f.AddString("alias", field.AliasName);
                    if (field.Type == esriFieldType.esriFieldTypeString)
                    {
                        js_f.AddString("length", field.Length.ToString());
                    }
                    else
                    {
                        js_f.AddString("length", "");
                    }
                    ls_fields_cache.Add(js_f);
                }
            }
            
            IDatabase client = m_redis.GetDatabase();
            int grid_id=0;
            string ns = "poi:" + this.tbxCacheName.Text+":";
            for (int y = ymin; y <= ymax; y += size)
            {
                for (int x = xmin; x <= xmax; x += size)
                {                 
                    List<String> str_poi_grid = new List<String>();
                    List<JsonObject> ls_features = new List<JsonObject>();
                    //String str_response = client.StringGet(ns+"response");
                    //JsonObject response = new JsonObject(str_response);
                    JsonObject response = new JsonObject();
                    IEnvelope box = new EnvelopeClass();
                    box.XMin = x ;
                    box.YMin = y ;
                    box.XMax = x +size;
                    box.YMax = y +size;
                    ISpatialFilter filter_poi = new SpatialFilterClass();
                    filter_poi.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    filter_poi.Geometry = box;
                    filter_poi.SubFields = "*";
                    IFeatureCursor cur_poi = fc_poi.Search(filter_poi, true);
                    IFeature fea_poi = cur_poi.NextFeature();
                    while (fea_poi != null)
                    {
                        JsonObject js_fea = new JsonObject();
                        if (!(fea_poi is IAnnotationFeature))
                        {
                            JsonObject js_attributes = new JsonObject();
                            int i = 0;
                            foreach (JsonObject js_field in ls_fields_cache)
                            {
                                object value = fea_poi.get_Value(i);
                                string fieldtype;
                                js_field.TryGetString("type", out fieldtype);
                                string fieldname;
                                js_field.TryGetString("name", out fieldname);
                                #region
                                if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeString))
                                {
                                    js_attributes.AddString(fieldname, value.ToString());
                                }
                                else if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeOID))
                                {
                                    js_attributes.AddLong(fieldname, long.Parse(value.ToString()));
                                }
                                else if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeInteger))
                                {
                                    if (value.ToString() == "")
                                    {
                                        value = 0;
                                    }
                                    js_attributes.AddLong(fieldname, long.Parse(value.ToString()));
                                }
                                else if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeSmallInteger))
                                {
                                    if (value.ToString() == "")
                                    {
                                        value = 0;
                                    }
                                    js_attributes.AddLong(fieldname, long.Parse(value.ToString()));
                                }
                                else if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeDouble))
                                {
                                    if (value.ToString() == "")
                                    {
                                        value = 0;
                                    }
                                    js_attributes.AddDouble(fieldname, double.Parse(value.ToString()));
                                }
                                else if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeDate))
                                {
                                    if (value.ToString() == "")
                                    {
                                        value = DateTime.MinValue;
                                    }
                                    js_attributes.AddDate(fieldname, DateTime.Parse(value.ToString()));
                                }
                                else if (fieldtype == Enum.GetName(typeof(esriFieldType), esriFieldType.esriFieldTypeSingle))
                                {
                                    if (value.ToString() == "")
                                    {
                                        value = 0;
                                    }
                                    js_attributes.AddBoolean(fieldname, bool.Parse(value.ToString()));
                                }
                                #endregion
                                i++;
                            }
                            js_fea.AddJsonObject("attributes", js_attributes);
                            js_fea.AddJsonObject("geometry", Conversion.ToJsonObject(fea_poi.Shape));
                        }
                        else
                        {
                            IAnnotationFeature anno_fea = fea_poi as IAnnotationFeature;
                            ITextElement ele = anno_fea.Annotation as ITextElement;
                            //string text = ele.Text.Replace(System.Environment.NewLine, " ");
                            string text = ele.Text;
                            ITextSymbol sym = ele.Symbol;
                            IFontDisp font = sym.Font;
                            double symsize = sym.Size;
                            string fontname = font.Name;
                            decimal fontsize = font.Size;
                            string.Format(@"a"":""");
                            JsonObject js_symbol = new JsonObject(
                            string.Format(@"{{""type"" : ""esriTS"",""color"": [255,255,255],""haloSize"" : 0,""haloColor"" : [255,255,255,0],""verticalAlignment"" : ""bottom"",""horizontalAlignment"" : ""center"",""size"": {0},""angle"": 0,""xoffset"": 0,""yoffset"": 0,""font"" : {{""family"" : ""{2}"",""size"" : {3},""style"" : ""normal"",""weight"" : ""normal"",""decoration"" : ""none""}},""text"":""{1}""}}",symsize,text,fontname,fontsize)); 
                            js_fea.AddJsonObject("symbol", js_symbol);
                            IArea pshp = fea_poi.Shape as IArea; 
                            js_fea.AddJsonObject("geometry", Conversion.ToJsonObject(pshp.Centroid));
                        }
                        ls_features.Add(js_fea);
                        fea_poi = cur_poi.NextFeature();
                    }
                    response.AddArray("features", ls_features.ToArray());
                    client.StringSet(ns+grid_id.ToString(), response.ToJson());
                    ReleaseCOMObject(cur_poi);
                    grid_id++;
                    progressBar1.BeginInvoke((Action)(() =>
                    {
                        progressBar1.Increment(1);
                    }));
                }
            }
            MessageBox.Show("Build Cache Successfully!");           
            this.button1.BeginInvoke((Action)(()=>
            {
                ListCaches();
                this.button1.Enabled = true;
            }));
        }

        /// <summary>
        /// Build Meta Info
        /// </summary>
        private void BuildMeta()
        {
            IFeatureClass fc_poi = m_fcName.Open() as IFeatureClass;
            IGeoDataset ds_poi = fc_poi as IGeoDataset;
            int xmin = (int)Math.Floor(ds_poi.Extent.XMin);
            int ymin = (int)Math.Floor(ds_poi.Extent.YMin);
            int xmax = (int)Math.Ceiling(ds_poi.Extent.XMax);
            int ymax = (int)Math.Ceiling(ds_poi.Extent.YMax);
            int size = int.Parse(this.tbxSize.Text);
            int step = (int)Math.Ceiling((xmax - xmin) * 1.0 / size);
            string ns = "poi:" + this.tbxCacheName.Text + ":";

            IDatabase client = m_redis.GetDatabase();
            client.StringSet(ns + "size", size.ToString());
            client.StringSet(ns + "xmin", xmin.ToString());
            client.StringSet(ns + "ymin", ymin.ToString());
            client.StringSet(ns + "xmax", xmax.ToString());
            client.StringSet(ns + "ymax", ymax.ToString());
            client.StringSet(ns + "step", step.ToString());


            JsonObject response = new JsonObject();

            List<JsonObject> ls_fields_cache = new List<JsonObject>();
            for (int i = 0; i < fc_poi.Fields.FieldCount; i++)
            {
                IField field = fc_poi.Fields.get_Field(i);
                JsonObject js_f = new JsonObject();
                js_f.AddString("name", field.Name);
                js_f.AddString("type", Enum.GetName(typeof(esriFieldType), field.Type));
                js_f.AddString("alias", field.AliasName);
                if (field.Type == esriFieldType.esriFieldTypeString)
                {
                    js_f.AddString("length", field.Length.ToString());
                }
                else
                {
                    js_f.AddString("length", "");
                }
                ls_fields_cache.Add(js_f);
            }
            response.AddArray("fields", ls_fields_cache.ToArray());
            if (fc_poi.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                response.AddString("geometryType", "esriGeometryPoint");
            }
            else if (fc_poi.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                response.AddString("geometryType", "esriGeometryPolyline");
            }
            else if (fc_poi.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                response.AddString("geometryType", "esriGeometryPolygon");
            }
            IGeoDataset gds_poi = fc_poi as IGeoDataset;
            JsonObject js_sr = new JsonObject();
            js_sr.AddLong("wkid", gds_poi.SpatialReference.FactoryCode);
            response.AddJsonObject("spatialReference", js_sr);

            client.StringSet(ns + "response", response.ToJson());

            client.SetAdd("poi:caches", this.tbxCacheName.Text);

        }

        /// <summary>
        /// Flush Cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string connstr = string.Format("{0},allowAdmin=true", this.tbxServer.Text);
                ConnectionMultiplexer redis_admin = ConnectionMultiplexer.Connect(connstr);
                IServer server = redis_admin.GetServer(this.tbxServer.Text);
                server.FlushDatabase(); 
                MessageBox.Show("Flush OK");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SuggestGridSizeForm sgsfrm = new SuggestGridSizeForm();
            if (sgsfrm.ShowDialog()==DialogResult.OK)
            {
                this.tbxSize.Text = sgsfrm.GridSize.ToString();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.button5.Enabled = false;
            try
            {
                if(this.lsCaches.SelectedIndex!=-1)
                {
                    string ns = "poi:" + this.lsCaches.SelectedItem.ToString() + ":";
                    IDatabase client = m_redis.GetDatabase();
                    IServer masterserver = m_redis.GetServer(this.tbxServer.Text);
                    foreach (var k in masterserver.Keys(pattern: ns + "*", pageSize: 100000))
                    {
                        client.KeyDelete(k);
                    }
                    client.SetRemove("poi:caches", this.lsCaches.SelectedItem.ToString());
                    MessageBox.Show("Delete Cache Successfully!");
                    ListCaches();
                }
            }
            catch
            { }
            this.button5.Enabled = true;         
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IGxDialog dlg = new GxDialogClass();
            IGxObjectFilter filter1 = new GxFilterFeatureClassesClass();
            IGxObjectFilter filter2 = new GxFilterAnnotationFeatureClassesClass();
            IGxObjectFilterCollection filters = dlg as IGxObjectFilterCollection;
            filters.AddFilter(filter1,true);
            filters.AddFilter(filter2,false);
            IEnumGxObject selection ;
            if(dlg.DoModalOpen(this.Handle.ToInt32(), out selection))
            {
                IGxObject gobj = selection.Next();
                if(gobj!=null)
                {
                    this.tbxDataSource.Text = gobj.FullName;
                    m_fcName = gobj.InternalObjectName;
                    this.tbxCacheName.Text = gobj.BaseName;
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.button7.Enabled = false;
            try
            {
                m_redis = ConnectionMultiplexer.Connect(this.tbxServer.Text);
                ListCaches();
                MessageBox.Show("Connect Redis Successfully!");
                this.tabControl1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Connect Redis Failed!");
            }
            this.button7.Enabled = true;
        }

        private void ListCaches()
        {
            this.lsCaches.Items.Clear();
            string ns = "poi:";
            IDatabase client = m_redis.GetDatabase();
            if(client.KeyExists(ns+"caches"))
            { 
                foreach (var k in client.SetMembers(ns + "caches"))
                {
                    this.lsCaches.Items.Add(k.ToString());
                }
            } 
        }

    }
}