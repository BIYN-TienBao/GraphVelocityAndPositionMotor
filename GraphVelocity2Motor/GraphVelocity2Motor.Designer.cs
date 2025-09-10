namespace GraphVelocity2Motor
{
    partial class GraphVelocity2Motor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SetButton = new System.Windows.Forms.Button();
            this.LabelSPPos = new System.Windows.Forms.Label();
            this.TextboxSPVelRight = new System.Windows.Forms.TextBox();
            this.TextboxSPVelLeft = new System.Windows.Forms.TextBox();
            this.SetPointGroupBox = new System.Windows.Forms.GroupBox();
            this.LabelSPVel = new System.Windows.Forms.Label();
            this.ExportCSVButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ControlGroupBox = new System.Windows.Forms.GroupBox();
            this.ManualGroupBox = new System.Windows.Forms.GroupBox();
            this.DownButton = new System.Windows.Forms.Button();
            this.RightButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.LabelCurErrVel = new System.Windows.Forms.Label();
            this.CurrentValueGroupBox = new System.Windows.Forms.GroupBox();
            this.TextBoxCurErrorVelLeft = new System.Windows.Forms.TextBox();
            this.LabelCurErrorPos = new System.Windows.Forms.Label();
            this.TextboxCurErrorVelRight = new System.Windows.Forms.TextBox();
            this.LabelCurPos = new System.Windows.Forms.Label();
            this.TextboxCurVelRight = new System.Windows.Forms.TextBox();
            this.LabelCurVel = new System.Windows.Forms.Label();
            this.TextboxCurVelLeft = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.VelocityLeftGraph = new ZedGraph.ZedGraphControl();
            this.TimerGraph = new System.Windows.Forms.Timer(this.components);
            this.VelocityRightGraph = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.SetupGroupBox = new System.Windows.Forms.GroupBox();
            this.TCPGroupBox = new System.Windows.Forms.GroupBox();
            this.IPComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.SerialGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClosePortButton = new System.Windows.Forms.Button();
            this.StatusProgressBar = new System.Windows.Forms.ProgressBar();
            this.OpenPortButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ParityBox = new System.Windows.Forms.ComboBox();
            this.PortBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BaudRateBox = new System.Windows.Forms.ComboBox();
            this.LabelAnnouncement = new System.Windows.Forms.Label();
            this.TimerSend = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.AutoModeButton = new System.Windows.Forms.Button();
            this.SetPointGroupBox.SuspendLayout();
            this.ControlGroupBox.SuspendLayout();
            this.ManualGroupBox.SuspendLayout();
            this.CurrentValueGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SetupGroupBox.SuspendLayout();
            this.TCPGroupBox.SuspendLayout();
            this.SerialGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetButton.Location = new System.Drawing.Point(408, 71);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(4);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(99, 43);
            this.ResetButton.TabIndex = 57;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SetButton
            // 
            this.SetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetButton.Location = new System.Drawing.Point(408, 22);
            this.SetButton.Margin = new System.Windows.Forms.Padding(4);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(99, 43);
            this.SetButton.TabIndex = 56;
            this.SetButton.Text = "Set";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // LabelSPPos
            // 
            this.LabelSPPos.AutoSize = true;
            this.LabelSPPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSPPos.Location = new System.Drawing.Point(14, 75);
            this.LabelSPPos.Name = "LabelSPPos";
            this.LabelSPPos.Size = new System.Drawing.Size(226, 20);
            this.LabelSPPos.TabIndex = 53;
            this.LabelSPPos.Text = "Velocity Motor Right (Rad/s):";
            // 
            // TextboxSPVelRight
            // 
            this.TextboxSPVelRight.Location = new System.Drawing.Point(277, 71);
            this.TextboxSPVelRight.Name = "TextboxSPVelRight";
            this.TextboxSPVelRight.Size = new System.Drawing.Size(124, 30);
            this.TextboxSPVelRight.TabIndex = 52;
            this.TextboxSPVelRight.TextChanged += new System.EventHandler(this.TextboxSPPos_TextChanged);
            // 
            // TextboxSPVelLeft
            // 
            this.TextboxSPVelLeft.Location = new System.Drawing.Point(277, 35);
            this.TextboxSPVelLeft.Name = "TextboxSPVelLeft";
            this.TextboxSPVelLeft.Size = new System.Drawing.Size(124, 30);
            this.TextboxSPVelLeft.TabIndex = 50;
            this.TextboxSPVelLeft.TextChanged += new System.EventHandler(this.TextboxSPVel_TextChanged);
            // 
            // SetPointGroupBox
            // 
            this.SetPointGroupBox.Controls.Add(this.ResetButton);
            this.SetPointGroupBox.Controls.Add(this.SetButton);
            this.SetPointGroupBox.Controls.Add(this.LabelSPPos);
            this.SetPointGroupBox.Controls.Add(this.TextboxSPVelRight);
            this.SetPointGroupBox.Controls.Add(this.LabelSPVel);
            this.SetPointGroupBox.Controls.Add(this.TextboxSPVelLeft);
            this.SetPointGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetPointGroupBox.Enabled = false;
            this.SetPointGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetPointGroupBox.Location = new System.Drawing.Point(3, 3);
            this.SetPointGroupBox.Name = "SetPointGroupBox";
            this.SetPointGroupBox.Size = new System.Drawing.Size(516, 134);
            this.SetPointGroupBox.TabIndex = 71;
            this.SetPointGroupBox.TabStop = false;
            this.SetPointGroupBox.Text = "Set Points";
            // 
            // LabelSPVel
            // 
            this.LabelSPVel.AutoSize = true;
            this.LabelSPVel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSPVel.Location = new System.Drawing.Point(12, 39);
            this.LabelSPVel.Name = "LabelSPVel";
            this.LabelSPVel.Size = new System.Drawing.Size(216, 20);
            this.LabelSPVel.TabIndex = 51;
            this.LabelSPVel.Text = "Velocity Motor Left (Rad/s):";
            // 
            // ExportCSVButton
            // 
            this.ExportCSVButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportCSVButton.Location = new System.Drawing.Point(176, 94);
            this.ExportCSVButton.Margin = new System.Windows.Forms.Padding(4);
            this.ExportCSVButton.Name = "ExportCSVButton";
            this.ExportCSVButton.Size = new System.Drawing.Size(113, 43);
            this.ExportCSVButton.TabIndex = 55;
            this.ExportCSVButton.Text = "Export to CSV";
            this.ExportCSVButton.UseVisualStyleBackColor = true;
            this.ExportCSVButton.Click += new System.EventHandler(this.ExportCSVButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseButton.Location = new System.Drawing.Point(176, 43);
            this.PauseButton.Margin = new System.Windows.Forms.Padding(4);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(113, 43);
            this.PauseButton.TabIndex = 54;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(18, 94);
            this.StopButton.Margin = new System.Windows.Forms.Padding(4);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(113, 43);
            this.StopButton.TabIndex = 53;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ControlGroupBox
            // 
            this.ControlGroupBox.Controls.Add(this.AutoModeButton);
            this.ControlGroupBox.Controls.Add(this.ManualGroupBox);
            this.ControlGroupBox.Controls.Add(this.ExportCSVButton);
            this.ControlGroupBox.Controls.Add(this.PauseButton);
            this.ControlGroupBox.Controls.Add(this.StopButton);
            this.ControlGroupBox.Controls.Add(this.StartButton);
            this.ControlGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlGroupBox.Enabled = false;
            this.ControlGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ControlGroupBox.Location = new System.Drawing.Point(3, 361);
            this.ControlGroupBox.Name = "ControlGroupBox";
            this.ControlGroupBox.Size = new System.Drawing.Size(516, 426);
            this.ControlGroupBox.TabIndex = 70;
            this.ControlGroupBox.TabStop = false;
            this.ControlGroupBox.Text = "Control";
            this.ControlGroupBox.Enter += new System.EventHandler(this.ControlGroupBox_Enter);
            // 
            // ManualGroupBox
            // 
            this.ManualGroupBox.Controls.Add(this.DownButton);
            this.ManualGroupBox.Controls.Add(this.RightButton);
            this.ManualGroupBox.Controls.Add(this.LeftButton);
            this.ManualGroupBox.Controls.Add(this.UpButton);
            this.ManualGroupBox.Location = new System.Drawing.Point(96, 173);
            this.ManualGroupBox.Name = "ManualGroupBox";
            this.ManualGroupBox.Size = new System.Drawing.Size(305, 209);
            this.ManualGroupBox.TabIndex = 56;
            this.ManualGroupBox.TabStop = false;
            this.ManualGroupBox.Text = "Manual Control";
            // 
            // DownButton
            // 
            this.DownButton.Location = new System.Drawing.Point(116, 144);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(73, 41);
            this.DownButton.TabIndex = 3;
            this.DownButton.Text = "↓";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DownButton_MouseDown);
            this.DownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DownButton_MouseUp);
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(219, 86);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(68, 41);
            this.RightButton.TabIndex = 2;
            this.RightButton.Text = "→";
            this.RightButton.UseVisualStyleBackColor = true;
            this.RightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightButton_MouseDown);
            this.RightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightButton_MouseUp);
            // 
            // LeftButton
            // 
            this.LeftButton.Location = new System.Drawing.Point(20, 86);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(68, 41);
            this.LeftButton.TabIndex = 1;
            this.LeftButton.Text = "←";
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftButton_MouseDown);
            this.LeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftButton_MouseUp);
            // 
            // UpButton
            // 
            this.UpButton.Location = new System.Drawing.Point(116, 29);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(73, 41);
            this.UpButton.TabIndex = 0;
            this.UpButton.Text = "↑";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpButton_MouseDown);
            this.UpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpButton_MouseUp);
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(18, 43);
            this.StartButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(113, 43);
            this.StartButton.TabIndex = 52;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // LabelCurErrVel
            // 
            this.LabelCurErrVel.AutoSize = true;
            this.LabelCurErrVel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCurErrVel.Location = new System.Drawing.Point(14, 79);
            this.LabelCurErrVel.Name = "LabelCurErrVel";
            this.LabelCurErrVel.Size = new System.Drawing.Size(259, 20);
            this.LabelCurErrVel.TabIndex = 57;
            this.LabelCurErrVel.Text = "Error Velocity Motor Left (Rad/s):";
            // 
            // CurrentValueGroupBox
            // 
            this.CurrentValueGroupBox.Controls.Add(this.LabelCurErrVel);
            this.CurrentValueGroupBox.Controls.Add(this.TextBoxCurErrorVelLeft);
            this.CurrentValueGroupBox.Controls.Add(this.LabelCurErrorPos);
            this.CurrentValueGroupBox.Controls.Add(this.TextboxCurErrorVelRight);
            this.CurrentValueGroupBox.Controls.Add(this.LabelCurPos);
            this.CurrentValueGroupBox.Controls.Add(this.TextboxCurVelRight);
            this.CurrentValueGroupBox.Controls.Add(this.LabelCurVel);
            this.CurrentValueGroupBox.Controls.Add(this.TextboxCurVelLeft);
            this.CurrentValueGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentValueGroupBox.Enabled = false;
            this.CurrentValueGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentValueGroupBox.Location = new System.Drawing.Point(3, 143);
            this.CurrentValueGroupBox.Name = "CurrentValueGroupBox";
            this.CurrentValueGroupBox.Size = new System.Drawing.Size(516, 212);
            this.CurrentValueGroupBox.TabIndex = 69;
            this.CurrentValueGroupBox.TabStop = false;
            this.CurrentValueGroupBox.Text = "Current Values";
            // 
            // TextBoxCurErrorVelLeft
            // 
            this.TextBoxCurErrorVelLeft.Location = new System.Drawing.Point(310, 75);
            this.TextBoxCurErrorVelLeft.Name = "TextBoxCurErrorVelLeft";
            this.TextBoxCurErrorVelLeft.Size = new System.Drawing.Size(153, 30);
            this.TextBoxCurErrorVelLeft.TabIndex = 56;
            // 
            // LabelCurErrorPos
            // 
            this.LabelCurErrorPos.AutoSize = true;
            this.LabelCurErrorPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCurErrorPos.Location = new System.Drawing.Point(14, 151);
            this.LabelCurErrorPos.Name = "LabelCurErrorPos";
            this.LabelCurErrorPos.Size = new System.Drawing.Size(253, 20);
            this.LabelCurErrorPos.TabIndex = 55;
            this.LabelCurErrorPos.Text = "Error Velocity Motor Right (mm):";
            // 
            // TextboxCurErrorVelRight
            // 
            this.TextboxCurErrorVelRight.Location = new System.Drawing.Point(310, 147);
            this.TextboxCurErrorVelRight.Name = "TextboxCurErrorVelRight";
            this.TextboxCurErrorVelRight.Size = new System.Drawing.Size(153, 30);
            this.TextboxCurErrorVelRight.TabIndex = 54;
            // 
            // LabelCurPos
            // 
            this.LabelCurPos.AutoSize = true;
            this.LabelCurPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCurPos.Location = new System.Drawing.Point(14, 115);
            this.LabelCurPos.Name = "LabelCurPos";
            this.LabelCurPos.Size = new System.Drawing.Size(210, 20);
            this.LabelCurPos.TabIndex = 53;
            this.LabelCurPos.Text = "Velocity Motor Right (mm):";
            // 
            // TextboxCurVelRight
            // 
            this.TextboxCurVelRight.Location = new System.Drawing.Point(310, 111);
            this.TextboxCurVelRight.Name = "TextboxCurVelRight";
            this.TextboxCurVelRight.Size = new System.Drawing.Size(153, 30);
            this.TextboxCurVelRight.TabIndex = 52;
            // 
            // LabelCurVel
            // 
            this.LabelCurVel.AutoSize = true;
            this.LabelCurVel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCurVel.Location = new System.Drawing.Point(12, 46);
            this.LabelCurVel.Name = "LabelCurVel";
            this.LabelCurVel.Size = new System.Drawing.Size(216, 20);
            this.LabelCurVel.TabIndex = 51;
            this.LabelCurVel.Text = "Velocity Motor Left (Rad/s):";
            // 
            // TextboxCurVelLeft
            // 
            this.TextboxCurVelLeft.Location = new System.Drawing.Point(310, 39);
            this.TextboxCurVelLeft.Name = "TextboxCurVelLeft";
            this.TextboxCurVelLeft.Size = new System.Drawing.Size(153, 30);
            this.TextboxCurVelLeft.TabIndex = 50;
            // 
            // VelocityLeftGraph
            // 
            this.VelocityLeftGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VelocityLeftGraph.Enabled = false;
            this.VelocityLeftGraph.Location = new System.Drawing.Point(4, 4);
            this.VelocityLeftGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VelocityLeftGraph.Name = "VelocityLeftGraph";
            this.VelocityLeftGraph.ScrollGrace = 0D;
            this.VelocityLeftGraph.ScrollMaxX = 0D;
            this.VelocityLeftGraph.ScrollMaxY = 0D;
            this.VelocityLeftGraph.ScrollMaxY2 = 0D;
            this.VelocityLeftGraph.ScrollMinX = 0D;
            this.VelocityLeftGraph.ScrollMinY = 0D;
            this.VelocityLeftGraph.ScrollMinY2 = 0D;
            this.VelocityLeftGraph.Size = new System.Drawing.Size(1150, 426);
            this.VelocityLeftGraph.TabIndex = 68;
            this.VelocityLeftGraph.UseExtendedPrintDialog = true;
            // 
            // VelocityRightGraph
            // 
            this.VelocityRightGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VelocityRightGraph.Enabled = false;
            this.VelocityRightGraph.Location = new System.Drawing.Point(4, 438);
            this.VelocityRightGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VelocityRightGraph.Name = "VelocityRightGraph";
            this.VelocityRightGraph.ScrollGrace = 0D;
            this.VelocityRightGraph.ScrollMaxX = 0D;
            this.VelocityRightGraph.ScrollMaxY = 0D;
            this.VelocityRightGraph.ScrollMaxY2 = 0D;
            this.VelocityRightGraph.ScrollMinX = 0D;
            this.VelocityRightGraph.ScrollMinY = 0D;
            this.VelocityRightGraph.ScrollMinY2 = 0D;
            this.VelocityRightGraph.Size = new System.Drawing.Size(1150, 426);
            this.VelocityRightGraph.TabIndex = 72;
            this.VelocityRightGraph.UseExtendedPrintDialog = true;
            this.VelocityRightGraph.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.VelocityLeftGraph, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.VelocityRightGraph, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1158, 868);
            this.tableLayoutPanel1.TabIndex = 73;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.SetPointGroupBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.CurrentValueGroupBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ControlGroupBox, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1167, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.72152F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.59494F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.68354F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(522, 790);
            this.tableLayoutPanel2.TabIndex = 74;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.60929F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.39071F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1698, 1055);
            this.tableLayoutPanel3.TabIndex = 75;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.79433F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.20567F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 178);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1692, 874);
            this.tableLayoutPanel4.TabIndex = 0;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.30733F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.7305F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.90307F));
            this.tableLayoutPanel5.Controls.Add(this.SetupGroupBox, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1692, 169);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // SetupGroupBox
            // 
            this.SetupGroupBox.Controls.Add(this.TCPGroupBox);
            this.SetupGroupBox.Controls.Add(this.SerialGroupBox);
            this.SetupGroupBox.Controls.Add(this.LabelAnnouncement);
            this.SetupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetupGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetupGroupBox.Location = new System.Drawing.Point(3, 3);
            this.SetupGroupBox.Name = "SetupGroupBox";
            this.SetupGroupBox.Size = new System.Drawing.Size(1686, 163);
            this.SetupGroupBox.TabIndex = 76;
            this.SetupGroupBox.TabStop = false;
            this.SetupGroupBox.Text = "Setup Communication";
            // 
            // TCPGroupBox
            // 
            this.TCPGroupBox.Controls.Add(this.IPComboBox);
            this.TCPGroupBox.Controls.Add(this.label4);
            this.TCPGroupBox.Controls.Add(this.label5);
            this.TCPGroupBox.Controls.Add(this.ConnectButton);
            this.TCPGroupBox.Controls.Add(this.PortTextBox);
            this.TCPGroupBox.Location = new System.Drawing.Point(6, 112);
            this.TCPGroupBox.Name = "TCPGroupBox";
            this.TCPGroupBox.Size = new System.Drawing.Size(601, 60);
            this.TCPGroupBox.TabIndex = 76;
            this.TCPGroupBox.TabStop = false;
            this.TCPGroupBox.Text = "TCP";
            // 
            // IPComboBox
            // 
            this.IPComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPComboBox.FormattingEnabled = true;
            this.IPComboBox.Items.AddRange(new object[] {
            "0.0.0.0"});
            this.IPComboBox.Location = new System.Drawing.Point(26, 24);
            this.IPComboBox.Name = "IPComboBox";
            this.IPComboBox.Size = new System.Drawing.Size(201, 28);
            this.IPComboBox.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(261, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "IP";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.Location = new System.Drawing.Point(496, 15);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(91, 38);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.StartConnectButton_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortTextBox.Location = new System.Drawing.Point(298, 25);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(118, 26);
            this.PortTextBox.TabIndex = 9;
            // 
            // SerialGroupBox
            // 
            this.SerialGroupBox.Controls.Add(this.label1);
            this.SerialGroupBox.Controls.Add(this.ClosePortButton);
            this.SerialGroupBox.Controls.Add(this.StatusProgressBar);
            this.SerialGroupBox.Controls.Add(this.OpenPortButton);
            this.SerialGroupBox.Controls.Add(this.label3);
            this.SerialGroupBox.Controls.Add(this.ParityBox);
            this.SerialGroupBox.Controls.Add(this.PortBox);
            this.SerialGroupBox.Controls.Add(this.label2);
            this.SerialGroupBox.Controls.Add(this.label8);
            this.SerialGroupBox.Controls.Add(this.BaudRateBox);
            this.SerialGroupBox.Location = new System.Drawing.Point(6, 25);
            this.SerialGroupBox.Name = "SerialGroupBox";
            this.SerialGroupBox.Size = new System.Drawing.Size(1610, 81);
            this.SerialGroupBox.TabIndex = 68;
            this.SerialGroupBox.TabStop = false;
            this.SerialGroupBox.Text = "Serial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Port";
            // 
            // ClosePortButton
            // 
            this.ClosePortButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ClosePortButton.AutoSize = true;
            this.ClosePortButton.Enabled = false;
            this.ClosePortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClosePortButton.Location = new System.Drawing.Point(1423, 22);
            this.ClosePortButton.Margin = new System.Windows.Forms.Padding(4);
            this.ClosePortButton.Name = "ClosePortButton";
            this.ClosePortButton.Size = new System.Drawing.Size(123, 44);
            this.ClosePortButton.TabIndex = 61;
            this.ClosePortButton.Text = "Close Port";
            this.ClosePortButton.UseVisualStyleBackColor = true;
            this.ClosePortButton.Click += new System.EventHandler(this.ClosePortButton_Click);
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Location = new System.Drawing.Point(946, 26);
            this.StatusProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(236, 23);
            this.StatusProgressBar.TabIndex = 60;
            // 
            // OpenPortButton
            // 
            this.OpenPortButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OpenPortButton.AutoSize = true;
            this.OpenPortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenPortButton.Location = new System.Drawing.Point(1285, 20);
            this.OpenPortButton.Margin = new System.Windows.Forms.Padding(10);
            this.OpenPortButton.Name = "OpenPortButton";
            this.OpenPortButton.Size = new System.Drawing.Size(108, 46);
            this.OpenPortButton.TabIndex = 59;
            this.OpenPortButton.Text = "Open Port";
            this.OpenPortButton.UseVisualStyleBackColor = true;
            this.OpenPortButton.Click += new System.EventHandler(this.OpenPortButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(894, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 64;
            this.label3.Text = "Status";
            // 
            // ParityBox
            // 
            this.ParityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ParityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParityBox.FormattingEnabled = true;
            this.ParityBox.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.ParityBox.Location = new System.Drawing.Point(643, 22);
            this.ParityBox.Margin = new System.Windows.Forms.Padding(4);
            this.ParityBox.Name = "ParityBox";
            this.ParityBox.Size = new System.Drawing.Size(223, 24);
            this.ParityBox.TabIndex = 65;
            // 
            // PortBox
            // 
            this.PortBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortBox.FormattingEnabled = true;
            this.PortBox.Location = new System.Drawing.Point(46, 22);
            this.PortBox.Margin = new System.Windows.Forms.Padding(4);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(191, 24);
            this.PortBox.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(261, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 63;
            this.label2.Text = "Baud rate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(594, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 16);
            this.label8.TabIndex = 66;
            this.label8.Text = "Parity";
            // 
            // BaudRateBox
            // 
            this.BaudRateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudRateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaudRateBox.FormattingEnabled = true;
            this.BaudRateBox.Items.AddRange(new object[] {
            "100",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "156000"});
            this.BaudRateBox.Location = new System.Drawing.Point(334, 22);
            this.BaudRateBox.Margin = new System.Windows.Forms.Padding(4);
            this.BaudRateBox.Name = "BaudRateBox";
            this.BaudRateBox.Size = new System.Drawing.Size(223, 24);
            this.BaudRateBox.TabIndex = 58;
            // 
            // LabelAnnouncement
            // 
            this.LabelAnnouncement.AutoSize = true;
            this.LabelAnnouncement.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAnnouncement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelAnnouncement.Location = new System.Drawing.Point(635, 136);
            this.LabelAnnouncement.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAnnouncement.Name = "LabelAnnouncement";
            this.LabelAnnouncement.Size = new System.Drawing.Size(31, 16);
            this.LabelAnnouncement.TabIndex = 67;
            this.LabelAnnouncement.Text = "Abc";
            // 
            // AutoModeButton
            // 
            this.AutoModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoModeButton.Location = new System.Drawing.Point(336, 43);
            this.AutoModeButton.Margin = new System.Windows.Forms.Padding(4);
            this.AutoModeButton.Name = "AutoModeButton";
            this.AutoModeButton.Size = new System.Drawing.Size(113, 43);
            this.AutoModeButton.TabIndex = 57;
            this.AutoModeButton.Text = "Auto";
            this.AutoModeButton.UseVisualStyleBackColor = true;
            this.AutoModeButton.Click += new System.EventHandler(this.AutoModeButton_Click);
            // 
            // GraphVelocity2Motor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1698, 1055);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "GraphVelocity2Motor";
            this.Text = "Velocity Graph 2 Motor";
            this.Load += new System.EventHandler(this.GraphVelocity2Motor_Load);
            this.SetPointGroupBox.ResumeLayout(false);
            this.SetPointGroupBox.PerformLayout();
            this.ControlGroupBox.ResumeLayout(false);
            this.ManualGroupBox.ResumeLayout(false);
            this.CurrentValueGroupBox.ResumeLayout(false);
            this.CurrentValueGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.SetupGroupBox.ResumeLayout(false);
            this.SetupGroupBox.PerformLayout();
            this.TCPGroupBox.ResumeLayout(false);
            this.TCPGroupBox.PerformLayout();
            this.SerialGroupBox.ResumeLayout(false);
            this.SerialGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Label LabelSPPos;
        private System.Windows.Forms.TextBox TextboxSPVelRight;
        private System.Windows.Forms.TextBox TextboxSPVelLeft;
        private System.Windows.Forms.GroupBox SetPointGroupBox;
        private System.Windows.Forms.Label LabelSPVel;
        private System.Windows.Forms.Button ExportCSVButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.GroupBox ControlGroupBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label LabelCurErrVel;
        private System.Windows.Forms.GroupBox CurrentValueGroupBox;
        private System.Windows.Forms.TextBox TextBoxCurErrorVelLeft;
        private System.Windows.Forms.Label LabelCurErrorPos;
        private System.Windows.Forms.TextBox TextboxCurErrorVelRight;
        private System.Windows.Forms.Label LabelCurPos;
        private System.Windows.Forms.TextBox TextboxCurVelRight;
        private System.Windows.Forms.Label LabelCurVel;
        private System.Windows.Forms.TextBox TextboxCurVelLeft;
        private System.IO.Ports.SerialPort serialPort1;
        private ZedGraph.ZedGraphControl VelocityLeftGraph;
        private System.Windows.Forms.Timer TimerGraph;
        private ZedGraph.ZedGraphControl VelocityRightGraph;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Timer TimerSend;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button ClosePortButton;
        private System.Windows.Forms.Button OpenPortButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox SetupGroupBox;
        private System.Windows.Forms.GroupBox TCPGroupBox;
        private System.Windows.Forms.ComboBox IPComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.GroupBox SerialGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar StatusProgressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ParityBox;
        private System.Windows.Forms.ComboBox PortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox BaudRateBox;
        private System.Windows.Forms.Label LabelAnnouncement;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox ManualGroupBox;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.Button LeftButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button AutoModeButton;
    }
}

