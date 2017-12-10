﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GinShellExtension.GinService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GinRepositoryData", Namespace="http://schemas.datacontract.org/2004/07/GinClientLibrary")]
    [System.SerializableAttribute()]
    public partial class GinRepositoryData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool CreateNewField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool MountedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.DirectoryInfo MountpointField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.DirectoryInfo PhysicalDirectoryField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool CreateNew {
            get {
                return this.CreateNewField;
            }
            set {
                if ((this.CreateNewField.Equals(value) != true)) {
                    this.CreateNewField = value;
                    this.RaisePropertyChanged("CreateNew");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Mounted {
            get {
                return this.MountedField;
            }
            set {
                if ((this.MountedField.Equals(value) != true)) {
                    this.MountedField = value;
                    this.RaisePropertyChanged("Mounted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.IO.DirectoryInfo Mountpoint {
            get {
                return this.MountpointField;
            }
            set {
                if ((object.ReferenceEquals(this.MountpointField, value) != true)) {
                    this.MountpointField = value;
                    this.RaisePropertyChanged("Mountpoint");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.IO.DirectoryInfo PhysicalDirectory {
            get {
                return this.PhysicalDirectoryField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysicalDirectoryField, value) != true)) {
                    this.PhysicalDirectoryField = value;
                    this.RaisePropertyChanged("PhysicalDirectory");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GinService.IGinService", CallbackContract=typeof(GinShellExtension.GinService.IGinServiceCallback))]
    public interface IGinService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/AddRepository", ReplyAction="http://tempuri.org/IGinService/AddRepositoryResponse")]
        bool AddRepository(string physicalDirectory, string mountpoint, string name, string commandline, bool performFullCheckout, bool createNew);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/AddRepository", ReplyAction="http://tempuri.org/IGinService/AddRepositoryResponse")]
        System.Threading.Tasks.Task<bool> AddRepositoryAsync(string physicalDirectory, string mountpoint, string name, string commandline, bool performFullCheckout, bool createNew);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/CreateNewRepository", ReplyAction="http://tempuri.org/IGinService/CreateNewRepositoryResponse")]
        bool CreateNewRepository(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/CreateNewRepository", ReplyAction="http://tempuri.org/IGinService/CreateNewRepositoryResponse")]
        System.Threading.Tasks.Task<bool> CreateNewRepositoryAsync(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/MountRepository", ReplyAction="http://tempuri.org/IGinService/MountRepositoryResponse")]
        bool MountRepository(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/MountRepository", ReplyAction="http://tempuri.org/IGinService/MountRepositoryResponse")]
        System.Threading.Tasks.Task<bool> MountRepositoryAsync(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UnmountRepository", ReplyAction="http://tempuri.org/IGinService/UnmountRepositoryResponse")]
        bool UnmountRepository(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UnmountRepository", ReplyAction="http://tempuri.org/IGinService/UnmountRepositoryResponse")]
        System.Threading.Tasks.Task<bool> UnmountRepositoryAsync(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/DeleteRepository", ReplyAction="http://tempuri.org/IGinService/DeleteRepositoryResponse")]
        void DeleteRepository(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/DeleteRepository", ReplyAction="http://tempuri.org/IGinService/DeleteRepositoryResponse")]
        System.Threading.Tasks.Task DeleteRepositoryAsync(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UnmmountAllRepositories", ReplyAction="http://tempuri.org/IGinService/UnmmountAllRepositoriesResponse")]
        bool UnmmountAllRepositories();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UnmmountAllRepositories", ReplyAction="http://tempuri.org/IGinService/UnmmountAllRepositoriesResponse")]
        System.Threading.Tasks.Task<bool> UnmmountAllRepositoriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/Login", ReplyAction="http://tempuri.org/IGinService/LoginResponse")]
        bool Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/Login", ReplyAction="http://tempuri.org/IGinService/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/GetRepositoryList", ReplyAction="http://tempuri.org/IGinService/GetRepositoryListResponse")]
        string GetRepositoryList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/GetRepositoryList", ReplyAction="http://tempuri.org/IGinService/GetRepositoryListResponse")]
        System.Threading.Tasks.Task<string> GetRepositoryListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UpdateRepository", ReplyAction="http://tempuri.org/IGinService/UpdateRepositoryResponse")]
        bool UpdateRepository(string repoName, GinShellExtension.GinService.GinRepositoryData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UpdateRepository", ReplyAction="http://tempuri.org/IGinService/UpdateRepositoryResponse")]
        System.Threading.Tasks.Task<bool> UpdateRepositoryAsync(string repoName, GinShellExtension.GinService.GinRepositoryData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/RetrieveFile", ReplyAction="http://tempuri.org/IGinService/RetrieveFileResponse")]
        bool RetrieveFile(string repoName, string filepath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/RetrieveFile", ReplyAction="http://tempuri.org/IGinService/RetrieveFileResponse")]
        System.Threading.Tasks.Task<bool> RetrieveFileAsync(string repoName, string filepath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UploadFile", ReplyAction="http://tempuri.org/IGinService/UploadFileResponse")]
        bool UploadFile(string repoName, string filepath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/UploadFile", ReplyAction="http://tempuri.org/IGinService/UploadFileResponse")]
        System.Threading.Tasks.Task<bool> UploadFileAsync(string repoName, string filepath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/StashFile", ReplyAction="http://tempuri.org/IGinService/StashFileResponse")]
        bool StashFile(string repoName, string filepath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/StashFile", ReplyAction="http://tempuri.org/IGinService/StashFileResponse")]
        System.Threading.Tasks.Task<bool> StashFileAsync(string repoName, string filepath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/DownloadUpdateInfo", ReplyAction="http://tempuri.org/IGinService/DownloadUpdateInfoResponse")]
        void DownloadUpdateInfo(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/DownloadUpdateInfo", ReplyAction="http://tempuri.org/IGinService/DownloadUpdateInfoResponse")]
        System.Threading.Tasks.Task DownloadUpdateInfoAsync(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/DownloadAllUpdateInfo", ReplyAction="http://tempuri.org/IGinService/DownloadAllUpdateInfoResponse")]
        void DownloadAllUpdateInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/DownloadAllUpdateInfo", ReplyAction="http://tempuri.org/IGinService/DownloadAllUpdateInfoResponse")]
        System.Threading.Tasks.Task DownloadAllUpdateInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/GetRepositoryFileInfo", ReplyAction="http://tempuri.org/IGinService/GetRepositoryFileInfoResponse")]
        string GetRepositoryFileInfo(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/GetRepositoryFileInfo", ReplyAction="http://tempuri.org/IGinService/GetRepositoryFileInfoResponse")]
        System.Threading.Tasks.Task<string> GetRepositoryFileInfoAsync(string repoName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/IsManagedPath", ReplyAction="http://tempuri.org/IGinService/IsManagedPathResponse")]
        bool IsManagedPath(string filePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/IsManagedPath", ReplyAction="http://tempuri.org/IGinService/IsManagedPathResponse")]
        System.Threading.Tasks.Task<bool> IsManagedPathAsync(string filePath);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGinServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/FileOperationStarted", ReplyAction="http://tempuri.org/IGinService/FileOperationStartedResponse")]
        void FileOperationStarted(string filename, string repository);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/FileOperationFinished", ReplyAction="http://tempuri.org/IGinService/FileOperationFinishedResponse")]
        void FileOperationFinished(string filename, string repository, bool success);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/FileOperationProgress", ReplyAction="http://tempuri.org/IGinService/FileOperationProgressResponse")]
        void FileOperationProgress(string filename, string repository, int progress, string speed, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGinService/GinServiceError", ReplyAction="http://tempuri.org/IGinService/GinServiceErrorResponse")]
        void GinServiceError(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGinServiceChannel : GinShellExtension.GinService.IGinService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GinServiceClient : System.ServiceModel.DuplexClientBase<GinShellExtension.GinService.IGinService>, GinShellExtension.GinService.IGinService {
        
        public GinServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GinServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GinServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GinServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GinServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool AddRepository(string physicalDirectory, string mountpoint, string name, string commandline, bool performFullCheckout, bool createNew) {
            return base.Channel.AddRepository(physicalDirectory, mountpoint, name, commandline, performFullCheckout, createNew);
        }
        
        public System.Threading.Tasks.Task<bool> AddRepositoryAsync(string physicalDirectory, string mountpoint, string name, string commandline, bool performFullCheckout, bool createNew) {
            return base.Channel.AddRepositoryAsync(physicalDirectory, mountpoint, name, commandline, performFullCheckout, createNew);
        }
        
        public bool CreateNewRepository(string repoName) {
            return base.Channel.CreateNewRepository(repoName);
        }
        
        public System.Threading.Tasks.Task<bool> CreateNewRepositoryAsync(string repoName) {
            return base.Channel.CreateNewRepositoryAsync(repoName);
        }
        
        public bool MountRepository(string repoName) {
            return base.Channel.MountRepository(repoName);
        }
        
        public System.Threading.Tasks.Task<bool> MountRepositoryAsync(string repoName) {
            return base.Channel.MountRepositoryAsync(repoName);
        }
        
        public bool UnmountRepository(string repoName) {
            return base.Channel.UnmountRepository(repoName);
        }
        
        public System.Threading.Tasks.Task<bool> UnmountRepositoryAsync(string repoName) {
            return base.Channel.UnmountRepositoryAsync(repoName);
        }
        
        public void DeleteRepository(string repoName) {
            base.Channel.DeleteRepository(repoName);
        }
        
        public System.Threading.Tasks.Task DeleteRepositoryAsync(string repoName) {
            return base.Channel.DeleteRepositoryAsync(repoName);
        }
        
        public bool UnmmountAllRepositories() {
            return base.Channel.UnmmountAllRepositories();
        }
        
        public System.Threading.Tasks.Task<bool> UnmmountAllRepositoriesAsync() {
            return base.Channel.UnmmountAllRepositoriesAsync();
        }
        
        public bool Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public string GetRepositoryList() {
            return base.Channel.GetRepositoryList();
        }
        
        public System.Threading.Tasks.Task<string> GetRepositoryListAsync() {
            return base.Channel.GetRepositoryListAsync();
        }
        
        public bool UpdateRepository(string repoName, GinShellExtension.GinService.GinRepositoryData data) {
            return base.Channel.UpdateRepository(repoName, data);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateRepositoryAsync(string repoName, GinShellExtension.GinService.GinRepositoryData data) {
            return base.Channel.UpdateRepositoryAsync(repoName, data);
        }
        
        public bool RetrieveFile(string repoName, string filepath) {
            return base.Channel.RetrieveFile(repoName, filepath);
        }
        
        public System.Threading.Tasks.Task<bool> RetrieveFileAsync(string repoName, string filepath) {
            return base.Channel.RetrieveFileAsync(repoName, filepath);
        }
        
        public bool UploadFile(string repoName, string filepath) {
            return base.Channel.UploadFile(repoName, filepath);
        }
        
        public System.Threading.Tasks.Task<bool> UploadFileAsync(string repoName, string filepath) {
            return base.Channel.UploadFileAsync(repoName, filepath);
        }
        
        public bool StashFile(string repoName, string filepath) {
            return base.Channel.StashFile(repoName, filepath);
        }
        
        public System.Threading.Tasks.Task<bool> StashFileAsync(string repoName, string filepath) {
            return base.Channel.StashFileAsync(repoName, filepath);
        }
        
        public void DownloadUpdateInfo(string repoName) {
            base.Channel.DownloadUpdateInfo(repoName);
        }
        
        public System.Threading.Tasks.Task DownloadUpdateInfoAsync(string repoName) {
            return base.Channel.DownloadUpdateInfoAsync(repoName);
        }
        
        public void DownloadAllUpdateInfo() {
            base.Channel.DownloadAllUpdateInfo();
        }
        
        public System.Threading.Tasks.Task DownloadAllUpdateInfoAsync() {
            return base.Channel.DownloadAllUpdateInfoAsync();
        }
        
        public string GetRepositoryFileInfo(string repoName) {
            return base.Channel.GetRepositoryFileInfo(repoName);
        }
        
        public System.Threading.Tasks.Task<string> GetRepositoryFileInfoAsync(string repoName) {
            return base.Channel.GetRepositoryFileInfoAsync(repoName);
        }
        
        public bool IsManagedPath(string filePath) {
            return base.Channel.IsManagedPath(filePath);
        }
        
        public System.Threading.Tasks.Task<bool> IsManagedPathAsync(string filePath) {
            return base.Channel.IsManagedPathAsync(filePath);
        }
    }
}
