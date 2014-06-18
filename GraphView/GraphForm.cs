﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphImplementation;

namespace GraphView
{
    public partial class GraphForm : Form
    {
        Graph graph = new Graph();

        public GraphForm()
        {
            InitializeComponent();

            graph.OnEdgeAdded += On_EdgeAdded;
            graph.OnEdgeRemoved += On_EdgeRemoved;
            graph.OnVertexRemoved += On_VertexRemoved;
            graph.OnVertexAdded += On_VertexAdded;
            graph.OnChange += On_Change;

            canvasView1.MouseMove += On_MouseMove;
        }

        public void On_Change(Graph sender){

        }
        public void On_VertexRemoved(Graph sender, Vertex vertex)
        {
            VertexView v = canvasView1.FindViewByVertex(vertex);
            v.MouseDown -= On_MouseDown;
            v.MouseUp -= On_MouseUp;
            v.MouseLeave -= On_MouseLeave;
            v.MouseEnter -= On_MouseEnter;
            canvasView1.Views.Remove(v);
            v = null;
        }
        public void On_VertexAdded(Graph sender, Vertex vertex)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            VertexView v = new VertexView();
            v.Vertex = vertex;
            v.Location = new Point(r.Next(0, canvasView1.Width), r.Next(0, canvasView1.Height));
            v.MouseDown += On_MouseDown;
            v.MouseUp += On_MouseUp;
            v.MouseLeave += On_MouseLeave;
            v.MouseEnter += On_MouseEnter;
            canvasView1.Views.Add(v);
            canvasView1.Refresh();
        }
        public void On_EdgeRemoved(Graph sender, Edge edge)
        {

        }
        public void On_EdgeAdded(Graph sender, Edge edge)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            graph.AddVertex();
        }

        VertexView moving = null;
        Point StartPoint;
        Point OldPoint;
        private void On_MouseDown(Views sender)
        {
            canvasView1.BringToFront(sender);
            moving = (VertexView)sender;
            OldPoint = moving.Location;
            StartPoint = MousePosition;
            //(sender as VertexView).BackColor = Color.Red;
        }

        private void On_MouseUp(Views sender)
        {
            moving = null;
           
           // (sender as VertexView).BackColor = Color.GreenYellow;
        }

        private void On_MouseMove(Object sender, MouseEventArgs e)
        {
            if (moving != null)
            {
                moving.Location = new Point(OldPoint.X + (MousePosition.X - StartPoint.X), OldPoint.Y + (MousePosition.Y - StartPoint.Y));
                //canvasView1.Refresh();
            }
            // (sender as VertexView).BackColor = Color.GreenYellow;
        }



        private void On_MouseEnter(Views sender)
        {
           // (sender as VertexView).BackColor = Color.Pink;
        }

        private void On_MouseLeave(Views sender)
        {
           // (sender as VertexView).BackColor = Color.GreenYellow;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
