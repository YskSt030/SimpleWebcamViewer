using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleWebcamViewer
{
    public partial class F_Main : Form
    {
        private VideoCapture vcap;
        private VideoWriter vw;
        private BackgroundWorker bw_cap, bw_chScan;

        //stock available video ids
        private List<int> vids;
        private int currentch;
        //image size
        private int w, h;
        //Movie Frame counter
        private int fcount = 0;

        //Mat file to capture image
        private Mat mat_cap;

        //Bitmap file to preview
        private Bitmap bmp;

        OpenCvSharp.Size size;

        //State identifier
        // live: True  -> show capturerd videocam image live.
        //      False  -> Playback mode, show recorded movie file(temp.mpg).
        // rec:  True  -> Recording mode, write image(mat_cap). Only turns true in live mode.
        //      False  -> Not Recording mode.
        private bool live = true;
        private bool rec = false;
        private static int FLEN = 100;

        //Temporaly movie file to playback
        private static string VIDEOFILE = Environment.CurrentDirectory + @"\temp.mpg";
        private static FourCC FCC = FourCC.DIVX;
        private double FPS;
        public F_Main()
        {
            InitializeComponent();

            //Recording BackgroundWorker
            bw_cap = new BackgroundWorker();
            bw_cap.DoWork += new DoWorkEventHandler(doWork_cap);
            bw_cap.ProgressChanged += new ProgressChangedEventHandler(progressChanged_cap); //Cancellation is not implimented yet.
            bw_cap.WorkerSupportsCancellation = true;
            bw_cap.WorkerReportsProgress = true;

            bw_chScan = new BackgroundWorker();
            bw_chScan.DoWork += new DoWorkEventHandler(doWork_chScan);
            bw_chScan.ProgressChanged += new ProgressChangedEventHandler(progressChanged_chScan); //Cancellation is not implimented yet.
            bw_chScan.WorkerSupportsCancellation = true;
            bw_chScan.WorkerReportsProgress = true;

            this.vids = new List<int>();
            if (this.scanCamera() == 0)
            {
                currentch = 0;
                this.vcap = new VideoCapture(this.vids[currentch]);
                this.vcap.Set(VideoCaptureProperties.Focus, 0.01);
                this.vcap.Set(VideoCaptureProperties.Gain, 0.0);
                FPS = this.vcap.Fps;
                this.w = this.vcap.FrameWidth; this.h = this.vcap.FrameHeight;
                this.size = new OpenCvSharp.Size(this.w, this.h);
                this.mat_cap = new Mat(this.h, this.w, MatType.CV_8UC3);
            }
            bw_cap.RunWorkerAsync();
            bw_chScan.RunWorkerAsync();
        }
        private void doWork_cap(object sender, DoWorkEventArgs e) {
            while (!bw_cap.CancellationPending)
            {
                if (this.live) //Live Mode
                {
                    this.mat_cap = new Mat();
                    this.vcap.Read(this.mat_cap);
                    Cv2.PutText(this.mat_cap, "CAP:"+this.currentch.ToString(), new OpenCvSharp.Point(this.w - 60, 20), 
                        HersheyFonts.HersheyPlain, 1, new Scalar(0, 255, 0), 
                        1, LineTypes.AntiAlias);
                    if (rec && this.fcount < FLEN && this.vw != null) vw.Write(this.mat_cap);
                    else if (rec && this.fcount == FLEN)
                    {
                        rec = false;
                        vw.Release();
                        bw_cap.ReportProgress(2);
                    }
                }
                else // Play mode
                {
                    if (!live && this.fcount < FLEN)
                    {
                        this.mat_cap = new Mat();
                        this.vcap.Set(VideoCaptureProperties.PosFrames, this.fcount);
                        this.vcap.Read(this.mat_cap);
                    }
                    else if (!live && this.fcount == FLEN)
                    {
                        this.vcap.Release();
                        this.vcap = new VideoCapture(1);
                        this.live = true;
                        bw_cap.ReportProgress(3);
                    }
                }
                if (!this.mat_cap.IsDisposed && this.mat_cap.Height > 0)
                {
                    this.bmp = this.mat_cap.ToBitmap();
                    this.mat_cap.Dispose();
                    this.fcount++;
                    if (this.FPS > 0)System.Threading.Thread.Sleep((int)(1000 / this.FPS));
                    bw_cap.ReportProgress(1);
                }
            }
        }

        private void progressChanged_cap(object sender, ProgressChangedEventArgs e) {
            if (e.ProgressPercentage == 1)
            {
                if (this.bmp != null)
                {
                    this.pictureBox1.Image = this.bmp;
                    this.pictureBox1.Refresh();
                }
            }
            else if (e.ProgressPercentage == 2)
            {
                this.btn_rec.Text = "REC";
                this.btn_rec.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                this.btn_play.Text = "PLAY";
                this.btn_play.Enabled = true;
            }
        }

        private void doWork_chScan(object sender, DoWorkEventArgs e)
        {
            VideoCapture cap_;
            List<int> vids_;
            while (!bw_chScan.CancellationPending && this.live)
            {
                vids_ = new List<int>();
                for (int i = 0; i < 10; i++)

                    if (i != this.currentch)
                    {
                        cap_ = new VideoCapture(i);
                        if (cap_.IsOpened())
                        {
                            vids_.Add(i);
                            cap_.Release();
                        }
                    }
                    else if (this.vcap.IsOpened())
                    {
                        vids_.Add(i);
                    }

                if (!this.vids.SequenceEqual(vids_))
                {
                    var syncObject = new object();
                    lock (syncObject) this.vids = vids_;               
                }
                bw_chScan.ReportProgress(1);
            }
        }

        private void progressChanged_chScan(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripStatusLabel_chnum.Text = "Channnels:" + this.vids.Count.ToString();
        }

        private int scanCamera()
        {
            for (int i = 0; i < 10; i++)
            {
                VideoCapture cap_ = new VideoCapture(i);
                if (cap_.IsOpened())
                {
                    this.vids.Add(i);
                    cap_.Release();
                }
            }
            if (this.vids.Count > 0) return 0;
            return -1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.bw_cap.CancelAsync();
            this.bw_chScan.CancelAsync();
            while (bw_cap.IsBusy || bw_chScan.IsBusy)
                Application.DoEvents();
        }

        private void btn_swcam_Click(object sender, EventArgs e)
        {          
            if (this.vids.Count > 0)
            {
                this.live = false;
                this.vcap.Release();
                var syncObject = new object();
                lock (syncObject)
                {
                    this.currentch = (this.currentch + 1) % this.vids.Count;
                    this.vcap = new VideoCapture(this.currentch);
                    FPS = this.vcap.Fps;
                    this.w = this.vcap.FrameWidth; this.h = this.vcap.FrameHeight;
                    this.size = new OpenCvSharp.Size(this.w, this.h);
                    this.mat_cap = new Mat(this.h, this.w, MatType.CV_8UC3);
                    this.live = true;
                }
            }
            
        }

        private void btn_rec_Click(object sender, EventArgs e)
        {
            this.live = true;
            rec = true;
            if (System.IO.File.Exists(VIDEOFILE)) System.IO.File.Delete(VIDEOFILE);
            this.vw = new VideoWriter(VIDEOFILE, FCC, 20.0, this.size, true);
            fcount = 0;
            this.btn_rec.Text = "WAIT";
            this.btn_rec.Enabled = false;
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(VIDEOFILE))
            {
                this.live = false;
                this.vcap.Release();
                this.vcap = new VideoCapture(VIDEOFILE);
                fcount = 0;
                this.btn_play.Text = "WAIT";
                this.btn_play.Enabled = false;
            }
        }


    }

}
