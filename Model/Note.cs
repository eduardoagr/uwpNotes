using SQLite;

using System;

using uwpEvernote.Interfaces;

namespace uwpEvernote.Model {
    public class Note: Notify {

        private int _id;

        [PrimaryKey, AutoIncrement]
        public int Id {
            get { return _id; }
            set {
                if (value != _id) {
                    _id = value;
                    OnPropertyChanged("id");
                }
            }
        }


        private int _notebookId;

        [Indexed]
        public int NotebookId {
            get { return _notebookId; }
            set {
                if (value != _notebookId) {
                    _notebookId = value;
                    OnPropertyChanged("notebookId");
                }
            }
        }


        private string _title;
        public string Title {
            get { return _title; }
            set {
                if (value != _title) {
                    _title = value;
                    OnPropertyChanged("title");
                }
            }
        }


        private DateTime _cratedTime;
        public DateTime CratedTime {
            get { return _cratedTime; }
            set {
                if (value != _cratedTime) {
                    _cratedTime = value;
                    OnPropertyChanged("cratedTime");
                }
            }
        }


        private DateTime _updatedTime;
        public DateTime UpdatedTime {
            get { return _updatedTime; }
            set {
                if (value != _updatedTime) {
                    _updatedTime = value;
                    OnPropertyChanged("updatedTime");
                }
            }
        }

        private string _filePath;
        public string FilePath {
            get { return _filePath; }
            set {
                if (value != _filePath) {
                    _filePath = value;
                    OnPropertyChanged("filePath");
                }
            }
        }
    }
}
