
namespace LifeS
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelTimeChild = new System.Windows.Forms.Label();
            this.labelGender = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.humanSatiety = new System.Windows.Forms.Label();
            this.TotalHuman = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonPause = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Density = new System.Windows.Forms.NumericUpDown();
            this.buttonStart = new System.Windows.Forms.Button();
            this.Resolution = new System.Windows.Forms.NumericUpDown();
            this.listView1 = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Density)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Resolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelTimeChild);
            this.splitContainer1.Panel1.Controls.Add(this.labelGender);
            this.splitContainer1.Panel1.Controls.Add(this.status);
            this.splitContainer1.Panel1.Controls.Add(this.humanSatiety);
            this.splitContainer1.Panel1.Controls.Add(this.TotalHuman);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPause);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.Density);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStart);
            this.splitContainer1.Panel1.Controls.Add(this.Resolution);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1198, 582);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelTimeChild
            // 
            this.labelTimeChild.AutoSize = true;
            this.labelTimeChild.Location = new System.Drawing.Point(49, 364);
            this.labelTimeChild.Name = "labelTimeChild";
            this.labelTimeChild.Size = new System.Drawing.Size(97, 13);
            this.labelTimeChild.TabIndex = 16;
            this.labelTimeChild.Text = "Time from last child";
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Location = new System.Drawing.Point(49, 351);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(42, 13);
            this.labelGender.TabIndex = 15;
            this.labelGender.Text = "Gender";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(49, 398);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(37, 13);
            this.status.TabIndex = 14;
            this.status.Text = "Status";
            // 
            // humanSatiety
            // 
            this.humanSatiety.AutoSize = true;
            this.humanSatiety.Location = new System.Drawing.Point(49, 338);
            this.humanSatiety.Name = "humanSatiety";
            this.humanSatiety.Size = new System.Drawing.Size(86, 13);
            this.humanSatiety.TabIndex = 13;
            this.humanSatiety.Text = "Satiety of human";
            // 
            // TotalHuman
            // 
            this.TotalHuman.AutoSize = true;
            this.TotalHuman.Location = new System.Drawing.Point(49, 325);
            this.TotalHuman.Name = "TotalHuman";
            this.TotalHuman.Size = new System.Drawing.Size(81, 13);
            this.TotalHuman.TabIndex = 9;
            this.TotalHuman.Text = "TotalOfHumans";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "INFORMATION:";
            // 
            // buttonPause
            // 
            this.buttonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPause.Location = new System.Drawing.Point(49, 220);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(120, 23);
            this.buttonPause.TabIndex = 10;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Density";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Resolution";
            // 
            // Density
            // 
            this.Density.Location = new System.Drawing.Point(49, 103);
            this.Density.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Density.Name = "Density";
            this.Density.Size = new System.Drawing.Size(120, 20);
            this.Density.TabIndex = 4;
            this.Density.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(49, 173);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(120, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // Resolution
            // 
            this.Resolution.Location = new System.Drawing.Point(49, 39);
            this.Resolution.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(120, 20);
            this.Resolution.TabIndex = 0;
            this.Resolution.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(39, 293);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(148, 139);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(958, 578);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 582);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Density)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Resolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown Resolution;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown Density;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TotalHuman;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label humanSatiety;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.Label labelTimeChild;
    }
}

