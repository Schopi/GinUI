﻿using GinClientLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GinClientApp.Dialogs
{
    public partial class MetroRepoBrowser : MetroFramework.Forms.MetroForm
    {
        public string SelectedRepository { get; set; }
        private RepositoryListing[] _repositories;

        public MetroRepoBrowser(GinApplicationContext context)
        {
            InitializeComponent();

            var repoListJSON = context.ServiceClient.GetRemoteRepositoryList();
            var repoList = JsonConvert.DeserializeObject<RepositoryListing[]>(repoListJSON);

            if (!repoList.Any()) return;

            _repositories = repoList.OrderByDescending(listing => listing.owner.username).ToArray();
            var paths = _repositories.Select(repoListing => repoListing.full_name).ToList();
            trVwRepositories.Nodes.Add(MakeTreeFromPaths(paths, "gin.g-node.org"));

            mTxBRepoDescription.Text = "";
        }

        TreeNode MakeTreeFromPaths(List<string> paths, string rootNodeName = "", char separator = '/')
        {
            var rootNode = new TreeNode(rootNodeName);
            foreach (var path in paths.Where(x => !string.IsNullOrEmpty(x.Trim())))
            {
                var currentNode = rootNode;
                var pathItems = path.Split(separator);
                foreach (var item in pathItems)
                {
                    var tmp = currentNode.Nodes.Cast<TreeNode>().Where(x => x.Text.Equals(item));
                    currentNode = tmp.Any() ? tmp.Single() : currentNode.Nodes.Add(item);
                }
            }
            return rootNode;
        }

        private void mBtnCheckout_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void trVwRepositories_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void trVwRepositories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 2)
            {
                mTxBRepoDescription.Text = "";
                mTxBRepoDescription.WaterMark = "Select a repository to see its description";
                return;
            }

            SelectedRepository = e.Node.Parent.Text + "/" + e.Node.Text;

            var repo = from rep in _repositories
                where string.Compare(rep.full_name, SelectedRepository, StringComparison.InvariantCultureIgnoreCase) ==
                      0
                select rep;

            var description = repo.First().description;
            if (string.IsNullOrEmpty(description))
                mTxBRepoDescription.WaterMark = "This repository has no description.";
            else
                mTxBRepoDescription.Text = repo.First().description;
        }
    }
}
