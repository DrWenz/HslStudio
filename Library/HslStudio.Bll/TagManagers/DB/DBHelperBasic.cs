using SQLite;
using static HslStudio.Com.Drivers.XCollection;

namespace HslStudio.Bll.TagManagers.DB
{
    public class DBHelperBasic
    {
      
        
        public async void InitializeDatabase()
        {
            
            TryCreateDatabase()
;
        }
        public SQLiteConnection TryCreateDatabase()
        {
            SQLiteConnection conn=null;
            try
            {
             
                //string databasePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\EVStudio_DB.db";
           
                //if (!File.Exists(databasePath))
                //{
                //    System.IO.File.WriteAllBytes(databasePath, ScadeDataAccess.ActiveEVStudio_DB());

                //}
 
                //conn = new SQLiteConnection(databasePath);
                
            }
            catch (System.Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
            return conn;

        }
    }


}
