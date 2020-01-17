using SQLite;
using System.IO;
using Windows.Storage;

namespace uwpEvernote.ViewModel {
    public class DatabaseHelper {

        public static string dbFile = Path.Combine(ApplicationData.Current.LocalFolder.Path, "notesBd.db3");

        public static bool Insert<T>(T item) {

            var result = false;

            using (var conn = new SQLiteConnection(dbFile)) {

                conn.CreateTable<T>();
                var numbersOfRows = conn.Insert(item);
                if (numbersOfRows > 0) {
                    result = true;
                }
            }
            return result;
        }
        public static bool Update<T>(T item) {

            var result = false;

            using (var conn = new SQLiteConnection(dbFile)) {

                conn.CreateTable<T>();
                var numbersOfRows = conn.Update(item);
                if (numbersOfRows > 0) {
                    result = true;
                }
            }
            return result;
        }
        public static bool Delete<T>(T item) {

            var result = false;

            using (var conn = new SQLiteConnection(dbFile)) {

                conn.CreateTable<T>();
                var numbersOfRows = conn.Delete(item);
                if (numbersOfRows > 0) {
                    result = true;
                }
            }
            return result;
        }
    }
}
