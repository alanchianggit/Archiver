﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using Microsoft.VisualBasic.FileIO;

using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Archiver
{
    public partial class frm_dm : Form
    {
        private bool boolOutput
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.txt_OutPutPath.Text);
            }

        }

        private bool boolFoldersCount
        {
            get
            {
                return CheckedNodes.Count() > 0;
            }
        }

        private bool Compressible
        {
            get
            {
                return boolFoldersCount && boolOutput;
            }
        }
        private List<string> CheckedNodes = new List<string>();

        private int intnodeChecked { get; set; }

        private List<string> FileNameList;
        private long sizeThreshold
        {
            get
            {
                return (long)this.numericUpDown1.Value;
            }
        }


        public frm_dm()
        {
            InitializeComponent();
            //this.progressBar1.Step = 1;
        }

        private string SelectFolder()
        {
            if (CommonFileDialog.IsPlatformSupported)
            {
                //Instantiate new common file dialog
                var folderSelectorDialog = new CommonOpenFileDialog();
                //Properties for dialog
                folderSelectorDialog.EnsureReadOnly = true;
                folderSelectorDialog.IsFolderPicker = true;
                folderSelectorDialog.AllowNonFileSystemItems = false;
                folderSelectorDialog.Multiselect = false;
                folderSelectorDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderSelectorDialog.Title = "Select Folder";
                //Start dialog
                folderSelectorDialog.ShowDialog();
                try
                {
                    FileNameList = folderSelectorDialog.FileNames.ToList();
                }
                catch (System.InvalidOperationException exc)
                {
                    UpdateStatusConsole(string.Format("Error occurred in {1} module : {0}.", exc.Message, System.Reflection.MethodBase.GetCurrentMethod().Name));
                }
            }
            try
            {
                return FileNameList.First();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            if (string.IsNullOrEmpty(path) == false)
            {
                DirectoryInfo rootDirectoryInfo = new DirectoryInfo(path);
                treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            }
            else
            {
                UpdateStatusConsole(string.Format("No Folder selected"));

            }


        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.FullName);
            directoryNode.Checked = true;
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(directory.FullName, directory.FullName);
                directoryNode.Nodes[directory.FullName].Checked = true;
                CheckedNodes.Add(directoryNode.Nodes[directory.FullName].Text.ToString());
            }
            //recursive sub-directories
            //foreach (var directory in directoryInfo.GetDirectories())
            //    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            //files 
            //foreach (var file in directoryInfo.GetFiles())
            //    directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }
        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
                if (node.Nodes.Count == 0)
                {
                    if (node.Checked == true)
                    {
                        CheckedNodes.Add(node.Text);

                    }
                    else if (node.Checked == false)
                    {
                        CheckedNodes.Remove(node.Text);

                    }
                }
            }
        }

        // NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event.
        // After a tree node's Checked property is changed, all its child nodes are updated to the same value.
        private void node_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            //if (e.Action != TreeViewAction.Unknown)
            //{
            if (e.Node.Nodes.Count > 0)
            {
                /* Calls the CheckAllChildNodes method, passing in the current 
                Checked value of the TreeNode whose checked state changed. */
                this.CheckAllChildNodes(e.Node, e.Node.Checked);
            }
            else if (e.Node.Nodes.Count == 0)
            {
                if (e.Node.Checked == true)
                {
                    CheckedNodes.Add(e.Node.Text);

                }
                else if (e.Node.Checked == false)
                {
                    CheckedNodes.Remove(e.Node.Text);

                }

            }

        }



        private void btn_SelectDirectory_Click(object sender, EventArgs e)
        {

            //TreeView Approach 
            ListDirectory(this.treeview_Directories, SelectFolder());
        }

        private string ParseFolderNames(string folderName)
        {
            //Start builder class
            StringBuilder sb = new StringBuilder(string.Empty);
            sb = sb.Append(folderName);

            return sb.ToString();
        }

        private void PackFilesInFolder()
        {

            List<Task> taskList = new List<Task>();

            //Select all folders from startpath except exclusion folder
            string[] foldersindirectory = CheckedNodes.ToArray();
            if (foldersindirectory.Length > 0 && this.txt_OutPutPath.ToString() != string.Empty)
            {
                //set progress bar maximum to the number of directories
                this.progressBar1.Maximum = foldersindirectory.Length;
                this.label_Progress.Text = string.Format(@"{0}/{1}", this.progressBar1.Value, this.progressBar1.Maximum);
                //disable compress button when compress starts
                this.btn_Compress.Enabled = false;
                UpdateStatusConsole(string.Format("Start packing {0} folders", foldersindirectory.Length));



                //Loop through folders in directory
                foreach (string dir in foldersindirectory)
                {
                    //start task for each folder
                    Task.Factory.StartNew(() =>
                    {
                        ParseFolder(dir);
                    }, TaskCreationOptions.LongRunning);
                }
                Task.WaitAll();
            }

        }

        private void UpdateProgress()
        {
            this.BeginInvoke
            (new Action(() =>
            {
                //If progressbar max is not at 0
                if (this.progressBar1.Maximum != 0)
                {
                    //Increment progress bar by step
                    this.progressBar1.PerformStep();
                    this.label_Progress.Text = string.Format(@"{0}/{1}", this.progressBar1.Value, this.progressBar1.Maximum);

                }
                //If progressbar is at maximum
                if (this.progressBar1.Value == this.progressBar1.Maximum)
                {
                    //enable all controls
                    this.btn_Compress.Enabled = true;
                    this.btn_SelectDirectory.Enabled = true;
                    this.chkbox_DeleteWhenComplete.Enabled = true;
                    this.btn_SelectOutPutPath.Enabled = true;
                    this.treeview_Directories.Enabled = true;
                    //this.txt_OutPutPath.Enabled = true;
                    //updates status to "Complete"
                    UpdateStatusConsole("Completed Zipping.");
                }
            }
            ));
        }

        private void UpdateStatusConsole(string message)
        {
            this.BeginInvoke
                        (new Action(() =>
                        {
                            //add message to status console
                            this.txt_status.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + " - " + message);
                            //add new line to status console
                            this.txt_status.AppendText(Environment.NewLine);
                        }
                        ));
        }

        private void ParseFolder(string folderPath)
        {

            DirectoryInfo di = new DirectoryInfo(folderPath);


            if (DirSize(di) / 1024f > sizeThreshold)
            {
                try
                {

                    UpdateStatusConsole(string.Format("Initiating '{0}'.", di.Name));
                    //Parse zip file name from output path and folder name
                    string zipFileName = this.txt_OutPutPath.Text + @"\\" + ParseFolderNames(di.Name) + ".zip";
                    //Compress file using IO.COmpression Zipfile Library
                    ZipFile.CreateFromDirectory(folderPath, zipFileName, CompressionLevel.Optimal, false);
                    //Delete algorithm
                    ProcessDelete(di);

                    UpdateStatusConsole(string.Format("'{0}' Zipped.", di.Name));
                    UpdateProgress();

                }
                //Catch Zip file existence exception
                catch (System.IO.IOException e)
                {
                    if (File.Exists(this.txt_OutPutPath.Text + @"\\" + ParseFolderNames(di.Name) + ".zip") == true)
                    {
                        UpdateStatusConsole(e.HResult.ToString() + " - " + e.Message);
                        UpdateStatusConsole(string.Format("Zip file for '{0}' exists. Skipped.", di.Name));
                        UpdateProgress();
                    }
                }
            }
            else
            {
                UpdateStatusConsole(string.Format("Folder '{0}' is below size limit, Folder is skipped.", di.Name));
                UpdateProgress();
            }

        }
        private void ProcessDelete(DirectoryInfo di)
        {
            if (this.chkbox_DeleteWhenComplete.Checked == true)
            {
                try
                {
                    //Delete file and move to recycling bin
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(di.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    UpdateStatusConsole(di.Name + " Deleted.");
                }
                catch (Exception Excep1)
                {
                    Console.WriteLine(Excep1.Message);
                    throw;
                }
            }
        }

        private void btn_Compress_Click(object sender, EventArgs e)
        {
            if (boolOutput == true && boolFoldersCount == true)
            {
                //disables all controls
                this.btn_Compress.Enabled = false;
                this.btn_SelectDirectory.Enabled = false;
                this.chkbox_DeleteWhenComplete.Enabled = false;
                this.btn_SelectOutPutPath.Enabled = false;
                //this.txt_OutPutPath.Enabled = false;
                this.treeview_Directories.Enabled = false;
                this.progressBar1.Value = 0;
                PackFilesInFolder();
            }
            else
            {
                UpdateStatusConsole("No folders selected or Output path is set incorrectly. Program will exit now.");
            }
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        private void btn_SelectOutPutPath_Click(object sender, EventArgs e)
        {
            try
            {
                this.txt_OutPutPath.Text = string.Join(",", SelectFolder());
            }
            catch (System.ArgumentNullException exc)
            {

                UpdateStatusConsole(string.Format("Error occurred in {1} module : {0}.", exc.Message, System.Reflection.MethodBase.GetCurrentMethod().Name));
            }
        }

    }
}
